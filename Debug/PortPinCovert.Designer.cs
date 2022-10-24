
namespace Debug
{
    partial class PortPinCovert
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cb_frameTag = new System.Windows.Forms.CheckBox();
            this.cb_cmd = new System.Windows.Forms.ComboBox();
            this.cb_netFun = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_chkSumOutput = new System.Windows.Forms.TextBox();
            this.softPortPin = new System.Windows.Forms.TextBox();
            this.tb_IPMB = new System.Windows.Forms.TextBox();
            this.tb_chkSumInput = new System.Windows.Forms.TextBox();
            this.tb_data = new System.Windows.Forms.TextBox();
            this.tb_serialNumber = new System.Windows.Forms.TextBox();
            this.tb_selfAddr = new System.Windows.Forms.TextBox();
            this.tb_LUN = new System.Windows.Forms.TextBox();
            this.tb_target = new System.Windows.Forms.TextBox();
            this.hwPortPin = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Gray;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitContainer1.Panel1.Controls.Add(this.cb_frameTag);
            this.splitContainer1.Panel1.Controls.Add(this.cb_cmd);
            this.splitContainer1.Panel1.Controls.Add(this.cb_netFun);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label11);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label12);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.tb_chkSumOutput);
            this.splitContainer1.Panel1.Controls.Add(this.softPortPin);
            this.splitContainer1.Panel1.Controls.Add(this.tb_IPMB);
            this.splitContainer1.Panel1.Controls.Add(this.tb_chkSumInput);
            this.splitContainer1.Panel1.Controls.Add(this.tb_data);
            this.splitContainer1.Panel1.Controls.Add(this.tb_serialNumber);
            this.splitContainer1.Panel1.Controls.Add(this.tb_selfAddr);
            this.splitContainer1.Panel1.Controls.Add(this.tb_LUN);
            this.splitContainer1.Panel1.Controls.Add(this.tb_target);
            this.splitContainer1.Panel1.Controls.Add(this.hwPortPin);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitContainer1.Size = new System.Drawing.Size(1431, 398);
            this.splitContainer1.SplitterDistance = 683;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 0;
            // 
            // cb_frameTag
            // 
            this.cb_frameTag.AutoSize = true;
            this.cb_frameTag.Location = new System.Drawing.Point(16, 131);
            this.cb_frameTag.Margin = new System.Windows.Forms.Padding(2);
            this.cb_frameTag.Name = "cb_frameTag";
            this.cb_frameTag.Size = new System.Drawing.Size(72, 16);
            this.cb_frameTag.TabIndex = 30;
            this.cb_frameTag.Text = "帧头帧尾";
            this.cb_frameTag.UseVisualStyleBackColor = true;
            this.cb_frameTag.CheckedChanged += new System.EventHandler(this.ipmb_TextChanged);
            // 
            // cb_cmd
            // 
            this.cb_cmd.FormattingEnabled = true;
            this.cb_cmd.ItemHeight = 12;
            this.cb_cmd.Location = new System.Drawing.Point(127, 282);
            this.cb_cmd.Name = "cb_cmd";
            this.cb_cmd.Size = new System.Drawing.Size(151, 20);
            this.cb_cmd.TabIndex = 26;
            this.cb_cmd.SelectedIndexChanged += new System.EventHandler(this.cb_cmd_SelectedIndexChanged);
            // 
            // cb_netFun
            // 
            this.cb_netFun.FormattingEnabled = true;
            this.cb_netFun.ItemHeight = 12;
            this.cb_netFun.Location = new System.Drawing.Point(127, 187);
            this.cb_netFun.Name = "cb_netFun";
            this.cb_netFun.Size = new System.Drawing.Size(151, 20);
            this.cb_netFun.TabIndex = 22;
            this.cb_netFun.SelectedIndexChanged += new System.EventHandler(this.cb_netFun_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Chartreuse;
            this.label2.Location = new System.Drawing.Point(222, 34);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "代码";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(377, 189);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 14);
            this.label11.TabIndex = 5;
            this.label11.Text = "LUN";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(13, 187);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 14);
            this.label5.TabIndex = 5;
            this.label5.Text = "netFun";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(11, 359);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(99, 14);
            this.label12.TabIndex = 5;
            this.label12.Text = "IPMB完整命令";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Maroon;
            this.label4.Location = new System.Drawing.Point(13, 98);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 14);
            this.label4.TabIndex = 5;
            this.label4.Text = "校验和结果";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(13, 321);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 14);
            this.label10.TabIndex = 5;
            this.label10.Text = "data";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(13, 288);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 14);
            this.label9.TabIndex = 5;
            this.label9.Text = "cmd";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(13, 257);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 14);
            this.label8.TabIndex = 5;
            this.label8.Text = "serialNumber";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(13, 220);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 14);
            this.label7.TabIndex = 5;
            this.label7.Text = "selfAddr(req)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(13, 163);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 14);
            this.label6.TabIndex = 5;
            this.label6.Text = "tarAddr(res)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Maroon;
            this.label3.Location = new System.Drawing.Point(13, 68);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "校验和数组";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Chartreuse;
            this.label1.Location = new System.Drawing.Point(13, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = "原理图标识";
            // 
            // tb_chkSumOutput
            // 
            this.tb_chkSumOutput.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.tb_chkSumOutput.Location = new System.Drawing.Point(127, 98);
            this.tb_chkSumOutput.Margin = new System.Windows.Forms.Padding(2);
            this.tb_chkSumOutput.Name = "tb_chkSumOutput";
            this.tb_chkSumOutput.Size = new System.Drawing.Size(166, 23);
            this.tb_chkSumOutput.TabIndex = 12;
            // 
            // softPortPin
            // 
            this.softPortPin.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.softPortPin.Location = new System.Drawing.Point(273, 25);
            this.softPortPin.Margin = new System.Windows.Forms.Padding(2);
            this.softPortPin.Name = "softPortPin";
            this.softPortPin.Size = new System.Drawing.Size(76, 23);
            this.softPortPin.TabIndex = 2;
            this.toolTip1.SetToolTip(this.softPortPin, "软件中的标号方式\r\n从0开始，一直顺序往后排\r\nA0对应0\r\nA7对应7\r\nB0对应8");
            this.softPortPin.TextChanged += new System.EventHandler(this.softPortPin_TextChanged);
            // 
            // tb_IPMB
            // 
            this.tb_IPMB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_IPMB.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.tb_IPMB.Location = new System.Drawing.Point(127, 356);
            this.tb_IPMB.Margin = new System.Windows.Forms.Padding(2);
            this.tb_IPMB.Name = "tb_IPMB";
            this.tb_IPMB.Size = new System.Drawing.Size(530, 23);
            this.tb_IPMB.TabIndex = 29;
            // 
            // tb_chkSumInput
            // 
            this.tb_chkSumInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_chkSumInput.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.tb_chkSumInput.Location = new System.Drawing.Point(127, 58);
            this.tb_chkSumInput.Margin = new System.Windows.Forms.Padding(2);
            this.tb_chkSumInput.Name = "tb_chkSumInput";
            this.tb_chkSumInput.Size = new System.Drawing.Size(530, 23);
            this.tb_chkSumInput.TabIndex = 11;
            this.tb_chkSumInput.TextChanged += new System.EventHandler(this.tb_chkSumInput_TextChanged);
            // 
            // tb_data
            // 
            this.tb_data.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.tb_data.Location = new System.Drawing.Point(127, 312);
            this.tb_data.Margin = new System.Windows.Forms.Padding(2);
            this.tb_data.Name = "tb_data";
            this.tb_data.Size = new System.Drawing.Size(76, 23);
            this.tb_data.TabIndex = 27;
            this.tb_data.TextChanged += new System.EventHandler(this.ipmb_TextChanged);
            // 
            // tb_serialNumber
            // 
            this.tb_serialNumber.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.tb_serialNumber.Location = new System.Drawing.Point(127, 248);
            this.tb_serialNumber.Margin = new System.Windows.Forms.Padding(2);
            this.tb_serialNumber.Name = "tb_serialNumber";
            this.tb_serialNumber.Size = new System.Drawing.Size(76, 23);
            this.tb_serialNumber.TabIndex = 25;
            this.tb_serialNumber.TextChanged += new System.EventHandler(this.ipmb_TextChanged);
            // 
            // tb_selfAddr
            // 
            this.tb_selfAddr.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.tb_selfAddr.Location = new System.Drawing.Point(127, 220);
            this.tb_selfAddr.Margin = new System.Windows.Forms.Padding(2);
            this.tb_selfAddr.Name = "tb_selfAddr";
            this.tb_selfAddr.Size = new System.Drawing.Size(76, 23);
            this.tb_selfAddr.TabIndex = 24;
            this.tb_selfAddr.TextChanged += new System.EventHandler(this.ipmb_TextChanged);
            // 
            // tb_LUN
            // 
            this.tb_LUN.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.tb_LUN.Location = new System.Drawing.Point(459, 184);
            this.tb_LUN.Margin = new System.Windows.Forms.Padding(2);
            this.tb_LUN.Name = "tb_LUN";
            this.tb_LUN.Size = new System.Drawing.Size(76, 23);
            this.tb_LUN.TabIndex = 23;
            this.tb_LUN.TextChanged += new System.EventHandler(this.ipmb_TextChanged);
            // 
            // tb_target
            // 
            this.tb_target.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.tb_target.Location = new System.Drawing.Point(127, 154);
            this.tb_target.Margin = new System.Windows.Forms.Padding(2);
            this.tb_target.Name = "tb_target";
            this.tb_target.Size = new System.Drawing.Size(76, 23);
            this.tb_target.TabIndex = 21;
            this.tb_target.TextChanged += new System.EventHandler(this.ipmb_TextChanged);
            // 
            // hwPortPin
            // 
            this.hwPortPin.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.hwPortPin.Location = new System.Drawing.Point(127, 25);
            this.hwPortPin.Margin = new System.Windows.Forms.Padding(2);
            this.hwPortPin.Name = "hwPortPin";
            this.hwPortPin.Size = new System.Drawing.Size(76, 23);
            this.hwPortPin.TabIndex = 1;
            this.toolTip1.SetToolTip(this.hwPortPin, "硬件原理图中的标号方式\r\n比如要查询A7，就输入A7\r\n大小写不敏感");
            this.hwPortPin.TextChanged += new System.EventHandler(this.hwPortPin_TextChanged);
            // 
            // PortPinCovert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1431, 398);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PortPinCovert";
            this.Text = "PortPinCovert";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PortPinCovert_FormClosing);
            this.Load += new System.EventHandler(this.PortPinCovert_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox softPortPin;
        private System.Windows.Forms.TextBox hwPortPin;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_chkSumInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_chkSumOutput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_netFun;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_target;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_selfAddr;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_serialNumber;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cb_cmd;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb_data;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tb_LUN;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tb_IPMB;
        private System.Windows.Forms.CheckBox cb_frameTag;
    }
}