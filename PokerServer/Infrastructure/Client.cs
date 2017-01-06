using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using PokerLibrary;

namespace PokerServer {
	public delegate void ConnectDelegate(object sender, EventArgs e);
	public delegate void DisconnectDelegate(object sender, EventArgs e);
	public delegate void MessageDelegate(object sender, MessageEventArgs e);

	public class MessageEventArgs : EventArgs {
		private string m_Message;

		public string Message {
			get {
				return this.m_Message;
			}
			set {
				this.m_Message = value;
			}
		}
	}

	public class Client {
		public event ConnectDelegate Connected;
		public event DisconnectDelegate Disconnected;
		public event MessageDelegate MessageReceived;

		public const int BufferSize = 1024;

		private ConnectionState m_ConnectionState;
		private StringBuilder m_StringBuffer;
		private TcpClient m_TcpClient;
		private NetworkStream m_Stream;
		private RSACryptoServiceProvider m_RSAEncoder;
		private string m_Username;
		private string m_Password;
		private byte[] m_PublicKey;
		private Guid m_ClientId;

		public Client(TcpClient client) {
			this.m_ConnectionState = ConnectionState.NotConnected;
			this.m_StringBuffer = new StringBuilder();
			this.m_TcpClient = client;
			this.m_Stream = client.GetStream();
		}

		public string Username {
			get {
				return this.m_Username;
			}
			set {
				this.m_Username = value;
			}
		}

		public byte[] PublicKey {
			get {
				return this.m_PublicKey;
			}
			set {
				this.m_PublicKey = value;
			}
		}

		public Guid ClientId {
			get {
				return this.m_ClientId;
			}
		}

		public ConnectionState ConnectionState {
			get {
				return this.m_ConnectionState;
			}
		}

		public void Connect() {
			this.m_ClientId = Guid.NewGuid();
			this.m_ConnectionState = ConnectionState.Preauthentication;
			this.Listen();
		}

		public void Listen() {
			byte[] _recByte = new byte[Client.BufferSize];

			AsyncCallback _readStreamMessageCallback = new AsyncCallback(this.ReadStreamMessage);
			this.m_Stream.BeginRead(_recByte, 0, _recByte.Length, _readStreamMessageCallback, _recByte);
		}

		public void ReadStreamMessage(IAsyncResult ar) {
			if(!ar.IsCompleted) {
				return;
			}

			int _intCount;

			try {
				if(!this.m_TcpClient.Connected) {
					this.Disconnect();
					return;
				}

				_intCount = this.m_Stream.EndRead(ar);

				if(_intCount < 1) {
					this.Disconnect();
					return;
				}

				byte[] _recByte = (byte[])ar.AsyncState;
				this.BuildText(_recByte, 0, _intCount);

				if(this.m_ConnectionState == ConnectionState.NotConnected) {
					return;
				}

				lock(this.m_Stream) {
					this.Listen();
				}
			}
			catch(IOException _ex) {
				if(_ex.InnerException != null && _ex.InnerException is SocketException) {
					SocketException _socketException = (SocketException)_ex.InnerException;
					this.Disconnect(_socketException);

					if(_socketException.SocketErrorCode != SocketError.ConnectionReset) {
						Console.WriteLine("{0}: {1}", _socketException.GetType().ToString(), _socketException.Message);
					}
				}
				else {
					this.Disconnect();
					Console.WriteLine("{0}: {1}", _ex.GetType().ToString(), _ex.Message);
				}
			}
		}

		public void CheckAuthentication(string authData) {
			byte[] _base64 = Convert.FromBase64String(authData);
			string _username = null;
			string _password = null;

			string _socket = this.m_TcpClient.Client.RemoteEndPoint.ToString();

			Console.WriteLine("{0} connected.", _socket);

			foreach(KeyValuePair<string, string> _pair in Server.Instance.Accounts) {
				byte[] _hash = EncryptionUtils.Hash<SHA256Managed>(_pair.Key + "|" + _pair.Value);

				if(_hash.SequenceEqual(_base64)) {
					_username = _pair.Key;
					_password = _pair.Value;
					break;
				}
			}

			if(_username == null) {
				Console.WriteLine("{0} is dropped due to invalid credentials.", _socket);
				this.Disconnect();
				return;
			}

			Console.WriteLine("{0} identified himself as {1}.", _socket, _username);

			this.m_Username = _username;
			this.m_Password = _password;
			this.m_ConnectionState = ConnectionState.Keytransfer;

			Console.WriteLine("initializing server publickey transfer for {0}...", this.m_Username);
			byte[] _encryptedPublicKey = EncryptionUtils.SymmetricEncrypt<AesManaged>(this.m_Password, Server.Instance.PublicKey);

			this.SendDirect(_encryptedPublicKey);
		}

