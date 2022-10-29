using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug.IPMB {
    static class NetFun {
        private static NetFuncDesc[] g_SensorDesc = new NetFuncDesc[] {
            new NetFuncDesc("CMD_GET_DEV_SDR_INFO",         "0x20"),
            new NetFuncDesc("CMD_GET_SENSOR_THRESHOLDS",    "0x27"),
            new NetFuncDesc("CMD_SET_SENSOR_EVENT_ENABLE",  "0x28"),
            new NetFuncDesc("CMD_GET_SENSOR_EVENT_STATUS",  "0x2B"),
            new NetFuncDesc("CMD_GET_SENSOR_READING",       "0x2d"),
            new NetFuncDesc("CMD_SET_SENSOR_READING",       "0x30"),
        };

        private static string g_ChassisCtrlDesc = "CHASSIS_POWER_OFF : 0" + "\n" +
                                                  "CHASSIS_POWER_ON :  1" + "\n";


        //private static NetFuncDesc[] g_ChassisCtrlDesc = new NetFuncDesc[] {
        //    new NetFuncDesc("CHASSIS_POWER_OFF",            "0x00"),
        //    new NetFuncDesc("CHASSIS_POWER_ON",             "0x01"),
        //    new NetFuncDesc("CHASSIS_POWER_CYCLE",          "0x02"),
        //    new NetFuncDesc("CHASSIS_POWER_RESET",          "0x03"),
        //    new NetFuncDesc("CHASSIS_DIAGNOSTIC_INTERRUPT", "0x04"),
        //    new NetFuncDesc("CHASSIS_SOFT_OFF",             "0x05"),
        //    new NetFuncDesc("CHASSIS_SMI_INTERRUPT",        "0x06"),
        //};
        private static NetFuncDesc[] g_ChassisDesc = new NetFuncDesc[] {
            new NetFuncDesc("CMD_CHASSIS_CONTROL",           "0x02", g_ChassisCtrlDesc),
            new NetFuncDesc("CMD_CHASSIS_RESET",             "0x03"),
            new NetFuncDesc("CMD_CHASSIS_IDENTIFY",          "0x04"),
        };
        private static NetFuncDesc[] g_AppDesc = new NetFuncDesc[] {
            new NetFuncDesc("CMD_GET_DEV_ID",       "0x01"),
            new NetFuncDesc("CMD_COLD_RESET",       "0x02"),
        };
        private static NetFuncDesc[] g_OEMDesc = new NetFuncDesc[] {
            new NetFuncDesc("CMD_GET_BLADE_ID",     "0x01"),
            new NetFuncDesc("CMD_GET_FAN_RPM",      "0x03", "index"),
            new NetFuncDesc("CMD_SET_FAN_PWM",      "0x04", "index, duty"),
            new NetFuncDesc("CMD_CPU_INFO",         "0x10"),
            new NetFuncDesc("CMD_BMC_INFO",         "0x11"),
        };
        public class NetFuncDesc {
            public string name { get; set; }
            public string val { get; set; }
            public object subCmd { get; set; }
            public NetFuncDesc(string name, string val) {
                this.name = name;
                this.val = val;
                this.subCmd = null;
            }
            public NetFuncDesc(string name, string val, object subCmd) {
                this.name = name;
                this.val = val;
                this.subCmd = subCmd;
            }
        }

        private static NetFuncDesc[] g_NetFunDesc = new NetFuncDesc[] {
            new NetFuncDesc("NETFN_CHASSIS", "0x00", g_ChassisDesc),
            new NetFuncDesc("netfn_bridge", "0x02", null),
            new NetFuncDesc("netfn_sensor", "0x04", g_SensorDesc),
            new NetFuncDesc("netfn_app", "0x06", g_AppDesc),
            new NetFuncDesc("netfn_firmware","0x08", null),
            new NetFuncDesc("netfn_storage","0x0a", null),
            new NetFuncDesc("netfn_transport","0x0c", null),
            new NetFuncDesc("netfn_oem","0x2e", g_OEMDesc),
            //new NetFuncDesc("netfn_picmg","0x2c", null),
            //new NetFuncDesc("netfn_ami","0x32", null),
            //new NetFuncDesc("netfn_opma1","0x30", null),
            //new NetFuncDesc("netfn_opma2","0x3e", null),
        };
        /// <summary>
        /// 获取所有的 netFunc 列表
        /// </summary>
        /// <returns></returns>
        public static string[] NetFunc_getAllName() {
            var lists = g_NetFunDesc.Select(t => t.name).ToList();
            return lists.ToArray();
        }
        /// <summary>
        /// 把 netFunc 转换成对应的 hex值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string NetFunc_convertVal(string name) {
            foreach(NetFuncDesc item in g_NetFunDesc) {
                if(item.name.Equals(name)) {
                    return item.val;
                }
            }
            return string.Empty;
        }

        public static NetFuncDesc[] NetFunc_getSubCmd(string funcName) {
            foreach(NetFuncDesc item in g_NetFunDesc) {
                if(item.name.Equals(funcName)) {
                    return (NetFuncDesc[])item.subCmd;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取指定的func 的所有 subcmd 列表
        /// </summary>
        /// <returns></returns>
        public static string[] SumCmd_getAllName(string funcName) {
            NetFuncDesc[] sumCmds = NetFunc_getSubCmd(funcName);
            if (sumCmds != null) {
                var lists = sumCmds.Select(t => t.name).ToList();
                return lists.ToArray();
            }
            return null;
        }
        /// <summary>
        /// 把 SumCmd 转换成对应的 hex值
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string SumCmd_convertVal(string funcName, string subCmdName) {
            NetFuncDesc[] sumCmds = NetFunc_getSubCmd(funcName);
            if(sumCmds != null) {
                foreach(NetFuncDesc item in sumCmds) {
                    if(item.name.Equals(subCmdName)) {
                        return item.val;
                    }
                }
            }
            return null;
        }
        public static string SumCmd_getDesc(string funcName, string subCmdName) {
            NetFuncDesc[] sumCmds = NetFunc_getSubCmd(funcName);
            if(sumCmds != null) {
                foreach(NetFuncDesc item in sumCmds) {
                    if(item.name.Equals(subCmdName)) {
                        return (string)item.subCmd;
                    }
                }
            }
            return string.Empty;
        }

    }
}

