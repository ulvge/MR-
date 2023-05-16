using Debug.tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Debug
{
    public partial class CPLDTable : Form, InterfaceINI {

        List<items> refTable = new List<items>();

        //string tabl2e = new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE" ,	"J14",	"VCCINT1V0_V7_PWRGD"));

        private class items {
            public string IOSTANDARD { get; set; }
            public string PULLTYPE { get; set; }
            public string PIN { get; set; }
            public string name { get; set; }
            public items(string aa, string bb, string cc, string dd) {
                this.IOSTANDARD = aa;
                this.PULLTYPE = bb;
                this.PIN = cc;
                this.name = dd;
            }
        }
        public CPLDTable()
        {
            InitializeComponent();
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "L10", "PECI_PERST_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "L7", "V7_INIT_B"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "L8", "CPU_UART0_TX"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "L9", "JM_CARD_RST"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "M10", "R_DEBUG_1_RX2"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "M11", "FT_CLK_FER_EN"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "M14", "WX_NRST"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "M15", "V7_DONE"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "M7", "V7_PROGRAM_B"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "M8", "PCIE_WAKE_N_LS"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "M9", "FT_PEU0_C0_CLKREQ"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "N10", "R_DEBUG_3_RX2"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "N11", "CPLD_IO2"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "N14", "SSD_JM_IO2"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "N15", "SSD_JM_IO1"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "N16", "SSD_PRST_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "N6", "CPU_LPC_RST"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "N7", "CPU_CLK_OBV"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "N8", "FT_GPIO_D3"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "N9", "CPU_CRU_RST_OK"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "P10", "CPLD_IO0"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "P11", "CPLD_IO1"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "P12", "CPLD_IO3"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "P13", "R_DEBUG_2_TX2"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "P15", "SSD_JM_IO3"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "P16", "JM_CARD_PRESENT"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "P4", "FT_PCIE_RST_O"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "P5", "FT_ALL_PLL_LOCK"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "P6", "FT_PEU1_C0_CLKREQ"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "P7", "CPU_CKOBV_SEL1"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "P8", "CPU_CKOBV_SEL2"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "P9", "R_DEBUG_4_TX2"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "R10", "CPU_RESET_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "R11", "FT_PEU0_C2_CLKREQ"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "R12", "CPU_RST_FSM4"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "R13", "CPU_RST_FSM0"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "R14", "SSD_PRESENT_V7"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "R16", "SSD_JM_IO0"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "R3", "FT_PEU1_C2_CLKREQ"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "R4", "CPU_PEU1_LINKUP0"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "R5", "CPU_CKOBV_SEL0"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "R6", "CPU_UART2_TX"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "R7", "FT_BOARD_OVT"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "R8", "FT_GPIO_D2"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "R9", "CPU_UART0_RX"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "T10", "CPU_UART1_TX"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "T11", "CPU_PEU0_LINKUP0"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "T12", "FT_PEU0_C1_CLKREQ"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "T13", "CPU_RST_FSM1"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "T14", "CPU_RST_FSM2"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "T15", "CPU_RST_FSM3"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "T2", "FT_PEU1_C1_CLKREQ"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "T3", "FT_CPU_OVT"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "T4", "CPU_CKOBV_SEL4"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "T5", "CPU_CKOBV_SEL3"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "T6", "CPU_UART2_RX"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "T7", "FT_UART_SOUT"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "T8", "CPU_UART1_RX"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS18", " PULLTYPE = NONE", "T9", "CPU_POR_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "K12", "A_DESTROY"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "F1", "BMC_DEBUG_RXD"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "E3", "BMC_DEBUG_TXD"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "F10", "BMC_GPIO0"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "C16", "BMC_GPIO1"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "C15", "BMC_GPIO2"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "D15", "BMC_GPIO3"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "A15", "BMC_GPIO4"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "D14", "BMC_IDBTN_IN_OUT_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "B13", "BMC_IDLED_ON_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "J3", "BMC_LED"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "J12", "BMC_LPC_LRST_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "A11", "BMC_P0_OUT_PWROK"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "A12", "BMC_PSU_PWROK"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "A14", "BMC_PWR_BTN_OUT_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "B14", "BMC_RSTBTN_OUT_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "C2", "BMC_RXD0_CN"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "C1", "BMC_TXD0_CN"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "E15", "BMC_UART0_RX"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "E14", "BMC_UART0_TX"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "H14", "BMC_UART1_CTS"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "F16", "BMC_UART1_RTS"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "H16", "BMC_UART1_RXD"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "H13", "BMC_UART1_TXD"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "C4", "CON_CPU_UART_RX"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "B1", "CON_CPU_UART_TX"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "M1", "CPLD_BP_GPIO0"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "K1", "CPLD_BP_GPIO1"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "J6", "CPLD_BP_GPIO2"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "L1", "CPLD_BP_GPIO3"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "N3", "CPLD_BP_SYSRST_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "H12", "CPLD_CLK_50M"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "F3", "CPLD_DEBUG1"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "E1", "CPLD_DEBUG2"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "E2", "CPLD_DEBUG3"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "G6", "CPLD_DEBUG4"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "E16", "CPLD_LED0_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "D16", "CPLD_LED1_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "F15", "CPLD_LED12_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "B16", "CPLD_LED2_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "H1", "CPLD_RST_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "F2", "CPU_UART_DEBUG_RX"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "F5", "CPU_UART_DEBUG_TX"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "J15", "CPU_VR_SALRT"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "D6", "DEBUG_1_RX1"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "L12", "DEBUG_10"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "M16", "DEBUG_11"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "L15", "DEBUG_12"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "F7", "DEBUG_2_TX1"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "E6", "DEBUG_3_RX1"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "E7", "DEBUG_4_TX1"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "C5", "DEBUG_5"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "D7", "DEBUG_6"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "H15", "DEBUG_7"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "G14", "DEBUG_8"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "F8", "DEBUG_9"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "G11", "EN_SSD_JM_IO0"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "H11", "EN_SSD_JM_IO1"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "K15", "EN_SSD_JM_IO2"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "K13", "EN_SSD_JM_IO3"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "G1", "FP_DEBUG_DET_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "K2", "FP1_LED"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "J4", "FP2_LED"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "G4", "FP3_LED"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "H5", "FP4_LED"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "K3", "HDD_LED"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "J2", "J_BMC_RST_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "H3", "J_MASKABLERESET_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "K6", "JM_MRST_N_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "G15", "JM_PRESENT_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "P2", "M_2_B_DESTROY_DONE_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "L13", "M_2_B_DESTROY_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "R1", "M_2_B_SSD_LED_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "K5", "M_2_B_SSD_RST_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "N2", "M_2_DESTROY_DONE_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "K4", "M_2_DESTROY_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "P1", "M_2_SSD_LED_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "L3", "M_2_SSD_RST_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "L4", "MACLINK"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "M3", "P0V6_VTT_PWRGD"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "C12", "P0V75_VCORE_EN"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "D11", "P0V75_VCORE_PWRGD"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "A9", "P0V85_CPU_EN"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "C9", "P0V85_CPU_PWRGD"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "E10", "P12V_EN"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "D10", "P12V_PWRGD"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "C11", "P1V2_VDDQ_EN"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "L5", "P1V2_VDDQ_PWRGD"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "D9", "P1V8_IO_EN"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "E9", "P1V8_IO_PWRGD"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "B11", "P1V8_STBY_PWRGD"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "B9", "P2V5_DDR4_EN"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "A10", "P2V5_DDR4_PWRGD"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "C7", "P3V3_EN"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "B6", "P3V3_PWRGD"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "M2", "PHY_RST_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "H6", "PWR_LED"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "J1", "R_DESTROY_LED"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "N1", "R_ONE_KEY_DESTROY"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "G2", "R_SLOT_PCIE_RST_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "K14", "REDIVER_SCL"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "L14", "REDIVER_SDA"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "L16", "RST_BMC_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "L2", "RSTBTN_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "J13", "RTC_SEL"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "F12", "RTL_PHYRSTB"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "H4", "RUN_LED"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "F13", "SGPIO_CLK"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "G16", "SGPIO_IN"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "G12", "SGPIO_LOAD"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "F14", "SGPIO_OUT"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "G13", "SGPIO_RST"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "E11", "SO_DIMM_DET_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "B5", "SSD_ERASE"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "A5", "SSD_MRST0_N_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "D3", "SSD_PRESENT_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "D2", "U_2_ACTIVITY"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "B3", "U_2_IFDET_N"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "A4", "U_2_RSVD1"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "B4", "U_2_RSVD2"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "A8", "U_2_RSVD3"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "A3", "U_2_RSVD4"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "C8", "V7_MGTAVCC_EN"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "B7", "V7_MGTAVCC_PWRGD"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "E8", "V7_MGTAVTT_EN"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "D8", "V7_MGTAVTT_PWRGD"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "K11", "V7_MGTVCCAUX_EN"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "J11", "V7_MGTVCCAUX_PWRGD"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "G3", "VCC_P0V9_EN"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "H2", "VCC_P0V9_PWRGD"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "J16", "VCCINT1V0_V7_EN"));
			refTable.Add(new items(" IOSTANDARD = LVCMOS33", " PULLTYPE = NONE", "J14", "VCCINT1V0_V7_PWRGD"));

		}


		private class DestTable {
            public DestTable(string name, string pin) {
				this.name = name;
				this.pin = pin;
				this.IOSTANDARD = "";
				this.PULLTYPE = "";
			}

            public string name { get; set; }
			public string pin { get; set; }
			public string IOSTANDARD { get; set; }
			public string PULLTYPE { get; set; }
		}

		List<DestTable> g_destTable = new List<DestTable>();
		private void PortPinCovert_Load(object sender, EventArgs e) {
			loadINI();
			{ 
			g_destTable.Add(new DestTable("A_DESTROY", "B5"));
			g_destTable.Add(new DestTable("BMC_BP_RXD", "F12"));
			g_destTable.Add(new DestTable("BMC_BP_TXD", "H11"));
			g_destTable.Add(new DestTable("BMC_DEBUG_RXD", "D11"));
			g_destTable.Add(new DestTable("BMC_DEBUG_TXD", "A10"));
			g_destTable.Add(new DestTable("BMC_GPIO0", "L12"));
			g_destTable.Add(new DestTable("BMC_GPIO1", "K15"));
			g_destTable.Add(new DestTable("BMC_GPIO2", "K16"));
			g_destTable.Add(new DestTable("BMC_GPIO3", "J15"));
			g_destTable.Add(new DestTable("BMC_GPIO4", "J16"));
			g_destTable.Add(new DestTable("BMC_GPIO5", "H13"));
			g_destTable.Add(new DestTable("BMC_GPIO6", "G13"));
			g_destTable.Add(new DestTable("BMC_GPIO7", "H14"));
			g_destTable.Add(new DestTable("BMC_IDBTN_IN_OUT_N", "F13"));
			g_destTable.Add(new DestTable("BMC_IDLED_ON_N", "H16"));
			g_destTable.Add(new DestTable("BMC_LED1", "P2"));
			g_destTable.Add(new DestTable("BMC_LED2", "N3"));
			g_destTable.Add(new DestTable("BMC_LED3", "E14"));
			g_destTable.Add(new DestTable("BMC_LED5", "F15"));
			g_destTable.Add(new DestTable("BMC_LED6", "E15"));
			g_destTable.Add(new DestTable("BMC_LPC_LRST_N", "K13"));
			g_destTable.Add(new DestTable("BMC_P0_OUT_PWROK", "G15"));
			g_destTable.Add(new DestTable("BMC_PCIE_RST_N", "K12"));
			g_destTable.Add(new DestTable("BMC_PSU_PWROK", "G14"));
			g_destTable.Add(new DestTable("BMC_PWR_BTN_OUT_N", "J12"));
			g_destTable.Add(new DestTable("BMC_PWR_OK", "L10"));
			g_destTable.Add(new DestTable("BMC_RSTBTN_OUT_N", "J13"));
			g_destTable.Add(new DestTable("BMC_RXD0_CN", "G5"));
			g_destTable.Add(new DestTable("BMC_TXD0_CN", "H4"));
			g_destTable.Add(new DestTable("BMC_UART0_RX", "D15"));
			g_destTable.Add(new DestTable("BMC_UART0_TX", "D10"));
			g_destTable.Add(new DestTable("BMC_UART1_CTS", "L15"));
			g_destTable.Add(new DestTable("BMC_UART1_RTS", "J14"));
			g_destTable.Add(new DestTable("BMC_UART1_RXD", "K14"));
			g_destTable.Add(new DestTable("BMC_UART1_TXD", "L14"));
			g_destTable.Add(new DestTable("CON_CPU_UART_RX", "G4"));
			g_destTable.Add(new DestTable("CON_CPU_UART_TX", "G1"));
			g_destTable.Add(new DestTable("CPLD_BP_GPIO0", "P1"));
			g_destTable.Add(new DestTable("CPLD_BP_GPIO1", "R1"));
			g_destTable.Add(new DestTable("CPLD_BP_SYSRST#", "J1"));
			g_destTable.Add(new DestTable("CPLD_BUZZER", "G3"));
			g_destTable.Add(new DestTable("CPLD_CLK_50M", "H12"));
			g_destTable.Add(new DestTable("CPLD_DEBUG1", "F3"));
			g_destTable.Add(new DestTable("CPLD_DEBUG2", "E1"));
			g_destTable.Add(new DestTable("CPLD_DEBUG3", "E2"));
			g_destTable.Add(new DestTable("CPLD_DEBUG4", "G6"));
			g_destTable.Add(new DestTable("CPLD_IO0", "M7"));
			g_destTable.Add(new DestTable("CPLD_IO1", "P6"));
			g_destTable.Add(new DestTable("CPLD_IO2", "N7"));
			g_destTable.Add(new DestTable("CPLD_IO3", "P7"));
			g_destTable.Add(new DestTable("CPLD_LED0_N", "E16"));
			g_destTable.Add(new DestTable("CPLD_LED1_N", "D16"));
			g_destTable.Add(new DestTable("CPLD_LED2_N", "B16"));
			g_destTable.Add(new DestTable("CPLD_RST_N", "L2"));
			g_destTable.Add(new DestTable("CPU_CKOBV_SEL0", "T6"));
			g_destTable.Add(new DestTable("CPU_CKOBV_SEL1", "R8"));
			g_destTable.Add(new DestTable("CPU_CKOBV_SEL2", "T8"));
			g_destTable.Add(new DestTable("CPU_CKOBV_SEL3", "T9"));
			g_destTable.Add(new DestTable("CPU_CKOBV_SEL4", "T5"));
			g_destTable.Add(new DestTable("CPU_CLK_OBV", "R7"));
			g_destTable.Add(new DestTable("CPU_CRU_RST_OK", "R13"));
			g_destTable.Add(new DestTable("CPU_LPC_RST", "N6"));
			g_destTable.Add(new DestTable("CPU_PEU0_LINKUP0", "R14"));
			g_destTable.Add(new DestTable("CPU_PEU1_LINKUP0", "R5"));
			g_destTable.Add(new DestTable("CPU_POR_N", "M8"));
			g_destTable.Add(new DestTable("CPU_RESET_N", "L7"));
			g_destTable.Add(new DestTable("CPU_RST_FSM0", "M15"));
			g_destTable.Add(new DestTable("CPU_RST_FSM1", "R16"));
			g_destTable.Add(new DestTable("CPU_RST_FSM2", "P16"));
			g_destTable.Add(new DestTable("CPU_RST_FSM3", "N16"));
			g_destTable.Add(new DestTable("CPU_RST_FSM4", "N15"));
			g_destTable.Add(new DestTable("CPU_UART_BP_RX", "G12"));
			g_destTable.Add(new DestTable("CPU_UART_BP_TX", "G11"));
			g_destTable.Add(new DestTable("CPU_UART_DEBUG_RX", "A11"));
			g_destTable.Add(new DestTable("CPU_UART_DEBUG_TX", "D9"));
			g_destTable.Add(new DestTable("CPU_UART0_RX", "T13"));
			g_destTable.Add(new DestTable("CPU_UART0_TX", "R12"));
			g_destTable.Add(new DestTable("CPU_UART1_RX", "T12"));
			g_destTable.Add(new DestTable("CPU_UART1_TX", "P13"));
			g_destTable.Add(new DestTable("CPU_UART2_RX", "R11"));
			g_destTable.Add(new DestTable("CPU_UART2_TX", "R9"));
			g_destTable.Add(new DestTable("CPU_VR_SALRT", "H5"));
			g_destTable.Add(new DestTable("DEBUG_1_RX1", "A8"));
			g_destTable.Add(new DestTable("DEBUG_10", "B3"));
			g_destTable.Add(new DestTable("DEBUG_11", "B4"));
			g_destTable.Add(new DestTable("DEBUG_12", "A3"));
			g_destTable.Add(new DestTable("DEBUG_2_TX1", "C8"));
			g_destTable.Add(new DestTable("DEBUG_3_RX1", "E8"));
			g_destTable.Add(new DestTable("DEBUG_4_TX1", "C9"));
			g_destTable.Add(new DestTable("DEBUG_5", "D8"));
			g_destTable.Add(new DestTable("DEBUG_6", "F8"));
			g_destTable.Add(new DestTable("DEBUG_7", "E6"));
			g_destTable.Add(new DestTable("DEBUG_8", "B6"));
			g_destTable.Add(new DestTable("DEBUG_9", "B7"));
			g_destTable.Add(new DestTable("DONE_CPLD", "C13"));
			g_destTable.Add(new DestTable("EN_SSD_JM_IO0", "C7"));
			g_destTable.Add(new DestTable("EN_SSD_JM_IO1", "C5"));
			g_destTable.Add(new DestTable("EN_SSD_JM_IO2", "A5"));
			g_destTable.Add(new DestTable("EN_SSD_JM_IO3", "A4"));
			g_destTable.Add(new DestTable("ERASE_LED", "D6"));
			g_destTable.Add(new DestTable("FP_DEBUG_DET_N", "B9"));
			g_destTable.Add(new DestTable("FT_ALL_PLL_LOCK", "R4"));
			g_destTable.Add(new DestTable("FT_BOARD_OVT", "T7"));
			g_destTable.Add(new DestTable("FT_CLK_FER_EN", "M11"));
			g_destTable.Add(new DestTable("FT_CPU_OVT", "R6"));
			g_destTable.Add(new DestTable("FT_GPIO_D2", "T10"));
			g_destTable.Add(new DestTable("FT_GPIO_D3", "R10"));
			g_destTable.Add(new DestTable("FT_PCIE_RST_O", "T2"));
			g_destTable.Add(new DestTable("FT_PEU0_C0_CLKREQ", "T14"));
			g_destTable.Add(new DestTable("FT_PEU0_C1_CLKREQ", "T15"));
			g_destTable.Add(new DestTable("FT_PEU0_C2_CLKREQ", "P15"));
			g_destTable.Add(new DestTable("FT_PEU1_C0_CLKREQ", "T4"));
			g_destTable.Add(new DestTable("FT_PEU1_C1_CLKREQ", "R3"));
			g_destTable.Add(new DestTable("FT_PEU1_C2_CLKREQ", "T3"));
			g_destTable.Add(new DestTable("FT_UART_SOUT", "T11"));
			g_destTable.Add(new DestTable("J_BMC_RST_N", "N2"));
			g_destTable.Add(new DestTable("J_DESTROY_STS", "K3"));
			g_destTable.Add(new DestTable("J_KX_LED_SIGNAL", "K2"));
			g_destTable.Add(new DestTable("J_MAC_ACT", "L4"));
			g_destTable.Add(new DestTable("J_MAC_SPD", "L3"));
			g_destTable.Add(new DestTable("J_MAC_SPD2", "K6"));
			g_destTable.Add(new DestTable("J_PWR_EN", "L5"));
			g_destTable.Add(new DestTable("J_PWRBTN_N", "M3"));
			g_destTable.Add(new DestTable("JM_CARD_PRESENT", "N10"));
			g_destTable.Add(new DestTable("JM_CARD_RST", "N11"));
			g_destTable.Add(new DestTable("JM_MRST#_N", "B1"));
			g_destTable.Add(new DestTable("JM_PRESENT_N", "A9"));
			g_destTable.Add(new DestTable("JTAG_CPLD_TCK_HDR", "A7"));
			g_destTable.Add(new DestTable("JTAG_CPLD_TDI_HDR", "A6"));
			g_destTable.Add(new DestTable("JTAG_CPLD_TDO_HDR", "C6"));
			g_destTable.Add(new DestTable("JTAG_CPLD_TMS_HDR", "B8"));
			g_destTable.Add(new DestTable("KX_DESTROY", "D3"));
			g_destTable.Add(new DestTable("KX_GPIO_TCMP_0", "D2"));
			g_destTable.Add(new DestTable("LED1", "F10"));
			g_destTable.Add(new DestTable("LED2", "F9"));
			g_destTable.Add(new DestTable("M.2_B_DESTROY_DONE_N", "F1"));
			g_destTable.Add(new DestTable("M.2_B_DESTROY_N", "G2"));
			g_destTable.Add(new DestTable("M.2_B_SSD_LED_N", "D1"));
			g_destTable.Add(new DestTable("M.2_B_SSD_RST_N", "F2"));
			g_destTable.Add(new DestTable("M.2_DESTROY_DONE_N", "E11"));
			g_destTable.Add(new DestTable("M.2_DESTROY_N", "D14"));
			g_destTable.Add(new DestTable("M.2_SSD_LED_N", "C16"));
			g_destTable.Add(new DestTable("M.2_SSD_RST_N", "C15"));
			g_destTable.Add(new DestTable("MACLINK", "J11"));
			g_destTable.Add(new DestTable("MINI_PCIE_RST_N", "C2"));
			g_destTable.Add(new DestTable("NCA9511_EN", "H1"));
			g_destTable.Add(new DestTable("P0V6_VTT_PWRGD", "J6"));
			g_destTable.Add(new DestTable("P0V75_VCORE_EN", "C12"));
			g_destTable.Add(new DestTable("P0V75_VCORE_PWRGD", "J2"));
			g_destTable.Add(new DestTable("P0V85_CPU_EN", "A15"));
			g_destTable.Add(new DestTable("P0V85_CPU_PWRGD", "B14"));
			g_destTable.Add(new DestTable("P12V_EN", "J4"));
			g_destTable.Add(new DestTable("P12V_PWRGD", "J3"));
			g_destTable.Add(new DestTable("P1V2_VDDQ_EN", "B13"));
			g_destTable.Add(new DestTable("P1V2_VDDQ_PWRGD", "K1"));
			g_destTable.Add(new DestTable("P1V8_IO_EN", "F7"));
			g_destTable.Add(new DestTable("P1V8_IO_PWRGD", "H6"));
			g_destTable.Add(new DestTable("P1V8_STBY_PWRGD", "B11"));
			g_destTable.Add(new DestTable("P2V5_DDR4_EN", "H2"));
			g_destTable.Add(new DestTable("P2V5_DDR4_PWRGD", "H3"));
			g_destTable.Add(new DestTable("P3V3_EN", "F5"));
			g_destTable.Add(new DestTable("P3V3_PWRGD", "F4"));
			g_destTable.Add(new DestTable("PCIE_WAKE#_LS", "P5"));
			g_destTable.Add(new DestTable("PECI_PERST#", "M6"));
			g_destTable.Add(new DestTable("PHY_RST_N", "K11"));
			g_destTable.Add(new DestTable("PROGRAMN", "B10"));
			g_destTable.Add(new DestTable("R_DEBUG_1_RX2", "L8"));
			g_destTable.Add(new DestTable("R_DEBUG_2_TX2", "P8"));
			g_destTable.Add(new DestTable("R_DEBUG_3_RX2", "L9"));
			g_destTable.Add(new DestTable("R_DEBUG_4_TX2", "N8"));
			g_destTable.Add(new DestTable("R_DESTROY_LED", "M1"));
			g_destTable.Add(new DestTable("R_KX_LED_SIGNAL", "E3"));
			g_destTable.Add(new DestTable("R_ONE_KEY_DESTROY", "N1"));
			g_destTable.Add(new DestTable("REDIVER_SCL", "A14"));
			g_destTable.Add(new DestTable("REDIVER_SDA", "B12"));
			g_destTable.Add(new DestTable("RST_BMC_N", "L16"));
			g_destTable.Add(new DestTable("RSTBTN_N", "M2"));
			g_destTable.Add(new DestTable("RTC_SEL", "A12"));
			g_destTable.Add(new DestTable("RTL_PHYRSTB", "L13"));
			g_destTable.Add(new DestTable("SLOT_PCIE_RST_N", "C1"));
			g_destTable.Add(new DestTable("SO-DIMM_DET_N", "C11"));
			g_destTable.Add(new DestTable("SOL_SEL", "M16"));
			g_destTable.Add(new DestTable("SSD_ERASE", "E7"));
			g_destTable.Add(new DestTable("SSD_JM_IO0", "N9"));
			g_destTable.Add(new DestTable("SSD_JM_IO1", "P11"));
			g_destTable.Add(new DestTable("SSD_JM_IO2", "P9"));
			g_destTable.Add(new DestTable("SSD_JM_IO3", "P10"));
			g_destTable.Add(new DestTable("SSD_MRST#_N", "C4"));
			g_destTable.Add(new DestTable("SSD_PRESENT_N", "D7"));
			g_destTable.Add(new DestTable("SSD_PRESENT_V7", "M9"));
			g_destTable.Add(new DestTable("SSD_PRST_N", "P12"));
			g_destTable.Add(new DestTable("V7_DONE", "M10"));
			g_destTable.Add(new DestTable("V7_INIT_B", "N14"));
			g_destTable.Add(new DestTable("V7_MGTAVCC_EN", "E10"));
			g_destTable.Add(new DestTable("V7_MGTAVCC_PWRGD", "E9"));
			g_destTable.Add(new DestTable("V7_MGTAVTT_EN", "J5"));
			g_destTable.Add(new DestTable("V7_MGTAVTT_PWRGD", "L1"));
			g_destTable.Add(new DestTable("V7_MGTVCCAUX_EN", "H15"));
			g_destTable.Add(new DestTable("V7_MGTVCCAUX_PWRGD", "F14"));
			g_destTable.Add(new DestTable("V7_PROGRAM_B", "M14"));
			g_destTable.Add(new DestTable("VCC_P0V9_EN", "K4"));
			g_destTable.Add(new DestTable("VCC_P0V9_PWRGD", "K5"));
			g_destTable.Add(new DestTable("VCCINT1V0_V7_EN", "G16"));
			g_destTable.Add(new DestTable("VCCINT1V0_V7_PWRGD", "F16"));
			g_destTable.Add(new DestTable("WX_NRST", "P4"));
			}

            foreach(var dst in g_destTable) {
				foreach(var refs in refTable) {
					if(dst.pin.Equals(refs.PIN)) {
						dst.PULLTYPE = refs.PULLTYPE;
						dst.IOSTANDARD = refs.IOSTANDARD;
					}
				}
			}
			List<string> nullIOSTANDARD = new List<string>();
			//set_pin_assignment { V7_DONE } { LOCATION = M15; IOSTANDARD = LVCMOS18; PULLTYPE = NONE; }
			string res = string.Empty;
			foreach(var item in g_destTable) {
				//res += string.Format("set_pin_assignment { V7_DONE }{LOCATION = M15; IOSTANDARD = LVCMOS18; PULLTYPE = NONE;)");
				//res += string.Format("set_pin_assignment { 0 }{LOCATION = {1}; IOSTANDARD = {2}; PULLTYPE = NONE;)", item.name, item.IOSTANDARD, item.pin);
				res += "set_pin_assignment { " +item.name + " }{LOCATION = " + item.pin + "; " + item.IOSTANDARD + "; PULLTYPE = NONE;)\r\n";
				if (string.IsNullOrEmpty(item.IOSTANDARD)) {
					nullIOSTANDARD.Add(item.pin);
				}
			}
			Console.WriteLine("res length = "+ res.Length+", errorCount: " +nullIOSTANDARD.Count); //所有信息
			Console.WriteLine(nullIOSTANDARD.ToString()); //error信息


			V2_AutoCreateAllItems();
		}

		private class V2_OriDesc {
			public string humanName { get; set; }// eg: nr cr
			public string pin { get; set; } // eg: -10, 20
			public string IOSTANDARD { get; set; }
			public V2_OriDesc(string humanName, string pin) {
				this.humanName = humanName.Replace("M.2", "M_2");
				this.pin = pin;
				this.IOSTANDARD = GetBankVolLevel(pin);
			}
			private class LVCMOS {
				public string rowName;
				public int min;
				public int max;

				public LVCMOS(string rowName, int min, int max) {
					this.rowName = rowName;
					this.min = min;
					this.max = max;
				}
			}
			private class BankVolotLevel {
				public static string BankVolotLevel_1P8 = "LVCMOS18";
				public static string BankVolotLevel_3P3 = "LVCMOS33";
			}
			private class BankClass {
				public string bankName;
				public string bankVolotLevel;
				public LVCMOS[] range;
                    
                public BankClass(string bankName, string bankVolotLevel, LVCMOS[] range_Bank) {
                    this.bankName = bankName;
                    this.bankVolotLevel = bankVolotLevel;
                    this.range = range_Bank;
				}
				public static LVCMOS[] Range_Bank0 = {
					new LVCMOS("A", 3, 15),
					new LVCMOS("B", 3, 14),
					new LVCMOS("C", 4, 13),
					new LVCMOS("D", 5, 12),
					new LVCMOS("E", 6, 11),
					new LVCMOS("F", 7, 10),
				};
				public static LVCMOS[] Range_Bank1 = {
					new LVCMOS("B", 16, 16),
					new LVCMOS("C", 15, 16),
					new LVCMOS("D", 14, 16),
					new LVCMOS("E", 13, 16),
					new LVCMOS("F", 12, 16),

					new LVCMOS("G", 11, 16),
					new LVCMOS("H", 10, 16),
					new LVCMOS("J", 10, 16),
					new LVCMOS("K", 11, 16),

					new LVCMOS("L", 12, 16),
					new LVCMOS("M", 13, 13),
					new LVCMOS("M", 16, 16),
				};
				public static LVCMOS[] Range_Bank2 = {
					new LVCMOS("L", 7, 10),
					new LVCMOS("M", 6, 11),
					new LVCMOS("M", 14, 15),
					new LVCMOS("N", 6, 16),
					new LVCMOS("P", 4, 16),
					new LVCMOS("R", 3, 16),
					new LVCMOS("T", 2, 16),
				};
				public static LVCMOS[] Range_Bank3 = {
					new LVCMOS("K", 4, 5),
					new LVCMOS("L", 1, 5),
					new LVCMOS("M", 1, 4),
					new LVCMOS("N", 1, 3),
					new LVCMOS("P", 1, 2),
					new LVCMOS("R", 1, 1),
				};
				public static LVCMOS[] Range_Bank4 = {
					new LVCMOS("G", 1, 1),
					new LVCMOS("H", 1, 5),
					new LVCMOS("H", 7, 7),
					new LVCMOS("J", 1, 7),
					new LVCMOS("K", 1, 3),
					new LVCMOS("K", 6, 6),
				};
				public static LVCMOS[] Range_Bank5 = {
					new LVCMOS("B", 1, 1),
					new LVCMOS("C", 1, 2),
					new LVCMOS("D", 1, 3),
					new LVCMOS("E", 1, 4),
					new LVCMOS("F", 1, 5),
					new LVCMOS("G", 2, 6),
					new LVCMOS("H", 6, 6),
				};
			};
			private BankClass[] g_BankClass =  {
				new BankClass("bank0", BankVolotLevel.BankVolotLevel_3P3, BankClass.Range_Bank0),
				new BankClass("bank1", BankVolotLevel.BankVolotLevel_3P3, BankClass.Range_Bank1),
				new BankClass("bank2", BankVolotLevel.BankVolotLevel_3P3, BankClass.Range_Bank2),
				new BankClass("bank3", BankVolotLevel.BankVolotLevel_1P8, BankClass.Range_Bank3),
				new BankClass("bank4", BankVolotLevel.BankVolotLevel_1P8, BankClass.Range_Bank4),
				new BankClass("bank5", BankVolotLevel.BankVolotLevel_1P8, BankClass.Range_Bank5),
			};
			private string GetBankVolLevel(string pin) {
				if(pin.Equals("T6")) {
					Console.WriteLine(pin);
                }
				string row = pin.Substring(0,1);
				int column = int.Parse(pin.Substring(1));

                foreach(var bank in g_BankClass) {
					LVCMOS[] range = bank.range;
                    foreach(var item in range) {
						if(item.rowName == row) {
							if((column >= item.min) && (column <= item.max)) {
								return bank.bankVolotLevel;
							}
						}
					}
                }
				return "LVCMOSERROR";
			}
		}

		private string[] StringCommit = {
				"JTAG_",
				"PROGRAMN",
			};
		private bool IsPinNameNeedCommit(string pinName) {
			if(string.IsNullOrEmpty(pinName)) {
				return true;
			}
			foreach(var item in StringCommit) {
				if(pinName.Contains(item)) {
					return true;
				}
			}
			return false;
		}
		string oriFile = "../../res/ori.txt";
		string destFile = "../../res/dest.txt";
		//1 read ori table
		//2 parsing split ori table
		//3 add column
		//4 format output
		private void V2_AutoCreateAllItems() {
			List<V2_OriDesc> destDesc = new List<V2_OriDesc>();	
			// 1 read ori table
			string[] oriItems = File.ReadAllLines(oriFile);
			//2 parsing split ori table
			foreach(var item in oriItems) {
				string removeSpace = item;
				int lastLen;
				while(true) {
					lastLen = removeSpace.Length;
					removeSpace = removeSpace.Replace("  ", " ");
					removeSpace = removeSpace.Replace("\t", " ");
					removeSpace = removeSpace.Replace("#", "");
					if (lastLen == removeSpace.Length) {
						break;
					}
				}
				string[] lines = removeSpace.Split(' ');
				if (lines.Length != 2) {
					continue;
                }

				//3 add column
				destDesc.Add(new V2_OriDesc(lines[0], lines[1]));
			}

			//4 format output
			StringBuilder sb = new StringBuilder();
            foreach(var item in destDesc) {
				//set_pin_assignment { CPU_CKOBV_SEL0 } {  LOCATION = T6   ;IOSTANDARD = LVCMOS18  ;PULLTYPE = NONE; }
				string assembly = ("set_pin_assignment { " + item.humanName + " } { LOCATION = " + item.pin + "; IOSTANDARD = " + item.IOSTANDARD + "; PULLTYPE = NONE; }\r\n");
				if(IsPinNameNeedCommit(item.humanName)) {
					sb.Insert(0, "#" + assembly);
                } else {
					sb.Append(assembly);
				}
			}
			File.WriteAllText(destFile, sb.ToString());
		}

		private void PortPinCovert_FormClosing(object sender,FormClosingEventArgs e) {
            //e.Cancel = true;                  //不执行操作
            updateINI();
        }

        public void loadINI() {
            IniHelper iniHelper = new IniHelper();
            iniHelper.IniLoader2Form(this);
        }

        public void updateINI() {
            IniHelper iniHelper = new IniHelper();
            iniHelper.IniUpdate2File(this);
        }
    }
}