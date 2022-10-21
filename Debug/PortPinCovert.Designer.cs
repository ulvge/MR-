
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
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_chkSumOutput = new System.Windows.Forms.TextBox();
            this.softPortPin = new System.Windows.Forms.TextBox();
            this.tb_chkSumInput = new System.Windows.Forms.TextBox();
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
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.tb_chkSumOutput);
            this.splitContainer1.Panel1.Controls.Add(this.softPortPin);
            this.splitContainer1.Panel1.Controls.Add(this.tb_chkSumInput);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(222, 34);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "代码";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(13, 98);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 14);
            this.label4.TabIndex = 5;
            this.label4.Text = "校验和结果";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(13, 68);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "校验和";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
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
            this.tb_chkSumOutput.Location = new System.Drawing.Point(116, 98);
            this.tb_chkSumOutput.Margin = new System.Windows.Forms.Padding(2);
            this.tb_chkSumOutput.Name = "tb_chkSumOutput";
            this.tb_chkSumOutput.Size = new System.Drawing.Size(166, 23);
            this.tb_chkSumOutput.TabIndex = 3;
            // 
            // softPortPin
            // 
            this.softPortPin.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.softPortPin.Location = new System.Drawing.Point(273, 25);
            this.softPortPin.Margin = new System.Windows.Forms.Padding(2);
            this.softPortPin.Name = "softPortPin";
            this.softPortPin.Size = new System.Drawing.Size(76, 23);
            this.softPortPin.TabIndex = 3;
            this.toolTip1.SetToolTip(this.softPortPin, "软件中的标号方式\r\n从0开始，一直顺序往后排\r\nA0对应0\r\nA7对应7\r\nB0对应8");
            this.softPortPin.TextChanged += new System.EventHandler(this.softPortPin_TextChanged);
            // 
            // tb_chkSumInput
            // 
            this.tb_chkSumInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_chkSumInput.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.tb_chkSumInput.Location = new System.Drawing.Point(116, 58);
            this.tb_chkSumInput.Margin = new System.Windows.Forms.Padding(2);
            this.tb_chkSumInput.Name = "tb_chkSumInput";
            this.tb_chkSumInput.Size = new System.Drawing.Size(530, 23);
            this.tb_chkSumInput.TabIndex = 4;
            this.tb_chkSumInput.TextChanged += new System.EventHandler(this.tb_chkSumInput_TextChanged);
            // 
            // hwPortPin
            // 
            this.hwPortPin.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.hwPortPin.Location = new System.Drawing.Point(116, 25);
            this.hwPortPin.Margin = new System.Windows.Forms.Padding(2);
            this.hwPortPin.Name = "hwPortPin";
            this.hwPortPin.Size = new System.Drawing.Size(76, 23);
            this.hwPortPin.TabIndex = 4;
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
    }
}