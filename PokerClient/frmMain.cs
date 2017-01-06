using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using PokerLibrary;
using System.Drawing;

namespace PokerClient {
	public partial class frmMain : Form {
		public bool m_Connected;
		public TcpClient m_TcpClient;
		public StreamWriter m_StreamWriter;
		public StreamReader m_StreamReader;
		public RSACryptoServiceProvider m_MyRSADecoder;
		public byte[] m_MyPublicKey;
		public RSACryptoServiceProvider m_ServerRSAEncoder;
		public byte[] m_ServerPublicKey;
		public Timer m_Timer;

		public frmMain() {
			this.InitializeComponent();

			this.m_Connected = false;
		}

		private void menuExit_Click(object sender, EventArgs e) {
			Application.Exit();
		}

		private void menuConnect_Click(object sender, EventArgs e) {
			string _host;
			int _port;
			string _username;
			string _password;

			using(frmConnect _form = new frmConnect()) {
				_form.ShowDialog(this);

				if(!_form.IsSuccess) {
					return;
				}

				_host = _form.textHost.Text;
				_port = int.Parse(_form.textPort.Text);

				_username = _form.textUsername.Text;
				_password = _form.textPassword.Text;
			}

			this.m_TcpClient = new TcpClient();
			this.m_Connected = true;
			this.textMessages.Enabled = true;
			this.textBox1.Enabled = true;
			this.menuConnect.Enabled = false;

			this.m_TcpClient.Connect(_host, _port);
			this.m_StreamWriter = new StreamWriter(this.m_TcpClient.GetStream());
			this.m_StreamReader = new StreamReader(this.m_TcpClient.GetStream());

			// u/p credentials hash
			byte[] _hash = EncryptionUtils.Hash<SHA256Managed>(_username + "|" + _password);
			string _hashBase64 = Convert.ToBase64String(_hash, Base64FormattingOptions.None);
			this.m_StreamWriter.WriteLine(_hashBase64);
			this.m_StreamWriter.Flush();

			// server public key
			string _serverBase64String = this.m_StreamReader.ReadLine();
			byte[] _serverBase64 = Convert.FromBase64String(_serverBase64String);

			this.m_ServerPublicKey = EncryptionUtils.SymmetricDecrypt<AesManaged>(_password, _serverBase64);
			this.m_ServerRSAEncoder = EncryptionUtils.LoadRSAForEncoder(this.m_ServerPublicKey);

			// client public key
			byte[] _encryptedPublicKey = EncryptionUtils.SymmetricEncrypt<AesManaged>(_password, this.m_MyPublicKey);
			string _clientBase64 = Convert.ToBase64String(_encryptedPublicKey, Base64FormattingOptions.None);
			this.m_StreamWriter.WriteLine(_clientBase64);
			this.m_StreamWriter.Flush();

			// set timer
			this.m_Timer = new Timer() {
				Interval = 500,
				Enabled = true
			};

			this.m_Timer.Tick += new EventHandler(this.m_Timer_Tick);

			// textbox focus
			this.textBox1.Focus();
		}

		private void m_Timer_Tick(object sender, EventArgs e) {
			while(this.m_TcpClient.Available > 0) {
				string _base64String = this.m_StreamReader.ReadLine();
				byte[] _base64 = Convert.FromBase64String(_base64String);

				byte[] _message = EncryptionUtils.RSADecrypt(this.m_MyRSADecoder, _base64);
				this.ReadFromServer(Encoding.Default.GetString(_message));
			}
		}

		public void ReadFromServer(string message) {
			string[] _messages = message.Split(new char[] { ' ' }, 2);

			switch(_messages[0]) {
			case "MSG":
				string[] _messageParts = _messages[1].Split(new char[] { ' ' }, 2);
				this.textMessages.AppendText(string.Format("{0}> {1}\r\n", _messageParts[0], _messageParts[1]));
				break;
			case "START":
				this.RichTextAppend("Game has started", Color.Red);
				break;
			case "GROUND":
				string[] _messageParts2 = _messages[1].Split(new char[] { ' ' }, 2);

				this.RichTextAppend(_messageParts2[0], Color.Red);

				StringBuilder _cards = new StringBuilder();
				foreach(string _line in _messageParts2[1].Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)) {
					string[] _cardInfo = _line.Split(new char[] { '|' });
					_cards.Append(_cardInfo[0]);
					_cards.Append(" ");
					_cards.Append(_cardInfo[1]);
					_cards.Append(", ");
				}

				this.RichTextAppend(_cards.ToString(0, _cards.Length - 2) + " are on the ground.", Color.SlateBlue);
				break;
			case "CARDS":
				StringBuilder _cards2 = new StringBuilder();
				foreach(string _line in _messages[1].Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)) {
					string[] _cardInfo = _line.Split(new char[] { '|' });
					_cards2.Append(_cardInfo[0]);
					_cards2.Append(" ");
					_cards2.Append(_cardInfo[1]);
					_cards2.Append(", ");
				}

				this.RichTextAppend("You have " + _cards2.ToString(0, _cards2.Length - 2), Color.Blue);
				break;
			default:
				MessageBox.Show(this, _messages[1], _messages[0], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				break;
			}
		}

		public void SendToServer(string message) {
			byte[] _bytes = EncryptionUtils.RSAEncrypt(this.m_ServerRSAEncoder, message);
			string _base64Bytes = Convert.ToBase64String(_bytes, Base64FormattingOptions.None);

			this.m_StreamWriter.WriteLine(_base64Bytes);
			this.m_StreamWriter.Flush();
		}

		public void SendToServer(byte[] message) {
			byte[] _bytes = EncryptionUtils.RSAEncrypt(this.m_ServerRSAEncoder, message);
			string _base64Bytes = Convert.ToBase64String(_bytes, Base64FormattingOptions.None);

			this.m_StreamWriter.WriteLine(_base64Bytes);
			this.m_StreamWriter.Flush();
		}

		private void menuAbout_Click(object sender, EventArgs e) {
			StringBuilder _string = new StringBuilder();
			_string.AppendLine("Poker over network");
			_string.AppendLine("---");
			_string.AppendLine();
			_string.AppendLine("Credits:");
			_string.AppendLine("\tEser Ozvataf");
			_string.AppendLine("\tEmir Kara");
			_string.AppendLine("\tOzer Yavuzaslan");
			_string.AppendLine();
			_string.AppendLine("Uses:");
			_string.AppendLine("\tRSA for asymmetric encryption");
			_string.AppendLine("\tAES for symmetric encryption");
			_string.AppendLine("\tSHA2 for hashing");
			_string.AppendLine();
			_string.AppendLine("Prepared for ITEC433 Cryptography and Network Security");

			MessageBox.Show(this, _string.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
		}

		private void textBox1_KeyPress(object sender, KeyPressEventArgs e) {
			if(e.KeyChar == 13) {
				this.SendToServer("MSG " + this.textBox1.Text);
				this.textBox1.Clear();
			}
		}

		private void frmMain_Load(object sender, EventArgs e) {
			this.m_MyRSADecoder = EncryptionUtils.CreateRSAForDecoder(out this.m_MyPublicKey);
		}

		public void RichTextAppend(string text, Color color) {
			this.richTextBox1.SelectionStart = this.richTextBox1.TextLength;
			this.richTextBox1.SelectionLength = 0;

			this.richTextBox1.SelectionColor = color;
			this.richTextBox1.AppendText(text + "\r\n");
			this.richTextBox1.SelectionColor = this.richTextBox1.ForeColor;
		}

		private void btnCheck_Click(object sender, EventArgs e) {
			this.SendToServer("CHECK");
		}
	}
}
