//#define OPEN_ROW_M  
//#define OPEN_ROW_R      
#define OPEN_ROW_M_HEX  
#define OPEN_ROW_R_HEX

using Debug.MR;
using Debug.tools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Debug {
    public partial class MRForm : Form, InterfaceINI {


        private class GearsDesc {
            public string humanName { get; set; }// eg: nr cr
            public int scale { get; set; } // eg: -10, 20
            public string columnName { get; set; }
            public GearsDesc(string humanName, int scale, string columnName) {
                this.humanName = humanName;
                this.scale = scale;
                this.columnName = columnName;
            }
            public GearsDesc(string humanName, int scale) {
                this.humanName = humanName;
                this.scale = scale;
                //this.columnName = "档位" + scale;
                this.columnName = scale.ToString();
            }
        }
        GearsDesc[] g_colGearsDesc = new GearsDesc[] {
            new GearsDesc("-5", -5),
            new GearsDesc("lower non critical", -10),
            new GearsDesc("-15", -15),
            new GearsDesc("lower critical", -20),
            new GearsDesc("-25", -25),
            new GearsDesc("lower non Recoverable", -30),

            new GearsDesc("5", 5),
            new GearsDesc("upper non critical", 10),
            new GearsDesc("15", 15),
            new GearsDesc("upper critical", 20),
            new GearsDesc("25", 25),
            new GearsDesc("upper non Recoverable", 30),
            new GearsDesc("ref", 0),
        };

        string[] g_rowDesc =  {
            "threshold",
            //"raw adc",
            "raw step", //原始ADC采样值1个单位，所对应的值。eg:255对应1.8.那么raw step = 1.8/255
#if OPEN_ROW_M
            "M", // 把raw step转换成科学计数法
#endif
#if OPEN_ROW_M_HEX
            "M(Hex)", // 把raw step转换成科学计数法
#endif
            
#if OPEN_ROW_R
            "R", 
#endif
            
#if OPEN_ROW_R_HEX
            "R(Hex)", // 
#endif
            "dec",
            "hex",
        };
        private void ColGetHeader(DataGridViewColumnCollection gvColCollection) {
            // add column
            for(int i = 0; i < g_colGearsDesc.Length; i++) {
                gvColCollection.Add(g_colGearsDesc[i].humanName, g_colGearsDesc[i].columnName);
                //gvColCollection[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // add row
            for(int i = 0; i < g_rowDesc.Length; i++) {
                dataGridView1.Rows.Add(1);
                dataGridView1.Rows[i].HeaderCell.Value = g_rowDesc[i];
            }
        }
        public MRForm() {
            InitializeComponent();
        }
        private void DataGridViewInit() {
            //设置数据表格上显示的列标题
            Font a = new Font("UTF-8", 12);//GB2312为字体名称，1为字体大小
            dataGridView1.Font = a;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dataGridView1.Columns[0].DataGridView.Font = new System.Drawing.Font("宋体", 15.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //dataGridView1.Columns[0].HeaderText = " xx ";
            //设置dataGridView的属性
            //设置数据表格为只读
            //dataGridView1.ReadOnly = true;
            //不允许添加行
            dataGridView1.AllowUserToAddRows = false;
            //背景为白色
            dataGridView1.BackgroundColor = Color.White;
            //只允许选中单行
            dataGridView1.MultiSelect = false;
            //整行选中
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            //列宽
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // 禁止换行
            dataGridView1.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.NotSet;
            //Header Rows 自动拉伸宽度
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

            dataGridView1.RowsDefaultCellStyle.BackColor = Color.Bisque;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;

            ColGetHeader(dataGridView1.Columns);
            GearsDispModeTbInit();
        }

        private void PortPinCovert_Load(object sender, EventArgs e) {
            loadINI();
            DataGridViewInit();

            //radioButton1.Checked = true;
            foreach(var item in groupBox1.Controls) {
                if(!(item is RadioButton)) {
                    continue;
                }
                if(((RadioButton)item).Name == ("radioButton" + ((int)gearsMode + 1))) {
                    ((RadioButton)item).Checked = true;
                } else {
                    Console.WriteLine(((RadioButton)item).Name);
                }
            }
            ref0.Checked = g_greasDisplayref0;
        }
        private bool IsANonHeaderLinkCell(DataGridView dg, DataGridViewCellEventArgs cellEvent) {
            if (cellEvent.ColumnIndex < 0 || cellEvent.RowIndex < 0) {
                return false;
            } else { 
                return true; 
            }
        }
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) {
            if(IsANonHeaderLinkCell(dataGridView1, e)) {
                object val = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if(val == null) return;
                string valStr = val.ToString();
                Console.WriteLine("cell val : "+ valStr);
                if(string.IsNullOrEmpty(valStr)) return;
                Clipboard.SetText(valStr);
            }
        }
        string g_greasDisplayModeKey = "gears_display_mode";
        string g_greasDisplayref0Key = "gears_display_ref0";
        private ThresholdGearsMode gearsMode = ThresholdGearsMode.gears_step_5;
        private bool g_greasDisplayref0 = false;

        public void loadINI() {
            IniHelper iniHelper = new IniHelper();
            iniHelper.IniLoader2Form(this);

            string dispMode = iniHelper.getString(this.Text, g_greasDisplayModeKey, string.Empty);
            try {
                int radioButtonIdx = int.Parse(dispMode);
                gearsMode = (ThresholdGearsMode)radioButtonIdx;
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }

            string isDispRef0 = iniHelper.getString(this.Text, g_greasDisplayref0Key, string.Empty);
            try {
                g_greasDisplayref0 = bool.Parse(isDispRef0);
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        public void updateINI() {
            IniHelper iniHelper = new IniHelper();
            iniHelper.IniUpdate2File(this);
            iniHelper.writeString(this.Text, g_greasDisplayModeKey, ((int)gearsMode).ToString());
            iniHelper.writeString(this.Text, g_greasDisplayref0Key, (g_greasDisplayref0).ToString());
        }
        private void MR_FormClosing(object sender, FormClosingEventArgs e) {
            updateINI();
        }

        private void ref0_CheckedChanged(object sender, EventArgs e) {
            g_greasDisplayref0 = ref0.Checked;
            updateGridView(gearsMode, g_greasDisplayref0);
        }
        private void ColFillThreshold(int index, string voltSysStr) {
            if(index >= dataGridView1.Rows.Count) return;
            float voltSys;
            if(float.TryParse(voltSysStr, out voltSys) == false) return;

            for(int i = 0; i < g_colGearsDesc.Length; i++) {
                //if(!g_colGearsDesc[i].isEnable) continue;
                int scale = g_colGearsDesc[i].scale;
                float voltThreshold = voltSys * (100 + scale) / 100;
                // add row
                dataGridView1.Rows[index].Cells[i].Value = voltThreshold;
            }
        }

        private void ColFillMisc(int index, string val) {
            if(index >= dataGridView1.Rows.Count) return;

            for(int i = 0; i < g_colGearsDesc.Length; i++) {
                // add row
                dataGridView1.Rows[index].Cells[i].Value = val;
            }
        }

        private float ColGetV1Max() {
            float R1, R2, Vref;
            if(string.IsNullOrEmpty(tbR1.Text)) tbR1.Text = "0";
            if(string.IsNullOrEmpty(tbR2.Text)) tbR2.Text = "0";
            if(float.TryParse(tbR1.Text, out R1) == false) return 0;
            if(float.TryParse(tbR2.Text, out R2) == false) return 0;

            if(float.TryParse(tbVoltRef.Text, out Vref) == false) {
                MessageBox.Show("缺少参数");
                return 0;
            }
            float V1Max;
            if(R2 == 0) { //没有电阻分压，直接接到pin的情况
                V1Max = Vref;
            } else {
                V1Max = ((R1 + R2) / R2) * Vref;
            }
            return V1Max;
        }
        private void ColGetMAndR(float rawStep, out int M, out int R) {
            float stepMultip = rawStep;
            float stepVid = rawStep;
            M = R = 0;
            for(int i = 0; i < 8; i++) {
                if((stepMultip >= (255 / 10)) && stepMultip < 255) {
                    M = (int)Math.Round(stepMultip);
                    R = -i;
                    return;
                } else {
                    stepMultip *= 10;
                }

                if((stepVid > (255 / 10)) && stepVid < 255) {
                    M = (int)Math.Round(stepVid);
                    R = i;
                    return;
                } else {
                    stepVid /= 10;
                }
            }
        }

        private void ColFillDecHex(int index, int thresholdIndex, int M, int R) {
            if(index >= dataGridView1.Rows.Count) return;
            if(thresholdIndex >= dataGridView1.Rows.Count) return;

            for(int i = 0; i < g_colGearsDesc.Length; i++) {
                // add row
                string thresholdStr = dataGridView1.Rows[thresholdIndex].Cells[i].Value.ToString();
                float voltSys;
                if(float.TryParse(thresholdStr, out voltSys) == false) return;

                float rawStep = (float)(M * Math.Pow(10, R));
                //int dec = (int) Math.Round((rawStep * 255 * voltSys));
                int dec = (int)Math.Round(voltSys / rawStep);
                dataGridView1.Rows[index].Cells[i].Value = dec;
                string hexStr = dec.ToString("x3");
                if(hexStr.StartsWith("0")) {
                    hexStr = hexStr.Substring(1);
                }
                dataGridView1.Rows[index + 1].Cells[i].Value = "0x" + hexStr;
            }
        }
        private void btCalc_Click(object sender, EventArgs e) {
            //calc 上下限
            int idx = 0;
            ColFillThreshold(idx++, tbVoltSys.Text);
            // 当ADC input=255时，V1的值设为 V1Max
            float rawStep = ColGetV1Max() / 255;
            ColFillMisc(idx++, rawStep.ToString());

            // 计算MR
            int M, R;
            ColGetMAndR(rawStep, out M, out R);
#if OPEN_ROW_M
            // M
            ColFillMisc(idx++, M.ToString());
#endif

#if OPEN_ROW_M_HEX
            // M(hex)
            string MHexStr = M.ToString("x3");
            if(MHexStr.StartsWith("0")) {
                MHexStr = MHexStr.Substring(1);
            }
            ColFillMisc(idx++, "0x" + MHexStr);
#endif

#if OPEN_ROW_R
            // R
            //ColFillMisc(idx++, R.ToString());
#endif
#if OPEN_ROW_R_HEX
            // R hex 
            byte Rchar = ((byte)('G' + R));
            string Rhex = ((char)Rchar).ToString();
            ColFillMisc(idx++, "0x" + Rhex);
#endif

            // 计算dec hex
            ColFillDecHex(idx++, 0, M, R); // idx+=2
        }
        //
        // 摘要:
        //     定义用于指定如何调整行标题宽度的值。
        public enum ThresholdGearsMode {
            //
            // 摘要:
            //     用户可以调整列宽标头使用鼠标。
            gears_step_5 = 0,
            gears_step_10,
            gears_disp_all,
            gears_disp_modify,
        }
        private class GearsDispMode {
            public ThresholdGearsMode mode;
            public List<int> list;
            public GearsDispMode(ThresholdGearsMode mode, List<int> list) {
                this.mode = mode;
                this.list = list;
            }
        }
        private GearsDispMode[] GearsDispModeTb = new GearsDispMode[4];
        private void GearsDispModeTbInit() {
            int indx = 0;
            List<int> arrayList1 = new List<int>();
            arrayList1.Add(5);
            arrayList1.Add(10);
            arrayList1.Add(15);
            GearsDispModeTb[indx++] = new GearsDispMode(ThresholdGearsMode.gears_step_5, arrayList1);

            List<int> arrayList2 = new List<int>();
            arrayList2.Add(10);
            arrayList2.Add(20);
            arrayList2.Add(30);
            GearsDispModeTb[indx++] = new GearsDispMode(ThresholdGearsMode.gears_step_10, arrayList2);

            //display all
            List<int> arrayList3 = new List<int>();
            arrayList3.AddRange(arrayList1);
            arrayList3.Add(20);
            arrayList3.Add(30);
            GearsDispModeTb[indx++] = new GearsDispMode(ThresholdGearsMode.gears_disp_all, arrayList3);

            //display user modify
            List<int> arrayList4 = new List<int>();
            GearsDispModeTb[indx++] = new GearsDispMode(ThresholdGearsMode.gears_disp_modify, arrayList4);
        }
        /// <summary>
        /// 如果在表GearsDispModeTb中，存在一样的key，说明需要显示，
        /// 不存在，则隐藏。类似于白名单
        /// </summary>
        /// <param name="scale"></param>
        /// <returns>true 显示</returns>
        public bool isGearsDisp(int dispMode, int scale) {
            try {
                if(dispMode >= GearsDispModeTb.Length) return false;
                if (GearsDispModeTb[dispMode] == null) return false;
                List<int> list = GearsDispModeTb[dispMode].list;
                foreach(var item in list) {
                    if(Math.Abs(scale) == item) {
                        return true;
                    }
                }
            } catch(Exception) {

            }
            return false;
        }
        private void updateGridView(ThresholdGearsMode dispMode, bool isRef0) {
            for(int i = 0; i < g_colGearsDesc.Length; i++) {
                int key = g_colGearsDesc[i].scale;
                if(isGearsDisp((int)dispMode, key)) {
                    dataGridView1.Columns[i].Visible = true;//显示列
                } else if((isRef0) & (key == 0)) {
                    dataGridView1.Columns[i].Visible = true;//显示列
                } else {
                    dataGridView1.Columns[i].Visible = false;//隐藏列
                }
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e) {
            if(radioButton1.Checked) {
                gearsMode = ThresholdGearsMode.gears_step_5;
            } else if(radioButton2.Checked) {
                gearsMode = ThresholdGearsMode.gears_step_10;
            } else if(radioButton3.Checked) {
                gearsMode = ThresholdGearsMode.gears_disp_all;
            } else if(radioButton4.Checked) {
                gearsMode = ThresholdGearsMode.gears_disp_modify;
            }
            updateGridView(gearsMode, g_greasDisplayref0);
        }


        private void bt_CreateSDR_Click(object sender, EventArgs e) {
            CreateSDR createSDR = new CreateSDR();
            string sdr = createSDR.SDRDescGetAllDesc(this);
            tb_SDR.Text = sdr;
        }

        private void tb_textBoxEnter(object sender, EventArgs e) {
            TextBox tb = (TextBox)sender;
            tb.SelectAll();

        }
    }
}