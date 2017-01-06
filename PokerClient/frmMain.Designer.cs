namespace PokerClient {
	partial class frmMain {
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.menuGame = new System.Windows.Forms.ToolStripMenuItem();
			this.menuConnect = new System.Windows.Forms.ToolStripMenuItem();
			this.menuGameSeperator1 = new System.Windows.Forms.ToolStripSeparator();
			this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.textMessages = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnCheck = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuGame,
            this.menuHelp});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(834, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// menuGame
			// 
			this.menuGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuConnect,
            this.menuGameSeperator1,
            this.menuExit});
			this.menuGame.Name = "menuGame";
			this.menuGame.Size = new System.Drawing.Size(50, 20);
			this.menuGame.Text = "&Game";
			// 
			// menuConnect
			// 
			this.menuConnect.Name = "menuConnect";
			this.menuConnect.ShortcutKeyDisplayString = "";
			this.menuConnect.ShortcutKeys = System.Windows.Forms.Keys.F2;
			this.menuConnect.Size = new System.Drawing.Size(147, 22);
			this.menuConnect.Text = "&Connect...";
			this.menuConnect.Click += new System.EventHandler(this.menuConnect_Click);
			// 
			// menuGameSeperator1
			// 
			this.menuGameSeperator1.Name = "menuGameSeperator1";
			this.menuGameSeperator1.Size = new System.Drawing.Size(144, 6);
			// 
			// menuExit
			// 
			this.menuExit.Name = "menuExit";
			this.menuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.menuExit.Size = new System.Drawing.Size(147, 22);
			this.menuExit.Text = "E&xit";
			this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
			// 
			// menuHelp
			// 
			this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAbout});
			this.menuHelp.Name = "menuHelp";
			this.menuHelp.Size = new System.Drawing.Size(44, 20);
			this.menuHelp.Text = "&Help";
			// 
			// menuAbout
			// 
			this.menuAbout.Name = "menuAbout";
			this.menuAbout.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.menuAbout.Size = new System.Drawing.Size(135, 22);
			this.menuAbout.Text = "&About...";
			this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
			// 
			// textMessages
			// 
			this.textMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textMessages.BackColor = System.Drawing.SystemColors.Control;
			this.textMessages.Enabled = false;
			this.textMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.textMessages.Location = new System.Drawing.Point(576, 27);
			this.textMessages.Multiline = true;
			this.textMessages.Name = "textMessages";
			this.textMessages.ReadOnly = true;
			this.textMessages.Size = new System.Drawing.Size(246, 364);
			this.textMessages.TabIndex = 1;
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Enabled = false;
			this.textBox1.Location = new System.Drawing.Point(629, 397);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(193, 20);
			this.textBox1.TabIndex = 2;
			this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(573, 400);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Chat:";
			// 
			// richTextBox1
			// 
			this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBox1.BackColor = System.Drawing.Color.White;
			this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.richTextBox1.Location = new System.Drawing.Point(12, 27);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
			this.richTextBox1.Size = new System.Drawing.Size(558, 300);
			this.richTextBox1.TabIndex = 4;
			this.richTextBox1.Text = "";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.button3);
			this.groupBox1.Controls.Add(this.button2);
			this.groupBox1.Controls.Add(this.btnCheck);
			this.groupBox1.Location = new System.Drawing.Point(12, 333);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(558, 84);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			// 
			// btnCheck
			// 
			this.btnCheck.Location = new System.Drawing.Point(6, 19);
			this.btnCheck.Name = "btnCheck";
			this.btnCheck.Size = new System.Drawing.Size(100, 59);
			this.btnCheck.TabIndex = 0;
			this.btnCheck.Text = "Check";
			this.btnCheck.UseVisualStyleBackColor = true;
			this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
			// 
			// button2
			// 
			this.button2.Enabled = false;
			this.button2.Location = new System.Drawing.Point(112, 19);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(100, 59);
			this.button2.TabIndex = 1;
			this.button2.Text = "Bet";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.Enabled = false;
			this.button3.Location = new System.Drawing.Point(218, 19);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(100, 59);
			this.button3.TabIndex = 2;
			this.button3.Text = "Fold";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(834, 429);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.textMessages);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Poker";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem menuGame;
		private System.Windows.Forms.ToolStripMenuItem menuConnect;
		private System.Windows.Forms.ToolStripSeparator menuGameSeperator1;
		private System.Windows.Forms.ToolStripMenuItem menuExit;
		private System.Windows.Forms.ToolStripMenuItem menuHelp;
		private System.Windows.Forms.ToolStripMenuItem menuAbout;
		private System.Windows.Forms.TextBox textMessages;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button btnCheck;
	}
}

