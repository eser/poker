using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading;
using System.Xml;
using PokerLibrary;

namespace PokerServer {
	public class Server : IDisposable {
		private static Server s_Instance = null;

		private readonly Game m_Game;

		private Dictionary<string, string> m_Accounts;
		private Dictionary<Guid, Client> m_Clients;
		private RSACryptoServiceProvider m_RSADecoder;
		private byte[] m_PublicKey;
		private TcpListener m_Listener;
		private Thread m_Thread;

		public Server() {
			if(Server.s_Instance == null) {
				Server.s_Instance = this;
			}

			this.m_Game = new Game();

			this.m_Accounts = new Dictionary<string, string>();
			this.m_Clients = new Dictionary<Guid, Client>();
		}

		public static Server Instance {
			get {
				return Server.s_Instance;
			}
		}

		public Game Game {
			get {
				return this.m_Game;
			}
		}

		public Dictionary<string, string> Accounts {
			get {
				return this.m_Accounts;
			}
		}

		public Dictionary<Guid, Client> Clients {
			get {
				return this.m_Clients;
			}
		}

		public RSACryptoServiceProvider RSADecoder {
			get {
				return this.m_RSADecoder;
			}
		}

		public byte[] PublicKey {
			get {
				return this.m_PublicKey;
			}
		}

		public TcpListener Listener {
			get {
				return this.m_Listener;
			}
		}

		public Thread Thread {
			get {
				return this.m_Thread;
			}
		}

		public void OnConnected(object sender, EventArgs e) {
			Client _client = (Client)sender;

			this.m_Clients.Add(_client.ClientId, _client);
			this.m_Game.Players.Add(_client.ClientId, new Player() { Name = _client.Username });

			this.ServiceMessage(string.Format("{0} connected", _client.Username), "Server");
			this.m_Game.CheckGame();
		}

		public void OnDisconnected(object sender, EventArgs e) {
			Client _client = (Client)sender;

			if(!this.m_Clients.ContainsKey(_client.ClientId)) {
				return;
			}

			this.m_Clients.Remove(_client.ClientId);

			if(this.m_Game.Players.ContainsKey(_client.ClientId)) {
				this.m_Game.Players.Remove(_client.ClientId);
			}

			if(_client.ConnectionState != ConnectionState.Authenticated) {
				return;
			}

			this.ServiceMessage(string.Format("{0} disconnected", _client.Username), "Server");
		}

		public void OnMessageReceived(object sender, MessageEventArgs e) {
			Client _client = (Client)sender;

			string[] _messages = e.Message.Split(new char[] { ' ' }, 2);

			switch(_messages[0]) {
			case "MSG":
				this.ServiceMessage(_messages[1], _client.Username);
				break;
			case "CHECK":
				this.m_Game.NextStage();
				break;
			}
		}

		public void ServiceMessage(string message, string originator) {
			Console.WriteLine(originator + "> " + message);
			this.Broadcast("MSG " + originator + " " + message);
		}

		public void Broadcast(string message) {
			foreach(Client _client in this.m_Clients.Values) {
				_client.SendEncrypted(message);
			}
		}

		public void StartListen() {
			this.m_Listener = new TcpListener(IPAddress.Any, 12345);
			this.m_Listener.Start();

			do {
				Client _client = new Client(this.m_Listener.AcceptTcpClient());

				_client.Disconnected += new DisconnectDelegate(this.OnDisconnected);
				_client.Connected += new ConnectDelegate(this.OnConnected);
				_client.MessageReceived += new MessageDelegate(this.OnMessageReceived);

				_client.Connect();
			}
			while(true);
		}

		public void Start() {
			this.m_RSADecoder = EncryptionUtils.CreateRSAForDecoder(out this.m_PublicKey);

			this.m_Accounts.Clear();

			XmlDocument _xmlDocument = new XmlDocument();
			_xmlDocument.Load("accounts.xml");

			foreach(XmlNode _xmlNode in _xmlDocument.SelectNodes("/accounts/account")) {
				this.m_Accounts.Add(
					_xmlNode.Attributes["username"].InnerText,
					_xmlNode.Attributes["password"].InnerText
				);
			}

			this.m_Thread = new Thread(new ThreadStart(this.StartListen));
			this.m_Thread.Start();

			Console.WriteLine("RUN: {0}", DateTime.UtcNow.Subtract(this.m_Game.StartTime).TotalMilliseconds);
		}

		public void Stop() {
		}

		#region IDisposable implementation
		public void Dispose() {

		}
		#endregion
	}
}
