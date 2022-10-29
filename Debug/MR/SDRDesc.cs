using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug.MR {
    public class SDRDesc {

        //string str4 = typeof(this).GetDescription();
        //TestClass test = new TestClass();
        //str4 = typeof(TestClass).GetDescription(nameof(test.Test1));


        /* SENSOR RECORD HEADER */
        [Description("Recoder ID")]
        public string _hdr_ID { get; set; }
        [Description("SDR Version")]
        public string _hdr_Version { get; set; }
        [Description("Record Type")]
        public string _hdr_Type { get; set; }
        [Description("Record Length = full sensor: 64")]
        public string _hdr_Len { get; set; }

        /* RECORD KEY BYTES */
        [Description("Sensor Owner ID")]
        public string OwnerID { get; set; }
        [Description("Sensor Owner LUN")]
        public string OwnerLUN { get; set; }
        [Description("Sensor number")]
        public string SensorNum { get; set; }

        /* RECORD BODY BYTES */
        [Description("Entity ID")]
        public string EntityID { get; set; }
        [Description("Entity Instance")]
        public string EntityIns { get; set; }
        [Description("Sensor Initialization")]
        public string SensorInit { get; set; }
        [Description("Sensor Capabilities")]
        public string SensorCaps { get; set; }
        [Description("Sensor Type")]
        public string SensorType { get; set; }
        [Description("Event/Read Type")]
        public string EventTypeCode { get; set; }
        [Description("Lower Threshold Reading Mask")]
        public string AssertionEventMask { get; set; }
        [Description("Upper Threshold Reading Mask")]
        public string DeAssertionEventMask { get; set; }
        [Description("Settable/Readable Threshold Mask")]
        public string DiscreteReadingMask { get; set; }
        [Description("Sensor Units 1")]
        public string Units1 { get; set; }
        [Description("Sensor Units 2 -Base Unit")]
        public string Units2 { get; set; }
        [Description("Sensor Units 3 -Modifier Unit")]
        public string Units3 { get; set; }
        [Description("Linearization")]
        public string Linearization { get; set; }
        [Description("M")]
        public string M { get; set; }
        [Description("M,Tolerance")]
        public string M_Tolerance { get; set; }
        [Description("B")]
        public string B { get; set; }
        [Description("B,Accuracy")]
        public string B_Accuracy { get; set; }
        [Description("Accuracy,Accuracy exponent   ")]
        public string Accuracy { get; set; }
        [Description("R exponent,B exponent ")]
        public string R_B_Exp { get; set; }
        [Description("Analog Characteristics Flags")]
        public string Flags { get; set; }
        [Description("Nominal Reading")]
        public string NominalReading { get; set; }
        [Description("Normal Maximum")]
        public string NormalMax { get; set; }
        [Description("Normal Minimum")]
        public string NormalMin { get; set; }
        [Description("Sensor Maximum Reading")]
        public string MaxReading { get; set; }
        [Description("Sensor Minimum Reading")]
        public string MinReading { get; set; }
        [Description("Upper Non-Recoverable Threshold")]
        public string UpperNonRecoverable { get; set; }
        [Description("Upper Critical Threshold")]
        public string UpperCritical { get; set; }
        [Description("Upper Non-Critical Threshold")]
        public string UpperNonCritical { get; set; }
        [Description("Lower Non-Recoverable Threshold")]
        public string LowerNonRecoverable { get; set; }
        [Description("Lower Critical Threshold")]
        public string LowerCritical { get; set; }
        [Description("Lower Non-Critical Threshold")]
        public string LowerNonCritical { get; set; }
        [Description("Positive-threshold Hysteresis calue")]
        public string PositiveHysterisis { get; set; }
        [Description("Negative-threshold Hysteresis calue")]
        public string NegativeHysterisis { get; set; }
        [Description("Reserved")]
        public string Reserved1 { get; set; }
        [Description("SenReservedsor")]
        public string Reserved2 { get; set; }
        [Description("OEM")]
        public string OEMField { get; set; }   //slave address (SUB_DEVICE_MODE)
        [Description("IDStrTypeLen")]
        public string IDStrTypeLen { get; set; }
        [Description("IDStr")]
        public string IDStr { get; set; }


        public SDRDesc() {
            /* SENSOR RECORD HEADER */
            //this._hdr_ID = "0x0004";
            this._hdr_Version = "0x51";
            this._hdr_Type = "0x01";
            this._hdr_Len = "0x40";

            /* RECORD KEY BYTES */
            this.OwnerID = "0x20";
            this.OwnerLUN = "0x00";
            //this.SensorNum = "0x00";

            /* RECORD BODY BYTES */
            this.EntityID = "0x07";
            this.EntityIns = "0x03";
            this.SensorInit = "0x7F";
            this.SensorCaps = "0x68";

            this.SensorType = "0x02";
            this.EventTypeCode = "0x01";
            this.AssertionEventMask = "0x7A95";
            this.DeAssertionEventMask = "0x7A95";
            this.DiscreteReadingMask = "0x3F3F";
            this.Units1 = "0x20";
                this.Units2 = "0x07";
            this.Units3 = "0x00";
            this.Linearization = "0x00";
                this.M = "0x07";
            this.M_Tolerance = "0x00";
            this.B = "0x00 & 0xff";
            this.B_Accuracy = "0x3E & 0xFF";
            this.Accuracy = "0x34";
            //this.R_B_Exp = "(0x0d << 4) + (0x0 & 0x0F)";
            this.Flags = "0x00";
            this.NominalReading = "0x00";
            this.NormalMax = "0x00";
            this.NormalMin = "0x00";
            this.MaxReading = "0xFF";
            this.MinReading = "0x00";

            this.UpperNonRecoverable = "0x07";
            this.UpperCritical = "0x07";
            this.UpperNonCritical = "0x07";

            this.LowerNonRecoverable = "0x07";
            this.LowerCritical = "0x07";
            this.LowerNonCritical = "0x07";

            this.PositiveHysterisis = "0x00";
            this.NegativeHysterisis = "0x00";
            this.Reserved1 = "0x00";
            this.Reserved2 = "0x00";
            this.OEMField = "0x00";   //slave address (SUB_DEVICE_MODE)
            this.IDStrTypeLen = "0xC0 + sizeof \"P12V123\"";
            //this.IDStr = "0x07";
        }

    }
}
