using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PokerClient {
	public partial class frmConnect : Form {
		private bool m_IsSuccess;

		public frmConnect() {
			this.InitializeComponent();
		}

		public bool IsSuccess {
			get {
				return this.m_IsSuccess;
			}
		}

		private void btnConnect_Click(object sender, EventArgs e) {
			this.errorProvider1.Clear();

			if(this.textHost.Text.Trim().Length == 0) {
				this.errorProvider1.SetError(this.textHost, "Shouldn't be left blank.");
				return;
			}

			int _portValue;
			if(this.textPort.Text.Trim().Length == 0 || !int.TryParse(this.textPort.Text, out _portValue) || _portValue < 0 || _portValue > 65535) {
				this.errorProvider1.SetError(this.textPort, "Must be in valid range.");
				return;
			}

			if(this.textUsername.Text.Trim().Length == 0) {
				this.errorProvider1.SetError(this.textUsername, "Shouldn't be left blank.");
				return;
			}

			if(this.textPassword.Text.Trim().Length == 0) {
				this.errorProvider1.SetError(this.textPassword, "Shouldn't be left blank.");
				return;
			}

			this.m_IsSuccess = true;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e) {
			this.m_IsSuccess = false;
			this.Close();
		}

		private void frmConnect_Shown(object sender, EventArgs e) {
			this.textUsername.Focus();
		}
	}
}
