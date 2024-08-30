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

        private static string SUB_DEVICE_SDR_NO_PARTIAL = "SUB_DEVICE_SDR_NO_PARTIAL";
        private static string SUB_DEVICE_SDR_P0V9 = "SUB_DEVICE_SDR_P0V9";
        private static string SUB_DEVICE_SDR_P1V2 = "SUB_DEVICE_SDR_P1V2";
        private static string SUB_DEVICE_SDR_P1V8 = "SUB_DEVICE_SDR_P1V8";
        private static string SUB_DEVICE_SDR_P2V5 = "SUB_DEVICE_SDR_P2V5";
        private static string SUB_DEVICE_SDR_P3V3 = "SUB_DEVICE_SDR_P3V3";
        private static string SUB_DEVICE_SDR_VBAT = "SUB_DEVICE_SDR_VBAT";
        private static string SUB_DEVICE_SDR_P12V = "SUB_DEVICE_SDR_P12V";
        private static string SUB_DEVICE_SDR_TEMP = "SUB_DEVICE_SDR_TEMP";
        private static string SUB_DEVICE_SDR_FAN = "SUB_DEVICE_SDR_FAN";

        private static string SUB_DEVICE_SDR_MAC5023_P = "SUB_DEVICE_SDR_MAC5023_P";
        private static string SUB_DEVICE_SDR_MAC5023_V = "SUB_DEVICE_SDR_MAC5023_V";
        private static string SUB_DEVICE_SDR_MAC5023_I = "SUB_DEVICE_SDR_MAC5023_I";
        private static string SUB_DEVICE_SDR_ERROR = "SUB_DEVICE_SDR_ERROR";


        //  SDR  SDR  SDR  SDR  SDR  SDR  SDR  SDR  SDR  SDR  SDR  SDR

        private static string g_IPMI_UNIT_VOLTS = "IPMI_UNIT_VOLTS";
        private static string g_IPMI_UNIT_DEGREES_C = "IPMI_UNIT_DEGREES_C";
        private static string g_IPMI_UNIT_RPM = "IPMI_UNIT_RPM";
        private static string g_IPMI_UNIT_AMPS = "IPMI_UNIT_AMPS";
        private static string g_IPMI_UNIT_WATTS = "IPMI_UNIT_WATTS";
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
        private string SDRGetOEMFiled(string powerName) {
            if(string.IsNullOrEmpty(powerName)) {
                return "0x00";
            }
            if(powerName.Length < 4) {
                return "0x00";
            }
            string nameUpper = powerName.ToUpper();
            if(nameUpper.Contains("TEMP")) {
                return SUB_DEVICE_SDR_TEMP;
            }
            if(nameUpper.Contains("BAT")) {
                return SUB_DEVICE_SDR_VBAT;
            }
            if(nameUpper.Contains("FAN")) {
                return SUB_DEVICE_SDR_FAN;
            }
            if(nameUpper.Contains("MAC5023_P")) {
                return SUB_DEVICE_SDR_MAC5023_P;
            }
            if(nameUpper.Contains("MAC5023_V")) {
                return SUB_DEVICE_SDR_MAC5023_V;
            }
            if(nameUpper.Contains("MAC5023_I")) {
                return SUB_DEVICE_SDR_MAC5023_I;
            }

            string[] arrary = nameUpper.Split('_');
            string val = "0";
            foreach(string item in arrary) {
                if(item[0] == 'P') {
                    if(item[2] == 'V') {
                        val = item[1].ToString() + item[3];
                        break;
                    }
                    if(item[3] == 'V') {
                        val = item[1].ToString() + item[2] + "0";
                        break;
                    }
                }
            }
            try {
                int vol = int.Parse(val);
                if (vol == 120 ) {
                    return SUB_DEVICE_SDR_P12V;
                }
                if(vol == 33) {
                    return SUB_DEVICE_SDR_P3V3;
                }
                if(vol == 25) {
                    return SUB_DEVICE_SDR_P2V5;
                }
                if(vol == 18) {
                    return SUB_DEVICE_SDR_P1V8;
                }
                if(vol == 12) {
                    return SUB_DEVICE_SDR_P1V2;
                }
                if(vol == 9) {
                    return SUB_DEVICE_SDR_P0V9;
                }
                else {
                    return SUB_DEVICE_SDR_ERROR;
                }
            } catch(Exception) {

            }
            return SUB_DEVICE_SDR_ERROR;
        }
        private bool SDRDescFillFiled(MRForm form) {
            try {
                DataGridView dg = form.dataGridView1;
                if (dg.Rows.Count < 5) {
                    return false;
                }
                sDRDesc.OEMField = SDRGetOEMFiled(form.tb_sensorName.Text);
                if(sDRDesc.OEMField.Equals(SUB_DEVICE_SDR_TEMP)) {
                    sDRDesc.Units2 = g_IPMI_UNIT_DEGREES_C;
                }else if(sDRDesc.OEMField.Equals(SUB_DEVICE_SDR_FAN)) {
                    sDRDesc.Units2 = g_IPMI_UNIT_RPM;
                } else if(sDRDesc.OEMField.Equals(SUB_DEVICE_SDR_MAC5023_P)) {
                    sDRDesc.Units2 = g_IPMI_UNIT_WATTS;
                } else if(sDRDesc.OEMField.Equals(SUB_DEVICE_SDR_MAC5023_V)) {
                    sDRDesc.Units2 = g_IPMI_UNIT_VOLTS;
                } else if(sDRDesc.OEMField.Equals(SUB_DEVICE_SDR_MAC5023_I)) {
                    sDRDesc.Units2 = g_IPMI_UNIT_AMPS;
                } else { 
                    sDRDesc.Units2 = g_IPMI_UNIT_VOLTS;
                    sDRDesc.M = (string)dg.Rows[2].Cells[0].Value + string.Empty;
                    sDRDesc.R_B_Exp = (string)dg.Rows[3].Cells[0].Value + "0";

                    int startIdx = -1;
                    sDRDesc.LowerNonCritical = SDRGetColVal(dg.Rows[5].Cells, ref startIdx);
                    sDRDesc.LowerCritical = SDRGetColVal(dg.Rows[5].Cells, ref startIdx);
                    sDRDesc.LowerNonRecoverable = SDRGetColVal(dg.Rows[5].Cells, ref startIdx);

                    sDRDesc.UpperNonCritical = SDRGetColVal(dg.Rows[5].Cells, ref startIdx);
                    sDRDesc.UpperCritical = SDRGetColVal(dg.Rows[5].Cells, ref startIdx);
                    sDRDesc.UpperNonRecoverable = SDRGetColVal(dg.Rows[5].Cells, ref startIdx);
                }

                if((sDRDesc.UpperNonRecoverable.Length != 4) || (sDRDesc.M.Length != 4)) {
                    return false;
                }

                sDRDesc._hdr_ID = "0x" + form.tb_recoderID.Text;

                sDRDesc.SensorNum = "0x" + form.tb_recoderID.Text.Substring(form.tb_recoderID.Text.Length-2 < 0 ? 0 : form.tb_recoderID.Text.Length - 2);
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

            sb.Append("\t{\t /* SDR Record    "+ sDRDesc.SensorNum +"   "+ sDRDesc.IDStr +"   "+ sDRDesc.OEMField + " */\r\n");
            
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
