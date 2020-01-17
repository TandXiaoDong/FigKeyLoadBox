using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using System.IO;
using System.Threading.Tasks;
using CommonUtils.Logger;
using CommonUtils.FileHelper;
using LoadBoxControl.Common;
using LoadBoxControl.Model;
using System.Threading;

namespace LoadBoxControl
{
    public partial class ShowCommand : RadForm
    {
        private SendCommand sendCommand;
        private System.Timers.Timer timer;
        private string configPath;
        private FileStream fileStream;
        private StreamReader streamReader;
        public ShowCommand(SendCommand sendCommand)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.WindowsDefaultLocation;
            //this.tbInput.ReadOnly = true;
            this.sendCommand = sendCommand;
        }

        private void ShowCommand_Load(object sender, EventArgs e)
        {
            StartSend();
            this.tbInput.TextChanged += TbInput_TextChanged;
            this.FormClosed += ShowCommand_FormClosed;
        }

        private void ShowCommand_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (streamReader != null)
                streamReader.Close();
            if (fileStream != null)
                fileStream.Close();
        }

        private void TbInput_TextChanged(object sender, EventArgs e)
        {
            this.tbInput.Focus();//获取焦点
            this.tbInput.Select(this.tbInput.TextLength, 0);//光标定位到文本最后
            this.tbInput.ScrollToCaret();//滚动到光标处
        }

        private void StartSend()
        {
            configPath = AppDomain.CurrentDomain.BaseDirectory + "config\\lbs.ini";
            if (!File.Exists(configPath))
            {
                this.tbInput.Text += "Executable file not found...";
                return;
            }
            if (sendCommand == null)
                return;
            ImportLastConfig();
        }

        private void ImportLastConfig()
        {
            Task.Run(() =>
            {
                this.tbInput.Text += "*************************************************************开始测试***********************************************************************\r\n";
                int delay = 1;//秒
                var path = AppDomain.CurrentDomain.BaseDirectory + "config\\lbs.ini";

                fileStream = new FileStream(path,FileMode.Open);
                streamReader = new StreamReader(fileStream,Encoding.Default);

                while (!streamReader.EndOfStream)
                {
                    var rdValue = streamReader.ReadLine();
                    if (rdValue.Trim().ToLower() == "reset")
                    {
                        this.tbInput.Text += "reset.....";
                        ResetCommand();
                        Task.Delay(1000).Wait();
                    }
                    if (!rdValue.Contains("="))
                        continue;
                    var stringTemp = rdValue.Substring(0, rdValue.IndexOf('=')).Trim();

                    switch (stringTemp)
                    {
                        #region voltage
                        case IniConfig.CONFIG_PARAM_V1:
                            //ch1_voltage=(0,100)       ;参数格式：(电压,延时时间)，单位：毫秒
                            string[] voltageResult1 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            int.TryParse(voltageResult1[1], out delay);
                            var sendResult1 = sendCommand.SendVoltageParam(1, voltageResult1[0]);
                            RefreshVotage(sendResult1, voltageResult1, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_V2:
                            //ch1_voltage=(0,100)       ;参数格式：(电压,延时时间)，单位：毫秒
                            string[] voltageResult2 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            int.TryParse(voltageResult2[1], out delay);
                            var sendResult2 = sendCommand.SendVoltageParam(2, voltageResult2[0]);
                            RefreshVotage(sendResult2, voltageResult2, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_V3:
                            //ch1_voltage=(0,100)       ;参数格式：(电压,延时时间)，单位：毫秒
                            string[] voltageResult3 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            int.TryParse(voltageResult3[1], out delay);
                            var sendResult3 = sendCommand.SendVoltageParam(3, voltageResult3[0]);
                            RefreshVotage(sendResult3, voltageResult3, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_V4:
                            //ch1_voltage=(0,100)       ;参数格式：(电压,延时时间)，单位：毫秒
                            string[] voltageResult4 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            int.TryParse(voltageResult4[1], out delay);
                            var sendResult4 = sendCommand.SendVoltageParam(4, voltageResult4[0]);
                            RefreshVotage(sendResult4, voltageResult4, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_V5:
                            //ch1_voltage=(0,100)       ;参数格式：(电压,延时时间)，单位：毫秒
                            string[] voltageResult5 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            int.TryParse(voltageResult5[1], out delay);
                            var sendResult5 = sendCommand.SendVoltageParam(5, voltageResult5[0]);
                            RefreshVotage(sendResult5, voltageResult5, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_V6:
                            //ch1_voltage=(0,100)       ;参数格式：(电压,延时时间)，单位：毫秒
                            string[] voltageResult6 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            int.TryParse(voltageResult6[1], out delay);
                            var sendResult6 = sendCommand.SendVoltageParam(6, voltageResult6[0]);
                            RefreshVotage(sendResult6, voltageResult6, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_V7:
                            //ch1_voltage=(0,100)       ;参数格式：(电压,延时时间)，单位：毫秒
                            string[] voltageResult7 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            int.TryParse(voltageResult7[1], out delay);
                            var sendResult7 = sendCommand.SendVoltageParam(7, voltageResult7[0]);
                            RefreshVotage(sendResult7, voltageResult7, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_V8:
                            //ch1_voltage=(0,100)       ;参数格式：(电压,延时时间)，单位：毫秒
                            string[] voltageResult8 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            int.TryParse(voltageResult8[1], out delay);
                            var sendResult8 = sendCommand.SendVoltageParam(8, voltageResult8[0]);
                            RefreshVotage(sendResult8, voltageResult8, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_V9:
                            //ch1_voltage=(0,100)       ;参数格式：(电压,延时时间)，单位：毫秒
                            string[] voltageResult9 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            int.TryParse(voltageResult9[1], out delay);
                            var sendResult9 = sendCommand.SendVoltageParam(9, voltageResult9[0]);
                            RefreshVotage(sendResult9, voltageResult9, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_V10:
                            //ch1_voltage=(0,100)       ;参数格式：(电压,延时时间)，单位：毫秒
                            string[] voltageResult10 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            int.TryParse(voltageResult10[1], out delay);
                            var sendResult10 = sendCommand.SendVoltageParam(10, voltageResult10[0]);
                            RefreshVotage(sendResult10, voltageResult10, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_V11:
                            //ch1_voltage=(0,100)       ;参数格式：(电压,延时时间)，单位：毫秒
                            string[] voltageResult11 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            int.TryParse(voltageResult11[1], out delay);
                            var sendResult11 = sendCommand.SendVoltageParam(11, voltageResult11[0]);
                            RefreshVotage(sendResult11, voltageResult11, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_V12:
                            //ch1_voltage=(0,100)       ;参数格式：(电压,延时时间)，单位：毫秒
                            string[] voltageResult12 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            int.TryParse(voltageResult12[1], out delay);
                            var sendResult12 = sendCommand.SendVoltageParam(12, voltageResult12[0]);
                            RefreshVotage(sendResult12, voltageResult12, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_V13:
                            //ch1_voltage=(0,100)       ;参数格式：(电压,延时时间)，单位：毫秒
                            string[] voltageResult13 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            int.TryParse(voltageResult13[1], out delay);
                            var sendResult13 = sendCommand.SendVoltageParam(13, voltageResult13[0]);
                            RefreshVotage(sendResult13, voltageResult13, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_V14:
                            //ch1_voltage=(0,100)       ;参数格式：(电压,延时时间)，单位：毫秒
                            string[] voltageResult14 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            int.TryParse(voltageResult14[1], out delay);
                            var sendResult14 = sendCommand.SendVoltageParam(14, voltageResult14[0]);
                            RefreshVotage(sendResult14, voltageResult14, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_V15:
                            //ch1_voltage=(0,100)       ;参数格式：(电压,延时时间)，单位：毫秒
                            string[] voltageResult15 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            int.TryParse(voltageResult15[1], out delay);
                            var sendResult15 = sendCommand.SendVoltageParam(15, voltageResult15[0]);
                            RefreshVotage(sendResult15, voltageResult15, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_V16:
                            //ch1_voltage=(0,100)       ;参数格式：(电压,延时时间)，单位：毫秒
                            string[] voltageResult16 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            int.TryParse(voltageResult16[1], out delay);
                            var sendResult16 = sendCommand.SendVoltageParam(16, voltageResult16[0]);
                            RefreshVotage(sendResult16, voltageResult16, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_V17:
                            //ch1_voltage=(0,100)       ;参数格式：(电压,延时时间)，单位：毫秒
                            string[] voltageResult17 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            int.TryParse(voltageResult17[1], out delay);
                            var sendResult17 = sendCommand.SendVoltageParam(17, voltageResult17[0]);
                            RefreshVotage(sendResult17, voltageResult17, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_V18:
                            //ch1_voltage=(0,100)       ;参数格式：(电压,延时时间)，单位：毫秒
                            string[] voltageResult18 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            int.TryParse(voltageResult18[1], out delay);
                            var sendResult18 = sendCommand.SendVoltageParam(18, voltageResult18[0]);
                            RefreshVotage(sendResult18, voltageResult18, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_V19:
                            //ch1_voltage=(0,100)       ;参数格式：(电压,延时时间)，单位：毫秒
                            string[] voltageResult19 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            int.TryParse(voltageResult19[1], out delay);
                            var sendResult19 = sendCommand.SendVoltageParam(19, voltageResult19[0]);
                            RefreshVotage(sendResult19, voltageResult19, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_V20:
                            //ch1_voltage=(0,100)       ;参数格式：(电压,延时时间)，单位：毫秒
                            string[] voltageResult20 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            int.TryParse(voltageResult20[1], out delay);
                            var sendResult20 = sendCommand.SendVoltageParam(20, voltageResult20[0]);
                            RefreshVotage(sendResult20, voltageResult20, delay);
                            break;
                        #endregion

                        #region pwm
                        //****************************PWM**************************
                        case IniConfig.CONFIG_PARAM_PF_PP1:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult1 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result1 = sendCommand.SendFrequencyParam(1, pwResult1[0]);
                            var result2 = sendCommand.SendFreqPersentParam(1, pwResult1[1]);
                            RefreshPWD(result1, result2, pwResult1, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP2:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult2 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result3 = sendCommand.SendFrequencyParam(2, pwResult2[0]);
                            var result4 = sendCommand.SendFreqPersentParam(2, pwResult2[1]);
                            RefreshPWD(result3, result4, pwResult2, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP3:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult3 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result5 = sendCommand.SendFrequencyParam(3, pwResult3[0]);
                            var result6 = sendCommand.SendFreqPersentParam(3, pwResult3[1]);
                            RefreshPWD(result5, result6, pwResult3, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP4:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult4 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result7 = sendCommand.SendFrequencyParam(4, pwResult4[0]);
                            var result8 = sendCommand.SendFreqPersentParam(4, pwResult4[1]);
                            RefreshPWD(result7, result8, pwResult4, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP5:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult5 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result9 = sendCommand.SendFrequencyParam(5, pwResult5[0]);
                            var result10 = sendCommand.SendFreqPersentParam(5, pwResult5[1]);
                            RefreshPWD(result9, result10, pwResult5, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP6:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult6 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result11 = sendCommand.SendFrequencyParam(6, pwResult6[0]);
                            var result12 = sendCommand.SendFreqPersentParam(6, pwResult6[1]);
                            RefreshPWD(result11, result12, pwResult6, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP7:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult7 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result13 = sendCommand.SendFrequencyParam(7, pwResult7[0]);
                            var result14 = sendCommand.SendFreqPersentParam(7, pwResult7[1]);
                            RefreshPWD(result13, result14, pwResult7, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP8:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult8 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result15 = sendCommand.SendFrequencyParam(8, pwResult8[0]);
                            var result16 = sendCommand.SendFreqPersentParam(8, pwResult8[1]);
                            RefreshPWD(result15, result16, pwResult8, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP9:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult9 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result17 = sendCommand.SendFrequencyParam(9, pwResult9[0]);
                            var result18 = sendCommand.SendFreqPersentParam(9, pwResult9[1]);
                            RefreshPWD(result17, result18, pwResult9, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP10:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult10 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result19 = sendCommand.SendFrequencyParam(10, pwResult10[0]);
                            var result20 = sendCommand.SendFreqPersentParam(10, pwResult10[1]);
                            RefreshPWD(result19, result20, pwResult10, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP11:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult11 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result21 = sendCommand.SendFrequencyParam(11, pwResult11[0]);
                            var result22 = sendCommand.SendFreqPersentParam(11, pwResult11[1]);
                            RefreshPWD(result21, result22, pwResult11, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP12:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult12 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result23 = sendCommand.SendFrequencyParam(12, pwResult12[0]);
                            var result24 = sendCommand.SendFreqPersentParam(12, pwResult12[1]);
                            RefreshPWD(result23, result24, pwResult12, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP13:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult13 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result25 = sendCommand.SendFrequencyParam(13, pwResult13[0]);
                            var result26 = sendCommand.SendFreqPersentParam(13, pwResult13[1]);
                            RefreshPWD(result25, result26, pwResult13, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP14:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult14 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result27 = sendCommand.SendFrequencyParam(14, pwResult14[0]);
                            var result28 = sendCommand.SendFreqPersentParam(14, pwResult14[1]);
                            RefreshPWD(result27, result28, pwResult14, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP15:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult15 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result29 = sendCommand.SendFrequencyParam(15, pwResult15[0]);
                            var result30 = sendCommand.SendFreqPersentParam(15, pwResult15[1]);
                            RefreshPWD(result29, result30, pwResult15, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP16:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult16 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result31 = sendCommand.SendFrequencyParam(16, pwResult16[0]);
                            var result32 = sendCommand.SendFreqPersentParam(16, pwResult16[1]);
                            RefreshPWD(result31, result32, pwResult16, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP17:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult17 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result33 = sendCommand.SendFrequencyParam(17, pwResult17[0]);
                            var result34 = sendCommand.SendFreqPersentParam(17, pwResult17[1]);
                            RefreshPWD(result33, result34, pwResult17, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP18:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult18 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result35 = sendCommand.SendFrequencyParam(18, pwResult18[0]);
                            var result36 = sendCommand.SendFreqPersentParam(18, pwResult18[1]);
                            RefreshPWD(result35, result36, pwResult18, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP19:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult19 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result37 = sendCommand.SendFrequencyParam(19, pwResult19[0]);
                            var result38 = sendCommand.SendFreqPersentParam(19, pwResult19[1]);
                            RefreshPWD(result37, result38, pwResult19, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP20:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult20 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result39 = sendCommand.SendFrequencyParam(20, pwResult20[0]);
                            var result40 = sendCommand.SendFreqPersentParam(20, pwResult20[1]);
                            RefreshPWD(result39, result40, pwResult20, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP21:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult21 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result41 = sendCommand.SendFrequencyParam(21, pwResult21[0]);
                            var result42 = sendCommand.SendFreqPersentParam(21, pwResult21[1]);
                            RefreshPWD(result41, result42, pwResult21, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP22:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult22 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result43 = sendCommand.SendFrequencyParam(22, pwResult22[0]);
                            var result44 = sendCommand.SendFreqPersentParam(22, pwResult22[1]);
                            RefreshPWD(result43, result44, pwResult22, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP23:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult23 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result45 = sendCommand.SendFrequencyParam(23, pwResult23[0]);
                            var result46 = sendCommand.SendFreqPersentParam(23, pwResult23[1]);
                            RefreshPWD(result45, result46, pwResult23, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP24:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult24 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result47 = sendCommand.SendFrequencyParam(24, pwResult24[0]);
                            var result48 = sendCommand.SendFreqPersentParam(24, pwResult24[1]);
                            RefreshPWD(result47, result48, pwResult24, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP25:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult25 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result49 = sendCommand.SendFrequencyParam(25, pwResult25[0]);
                            var result50 = sendCommand.SendFreqPersentParam(25, pwResult25[1]);
                            RefreshPWD(result49, result50, pwResult25, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP26:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult26 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result51 = sendCommand.SendFrequencyParam(26, pwResult26[0]);
                            var result52 = sendCommand.SendFreqPersentParam(26, pwResult26[1]);
                            RefreshPWD(result51, result52, pwResult26, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP27:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult27 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result53 = sendCommand.SendFrequencyParam(27, pwResult27[0]);
                            var result54 = sendCommand.SendFreqPersentParam(27, pwResult27[1]);
                            RefreshPWD(result53, result54, pwResult27, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP28:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult28 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result55 = sendCommand.SendFrequencyParam(28, pwResult28[0]);
                            var result56 = sendCommand.SendFreqPersentParam(28, pwResult28[1]);
                            RefreshPWD(result55, result56, pwResult28, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP29:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult29 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result57 = sendCommand.SendFrequencyParam(29, pwResult29[0]);
                            var result58 = sendCommand.SendFreqPersentParam(29, pwResult29[1]);
                            RefreshPWD(result57, result58, pwResult29, delay);
                            break;
                        case IniConfig.CONFIG_PARAM_PF_PP30:
                            //ch1_frequency_dutyCycle=(0,0,0)       ;参数格式：(频率,占空比,延时时间)，单位：毫秒
                            string[] pwResult30 = rdValue.Substring(rdValue.IndexOf('(') + 1, rdValue.IndexOf(')') - rdValue.IndexOf('(') - 1).Split(',');
                            var result59 = sendCommand.SendFrequencyParam(30, pwResult30[0]);
                            var result60 = sendCommand.SendFreqPersentParam(30, pwResult30[1]);
                            RefreshPWD(result59, result60, pwResult30, delay);
                            break;
                            #endregion
                    }
                }

                streamReader.Close();
                fileStream.Close();
                this.tbInput.Text += "*************************************************************执行完成***********************************************************************\r\n";
            });
        }

        public  void ResetCommand()
        {
            #region voltage
            sendCommand.SendVoltageParam(1, "0");
            sendCommand.SendVoltageParam(2, "0");
            sendCommand.SendVoltageParam(3, "0");
            sendCommand.SendVoltageParam(4, "0");
            sendCommand.SendVoltageParam(5, "0");
            sendCommand.SendVoltageParam(6, "0");
            sendCommand.SendVoltageParam(7, "0");
            sendCommand.SendVoltageParam(8, "0");
            sendCommand.SendVoltageParam(9, "0");
            sendCommand.SendVoltageParam(10, "0");

            sendCommand.SendVoltageParam(11, "0");
            sendCommand.SendVoltageParam(12, "0");
            sendCommand.SendVoltageParam(13, "0");
            sendCommand.SendVoltageParam(14, "0");
            sendCommand.SendVoltageParam(15, "0");
            sendCommand.SendVoltageParam(16, "0");
            sendCommand.SendVoltageParam(17, "0");
            sendCommand.SendVoltageParam(18, "0");
            sendCommand.SendVoltageParam(19, "0");
            sendCommand.SendVoltageParam(20, "0");
            #endregion

            #region freq
            sendCommand.SendFrequencyParam(1, "0");
            sendCommand.SendFrequencyParam(2, "0");
            sendCommand.SendFrequencyParam(3, "0");
            sendCommand.SendFrequencyParam(4, "0");
            sendCommand.SendFrequencyParam(5, "0");
            sendCommand.SendFrequencyParam(6, "0");
            sendCommand.SendFrequencyParam(7, "0");
            sendCommand.SendFrequencyParam(8, "0");
            sendCommand.SendFrequencyParam(9, "0");
            sendCommand.SendFrequencyParam(10, "0");

            sendCommand.SendFrequencyParam(11, "0");
            sendCommand.SendFrequencyParam(12, "0");
            sendCommand.SendFrequencyParam(13, "0");
            sendCommand.SendFrequencyParam(14, "0");
            sendCommand.SendFrequencyParam(15, "0");
            sendCommand.SendFrequencyParam(16, "0");
            sendCommand.SendFrequencyParam(17, "0");
            sendCommand.SendFrequencyParam(18, "0");
            sendCommand.SendFrequencyParam(19, "0");
            sendCommand.SendFrequencyParam(20, "0");

            sendCommand.SendFrequencyParam(21, "0");
            sendCommand.SendFrequencyParam(22, "0");
            sendCommand.SendFrequencyParam(23, "0");
            sendCommand.SendFrequencyParam(24, "0");
            sendCommand.SendFrequencyParam(25, "0");
            sendCommand.SendFrequencyParam(26, "0");
            sendCommand.SendFrequencyParam(27, "0");
            sendCommand.SendFrequencyParam(28, "0");
            sendCommand.SendFrequencyParam(29, "0");
            sendCommand.SendFrequencyParam(30, "0");
            #endregion

            #region pp
            sendCommand.SendFreqPersentParam(1, "0");
            sendCommand.SendFreqPersentParam(2, "0");
            sendCommand.SendFreqPersentParam(3, "0");
            sendCommand.SendFreqPersentParam(4, "0");
            sendCommand.SendFreqPersentParam(5, "0");
            sendCommand.SendFreqPersentParam(6, "0");
            sendCommand.SendFreqPersentParam(7, "0");
            sendCommand.SendFreqPersentParam(8, "0");
            sendCommand.SendFreqPersentParam(9, "0");
            sendCommand.SendFreqPersentParam(10, "0");

            sendCommand.SendFreqPersentParam(11, "0");
            sendCommand.SendFreqPersentParam(12, "0");
            sendCommand.SendFreqPersentParam(13, "0");
            sendCommand.SendFreqPersentParam(14, "0");
            sendCommand.SendFreqPersentParam(15, "0");
            sendCommand.SendFreqPersentParam(16, "0");
            sendCommand.SendFreqPersentParam(17, "0");
            sendCommand.SendFreqPersentParam(18, "0");
            sendCommand.SendFreqPersentParam(19, "0");
            sendCommand.SendFreqPersentParam(20, "0");

            sendCommand.SendFreqPersentParam(21, "0");
            sendCommand.SendFreqPersentParam(22, "0");
            sendCommand.SendFreqPersentParam(23, "0");
            sendCommand.SendFreqPersentParam(24, "0");
            sendCommand.SendFreqPersentParam(25, "0");
            sendCommand.SendFreqPersentParam(26, "0");
            sendCommand.SendFreqPersentParam(27, "0");
            sendCommand.SendFreqPersentParam(28, "0");
            sendCommand.SendFreqPersentParam(29, "0");
            sendCommand.SendFreqPersentParam(30, "0");
            #endregion
        }

        private void RefreshVotage(SendResult sendResult, string[] voltageResult, int delay)
        {
            if (sendResult.IsSendSuccess)
                this.tbInput.Text += $"电压-通道1 参数({voltageResult[0]},{voltageResult[1]});已发送指令：{sendResult.sendString} \r\n";
            this.tbInput.Text += $"Please wait {delay}s .......\r\n";
            Task.Delay(delay * 1000).Wait();
        }

        private void RefreshPWD(SendResult result1, SendResult result2, string[] pwResult, int dlay)
        {
            if (result1.IsSendSuccess && result2.IsSendSuccess)
                this.tbInput.Text += $"频率-占空比-通道1 参数({pwResult[0]},{pwResult[1]},{pwResult[2]});已发送指令：{result1.sendString} {result2.sendString} \r\n";
            int.TryParse(pwResult[2], out dlay);
            this.tbInput.Text += $"Please wait {dlay}s .......\r\n";
            Task.Delay(dlay * 1000).Wait();
        }
    }
}
