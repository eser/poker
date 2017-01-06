namespace PokerClient {
	partial class frmConnect {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.btnConnect = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textPort = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textHost = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.textUsername = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textPassword = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
			this.SuspendLayout();
			// 
			// btnConnect
			// 
			this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnConnect.Location = new System.Drawing.Point(235, 202);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(75, 23);
			this.btnConnect.TabIndex = 9;
			this.btnConnect.Text = "&Connect";
			this.btnConnect.UseVisualStyleBackColor = true;
			this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.textPassword);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.textUsername);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.textPort);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.textHost);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(381, 184);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// textPort
			// 
			this.textPort.Location = new System.Drawing.Point(98, 50);
			this.textPort.Name = "textPort";
			this.textPort.Size = new System.Drawing.Size(78, 20);
			this.textPort.TabIndex = 4;
			this.textPort.Text = "12345";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(17, 53);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "&Port:";
			// 
			// textHost
			// 
			this.textHost.Location = new System.Drawing.Point(98, 22);
			this.textHost.Name = "textHost";
			this.textHost.Size = new System.Drawing.Size(262, 20);
			this.textHost.TabIndex = 2;
			this.textHost.Text = "localhost";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(17, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "&Host:";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(318, 202);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 10;
			this.btnCancel.Text = "Ca&ncel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// textUsername
			// 
			this.textUsername.Location = new System.Drawing.Point(98, 105);
			this.textUsername.Name = "textUsername";
			this.textUsername.Size = new System.Drawing.Size(262, 20);
			this.textUsername.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(17, 108);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(58, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "&Username:";
			// 
			// textPassword
			// 
			this.textPassword.Location = new System.Drawing.Point(98, 131);
			this.textPassword.Name = "textPassword";
			this.textPassword.Size = new System.Drawing.Size(262, 20);
			this.textPassword.TabIndex = 8;
			this.textPassword.UseSystemPasswordChar = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(17, 134);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "P&assword:";
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// frmConnect
			// 
			this.AcceptButton = this.btnConnect;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(405, 237);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnConnect);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "frmConnect";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Connect To Server";
			this.Shown += new System.EventHandler(this.frmConnect_Shown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnCancel;
		public System.Windows.Forms.TextBox textPort;
		public System.Windows.Forms.TextBox textHost;
		public System.Windows.Forms.TextBox textPassword;
		private System.Windows.Forms.Label label4;
		public System.Windows.Forms.TextBox textUsername;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ErrorProvider errorProvider1;
	}
}