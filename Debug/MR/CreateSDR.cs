using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace Debug.MR {
    class CreateSDR {

        SDRDesc sDRDesc = new SDRDesc();
        public CreateSDR() {
        }

        private string SDRGetColVal(DataGridViewCellCollection cellCollection,ref int startIdx) {
            for(int i = startIdx + 1; i < cellCollection.Count; i++) {
                if (cellCollection[i].Displayed) {
                    startIdx = i;
                    return cellCollection[i].Value + string.Empty;
                }
            }
            return string.Empty;
        }
        private bool SDRDescFillFiled(MRForm form) {
            try {
                DataGridView dg = form.dataGridView1;
                if (dg.Rows.Count < 5) {
                    return false;
                }
                sDRDesc.M = (string)dg.Rows[2].Cells[0].Value + string.Empty;
                sDRDesc.R_B_Exp = (string)dg.Rows[3].Cells[0].Value+"0";

                int startIdx = -1;
                sDRDesc.LowerNonCritical = SDRGetColVal(dg.Rows[5].Cells,ref startIdx);
                sDRDesc.LowerCritical = SDRGetColVal(dg.Rows[5].Cells, ref startIdx);
                sDRDesc.LowerNonRecoverable = SDRGetColVal(dg.Rows[5].Cells, ref startIdx);

                sDRDesc.UpperNonCritical = SDRGetColVal(dg.Rows[5].Cells, ref startIdx);
                sDRDesc.UpperCritical = SDRGetColVal(dg.Rows[5].Cells, ref startIdx);
                sDRDesc.UpperNonRecoverable = SDRGetColVal(dg.Rows[5].Cells, ref startIdx);

                if((sDRDesc.UpperNonRecoverable.Length != 4) || (sDRDesc.M.Length != 4)) {
                    return false;
                }

                sDRDesc._hdr_ID = "0x"+form.tb_recoderID.Text;
                sDRDesc.SensorNum = (string)form.cb_sensorNum.SelectedValue;
                sDRDesc.Units2 = (string)form.cb_Units2.SelectedValue;
                sDRDesc.IDStrTypeLen = "0xC0 + sizeof(\"" +form.tb_sensorName.Text+ "\")"; //0xC0 + sizeof "P12V"
                sDRDesc.IDStr = "\"" + form.tb_sensorName.Text+"\"";
                return true;
            } catch(NullReferenceException ) {
                return false;
            }
        }
        public string SDRDescGetAllDesc(MRForm form) {
            if(!SDRDescFillFiled(form)) {
                return "Create SDR error";
            }

            Type testType = typeof(SDRDesc);
            PropertyInfo[] properties = testType.GetProperties();
            StringBuilder sb = new StringBuilder();

            sb.Append("\t{\t /* SDR Record    "+ sDRDesc.SensorNum +"   "+ sDRDesc.IDStr +"    */\r\n");
            
            foreach(PropertyInfo item in properties) {
                string desc = item.GetDescription() + string.Empty;
                string val = (string)item.GetValue(sDRDesc, null) + string.Empty;
                string itemName = item.Name;
                itemName = itemName.Replace("_hdr_", "hdr.");
                if (desc.Contains("Sensor Owner ID") || desc.Contains("Entity ID") || desc.Contains("Upper Non-Recoverable Threshold")) {
                    sb.Append("\r\n");
                }
                //0x81,			//M
                //sb.Append("\t\t" + val + ",\t\t\t//" + desc + "\r\n");    

                //.M = 0x81,			//M
                //sb.Append("\t\t." + item.Name + " = "+val + ",\t\t\t//" + desc + "\r\n");
                //.M = 0x81,
                
                sb.Append(string.Format("\t\t.{0,-20}" + " = {1}"  + ",\r\n", itemName, val));
            }

            sb.Append("\t},\r\n\r\n");
            return sb.ToString();
        }

    }

}
