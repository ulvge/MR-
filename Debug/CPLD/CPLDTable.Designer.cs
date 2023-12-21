
namespace Debug {
    partial class CPLDTable {
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_input = new System.Windows.Forms.TextBox();
            this.bt_createAgain = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_devs = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tb_res = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.bt_createAgain);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.cb_devs);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(1121, 498);
            this.splitContainer1.SplitterDistance = 485;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tb_input);
            this.groupBox1.Location = new System.Drawing.Point(13, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(456, 421);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "简述";
            // 
            // tb_input
            // 
            this.tb_input.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_input.Location = new System.Drawing.Point(7, 25);
            this.tb_input.Margin = new System.Windows.Forms.Padding(4);
            this.tb_input.Multiline = true;
            this.tb_input.Name = "tb_input";
            this.tb_input.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_input.Size = new System.Drawing.Size(429, 389);
            this.tb_input.TabIndex = 31;
            // 
            // bt_createAgain
            // 
            this.bt_createAgain.Location = new System.Drawing.Point(381, 28);
            this.bt_createAgain.Margin = new System.Windows.Forms.Padding(4);
            this.bt_createAgain.Name = "bt_createAgain";
            this.bt_createAgain.Size = new System.Drawing.Size(100, 29);
            this.bt_createAgain.TabIndex = 29;
            this.bt_createAgain.Text = "重新生成";
            this.bt_createAgain.UseVisualStyleBackColor = true;
            this.bt_createAgain.Click += new System.EventHandler(this.bt_createAgain_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 15);
            this.label1.TabIndex = 28;
            this.label1.Text = "CPLD device";
            this.toolTip1.SetToolTip(this.label1, "仿照 ori.txt里面的格式，填写原始信息");
            // 
            // cb_devs
            // 
            this.cb_devs.FormattingEnabled = true;
            this.cb_devs.ItemHeight = 15;
            this.cb_devs.Items.AddRange(new object[] {
            "EF2L45UG132B",
            "EF2L45BG256B",
            "EF3"});
            this.cb_devs.Location = new System.Drawing.Point(183, 28);
            this.cb_devs.Margin = new System.Windows.Forms.Padding(4);
            this.cb_devs.Name = "cb_devs";
            this.cb_devs.Size = new System.Drawing.Size(181, 23);
            this.cb_devs.TabIndex = 27;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.tb_res);
            this.groupBox2.Location = new System.Drawing.Point(23, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(598, 474);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "结果";
            // 
            // tb_res
            // 
            this.tb_res.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_res.Location = new System.Drawing.Point(7, 26);
            this.tb_res.Margin = new System.Windows.Forms.Padding(4);
            this.tb_res.Multiline = true;
            this.tb_res.Name = "tb_res";
            this.tb_res.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_res.Size = new System.Drawing.Size(584, 441);
            this.tb_res.TabIndex = 31;
            // 
            // CPLDTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 498);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CPLDTable";
            this.Text = "CPLDTable";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PortPinCovert_FormClosing);
            this.Load += new System.EventHandler(this.PortPinCovert_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cb_devs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt_createAgain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_input;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tb_res;
    }
}