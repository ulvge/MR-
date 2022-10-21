using Debug.tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Debug
{
    public partial class form : Form
    {
        List<Form> formList = new List<Form>();

        PortPinCovert portPinCovert = new PortPinCovert();
        MR mr = new MR();
        public form()
        {
            InitializeComponent();
            formList.Add(mr);
            formList.Add(portPinCovert);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for(int i = 0;i < formList.Count;i++) {
                tabAddForm(formList[i] , i);
            }
            IniHelper iniHelper = new IniHelper();
            iniHelper.IniLoader2Form(this);
            try {
                string readVal = iniHelper.getString(this.Text, "formIdex", "0");
                tabControl1.SelectedIndex = int.Parse(readVal);
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }

            //formList[tabControl1.SelectedIndex].Show();
            tabControl1_SelectedIndexChanged(null, null);
        }
        /// <summary>
        /// 向tabControl中添加一个Form
        /// </summary>
        private void tabAddForm(Form form, Int32 idx) {
            form.TopLevel = false;   //这个必须有不然会提示:"不能向tabControl中添加顶级控件"      
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;

            tabControl1.Dock = DockStyle.Fill;
            tabControl1.TabPages[idx].Controls.Add(form);
            tabControl1.TabPages[idx].Text = form.Name;
        }
        private static int g_formIndex=0;
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(var item in formList) {
                item.Hide();
            }
            try {
                formList[tabControl1.SelectedIndex].Show();
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void form_FormClosing(object sender,FormClosingEventArgs e) {
            IniHelper iniHelper = new IniHelper();
            iniHelper.writeString(this.Text, "formIdex", tabControl1.SelectedIndex.ToString());
            foreach(var item in formList) {
                item.Close();
            }
            try {
                Thread.Sleep(200);
            } catch(Exception) {

            }
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e) {
            //https://www.freesion.com/article/6948627142/
            //https://blog.csdn.net/qq_44365878/article/details/104987247
            //Rectangle tabArea = tabControl1.GetTabRect(e.Index);//主要是做个转换来获得TAB项的RECTANGELF
            //RectangleF tabTextArea = (RectangleF)(tabControl1.GetTabRect(e.Index));
            //Graphics g = e.Graphics;
            //StringFormat sf = new StringFormat();//封装文本布局信息
            //sf.LineAlignment = StringAlignment.Center;
            //sf.Alignment = StringAlignment.Center;
            //Font font = this.tabControl1.Font;
            //SolidBrush brush = new SolidBrush(Color.Black);//绘制边框的画笔
            //g.DrawString(((TabControl)(sender)).TabPages[e.Index].Text, font, brush, tabTextArea, sf);
        }
    }
}
