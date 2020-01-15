using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBoxControl.Model
{
    class IniConfig
    {
        #region 保存参数常量
        public const string CONFIG_FILE_NAME = "params.ini";
        public const string CONFIG_SECTION_VOLTAGE = "voltage";
        public const string CONFIG_SECTION_PVM = "pvm";
        public const string CONFIG_SECTION_INTERVAL = "interval";
        public const string CONFIG_VOLTAGE_FREQ_INTERVAL = "voltageAndpvm_Inerval";
        public const string CONFIG_SIGNAL_INTERVAL = "signal_Interval";
        public const string CONFIG_PARAM_V1 = "ch1_voltage";
        public const string CONFIG_PARAM_V2 = "ch2_voltage";
        public const string CONFIG_PARAM_V3 = "ch3_voltage";
        public const string CONFIG_PARAM_V4 = "ch4_voltage";
        public const string CONFIG_PARAM_V5 = "ch5_voltage";
        public const string CONFIG_PARAM_V6 = "ch6_voltage";
        public const string CONFIG_PARAM_V7 = "ch7_voltage";
        public const string CONFIG_PARAM_V8 = "ch8_voltage";
        public const string CONFIG_PARAM_V9 = "ch9_voltage";
        public const string CONFIG_PARAM_V10 = "ch10_voltage";
        public const string CONFIG_PARAM_V11 = "ch11_voltage";
        public const string CONFIG_PARAM_V12 = "ch12_voltage";
        public const string CONFIG_PARAM_V13 = "ch13_voltage";
        public const string CONFIG_PARAM_V14 = "ch14_voltage";
        public const string CONFIG_PARAM_V15 = "ch15_voltage";
        public const string CONFIG_PARAM_V16 = "ch16_voltage";
        public const string CONFIG_PARAM_V17 = "ch17_voltage";
        public const string CONFIG_PARAM_V18 = "ch18_voltage";
        public const string CONFIG_PARAM_V19 = "ch19_voltage";
        public const string CONFIG_PARAM_V20 = "ch20_voltage";

        public const string CONFIG_PARAM_PF1 = "ch1_frequency";
        public const string CONFIG_PARAM_PF2 = "ch2_frequency";
        public const string CONFIG_PARAM_PF3 = "ch3_frequency";
        public const string CONFIG_PARAM_PF4 = "ch4_frequency";
        public const string CONFIG_PARAM_PF5 = "ch5_frequency";
        public const string CONFIG_PARAM_PF6 = "ch6_frequency";
        public const string CONFIG_PARAM_PF7 = "ch7_frequency";
        public const string CONFIG_PARAM_PF8 = "ch8_frequency";
        public const string CONFIG_PARAM_PF9 = "ch9_frequency";
        public const string CONFIG_PARAM_PF10 = "ch10_frequency";
        public const string CONFIG_PARAM_PF11 = "ch11_frequency";
        public const string CONFIG_PARAM_PF12 = "ch12_frequency";
        public const string CONFIG_PARAM_PF13 = "ch13_frequency";
        public const string CONFIG_PARAM_PF14 = "ch14_frequency";
        public const string CONFIG_PARAM_PF15 = "ch15_frequency";
        public const string CONFIG_PARAM_PF16 = "ch16_frequency";
        public const string CONFIG_PARAM_PF17 = "ch17_frequency";
        public const string CONFIG_PARAM_PF18 = "ch18_frequency";
        public const string CONFIG_PARAM_PF19 = "ch19_frequency";
        public const string CONFIG_PARAM_PF20 = "ch20_frequency";
        public const string CONFIG_PARAM_PF21 = "ch21_frequency";
        public const string CONFIG_PARAM_PF22 = "ch22_frequency";
        public const string CONFIG_PARAM_PF23 = "ch23_frequency";
        public const string CONFIG_PARAM_PF24 = "ch24_frequency";
        public const string CONFIG_PARAM_PF25 = "ch25_frequency";
        public const string CONFIG_PARAM_PF26 = "ch26_frequency";
        public const string CONFIG_PARAM_PF27 = "ch27_frequency";
        public const string CONFIG_PARAM_PF28 = "ch28_frequency";
        public const string CONFIG_PARAM_PF29 = "ch29_frequency";
        public const string CONFIG_PARAM_PF30 = "ch30_frequency";

        public const string CONFIG_PARAM_PP1 = "ch1_dutyCycle";
        public const string CONFIG_PARAM_PP2 = "ch2_dutyCycle";
        public const string CONFIG_PARAM_PP3 = "ch3_dutyCycle";
        public const string CONFIG_PARAM_PP4 = "ch4_dutyCycle";
        public const string CONFIG_PARAM_PP5 = "ch5_dutyCycle";
        public const string CONFIG_PARAM_PP6 = "ch6_dutyCycle";
        public const string CONFIG_PARAM_PP7 = "ch7_dutyCycle";
        public const string CONFIG_PARAM_PP8 = "ch8_dutyCycle";
        public const string CONFIG_PARAM_PP9 = "ch9_dutyCycle";
        public const string CONFIG_PARAM_PP10 = "ch10_dutyCycle";
        public const string CONFIG_PARAM_PP11 = "ch11_dutyCycle";
        public const string CONFIG_PARAM_PP12 = "ch12_dutyCycle";
        public const string CONFIG_PARAM_PP13 = "ch13_dutyCycle";
        public const string CONFIG_PARAM_PP14 = "ch14_dutyCycle";
        public const string CONFIG_PARAM_PP15 = "ch15_dutyCycle";
        public const string CONFIG_PARAM_PP16 = "ch16_dutyCycle";
        public const string CONFIG_PARAM_PP17 = "ch17_dutyCycle";
        public const string CONFIG_PARAM_PP18 = "ch18_dutyCycle";
        public const string CONFIG_PARAM_PP19 = "ch19_dutyCycle";
        public const string CONFIG_PARAM_PP20 = "ch20_dutyCycle";
        public const string CONFIG_PARAM_PP21 = "ch21_dutyCycle";
        public const string CONFIG_PARAM_PP22 = "ch22_dutyCycle";
        public const string CONFIG_PARAM_PP23 = "ch23_dutyCycle";
        public const string CONFIG_PARAM_PP24 = "ch24_dutyCycle";
        public const string CONFIG_PARAM_PP25 = "ch25_dutyCycle";
        public const string CONFIG_PARAM_PP26 = "ch26_dutyCycle";
        public const string CONFIG_PARAM_PP27 = "ch27_dutyCycle";
        public const string CONFIG_PARAM_PP28 = "ch28_dutyCycle";
        public const string CONFIG_PARAM_PP29 = "ch29_dutyCycle";
        public const string CONFIG_PARAM_PP30 = "ch30_dutyCycle";
        #endregion
    }
}
