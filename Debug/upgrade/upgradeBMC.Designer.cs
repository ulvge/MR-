
namespace Debug {
    partial class UpgradeBMC {
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
            this.bt_telnet = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_fileTelnet = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_fileHpm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bt_hpm = new System.Windows.Forms.Button();
            this.cb_upgradeIP = new System.Windows.Forms.ComboBox();
            this.checkBox_decryptOnly = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_loginUserName = new System.Windows.Forms.TextBox();
            this.tb_loginPwd = new System.Windows.Forms.TextBox();
            this.tb_upgradeLog = new System.Windows.Forms.RichTextBox();
            this.dg_upgradeProcessBar = new System.Windows.Forms.DataGridView();
            this.cb_ipmiCmd = new System.Windows.Forms.ComboBox();
            this.bt_ipmiCmd = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dg_upgradeProcessBar)).BeginInit();
            this.SuspendLayout();
            // 
            // bt_telnet
            // 
            this.bt_telnet.Location = new System.Drawing.Point(657, 134);
            this.bt_telnet.Name = "bt_telnet";
            this.bt_telnet.Size = new System.Drawing.Size(75, 23);
            this.bt_telnet.TabIndex = 0;
            this.bt_telnet.Text = "破解";
            this.bt_telnet.UseVisualStyleBackColor = true;
            this.bt_telnet.Click += new System.EventHandler(this.bt_telnet_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "telnet破解";
            // 
            // tb_fileTelnet
            // 
            this.tb_fileTelnet.Location = new System.Drawing.Point(118, 138);
            this.tb_fileTelnet.Name = "tb_fileTelnet";
            this.tb_fileTelnet.Size = new System.Drawing.Size(513, 21);
            this.tb_fileTelnet.TabIndex = 2;
            this.tb_fileTelnet.DoubleClick += new System.EventHandler(this.tb_fileTelnet_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 174);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "升级包";
            // 
            // tb_fileHpm
            // 
            this.tb_fileHpm.Location = new System.Drawing.Point(118, 170);
            this.tb_fileHpm.Name = "tb_fileHpm";
            this.tb_fileHpm.Size = new System.Drawing.Size(513, 21);
            this.tb_fileHpm.TabIndex = 2;
            this.tb_fileHpm.DoubleClick += new System.EventHandler(this.tb_fileBMCUpgradeHpm_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "指定IP";
            // 
            // bt_hpm
            // 
            this.bt_hpm.Location = new System.Drawing.Point(657, 169);
            this.bt_hpm.Name = "bt_hpm";
            this.bt_hpm.Size = new System.Drawing.Size(75, 23);
            this.bt_hpm.TabIndex = 31;
            this.bt_hpm.Text = "升级";
            this.bt_hpm.UseVisualStyleBackColor = true;
            this.bt_hpm.Click += new System.EventHandler(this.bt_hpm_Click);
            // 
            // cb_upgradeIP
            // 
            this.cb_upgradeIP.FormattingEnabled = true;
            this.cb_upgradeIP.ItemHeight = 12;
            this.cb_upgradeIP.Location = new System.Drawing.Point(118, 200);
            this.cb_upgradeIP.Name = "cb_upgradeIP";
            this.cb_upgradeIP.Size = new System.Drawing.Size(292, 20);
            this.cb_upgradeIP.TabIndex = 32;
            // 
            // checkBox_decryptOnly
            // 
            this.checkBox_decryptOnly.AutoSize = true;
            this.checkBox_decryptOnly.Location = new System.Drawing.Point(499, 202);
            this.checkBox_decryptOnly.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_decryptOnly.Name = "checkBox_decryptOnly";
            this.checkBox_decryptOnly.Size = new System.Drawing.Size(132, 16);
            this.checkBox_decryptOnly.TabIndex = 33;
            this.checkBox_decryptOnly.Text = "升级完成后是否重启";
            this.checkBox_decryptOnly.UseVisualStyleBackColor = true;
            this.checkBox_decryptOnly.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "BMC用户名";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(42, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "BMC密码";
            // 
            // tb_loginUserName
            // 
            this.tb_loginUserName.Location = new System.Drawing.Point(118, 27);
            this.tb_loginUserName.Name = "tb_loginUserName";
            this.tb_loginUserName.Size = new System.Drawing.Size(154, 21);
            this.tb_loginUserName.TabIndex = 2;
            // 
            // tb_loginPwd
            // 
            this.tb_loginPwd.Location = new System.Drawing.Point(118, 54);
            this.tb_loginPwd.Name = "tb_loginPwd";
            this.tb_loginPwd.Size = new System.Drawing.Size(154, 21);
            this.tb_loginPwd.TabIndex = 2;
            // 
            // tb_upgradeLog
            // 
            this.tb_upgradeLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_upgradeLog.Location = new System.Drawing.Point(738, 12);
            this.tb_upgradeLog.Name = "tb_upgradeLog";
            this.tb_upgradeLog.Size = new System.Drawing.Size(597, 350);
            this.tb_upgradeLog.TabIndex = 34;
            this.tb_upgradeLog.Text = "";
            this.tb_upgradeLog.TextChanged += new System.EventHandler(this.tb_upgradeLog_TextChanged);
            // 
            // dg_upgradeProcessBar
            // 
            this.dg_upgradeProcessBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dg_upgradeProcessBar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
            this.dg_upgradeProcessBar.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dg_upgradeProcessBar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_upgradeProcessBar.Location = new System.Drawing.Point(738, 363);
            this.dg_upgradeProcessBar.Name = "dg_upgradeProcessBar";
            this.dg_upgradeProcessBar.RowTemplate.Height = 23;
            this.dg_upgradeProcessBar.Size = new System.Drawing.Size(597, 189);
            this.dg_upgradeProcessBar.TabIndex = 35;
            this.dg_upgradeProcessBar.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // cb_ipmiCmd
            // 
            this.cb_ipmiCmd.FormattingEnabled = true;
            this.cb_ipmiCmd.ItemHeight = 12;
            this.cb_ipmiCmd.Items.AddRange(new object[] {
            "power status",
            "power reset",
            "power on",
            "BMC版本"});
            this.cb_ipmiCmd.Location = new System.Drawing.Point(118, 245);
            this.cb_ipmiCmd.Name = "cb_ipmiCmd";
            this.cb_ipmiCmd.Size = new System.Drawing.Size(292, 20);
            this.cb_ipmiCmd.TabIndex = 32;
            // 
            // bt_ipmiCmd
            // 
            this.bt_ipmiCmd.Location = new System.Drawing.Point(657, 245);
            this.bt_ipmiCmd.Name = "bt_ipmiCmd";
            this.bt_ipmiCmd.Size = new System.Drawing.Size(75, 23);
            this.bt_ipmiCmd.TabIndex = 31;
            this.bt_ipmiCmd.Text = "执行命令";
            this.bt_ipmiCmd.UseVisualStyleBackColor = true;
            this.bt_ipmiCmd.Click += new System.EventHandler(this.bt_ipmiCmd_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(42, 250);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "ipmiCmd";
            // 
            // UpgradeBMC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1347, 548);
            this.Controls.Add(this.dg_upgradeProcessBar);
            this.Controls.Add(this.tb_upgradeLog);
            this.Controls.Add(this.checkBox_decryptOnly);
            this.Controls.Add(this.cb_ipmiCmd);
            this.Controls.Add(this.cb_upgradeIP);
            this.Controls.Add(this.bt_ipmiCmd);
            this.Controls.Add(this.bt_hpm);
            this.Controls.Add(this.tb_fileHpm);
            this.Controls.Add(this.tb_loginPwd);
            this.Controls.Add(this.tb_loginUserName);
            this.Controls.Add(this.tb_fileTelnet);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_telnet);
            this.Name = "UpgradeBMC";
            this.Text = "upgrade";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpgradeBMC_FormClosing);
            this.Load += new System.EventHandler(this.UpgradeBMC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg_upgradeProcessBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_telnet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_fileTelnet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_fileHpm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bt_hpm;
        private System.Windows.Forms.ComboBox cb_upgradeIP;
        private System.Windows.Forms.CheckBox checkBox_decryptOnly;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_loginUserName;
        private System.Windows.Forms.TextBox tb_loginPwd;
        private System.Windows.Forms.RichTextBox tb_upgradeLog;
        private System.Windows.Forms.DataGridView dg_upgradeProcessBar;
        private System.Windows.Forms.ComboBox cb_ipmiCmd;
        private System.Windows.Forms.Button bt_ipmiCmd;
        private System.Windows.Forms.Label label6;
    }
}