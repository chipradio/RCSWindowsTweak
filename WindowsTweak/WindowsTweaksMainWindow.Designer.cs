namespace WindowsTweak
{
    partial class WindowsTweaksMainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowsTweaksMainWindow));
            this.lvWindowsTweaks = new System.Windows.Forms.ListView();
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRunTweaks = new System.Windows.Forms.Button();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnInstallTeamViewer = new System.Windows.Forms.Button();
            this.btnInstallUltraVNC = new System.Windows.Forms.Button();
            this.btnInstallSentinelDrivers = new System.Windows.Forms.Button();
            this.btnInstallASIDrivers = new System.Windows.Forms.Button();
            this.btnSetAutoLogin = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._btnSysInternalsSuite = new System.Windows.Forms.Button();
            this._btnProcessHacker = new System.Windows.Forms.Button();
            this.btnInstallPowerShell = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this._radioNexGen = new System.Windows.Forms.RadioButton();
            this._radioZetta = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this._radioNone = new System.Windows.Forms.RadioButton();
            this.btnRCSTVHost = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lvWindowsTweaks
            // 
            this.lvWindowsTweaks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvWindowsTweaks.CheckBoxes = true;
            this.lvWindowsTweaks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader17,
            this.columnHeader18,
            this.columnHeader1});
            this.lvWindowsTweaks.FullRowSelect = true;
            this.lvWindowsTweaks.GridLines = true;
            this.lvWindowsTweaks.Location = new System.Drawing.Point(24, 37);
            this.lvWindowsTweaks.MultiSelect = false;
            this.lvWindowsTweaks.Name = "lvWindowsTweaks";
            this.lvWindowsTweaks.Size = new System.Drawing.Size(740, 361);
            this.lvWindowsTweaks.TabIndex = 1;
            this.lvWindowsTweaks.UseCompatibleStateImageBehavior = false;
            this.lvWindowsTweaks.View = System.Windows.Forms.View.Details;
            this.lvWindowsTweaks.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvWindowsTweaks_ColumnClick);
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "Setting/Action";
            this.columnHeader17.Width = 292;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "Description";
            this.columnHeader18.Width = 341;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Status";
            // 
            // btnRunTweaks
            // 
            this.btnRunTweaks.Location = new System.Drawing.Point(336, 410);
            this.btnRunTweaks.Name = "btnRunTweaks";
            this.btnRunTweaks.Size = new System.Drawing.Size(124, 23);
            this.btnRunTweaks.TabIndex = 2;
            this.btnRunTweaks.Text = "Run Tweaks";
            this.btnRunTweaks.UseVisualStyleBackColor = true;
            this.btnRunTweaks.Click += new System.EventHandler(this.btnRunTweaks_Click);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(6, 39);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(204, 20);
            this.txtUserName.TabIndex = 3;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(216, 39);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(204, 20);
            this.txtPassword.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "UserName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(216, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Password";
            // 
            // btnInstallTeamViewer
            // 
            this.btnInstallTeamViewer.Location = new System.Drawing.Point(8, 21);
            this.btnInstallTeamViewer.Name = "btnInstallTeamViewer";
            this.btnInstallTeamViewer.Size = new System.Drawing.Size(124, 23);
            this.btnInstallTeamViewer.TabIndex = 7;
            this.btnInstallTeamViewer.Text = "RCS TeamViewer QS";
            this.btnInstallTeamViewer.UseVisualStyleBackColor = true;
            this.btnInstallTeamViewer.Click += new System.EventHandler(this.btnInstallTeamViewer_Click);
            // 
            // btnInstallUltraVNC
            // 
            this.btnInstallUltraVNC.Location = new System.Drawing.Point(159, 21);
            this.btnInstallUltraVNC.Name = "btnInstallUltraVNC";
            this.btnInstallUltraVNC.Size = new System.Drawing.Size(124, 23);
            this.btnInstallUltraVNC.TabIndex = 8;
            this.btnInstallUltraVNC.Text = "UltraVNC";
            this.btnInstallUltraVNC.UseVisualStyleBackColor = true;
            this.btnInstallUltraVNC.Click += new System.EventHandler(this.btnInstallUltraVNC_Click);
            // 
            // btnInstallSentinelDrivers
            // 
            this.btnInstallSentinelDrivers.Location = new System.Drawing.Point(310, 21);
            this.btnInstallSentinelDrivers.Name = "btnInstallSentinelDrivers";
            this.btnInstallSentinelDrivers.Size = new System.Drawing.Size(124, 23);
            this.btnInstallSentinelDrivers.TabIndex = 9;
            this.btnInstallSentinelDrivers.Text = "Sentinel Drivers";
            this.btnInstallSentinelDrivers.UseVisualStyleBackColor = true;
            this.btnInstallSentinelDrivers.Click += new System.EventHandler(this.btnInstallSentinelDrivers_Click);
            // 
            // btnInstallASIDrivers
            // 
            this.btnInstallASIDrivers.Location = new System.Drawing.Point(611, 19);
            this.btnInstallASIDrivers.Name = "btnInstallASIDrivers";
            this.btnInstallASIDrivers.Size = new System.Drawing.Size(124, 23);
            this.btnInstallASIDrivers.TabIndex = 10;
            this.btnInstallASIDrivers.Text = "ASI Combo Drivers";
            this.btnInstallASIDrivers.UseVisualStyleBackColor = true;
            this.btnInstallASIDrivers.Click += new System.EventHandler(this.btnInstallASIDrivers_Click);
            // 
            // btnSetAutoLogin
            // 
            this.btnSetAutoLogin.Location = new System.Drawing.Point(580, 37);
            this.btnSetAutoLogin.Name = "btnSetAutoLogin";
            this.btnSetAutoLogin.Size = new System.Drawing.Size(138, 23);
            this.btnSetAutoLogin.TabIndex = 11;
            this.btnSetAutoLogin.Text = "Set AutoLogin for User";
            this.btnSetAutoLogin.UseVisualStyleBackColor = true;
            this.btnSetAutoLogin.Click += new System.EventHandler(this.btnSetAutoLogin_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtDomain);
            this.groupBox1.Controls.Add(this.btnSetAutoLogin);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Location = new System.Drawing.Point(24, 465);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(740, 72);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "AutoLogin ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(422, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Domain (*Optional)";
            // 
            // txtDomain
            // 
            this.txtDomain.Location = new System.Drawing.Point(425, 39);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.PasswordChar = '*';
            this.txtDomain.Size = new System.Drawing.Size(149, 20);
            this.txtDomain.TabIndex = 12;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnRCSTVHost);
            this.groupBox2.Controls.Add(this._btnSysInternalsSuite);
            this.groupBox2.Controls.Add(this._btnProcessHacker);
            this.groupBox2.Controls.Add(this.btnInstallPowerShell);
            this.groupBox2.Controls.Add(this.btnInstallTeamViewer);
            this.groupBox2.Controls.Add(this.btnInstallASIDrivers);
            this.groupBox2.Controls.Add(this.btnInstallUltraVNC);
            this.groupBox2.Controls.Add(this.btnInstallSentinelDrivers);
            this.groupBox2.Location = new System.Drawing.Point(24, 571);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(743, 96);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Additional Installs";
            // 
            // _btnSysInternalsSuite
            // 
            this._btnSysInternalsSuite.Location = new System.Drawing.Point(460, 58);
            this._btnSysInternalsSuite.Name = "_btnSysInternalsSuite";
            this._btnSysInternalsSuite.Size = new System.Drawing.Size(124, 23);
            this._btnSysInternalsSuite.TabIndex = 13;
            this._btnSysInternalsSuite.Text = "SysInternals Suite";
            this._btnSysInternalsSuite.UseVisualStyleBackColor = true;
            this._btnSysInternalsSuite.Click += new System.EventHandler(this._btnSysInternalsSuite_Click);
            // 
            // _btnProcessHacker
            // 
            this._btnProcessHacker.Location = new System.Drawing.Point(310, 58);
            this._btnProcessHacker.Name = "_btnProcessHacker";
            this._btnProcessHacker.Size = new System.Drawing.Size(124, 23);
            this._btnProcessHacker.TabIndex = 12;
            this._btnProcessHacker.Text = "Process Hacker";
            this._btnProcessHacker.UseVisualStyleBackColor = true;
            this._btnProcessHacker.Click += new System.EventHandler(this._btnProcessHacker_Click);
            // 
            // btnInstallPowerShell
            // 
            this.btnInstallPowerShell.Location = new System.Drawing.Point(460, 19);
            this.btnInstallPowerShell.Name = "btnInstallPowerShell";
            this.btnInstallPowerShell.Size = new System.Drawing.Size(124, 23);
            this.btnInstallPowerShell.TabIndex = 11;
            this.btnInstallPowerShell.Text = "PowerShell";
            this.btnInstallPowerShell.UseVisualStyleBackColor = true;
            this.btnInstallPowerShell.Click += new System.EventHandler(this.btnInstallPowerShell_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(24, 449);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(743, 2);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Black;
            this.pictureBox2.Location = new System.Drawing.Point(24, 553);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(743, 2);
            this.pictureBox2.TabIndex = 15;
            this.pictureBox2.TabStop = false;
            // 
            // _radioNexGen
            // 
            this._radioNexGen.AutoSize = true;
            this._radioNexGen.Location = new System.Drawing.Point(130, 12);
            this._radioNexGen.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._radioNexGen.Name = "_radioNexGen";
            this._radioNexGen.Size = new System.Drawing.Size(64, 17);
            this._radioNexGen.TabIndex = 16;
            this._radioNexGen.TabStop = true;
            this._radioNexGen.Text = "NexGen";
            this._radioNexGen.UseVisualStyleBackColor = true;
            this._radioNexGen.CheckedChanged += new System.EventHandler(this._radioNexGen_CheckedChanged);
            // 
            // _radioZetta
            // 
            this._radioZetta.AutoSize = true;
            this._radioZetta.Location = new System.Drawing.Point(208, 12);
            this._radioZetta.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._radioZetta.Name = "_radioZetta";
            this._radioZetta.Size = new System.Drawing.Size(50, 17);
            this._radioZetta.TabIndex = 17;
            this._radioZetta.TabStop = true;
            this._radioZetta.Text = "Zetta";
            this._radioZetta.UseVisualStyleBackColor = true;
            this._radioZetta.CheckedChanged += new System.EventHandler(this._radioZetta_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Software Application";
            // 
            // _radioNone
            // 
            this._radioNone.AutoSize = true;
            this._radioNone.Location = new System.Drawing.Point(279, 12);
            this._radioNone.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._radioNone.Name = "_radioNone";
            this._radioNone.Size = new System.Drawing.Size(51, 17);
            this._radioNone.TabIndex = 19;
            this._radioNone.TabStop = true;
            this._radioNone.Text = "None";
            this._radioNone.UseVisualStyleBackColor = true;
            this._radioNone.CheckedChanged += new System.EventHandler(this._radioNone_CheckedChanged);
            // 
            // btnRCSTVHost
            // 
            this.btnRCSTVHost.Location = new System.Drawing.Point(159, 58);
            this.btnRCSTVHost.Name = "btnRCSTVHost";
            this.btnRCSTVHost.Size = new System.Drawing.Size(124, 23);
            this.btnRCSTVHost.TabIndex = 14;
            this.btnRCSTVHost.Text = "RCS TeamViewer Host";
            this.btnRCSTVHost.UseVisualStyleBackColor = true;
            this.btnRCSTVHost.Click += new System.EventHandler(this.btnRCSTVHost_Click);
            // 
            // WindowsTweaksMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 678);
            this.Controls.Add(this._radioNone);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._radioZetta);
            this.Controls.Add(this._radioNexGen);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRunTweaks);
            this.Controls.Add(this.lvWindowsTweaks);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WindowsTweaksMainWindow";
            this.Text = "Windows Tweaks for RCS V2";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvWindowsTweaks;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ColumnHeader columnHeader18;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnRunTweaks;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnInstallTeamViewer;
        private System.Windows.Forms.Button btnInstallUltraVNC;
        private System.Windows.Forms.Button btnInstallSentinelDrivers;
        private System.Windows.Forms.Button btnInstallASIDrivers;
        private System.Windows.Forms.Button btnSetAutoLogin;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnInstallPowerShell;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.RadioButton _radioNexGen;
        private System.Windows.Forms.RadioButton _radioZetta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button _btnProcessHacker;
        private System.Windows.Forms.Button _btnSysInternalsSuite;
        private System.Windows.Forms.RadioButton _radioNone;
        private System.Windows.Forms.Button btnRCSTVHost;
    }
}

