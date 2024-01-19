
namespace Debug {
    partial class IconConvert {
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_fileRoot = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_items = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_station = new System.Windows.Forms.TextBox();
            this.tb_python = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(644, 75);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "选择文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "文件目录";
            // 
            // tb_fileRoot
            // 
            this.tb_fileRoot.Location = new System.Drawing.Point(125, 75);
            this.tb_fileRoot.Name = "tb_fileRoot";
            this.tb_fileRoot.Size = new System.Drawing.Size(513, 21);
            this.tb_fileRoot.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "item";
            // 
            // tb_items
            // 
            this.tb_items.Location = new System.Drawing.Point(125, 107);
            this.tb_items.Name = "tb_items";
            this.tb_items.Size = new System.Drawing.Size(513, 21);
            this.tb_items.TabIndex = 2;
            this.tb_items.TextChanged += new System.EventHandler(this.tb_items_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "STATION";
            // 
            // tb_station
            // 
            this.tb_station.Location = new System.Drawing.Point(125, 134);
            this.tb_station.Name = "tb_station";
            this.tb_station.Size = new System.Drawing.Size(513, 21);
            this.tb_station.TabIndex = 2;
            this.tb_station.TextChanged += new System.EventHandler(this.tb_station_TextChanged);
            // 
            // tb_python
            // 
            this.tb_python.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_python.Location = new System.Drawing.Point(51, 206);
            this.tb_python.Multiline = true;
            this.tb_python.Name = "tb_python";
            this.tb_python.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_python.Size = new System.Drawing.Size(587, 168);
            this.tb_python.TabIndex = 30;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(644, 275);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 31;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // IconConvert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tb_python);
            this.Controls.Add(this.tb_station);
            this.Controls.Add(this.tb_items);
            this.Controls.Add(this.tb_fileRoot);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "IconConvert";
            this.Text = "iconConvert";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_fileRoot;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_items;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_station;
        private System.Windows.Forms.TextBox tb_python;
        private System.Windows.Forms.Button button2;
    }
}