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

namespace LoadBoxControl
{
    public partial class ShowCommand : RadForm
    {
        private SendCommand sendCommand;
        private System.Timers.Timer timer;
        private string configPath;
        public ShowCommand(SendCommand sendCommand)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.tbInput.ReadOnly = true;
            this.sendCommand = sendCommand;
            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            StartSend();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            
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

        private async void ImportLastConfig()
        {
            await Task.Run(() =>
            {
                var voltageAndFreq = INIFile.GetValue(IniConfig.CONFIG_SECTION_INTERVAL, IniConfig.CONFIG_VOLTAGE_FREQ_INTERVAL,configPath);
                var signal = INIFile.GetValue(IniConfig.CONFIG_SECTION_INTERVAL, IniConfig.CONFIG_SIGNAL_INTERVAL, configPath);
                int voltageAndFreqDelay = 1000;
                int signalDelay = 1000;
                int.TryParse(voltageAndFreq,out voltageAndFreqDelay);
                int.TryParse(signal,out signalDelay);
                #region voltage
                var result = sendCommand.SendVoltageParam(1, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V1, configPath));
                if(result.IsSendSuccess)
                    this.tbInput.Text += $"电压-通道1;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendVoltageParam(2, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V2, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"电压-通道2;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendVoltageParam(3, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V3, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"电压-通道3;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendVoltageParam(4, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V4, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"电压-通道4;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendVoltageParam(5, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V5, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"电压-通道5;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendVoltageParam(6, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V6, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"电压-通道6;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendVoltageParam(7, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V7, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"电压-通道7;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendVoltageParam(8, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V8, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"电压-通道8;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendVoltageParam(9, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V9, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"电压-通道9;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendVoltageParam(10, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V10, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"电压-通道10;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendVoltageParam(11, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V11, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"电压-通道11;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendVoltageParam(12, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V12, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"电压-通道12;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendVoltageParam(13, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V13, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"电压-通道13;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendVoltageParam(14, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V14, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"电压-通道14;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendVoltageParam(15, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V15, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"电压-通道15;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendVoltageParam(16, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V16, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"电压-通道16;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendVoltageParam(17, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V17, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"电压-通道17;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendVoltageParam(18, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V18, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"电压-通道18;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendVoltageParam(19, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V19, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"电压-通道19;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendVoltageParam(20, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V20, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"电压-通道20;已发送指令：{result.sendString}";
                Task.Delay(voltageAndFreqDelay);
                #endregion

                #region pvm-pf
                result = sendCommand.SendFrequencyParam(1, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF1, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道1;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(1, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP1, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道1;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);


                result = sendCommand.SendFrequencyParam(2, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF2, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道2;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(2, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP2, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道2;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(3, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF3, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道3;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(3, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP3, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道3;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(4, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF4, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道4;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(4, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP4, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道4;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(5, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF5, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道5;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(5, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP5, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道5;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(6, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF6, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道6;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(6, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP6, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道6;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(7, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF7, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道7;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(7, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP7, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道7;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(8, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF8, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道8;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(8, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP8, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道8;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(9, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF9, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道9;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(9, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP9, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道9;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(10, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP10, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道10;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFrequencyParam(10, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF10, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道10;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFreqPersentParam(11, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP11, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道11;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFrequencyParam(11, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF11, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道11;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(12, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF12, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道12;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(12, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP12, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道12;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(13, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF13, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道13;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(13, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP13, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道13;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(14, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF14, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道14;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(14, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP14, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道14;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(15, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF15, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道15;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(15, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP15, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道15;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(16, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF16, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道16;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(16, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP16, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道16;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(17, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF17, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道17;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(17, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP17, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道17;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(18, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF18, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道18;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(18, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP18, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道18;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(19, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF19, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道19;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(19, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP19, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道19;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(20, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF20, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道20;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(20, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP20, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道20;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(21, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF21, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道21;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(21, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP21, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道21;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(22, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF22, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道22;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(22, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP22, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道22;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);


                result = sendCommand.SendFrequencyParam(23, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF23, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道23;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(23, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP23, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道23;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(24, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF24, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道24;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(24, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP24, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道24;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(25, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF25, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道25;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(25, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP25, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道25;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(26, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF26, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道26;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(26, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP26, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道26;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(27, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF27, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道27;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(27, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP27, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道27;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(28, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF28, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道28;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(28, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP28, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道28;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(29, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF29, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道29;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(29, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP29, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道29;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);

                result = sendCommand.SendFrequencyParam(30, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF30, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"频率-通道30;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                result = sendCommand.SendFreqPersentParam(30, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP30, configPath));
                if (result.IsSendSuccess)
                    this.tbInput.Text += $"占空比-通道30;已发送指令：{result.sendString}";
                Task.Delay(signalDelay);
                #endregion
            });
        }
    }
}