		public void KeyTransfer(string authData) {
			byte[] _base64 = Convert.FromBase64String(authData);
			this.m_PublicKey = EncryptionUtils.SymmetricDecrypt<AesManaged>(this.m_Password, _base64);

			this.m_RSAEncoder = EncryptionUtils.LoadRSAForEncoder(this.m_PublicKey);

			this.m_ConnectionState = ConnectionState.Authenticated;

			if(this.Connected != null) {
				EventArgs _e = new EventArgs();
				this.Connected(this, _e);
			}
		}

		public void SendDirect(string message) {
			byte[] _bytes = Encoding.Default.GetBytes(message);
			this.SendDirect(_bytes);
		}

		public void SendDirect(byte[] input) {
			string _base64 = Convert.ToBase64String(input, Base64FormattingOptions.None) + "\n";
			byte[] _base64Bytes = Encoding.Default.GetBytes(_base64);

			this.m_Stream.BeginWrite(_base64Bytes, 0, _base64Bytes.Length, this.SendStreamMessage, null);
		}

		public void SendEncrypted(string message) {
			byte[] _bytes = Encoding.Default.GetBytes(message);
			this.SendEncrypted(_bytes);
		}

		public void SendEncrypted(byte[] input) {
			byte[] _input = EncryptionUtils.RSAEncrypt(Server.Instance.RSADecoder, input);
			string _base64 = Convert.ToBase64String(_input, Base64FormattingOptions.None) + "\n";
			byte[] _base64Bytes = Encoding.Default.GetBytes(_base64);

			this.m_Stream.BeginWrite(_base64Bytes, 0, _base64Bytes.Length, this.SendStreamMessage, null);
		}

		public void SendStreamMessage(IAsyncResult ar) {
			if(!ar.IsCompleted) {
				return;
			}

			if(!this.m_TcpClient.Connected) {
				this.Disconnect();
				return;
			}

			this.m_Stream.EndWrite(ar);
			this.m_Stream.Flush();
		}

		public void BuildText(byte[] dataByte, int offset, int count) {
			for(int i = 0;i < count;i++) {
				char _char = Convert.ToChar(dataByte[i]);

				if(_char == '\n') { // line-termination: enter
					if(this.m_ConnectionState == ConnectionState.Preauthentication) {
						this.CheckAuthentication(this.m_StringBuffer.ToString());
					}
					else if(this.m_ConnectionState == ConnectionState.Keytransfer) {
						this.KeyTransfer(this.m_StringBuffer.ToString());
					}
					else if(this.MessageReceived != null && this.m_ConnectionState == ConnectionState.Authenticated) {
						byte[] _base64 = Convert.FromBase64String(this.m_StringBuffer.ToString());
						byte[] _bytes = EncryptionUtils.RSADecrypt(Server.Instance.RSADecoder, _base64);

						string _message = Encoding.Default.GetString(_bytes);
						MessageEventArgs _e = new MessageEventArgs() {
							Message = _message
						};

						this.MessageReceived(this, _e);
					}

					// this.m_StringBuffer.Remove(0, this.m_StringBuffer.Length);
					this.m_StringBuffer.Clear();
					continue;
				}

				this.m_StringBuffer.Append(_char);
			}
		}

		public void Disconnect(SocketException socketException = null) {
			this.m_Stream.Dispose();
			// this.m_TcpClient.Close();

			if(this.Disconnected != null) {
				EventArgs _e = new EventArgs();
				this.Disconnected(this, _e);
			}

			this.m_ConnectionState = ConnectionState.NotConnected;
		}
	}
}

