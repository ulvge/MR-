
namespace Debug {
    partial class MRForm {
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ref0 = new System.Windows.Forms.CheckBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btCalc = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbR2 = new System.Windows.Forms.TextBox();
            this.tbR1 = new System.Windows.Forms.TextBox();
            this.tbVoltRef = new System.Windows.Forms.TextBox();
            this.tbVoltSys = new System.Windows.Forms.TextBox();
            this.tb_SDR = new System.Windows.Forms.TextBox();
            this.cb_sensorNum = new System.Windows.Forms.ComboBox();
            this.cb_Units2 = new System.Windows.Forms.ComboBox();
            this.bt_CreateSDR = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lb_Units2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_recoderID = new System.Windows.Forms.TextBox();
            this.tb_sensorName = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.Gray;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel1.Controls.Add(this.btCalc);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.tbR2);
            this.splitContainer1.Panel1.Controls.Add(this.tbR1);
            this.splitContainer1.Panel1.Controls.Add(this.tbVoltRef);
            this.splitContainer1.Panel1.Controls.Add(this.tbVoltSys);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitContainer1.Panel2.Controls.Add(this.tb_SDR);
            this.splitContainer1.Panel2.Controls.Add(this.cb_sensorNum);
            this.splitContainer1.Panel2.Controls.Add(this.cb_Units2);
            this.splitContainer1.Panel2.Controls.Add(this.bt_CreateSDR);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.lb_Units2);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.tb_recoderID);
            this.splitContainer1.Panel2.Controls.Add(this.tb_sensorName);
            this.splitContainer1.Size = new System.Drawing.Size(1105, 567);
            this.splitContainer1.SplitterDistance = 418;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.ref0);
            this.groupBox1.Controls.Add(this.radioButton4);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Location = new System.Drawing.Point(31, 328);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(186, 122);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "阈值等级+/-";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(94, 78);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(76, 21);
            this.textBox1.TabIndex = 2;
            // 
            // ref0
            // 
            this.ref0.AutoSize = true;
            this.ref0.Location = new System.Drawing.Point(94, 19);
            this.ref0.Margin = new System.Windows.Forms.Padding(2);
            this.ref0.Name = "ref0";
            this.ref0.Size = new System.Drawing.Size(54, 16);
            this.ref0.TabIndex = 1;
            this.ref0.Text = "ref列";
            this.ref0.UseVisualStyleBackColor = true;
            this.ref0.CheckedChanged += new System.EventHandler(this.ref0_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(4, 79);
            this.radioButton4.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(59, 16);
            this.radioButton4.TabIndex = 0;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "自定义";
            this.toolTip1.SetToolTip(this.radioButton4, "未实现");
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(4, 59);
            this.radioButton3.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(47, 16);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "全部";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(4, 19);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(65, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "5 10 15";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(4, 39);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(71, 16);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "10 20 30";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Debug.Properties.Resources.分压采样;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(31, 20);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 127);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // btCalc
            // 
            this.btCalc.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.btCalc.Location = new System.Drawing.Point(236, 379);
            this.btCalc.Margin = new System.Windows.Forms.Padding(2);
            this.btCalc.Name = "btCalc";
            this.btCalc.Size = new System.Drawing.Size(62, 36);
            this.btCalc.TabIndex = 5;
            this.btCalc.Text = "计 算";
            this.btCalc.UseVisualStyleBackColor = true;
            this.btCalc.Click += new System.EventHandler(this.btCalc_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(28, 292);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "R2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(28, 251);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "R1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(28, 178);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(198, 14);
            this.label4.TabIndex = 5;
            this.label4.Text = "Vref(采样芯片的参考电压)V";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(28, 210);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = "V1(未分压前的电压)V";
            // 
            // tbR2
            // 
            this.tbR2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbR2.Location = new System.Drawing.Point(236, 282);
            this.tbR2.Margin = new System.Windows.Forms.Padding(2);
            this.tbR2.Name = "tbR2";
            this.tbR2.Size = new System.Drawing.Size(76, 26);
            this.tbR2.TabIndex = 4;
            this.toolTip1.SetToolTip(this.tbR2, "如果没有分压，此处可随便写");
            // 
            // tbR1
            // 
            this.tbR1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbR1.Location = new System.Drawing.Point(236, 241);
            this.tbR1.Margin = new System.Windows.Forms.Padding(2);
            this.tbR1.Name = "tbR1";
            this.tbR1.Size = new System.Drawing.Size(76, 26);
            this.tbR1.TabIndex = 3;
            this.toolTip1.SetToolTip(this.tbR1, "如果没有分压，则写0");
            // 
            // tbVoltRef
            // 
            this.tbVoltRef.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbVoltRef.Location = new System.Drawing.Point(236, 167);
            this.tbVoltRef.Margin = new System.Windows.Forms.Padding(2);
            this.tbVoltRef.Name = "tbVoltRef";
            this.tbVoltRef.Size = new System.Drawing.Size(76, 26);
            this.tbVoltRef.TabIndex = 1;
            this.toolTip1.SetToolTip(this.tbVoltRef, "一般是1.8");
            // 
            // tbVoltSys
            // 
            this.tbVoltSys.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbVoltSys.Location = new System.Drawing.Point(236, 200);
            this.tbVoltSys.Margin = new System.Windows.Forms.Padding(2);
            this.tbVoltSys.Name = "tbVoltSys";
            this.tbVoltSys.Size = new System.Drawing.Size(76, 26);
            this.tbVoltSys.TabIndex = 2;
            this.toolTip1.SetToolTip(this.tbVoltSys, "比如P12，即12");
            // 
            // tb_SDR
            // 
            this.tb_SDR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_SDR.Location = new System.Drawing.Point(3, 249);
            this.tb_SDR.Multiline = true;
            this.tb_SDR.Name = "tb_SDR";
            this.tb_SDR.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_SDR.Size = new System.Drawing.Size(406, 315);
            this.tb_SDR.TabIndex = 29;
            // 
            // cb_sensorNum
            // 
            this.cb_sensorNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_sensorNum.FormattingEnabled = true;
            this.cb_sensorNum.ItemHeight = 12;
            this.cb_sensorNum.Location = new System.Drawing.Point(515, 303);
            this.cb_sensorNum.Name = "cb_sensorNum";
            this.cb_sensorNum.Size = new System.Drawing.Size(151, 20);
            this.cb_sensorNum.TabIndex = 27;
            // 
            // cb_Units2
            // 
            this.cb_Units2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_Units2.FormattingEnabled = true;
            this.cb_Units2.ItemHeight = 12;
            this.cb_Units2.Location = new System.Drawing.Point(515, 331);
            this.cb_Units2.Name = "cb_Units2";
            this.cb_Units2.Size = new System.Drawing.Size(151, 20);
            this.cb_Units2.TabIndex = 27;
            // 
            // bt_CreateSDR
            // 
            this.bt_CreateSDR.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.bt_CreateSDR.Location = new System.Drawing.Point(573, 396);
            this.bt_CreateSDR.Margin = new System.Windows.Forms.Padding(2);
            this.bt_CreateSDR.Name = "bt_CreateSDR";
            this.bt_CreateSDR.Size = new System.Drawing.Size(62, 36);
            this.bt_CreateSDR.TabIndex = 5;
            this.bt_CreateSDR.Text = "SDR";
            this.bt_CreateSDR.UseVisualStyleBackColor = true;
            this.bt_CreateSDR.Click += new System.EventHandler(this.bt_CreateSDR_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeight = 29;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(685, 236);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(414, 266);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 14);
            this.label6.TabIndex = 6;
            this.label6.Text = "Recoder ID";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(414, 303);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 14);
            this.label7.TabIndex = 6;
            this.label7.Text = "sensorNum";
            // 
            // lb_Units2
            // 
            this.lb_Units2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_Units2.AutoSize = true;
            this.lb_Units2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.lb_Units2.Location = new System.Drawing.Point(414, 333);
            this.lb_Units2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_Units2.Name = "lb_Units2";
            this.lb_Units2.Size = new System.Drawing.Size(79, 14);
            this.lb_Units2.TabIndex = 6;
            this.lb_Units2.Text = "lb_Units2";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(414, 376);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 14);
            this.label5.TabIndex = 5;
            this.label5.Text = "sensorName";
            // 
            // tb_recoderID
            // 
            this.tb_recoderID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_recoderID.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_recoderID.Location = new System.Drawing.Point(515, 260);
            this.tb_recoderID.Margin = new System.Windows.Forms.Padding(2);
            this.tb_recoderID.Name = "tb_recoderID";
            this.tb_recoderID.Size = new System.Drawing.Size(151, 26);
            this.tb_recoderID.TabIndex = 4;
            // 
            // tb_sensorName
            // 
            this.tb_sensorName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_sensorName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_sensorName.Location = new System.Drawing.Point(515, 364);
            this.tb_sensorName.Margin = new System.Windows.Forms.Padding(2);
            this.tb_sensorName.Name = "tb_sensorName";
            this.tb_sensorName.Size = new System.Drawing.Size(151, 26);
            this.tb_sensorName.TabIndex = 4;
            // 
            // MRForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1105, 568);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MRForm";
            this.Text = "MR";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MR_FormClosing);
            this.Load += new System.EventHandler(this.PortPinCovert_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbR1;
        private System.Windows.Forms.TextBox tbVoltSys;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btCalc;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbR2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbVoltRef;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ref0;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lb_Units2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button bt_CreateSDR;
        private System.Windows.Forms.TextBox tb_SDR;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox tb_recoderID;
        public System.Windows.Forms.ComboBox cb_Units2;
        public System.Windows.Forms.TextBox tb_sensorName;
        public System.Windows.Forms.ComboBox cb_sensorNum;
    }
}