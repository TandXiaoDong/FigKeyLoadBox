using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using System.IO.Ports;
using LoadBoxControl.Model;
using CommonUtils.Logger;
using CommonUtils.FileHelper;
using System.IO;
using System.Threading.Tasks;
using LoadBoxControl.Common;
using System.Threading;

namespace LoadBoxControl
{
    public partial class LoadBoxMainForm : RadForm
    {
        private DataTable dtVoltage;
        private DataTable dtPVM;
        private DataTypeEnum dataType;
        private const string V_CHANNEL = "通道";
        private const string V_SIMULATION = "模拟量";

        private const string F_CHANNEL = "通道";
        private const string F_FREQUENCY = "频率";
        private const string F_PERCENT = "占空比";

        private SerialPort serialPort;
        private delegate void MessageDelegate(byte[] msg);
        private VoltageParams voltageParams;
        private PwmParams pwmParams;
        private byte[] pwdbufferTemp = new byte[1024];
        private int lastBufferLen;
        private bool IsFirstReceive = true;
        private string configPath;
        private VoltageParams voltageParams1;
        private PwmParams pwmParams1;
        private SendCommand sendCommand;
        private int cacheBufferCount = 172;//频率与电压两帧数据总长度
        private byte[] bufferTemp;
        private bool IsFirstBuffer = true;
        private int rframe;//解析数量
        private int tframe;//缓冲数量
        private int cacheCount;

        public LoadBoxMainForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
        }

        private enum DataTypeEnum
        {
            Voltage = 0,
            PVM = 1
        }

        private void LoadBoxMainForm_Load(object sender, EventArgs e)
        {
            bufferTemp = new byte[cacheBufferCount];
            serialPort = new SerialPort();
            voltageParams = new VoltageParams();
            pwmParams = new PwmParams();
            configPath = AppDomain.CurrentDomain.BaseDirectory + "config\\";


            if (!Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
            }
            configPath += IniConfig.CONFIG_FILE_NAME;
            voltageParams1 = new VoltageParams();
            pwmParams1 = new PwmParams();
            InitControl();
            InitAutoSendParam(false);
            EventHandlers();
        }

        private void InitControl()
        {
            //获取窗口序列
            this.tool_cb_serialItem.Items.Clear();
            foreach (var portName in SerialPort.GetPortNames())
            {
                this.tool_cb_serialItem.Items.Add(portName);
            }
            if (this.tool_cb_serialItem.Items.Count < 1)
                return;
            this.tool_cb_serialItem.SelectedIndex = 0;
        }

        private void EventHandlers()
        {
            this.tool_refresh.Click += Tool_refresh_Click;
            this.tool_open_searial.Click += Tool_open_searial_Click;
            this.tool_close_serial.Click += Tool_close_serial_Click;
            this.serialPort.DataReceived += SerialPort_DataReceived;
            this.FormClosed += LoadBoxMainForm_FormClosed;
            this.tool_help.Click += Tool_help_Click;
            this.tool_abort.Click += Tool_abort_Click;
            this.tool_import.Click += Tool_import_Click;
            this.tool_save.Click += Tool_save_Click;
            this.tool_autosend.Click += Tool_autosend_Click;
            this.tool_autoSendCfg.Click += Tool_autoSendCfg_Click;
            this.tool_resetparam.Click += Tool_resetparam_Click;

            #region voltage click event hander
            this.tb_v1.Click += Tb_v1_Click;
            this.tb_v2.Click += Tb_v2_Click;
            this.tb_v3.Click += Tb_v3_Click;
            this.tb_v4.Click += Tb_v4_Click;
            this.tb_v5.Click += Tb_v5_Click;
            this.tb_v6.Click += Tb_v6_Click;
            this.tb_v7.Click += Tb_v7_Click;
            this.tb_v8.Click += Tb_v8_Click;
            this.tb_v9.Click += Tb_v9_Click;
            this.tb_v10.Click += Tb_v10_Click;
            this.tb_v11.Click += Tb_v11_Click;
            this.tb_v12.Click += Tb_v12_Click;
            this.tb_v13.Click += Tb_v13_Click;
            this.tb_v14.Click += Tb_v14_Click;
            this.tb_v15.Click += Tb_v15_Click;
            this.tb_v16.Click += Tb_v16_Click;
            this.tb_v17.Click += Tb_v17_Click;
            this.tb_v18.Click += Tb_v18_Click;
            this.tb_v19.Click += Tb_v19_Click;
            this.tb_v20.Click += Tb_v20_Click;
            #endregion

            #region pwm frequency 
            this.tb_pf1.Click += Tb_pf1_Click;
            this.tb_pf2.Click += Tb_pf2_Click;
            this.tb_pf3.Click += Tb_pf3_Click;
            this.tb_pf4.Click += Tb_pf4_Click;
            this.tb_pf5.Click += Tb_pf5_Click;
            this.tb_pf6.Click += Tb_pf6_Click;
            this.tb_pf7.Click += Tb_pf7_Click;
            this.tb_pf8.Click += Tb_pf8_Click;
            this.tb_pf9.Click += Tb_pf9_Click;
            this.tb_pf10.Click += Tb_pf10_Click;
            this.tb_pf11.Click += Tb_pf11_Click;
            this.tb_pf12.Click += Tb_pf12_Click;
            this.tb_pf13.Click += Tb_pf13_Click;
            this.tb_pf14.Click += Tb_pf14_Click;
            this.tb_pf15.Click += Tb_pf15_Click;
            this.tb_pf16.Click += Tb_pf16_Click;
            this.tb_pf17.Click += Tb_pf17_Click;
            this.tb_pf18.Click += Tb_pf18_Click;
            this.tb_pf19.Click += Tb_pf19_Click;
            this.tb_pf20.Click += Tb_pf20_Click;
            this.tb_pf21.Click += Tb_pf21_Click;
            this.tb_pf22.Click += Tb_pf22_Click;
            this.tb_pf23.Click += Tb_pf23_Click;
            this.tb_pf24.Click += Tb_pf24_Click;
            this.tb_pf25.Click += Tb_pf25_Click;
            this.tb_pf26.Click += Tb_pf26_Click;
            this.tb_pf27.Click += Tb_pf27_Click;
            this.tb_pf28.Click += Tb_pf28_Click;
            this.tb_pf29.Click += Tb_pf29_Click;
            this.tb_pf30.Click += Tb_pf30_Click;
            #endregion

            #region pwm frequency persent
            this.tb_pp1.Click += Tb_pp1_Click;
            this.tb_pp2.Click += Tb_pp2_Click;
            this.tb_pp3.Click += Tb_pp3_Click;
            this.tb_pp4.Click += Tb_pp4_Click;
            this.tb_pp5.Click += Tb_pp5_Click;
            this.tb_pp6.Click += Tb_pp6_Click;
            this.tb_pp7.Click += Tb_pp7_Click;
            this.tb_pp8.Click += Tb_pp8_Click;
            this.tb_pp9.Click += Tb_pp9_Click;
            this.tb_pp10.Click += Tb_pp10_Click;
            this.tb_pp11.Click += Tb_pp11_Click;
            this.tb_pp12.Click += Tb_pp12_Click;
            this.tb_pp13.Click += Tb_pp13_Click;
            this.tb_pp14.Click += Tb_pp14_Click;
            this.tb_pp15.Click += Tb_pp15_Click;
            this.tb_pp16.Click += Tb_pp16_Click;
            this.tb_pp17.Click += Tb_pp17_Click;
            this.tb_pp18.Click += Tb_pp18_Click;
            this.tb_pp19.Click += Tb_pp19_Click;
            this.tb_pp20.Click += Tb_pp20_Click;
            this.tb_pp21.Click += Tb_pp21_Click;
            this.tb_pp22.Click += Tb_pp22_Click;
            this.tb_pp23.Click += Tb_pp23_Click;
            this.tb_pp24.Click += Tb_pp24_Click;
            this.tb_pp25.Click += Tb_pp25_Click;
            this.tb_pp26.Click += Tb_pp26_Click;
            this.tb_pp27.Click += Tb_pp27_Click;
            this.tb_pp28.Click += Tb_pp28_Click;
            this.tb_pp29.Click += Tb_pp29_Click;
            this.tb_pp30.Click += Tb_pp30_Click;

            #endregion
        }

        private void Tool_resetparam_Click(object sender, EventArgs e)
        {
            //InitAutoSendParam(true);
            if (sendCommand == null)
            {
                MessageBox.Show("请检查串口状态！", "提示", MessageBoxButtons.OK);
                return;
            }
            ShowCommand showCommand = new ShowCommand(sendCommand);
            showCommand.ResetCommand();
        }

        private void Tool_autoSendCfg_Click(object sender, EventArgs e)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "config\\lbs.ini";
            System.Diagnostics.Process.Start(path);
        }

        private void Tool_autosend_Click(object sender, EventArgs e)
        {
            if (sendCommand == null)
            {
                MessageBox.Show("请检查串口状态！", "提示", MessageBoxButtons.OK);
                return;
            }
            ShowCommand showCommand = new ShowCommand(sendCommand);
            showCommand.ShowDialog();
        }

        private void Tool_save_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private void Tool_import_Click(object sender, EventArgs e)
        {
            ImportLastConfig();
        }

        private void Tool_abort_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox1 = new AboutBox1();
            aboutBox1.ShowDialog();
        }

        private void Tool_help_Click(object sender, EventArgs e)
        {
            FHelp fHelp = new FHelp();
            fHelp.ShowDialog();
        }

        #region pwm frequency persent
        private void Tb_pp30_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(30, this.tb_pp30.Text))
                ;// this.tb_pp30.Text = EditInput.inputValue.ToString(); 
        }

        private void Tb_pp29_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(29, this.tb_pp29.Text))
                ;// this.tb_pp29.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp28_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(28, this.tb_pp28.Text))
                ;// this.tb_pp28.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp27_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(27, this.tb_pp27.Text))
                ;// this.tb_pp27.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp26_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(26, this.tb_pp26.Text))
                ;// this.tb_pp26.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp25_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(25, this.tb_pp25.Text))
                ;// this.tb_pp25.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp24_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(24, this.tb_pp24.Text))
                ;// this.tb_pp24.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp23_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(23, this.tb_pp23.Text))
                ;// this.tb_pp23.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp22_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(22, this.tb_pp22.Text))
                ;// this.tb_pp22.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp21_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(21, this.tb_pp21.Text))
                ;// this.tb_pp21.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp20_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(20, this.tb_pp20.Text))
                ;// this.tb_pp20.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp19_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(19, this.tb_pp19.Text))
                ;// this.tb_pp19.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp18_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(18, this.tb_pp18.Text))
                ;// this.tb_pp18.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp17_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(17, this.tb_pp17.Text))
                ;// this.tb_pp17.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp16_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(16, this.tb_pp16.Text))
                ;// this.tb_pp16.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp15_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(15, this.tb_pp15.Text))
                ;// this.tb_pp15.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp14_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(14, this.tb_pp14.Text))
                ;// this.tb_pp14.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp13_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(13, this.tb_pp13.Text))
                ;// this.tb_pp13.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp12_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(12, this.tb_pp12.Text))
                ;// this.tb_pp12.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp11_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(11, this.tb_pp11.Text))
                ;// this.tb_pp11.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp10_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(10, this.tb_pp10.Text))
                ;// this.tb_pp10.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp9_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(9, this.tb_pp9.Text))
                ;// this.tb_pp9.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp8_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(8, this.tb_pp8.Text))
                ;// this.tb_pp8.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp7_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(7, this.tb_pp7.Text))
                ;// this.tb_pp7.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp6_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(6, this.tb_pp6.Text))
                ;// this.tb_pp6.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp5_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(5, this.tb_pp5.Text))
                ;// this.tb_pp5.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp4_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(4, this.tb_pp4.Text))
                ;// this.tb_pp4.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp3_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(3, this.tb_pp3.Text))
                ;// this.tb_pp3.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp2_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(2, this.tb_pp2.Text))
                ;// this.tb_pp2.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pp1_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqPersentSendString(1, this.tb_pp1.Text))
                ;// this.tb_pp1.Text = EditInput.inputValue.ToString();
        }
        #endregion

        #region pwm frequency
        private void Tb_pf30_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(30, this.tb_pf30.Text))
                ;// this.tb_pf30.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf29_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(29, this.tb_pf29.Text))
                ;// this.tb_pf29.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf28_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(28, this.tb_pf28.Text))
                ;// this.tb_pf28.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf27_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(27, this.tb_pf27.Text))
                ;// this.tb_pf27.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf26_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(26, this.tb_pf26.Text))
                ;// this.tb_pf26.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf25_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(25, this.tb_pf25.Text))
                ;// this.tb_pf25.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf24_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(24, this.tb_pf24.Text))
                ;// this.tb_pf24.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf23_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(23, this.tb_pf23.Text))
                ;// this.tb_pf23.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf22_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(22, this.tb_pf22.Text))
                ;// this.tb_pf22.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf21_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(21, this.tb_pf21.Text))
                ;// this.tb_pf21.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf20_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(20, this.tb_pf20.Text))
                ;// this.tb_pf20.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf19_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(19, this.tb_pf19.Text))
                ;// this.tb_pf19.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf18_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(18, this.tb_pf18.Text))
                ;// this.tb_pf18.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf17_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(17, this.tb_pf17.Text))
                ;// this.tb_pf17.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf16_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(16, this.tb_pf16.Text))
                ;// this.tb_pf16.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf15_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(15, this.tb_pf15.Text))
                ;// this.tb_pf15.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf14_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(14, this.tb_pf14.Text))
                ;// this.tb_pf14.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf13_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(13, this.tb_pf13.Text))
                ;// this.tb_pf13.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf12_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(12, this.tb_pf12.Text))
                ;// this.tb_pf12.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf11_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(11, this.tb_pf11.Text))
                ;// this.tb_pf11.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf10_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(10, this.tb_pf10.Text))
                ;// this.tb_pf10.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf9_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(9, this.tb_pf9.Text))
                ;// this.tb_pf9.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf8_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(8, this.tb_pf8.Text))
                ;// this.tb_pf8.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf7_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(7, this.tb_pf7.Text))
                ;// this.tb_pf7.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf6_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(6, this.tb_pf6.Text))
                ;// this.tb_pf6.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf5_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(5, this.tb_pf5.Text))
                ;// this.tb_pf5.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf4_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(4, this.tb_pf4.Text))
                ;// this.tb_pf4.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf3_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(3, this.tb_pf3.Text))
                ;// this.tb_pf3.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf2_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(2, this.tb_pf2.Text))
                ;// this.tb_pf2.Text = EditInput.inputValue.ToString();
        }

        private void Tb_pf1_Click(object sender, EventArgs e)
        {
            if (EditPwdFreqSendString(1, this.tb_pf1.Text))
                ;// this.tb_pf1.Text = EditInput.inputValue.ToString();
        }
        #endregion

        #region voltage
        private void Tb_v20_Click(object sender, EventArgs e)
        {
            if (EditVoltageSendString(20, this.tb_v20.Text))
                ;// this.tb_v20.Text = EditInput.inputValue.ToString();
        }

        private void Tb_v19_Click(object sender, EventArgs e)
        {
            if (EditVoltageSendString(19, this.tb_v19.Text))
                ;// this.tb_v19.Text = EditInput.inputValue.ToString();
        }

        private void Tb_v18_Click(object sender, EventArgs e)
        {
            if (EditVoltageSendString(18, this.tb_v18.Text))
                ;// this.tb_v18.Text = EditInput.inputValue.ToString();
        }

        private void Tb_v17_Click(object sender, EventArgs e)
        {
            if (EditVoltageSendString(17, this.tb_v17.Text))
                ;// this.tb_v17.Text = EditInput.inputValue.ToString();
        }

        private void Tb_v16_Click(object sender, EventArgs e)
        {
            if (EditVoltageSendString(16, this.tb_v16.Text))
                ;// this.tb_v16.Text = EditInput.inputValue.ToString();
        }

        private void Tb_v15_Click(object sender, EventArgs e)
        {
            if (EditVoltageSendString(15, this.tb_v15.Text))
                ;// this.tb_v15.Text = EditInput.inputValue.ToString();
        }

        private void Tb_v14_Click(object sender, EventArgs e)
        {
            if (EditVoltageSendString(14, this.tb_v14.Text))
                ;// this.tb_v14.Text = EditInput.inputValue.ToString();
        }

        private void Tb_v13_Click(object sender, EventArgs e)
        {
            if (EditVoltageSendString(13, this.tb_v13.Text))
                ;// this.tb_v13.Text = EditInput.inputValue.ToString();
        }

        private void Tb_v12_Click(object sender, EventArgs e)
        {
            if (EditVoltageSendString(12, this.tb_v12.Text))
                ;// this.tb_v12.Text = EditInput.inputValue.ToString();
        }

        private void Tb_v11_Click(object sender, EventArgs e)
        {
            if (EditVoltageSendString(11, this.tb_v11.Text))
                ;// this.tb_v11.Text = EditInput.inputValue.ToString();
        }

        private void Tb_v10_Click(object sender, EventArgs e)
        {
            if (EditVoltageSendString(10, this.tb_v10.Text))
                ;// this.tb_v10.Text = EditInput.inputValue.ToString();
        }

        private void Tb_v9_Click(object sender, EventArgs e)
        {
            if (EditVoltageSendString(9, this.tb_v9.Text))
                ;// this.tb_v9.Text = EditInput.inputValue.ToString();
        }

        private void Tb_v8_Click(object sender, EventArgs e)
        {
            if (EditVoltageSendString(8, this.tb_v8.Text))
                ;// this.tb_v8.Text = EditInput.inputValue.ToString();
        }

        private void Tb_v7_Click(object sender, EventArgs e)
        {
            if (EditVoltageSendString(7, this.tb_v7.Text))
                ;// this.tb_v7.Text = EditInput.inputValue.ToString();
        }

        private void Tb_v6_Click(object sender, EventArgs e)
        {
            if (EditVoltageSendString(6, this.tb_v6.Text))
                ;// this.tb_v6.Text = EditInput.inputValue.ToString();
        }

        private void Tb_v5_Click(object sender, EventArgs e)
        {
            if (EditVoltageSendString(5, this.tb_v5.Text))
                ;// this.tb_v5.Text = EditInput.inputValue.ToString();
        }

        private void Tb_v4_Click(object sender, EventArgs e)
        {
            if (EditVoltageSendString(4, this.tb_v4.Text))
                ;// this.tb_v4.Text = EditInput.inputValue.ToString();
        }

        private void Tb_v3_Click(object sender, EventArgs e)
        {
            if (EditVoltageSendString(3, this.tb_v3.Text))
                ;// this.tb_v3.Text = EditInput.inputValue.ToString();
        }

        private void Tb_v2_Click(object sender, EventArgs e)
        {
            if (EditVoltageSendString(2, this.tb_v2.Text))
                ;// this.tb_v2.Text = EditInput.inputValue.ToString();
        }

        private void Tb_v1_Click(object sender, EventArgs e)
        {
            if (EditVoltageSendString(1, this.tb_v1.Text))
                ;//this.tb_v1.Text = EditInput.inputValue.ToString();
        }
        #endregion

        /// <summary>
        /// 起始位置从1开始
        /// </summary>
        /// <param name="startIndex"></param>
        private bool EditVoltageSendString(int startIndex,string inputString)
        {
            EditInput editInput = new EditInput(inputString,EditInput.DataType.Voltage);
            editInput.ShowDialog();
            if (editInput.DialogResult != DialogResult.OK)
            {
                return false;
            }
            if (sendCommand == null)
                return false;
            var sendByte = sendCommand.SendVoltageString(startIndex, EditInput.inputValue);
            LogHelper.Log.Info($"【发送字符串】index={startIndex} " + BitConverter.ToString(sendByte));
            if (sendCommand.SendDevConfigMsg(sendByte))
                return true;
            return false;
        }

        private bool EditPwdFreqSendString(int startIndex,string inputString)
        {
            EditInput editInput = new EditInput(inputString,EditInput.DataType.PwmFrequency);
            editInput.ShowDialog();
            if (editInput.DialogResult != DialogResult.OK)
            {
                return false;
            }
            if (sendCommand == null)
                return false;
            var sendByte = sendCommand.SendPwmFreqString(startIndex, EditInput.inputValue);
            LogHelper.Log.Info($"【pwd-freq】index={startIndex} " + BitConverter.ToString(sendByte));
            if (sendCommand.SendDevConfigMsg(sendByte))
                return true;
            return false;
        }

        private bool EditPwdFreqPersentSendString(int startIndex,string inputString)
        {
            EditInput editInput = new EditInput(inputString,EditInput.DataType.PwmFrequencyPersent);
            editInput.ShowDialog();
            if (editInput.DialogResult != DialogResult.OK)
            {
                return false;
            }
            if (sendCommand == null)
                return false;
            var sendByte = sendCommand.SendPwmFreqPersentString(startIndex, EditInput.inputValue);
            LogHelper.Log.Info($"【pwd-persent】index={startIndex} " + BitConverter.ToString(sendByte));
            if (sendCommand.SendDevConfigMsg(sendByte))
                return true;
            return false;
        }

        private void LoadBoxMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort.IsOpen)
                serialPort.Close();
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPort.IsOpen)
            {
                byte[] receiveData = new byte[serialPort.BytesToRead];
                int readCount = serialPort.Read(receiveData, 0, receiveData.Length);
                if (readCount < 1)
                    return;

                MessageDelegate myDelegate = new MessageDelegate(ShowData);
                this.BeginInvoke(myDelegate, new object[] { receiveData });
                //this.Invoke((EventHandler)delegate
                //{
                //    ShowData(receiveData);
                //});
            }
            else
            {
                MessageBox.Show("请打开某个串口", "错误提示");
            }
        }

        private void Tool_close_serial_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
                serialPort.Close();
        }

        private void Tool_open_searial_Click(object sender, EventArgs e)
        {
            SerialOpen();
        }

        private void Tool_refresh_Click(object sender, EventArgs e)
        {
            //重新刷新串口
            this.tool_cb_serialItem.Items.Clear();
            foreach (var portName in SerialPort.GetPortNames())
            {
                this.tool_cb_serialItem.Items.Add(portName);
            }
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <returns></returns>
        private bool SerialOpen()
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    ///串口参数设置
                    //串口号
                    if (string.IsNullOrEmpty(this.tool_cb_serialItem.Text))
                    {
                        MessageBox.Show("串口未选择！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    serialPort.PortName = this.tool_cb_serialItem.Text;
                    //设置串口参数
                    this.serialPort.BaudRate = 115200;
                    this.serialPort.DataBits = 8;
                    this.serialPort.StopBits = StopBits.One;
                    this.serialPort.Parity = Parity.None;

                    serialPort.Open();
                    sendCommand = new SendCommand(serialPort);
                }
                return true;
            }
            catch (Exception Err)
            {
                MessageBox.Show($"{Err.Message}","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
        }

        private void ShowData(byte[] buffer)
        {
            //LogHelper.Log.Info($"********【收到下位机返回消息】len={buffer.Length} " + BitConverter.ToString(buffer));
            //this.txtReceive.Text += $"{BitConverter.ToString(buffer).Replace("-", " ")}";
            //解析数据
            /*
             * 数据格式：数据接收方+数据发送方+数据长度+标识+子标识+信息数据+数据有效性判断
             *  总长=126          C1 + C0 +len + sid + subid + data + crc(len+sid+subid+data)
             *  电压126 C1 C0 2B
             *  频率46 C1 C0 7B
             */
            //将数据添加到缓冲区
            if (IsFirstBuffer)
            {
                IsFirstBuffer = false;
                if (buffer[0] != 0XC1 && buffer[1] != 0XC0)
                {
                    return;
                }
            }
            if (tframe + buffer.Length > cacheBufferCount || (buffer.Length == 4 && buffer[1] == 0XFF && buffer[2] == 0XFF && buffer[3] == 0XFF))
            {
                //重置缓冲区
                tframe = 0;
                cacheCount = 0;
                rframe = 0;
                if (buffer[0] != 0XC1 && buffer[1] != 0XC0)
                {
                    bufferTemp = new byte[cacheBufferCount + buffer.Length];
                    return;
                }
                else if (buffer[0] == 0XC1 && buffer[1] == 0XC0)
                {
                    bufferTemp = new byte[cacheBufferCount + buffer.Length];
                }
            }
            Array.Copy(buffer,0,bufferTemp,tframe,buffer.Length);
            tframe += buffer.Length;
            cacheCount += buffer.Length;

            if (bufferTemp[rframe + 2] == 0X2B)
            {
                if (cacheCount >= 0X2B + 3)
                {
                    byte[] buffer2b = new byte[0X2B + 3];
                    Array.Copy(bufferTemp, rframe, buffer2b, 0, buffer2b.Length);
                    rframe += buffer2b.Length;
                    cacheCount -= rframe;
                    AnalysisVoltageData(buffer2b);
                }
            }
            else if (bufferTemp[rframe + 2] == 0X7B)
            {
                if (cacheCount >= 0X7B + 3)
                {
                    byte[] buffer7b = new byte[0X7B + 3];
                    Array.Copy(bufferTemp, rframe, buffer7b, 0, buffer7b.Length);
                    rframe += buffer7b.Length;
                    cacheCount -= rframe;
                    AnalysisVoltageData(buffer7b);
                }
            }
        }

        private void ShowData1(byte[] buffer)
        {
            LogHelper.Log.Info($"********【收到下位机返回消息】len={buffer.Length} " + BitConverter.ToString(buffer));
            //解析数据
            /*
             * 数据格式：数据接收方+数据发送方+数据长度+标识+子标识+信息数据+数据有效性判断
             *  总长=126          C1 + C0 +len + sid + subid + data + crc(len+sid+subid+data)
             */
            if (IsFirstReceive)
            {
                if (buffer.Length <= 1)
                {
                    if (buffer[0] != 0XC1)
                    {
                        IsFirstReceive = false;
                        return;
                    }
                }
                else if (buffer.Length >= 2)
                {
                    if (buffer[0] != 0XC1 && buffer[1] != 0XC0)
                    {
                        IsFirstReceive = false;
                        return;
                    }
                }
                IsFirstReceive = false;

            }
            if (buffer[0] == 0XC1)
            {
                //可能不完整
                //进一步判断
                if (buffer.Length < 3)
                {
                    //数据头不完整
                    LogHelper.Log.Info("帧头不完整"+BitConverter.ToString(buffer));
                    buffer.CopyTo(pwdbufferTemp, lastBufferLen);
                    lastBufferLen = buffer.Length;
                    return;
                }
                else if (buffer.Length < buffer[2] + 3) 
                {
                    //数据区尚未接收完整
                    LogHelper.Log.Info("【第一次接收不完整数据】" + buffer.Length + "  " + BitConverter.ToString(buffer));
                    //第一次不完整数组
                    buffer.CopyTo(pwdbufferTemp, lastBufferLen);
                    lastBufferLen = buffer.Length;
                    return;
                }
                else if (buffer.Length > buffer[2] + 3)
                {
                    //一次接收多组数据
                    LogHelper.Log.Info("【接收缓存大于需要的一帧数据-循环取出】");

                    while (buffer.Length >= buffer[2] + 3)
                    {
                        //取出需要的长度，进行解析
                        LogHelper.Log.Info($"【长度满足条件-取出数据-开始解析】lastBufferLen={lastBufferLen} pwdbufferTemp={pwdbufferTemp[2] + 3}");
                        byte[] needBuffer = new byte[buffer[2] + 3];
                        Array.Copy(buffer, 0, needBuffer, 0, buffer[2] + 3);
                        AnalysisVoltageData(needBuffer);

                        //更新剩余部分
                        int nextIndex = buffer.Length - needBuffer.Length;
                        byte[] nbuffer = new byte[nextIndex];
                        Array.Copy(buffer, needBuffer.Length, nbuffer,0,nextIndex);
                        buffer = nbuffer;
                        if (buffer.Length <= 2)
                            break;
                    }
                }
                else if (buffer.Length == buffer[2] + 3)
                {
                    //数据完整
                    AnalysisVoltageData(buffer);
                }
            }
            else
            {
                //继续缓存不完整数据
                LogHelper.Log.Info("【继续接收不完整数据】" + buffer.Length + "  " + BitConverter.ToString(buffer));
                //buffer.CopyTo(pwdbufferTemp, lastBufferLen + 1);
                Array.Copy(buffer,0,pwdbufferTemp,lastBufferLen,buffer.Length);
                lastBufferLen += buffer.Length;
                //每次缓存后进行完整性判断
                while (lastBufferLen >= pwdbufferTemp[2] + 3)
                {
                    //取出需要的长度，进行解析
                    LogHelper.Log.Info($"【长度满足条件-取出数据-开始解析】lastBufferLen={lastBufferLen} pwdbufferTemp={pwdbufferTemp[2] + 3}");
                    byte[] needBuffer = new byte[pwdbufferTemp[2] + 3];
                    Array.Copy(pwdbufferTemp,0,needBuffer,0,pwdbufferTemp[2] + 3);
                    AnalysisVoltageData(needBuffer);
                    //更新原buffer缓存
                    int sourchIndex = pwdbufferTemp[2] + 3;
                    byte[] nbuffer = new byte[1024];
                    //int destinLen = pwdbufferTemp.Length - pwdbufferTemp[2] - 3;
                    Array.Copy(pwdbufferTemp,sourchIndex,nbuffer,0,pwdbufferTemp[2] + 3);
                    lastBufferLen -= pwdbufferTemp[2] + 3;
                    pwdbufferTemp = nbuffer;
                    LogHelper.Log.Info($"取出数据后-更新lastBufferLen={lastBufferLen} pwdbufferTemp={pwdbufferTemp[2] + 3}");
                }
            }
        }

        private void AnalysisVoltageData(byte[] buffer)
        {
            //数据包完成，检查校验
            var validValue = CrcValid(buffer);
            //if (validValue != buffer[buffer.Length - 1]) //校验失败，最后一个字节是校验位
            //{
            //    LogHelper.Log.Info("校验失败！数据包不正确！");
            //    break;
            //}
            if (buffer[2] == 0X2B)
            {
                //电压数据
                //LogHelper.Log.Info("【电压完整数据】" + BitConverter.ToString(buffer));

                #region 1-20
                voltageParams.VoltageChannel1 = ((double)BitConverter.ToInt16(buffer, 5) / 10).ToString();
                voltageParams.VoltageChannel2 = ((double)BitConverter.ToInt16(buffer, 7) / 10).ToString();
                voltageParams.VoltageChannel3 = ((double)BitConverter.ToInt16(buffer, 9) / 10).ToString();
                voltageParams.VoltageChannel4 = ((double)BitConverter.ToInt16(buffer, 11) / 10).ToString();
                voltageParams.VoltageChannel5 = ((double)BitConverter.ToInt16(buffer, 13) / 10).ToString();
                voltageParams.VoltageChannel6 = ((double)BitConverter.ToInt16(buffer, 15) / 10).ToString();
                voltageParams.VoltageChannel7 = ((double)BitConverter.ToInt16(buffer, 17) / 10).ToString();
                voltageParams.VoltageChannel8 = ((double)BitConverter.ToInt16(buffer, 19) / 10).ToString();
                voltageParams.VoltageChannel9 = ((double)BitConverter.ToInt16(buffer, 21) / 10).ToString();
                voltageParams.VoltageChannel10 = ((double)BitConverter.ToInt16(buffer, 23) / 10).ToString();
                voltageParams.VoltageChannel11 = ((double)BitConverter.ToInt16(buffer, 25) / 10).ToString();
                voltageParams.VoltageChannel12 = ((double)BitConverter.ToInt16(buffer, 27) / 10).ToString();
                voltageParams.VoltageChannel13 = ((double)BitConverter.ToInt16(buffer, 29) / 10).ToString();
                voltageParams.VoltageChannel14 = ((double)BitConverter.ToInt16(buffer, 31) / 10).ToString();
                voltageParams.VoltageChannel15 = ((double)BitConverter.ToInt16(buffer, 33) / 10).ToString();
                voltageParams.VoltageChannel16 = ((double)BitConverter.ToInt16(buffer, 35) / 10).ToString();
                voltageParams.VoltageChannel17 = ((double)BitConverter.ToInt16(buffer, 37) / 10).ToString();
                voltageParams.VoltageChannel18 = ((double)BitConverter.ToInt16(buffer, 39) / 10).ToString();
                voltageParams.VoltageChannel19 = ((double)BitConverter.ToInt16(buffer, 41) / 10).ToString();
                voltageParams.VoltageChannel20 = ((double)BitConverter.ToInt16(buffer, 43) / 10).ToString();
                #endregion

                RefreshVoltageUI(voltageParams);
            }
            else if (buffer[2] == 0X7B)
            {
                //频率和占空比
                //LogHelper.Log.Info("【频率和占空比完整数据】" + BitConverter.ToString(buffer));
                //传入格式为：频率1-10+占空比1-10；频率11-20+占空比11-20；频率21-30+占空比21-30；
                #region 1-10
                pwmParams.PwmFrequencyCh1 = BitConverter.ToInt16(buffer, 5).ToString();
                pwmParams.PwmFrequencyCh2 = BitConverter.ToInt16(buffer, 7).ToString();
                pwmParams.PwmFrequencyCh3 = BitConverter.ToInt16(buffer, 9).ToString();
                pwmParams.PwmFrequencyCh4 = BitConverter.ToInt16(buffer, 11).ToString();
                pwmParams.PwmFrequencyCh5 = BitConverter.ToInt16(buffer, 13).ToString();
                pwmParams.PwmFrequencyCh6 = BitConverter.ToInt16(buffer, 15).ToString();
                pwmParams.PwmFrequencyCh7 = BitConverter.ToInt16(buffer, 17).ToString();
                pwmParams.PwmFrequencyCh8 = BitConverter.ToInt16(buffer, 19).ToString();
                pwmParams.PwmFrequencyCh9 = BitConverter.ToInt16(buffer, 21).ToString();
                pwmParams.PwmFrequencyCh10 = BitConverter.ToInt16(buffer, 23).ToString();

                pwmParams.PwmFreqPersentCh1 = BitConverter.ToInt16(buffer, 25).ToString();
                pwmParams.PwmFreqPersentCh2 = BitConverter.ToInt16(buffer, 27).ToString();
                pwmParams.PwmFreqPersentCh3 = BitConverter.ToInt16(buffer, 29).ToString();
                pwmParams.PwmFreqPersentCh4 = BitConverter.ToInt16(buffer, 31).ToString();
                pwmParams.PwmFreqPersentCh5 = BitConverter.ToInt16(buffer, 33).ToString();
                pwmParams.PwmFreqPersentCh6 = BitConverter.ToInt16(buffer, 35).ToString();
                pwmParams.PwmFreqPersentCh7 = BitConverter.ToInt16(buffer, 37).ToString();
                pwmParams.PwmFreqPersentCh8 = BitConverter.ToInt16(buffer, 39).ToString();
                pwmParams.PwmFreqPersentCh9 = BitConverter.ToInt16(buffer, 41).ToString();
                pwmParams.PwmFreqPersentCh10 = BitConverter.ToInt16(buffer, 43).ToString();
                #endregion

                #region 11-20
                pwmParams.PwmFrequencyCh11 = BitConverter.ToInt16(buffer, 45).ToString();
                pwmParams.PwmFrequencyCh12 = BitConverter.ToInt16(buffer, 47).ToString();
                pwmParams.PwmFrequencyCh13 = BitConverter.ToInt16(buffer, 49).ToString();
                pwmParams.PwmFrequencyCh14 = BitConverter.ToInt16(buffer, 51).ToString();
                pwmParams.PwmFrequencyCh15 = BitConverter.ToInt16(buffer, 53).ToString();
                pwmParams.PwmFrequencyCh16 = BitConverter.ToInt16(buffer, 55).ToString();
                pwmParams.PwmFrequencyCh17 = BitConverter.ToInt16(buffer, 57).ToString();
                pwmParams.PwmFrequencyCh18 = BitConverter.ToInt16(buffer, 59).ToString();
                pwmParams.PwmFrequencyCh19 = BitConverter.ToInt16(buffer, 61).ToString();
                pwmParams.PwmFrequencyCh20 = BitConverter.ToInt16(buffer, 63).ToString();

                pwmParams.PwmFreqPersentCh11 = BitConverter.ToInt16(buffer, 65).ToString();
                pwmParams.PwmFreqPersentCh12 = BitConverter.ToInt16(buffer, 67).ToString();
                pwmParams.PwmFreqPersentCh13 = BitConverter.ToInt16(buffer, 69).ToString();
                pwmParams.PwmFreqPersentCh14 = BitConverter.ToInt16(buffer, 71).ToString();
                pwmParams.PwmFreqPersentCh15 = BitConverter.ToInt16(buffer, 73).ToString();
                pwmParams.PwmFreqPersentCh16 = BitConverter.ToInt16(buffer, 75).ToString();
                pwmParams.PwmFreqPersentCh17 = BitConverter.ToInt16(buffer, 77).ToString();
                pwmParams.PwmFreqPersentCh18 = BitConverter.ToInt16(buffer, 79).ToString();
                pwmParams.PwmFreqPersentCh19 = BitConverter.ToInt16(buffer, 81).ToString();
                pwmParams.PwmFreqPersentCh20 = BitConverter.ToInt16(buffer, 83).ToString();
                #endregion

                #region 21-30
                pwmParams.PwmFrequencyCh21 = BitConverter.ToInt16(buffer, 85).ToString();
                pwmParams.PwmFrequencyCh22 = BitConverter.ToInt16(buffer, 87).ToString();
                pwmParams.PwmFrequencyCh23 = BitConverter.ToInt16(buffer, 89).ToString();
                pwmParams.PwmFrequencyCh24 = BitConverter.ToInt16(buffer, 91).ToString();
                pwmParams.PwmFrequencyCh25 = BitConverter.ToInt16(buffer, 93).ToString();
                pwmParams.PwmFrequencyCh26 = BitConverter.ToInt16(buffer, 95).ToString();
                pwmParams.PwmFrequencyCh27 = BitConverter.ToInt16(buffer, 97).ToString();
                pwmParams.PwmFrequencyCh28 = BitConverter.ToInt16(buffer, 99).ToString();
                pwmParams.PwmFrequencyCh29 = BitConverter.ToInt16(buffer, 101).ToString();
                pwmParams.PwmFrequencyCh30 = BitConverter.ToInt16(buffer, 103).ToString();

                pwmParams.PwmFreqPersentCh21 = BitConverter.ToInt16(buffer, 105).ToString();
                pwmParams.PwmFreqPersentCh22 = BitConverter.ToInt16(buffer, 107).ToString();
                pwmParams.PwmFreqPersentCh23 = BitConverter.ToInt16(buffer, 109).ToString();
                pwmParams.PwmFreqPersentCh24 = BitConverter.ToInt16(buffer, 111).ToString();
                pwmParams.PwmFreqPersentCh25 = BitConverter.ToInt16(buffer, 113).ToString();
                pwmParams.PwmFreqPersentCh26 = BitConverter.ToInt16(buffer, 115).ToString();
                pwmParams.PwmFreqPersentCh27 = BitConverter.ToInt16(buffer, 117).ToString();
                pwmParams.PwmFreqPersentCh28 = BitConverter.ToInt16(buffer, 119).ToString();
                pwmParams.PwmFreqPersentCh29 = BitConverter.ToInt16(buffer, 121).ToString();
                pwmParams.PwmFreqPersentCh30 = BitConverter.ToInt16(buffer, 123).ToString();
                #endregion

                RefreshPwdUI(pwmParams);

                //byte[] restBuffer = new byte[buffer.Length - buffer[2] - 3];
                ////restBuffer.CopyTo(buffer,buffer[2] + 3 + 1);//复制剩余长度到新数组继续解析
                //Array.Copy(buffer,buffer[2] + 3 +1,restBuffer,0,restBuffer.Length);
                //AnalysisVoltageData(restBuffer);
            }
        }

        private void RefreshVoltageUI(VoltageParams voltageParams)
        {
            this.tb_v1.Text = voltageParams.VoltageChannel1;
            this.tb_v2.Text = voltageParams.VoltageChannel2;
            this.tb_v3.Text = voltageParams.VoltageChannel3;
            this.tb_v4.Text = voltageParams.VoltageChannel4;
            this.tb_v5.Text = voltageParams.VoltageChannel5;
            this.tb_v6.Text = voltageParams.VoltageChannel6;
            this.tb_v7.Text = voltageParams.VoltageChannel7;
            this.tb_v8.Text = voltageParams.VoltageChannel8;
            this.tb_v9.Text = voltageParams.VoltageChannel9;
            this.tb_v10.Text = voltageParams.VoltageChannel10;
            this.tb_v11.Text = voltageParams.VoltageChannel11;
            this.tb_v12.Text = voltageParams.VoltageChannel12;
            this.tb_v13.Text = voltageParams.VoltageChannel13;
            this.tb_v14.Text = voltageParams.VoltageChannel14;
            this.tb_v15.Text = voltageParams.VoltageChannel15;
            this.tb_v16.Text = voltageParams.VoltageChannel16;
            this.tb_v17.Text = voltageParams.VoltageChannel17;
            this.tb_v18.Text = voltageParams.VoltageChannel18;
            this.tb_v19.Text = voltageParams.VoltageChannel19;
            this.tb_v20.Text = voltageParams.VoltageChannel20;
        }

        private void RefreshPwdUI(PwmParams pwmParams)
        {
            this.tb_pf1.Text = pwmParams.PwmFrequencyCh1;
            this.tb_pf2.Text = pwmParams.PwmFrequencyCh2;
            this.tb_pf3.Text = pwmParams.PwmFrequencyCh3;
            this.tb_pf4.Text = pwmParams.PwmFrequencyCh4;
            this.tb_pf5.Text = pwmParams.PwmFrequencyCh5;
            this.tb_pf6.Text = pwmParams.PwmFrequencyCh6;
            this.tb_pf7.Text = pwmParams.PwmFrequencyCh7;
            this.tb_pf8.Text = pwmParams.PwmFrequencyCh8;
            this.tb_pf9.Text = pwmParams.PwmFrequencyCh9;
            this.tb_pf10.Text = pwmParams.PwmFrequencyCh10;
            this.tb_pf11.Text = pwmParams.PwmFrequencyCh11;
            this.tb_pf12.Text = pwmParams.PwmFrequencyCh12;
            this.tb_pf13.Text = pwmParams.PwmFrequencyCh13;
            this.tb_pf14.Text = pwmParams.PwmFrequencyCh14;
            this.tb_pf15.Text = pwmParams.PwmFrequencyCh15;
            this.tb_pf16.Text = pwmParams.PwmFrequencyCh16;
            this.tb_pf17.Text = pwmParams.PwmFrequencyCh17;
            this.tb_pf18.Text = pwmParams.PwmFrequencyCh18;
            this.tb_pf19.Text = pwmParams.PwmFrequencyCh19;
            this.tb_pf20.Text = pwmParams.PwmFrequencyCh20;
            this.tb_pf21.Text = pwmParams.PwmFrequencyCh21;
            this.tb_pf22.Text = pwmParams.PwmFrequencyCh22;
            this.tb_pf23.Text = pwmParams.PwmFrequencyCh23;
            this.tb_pf24.Text = pwmParams.PwmFrequencyCh24;
            this.tb_pf25.Text = pwmParams.PwmFrequencyCh25;
            this.tb_pf26.Text = pwmParams.PwmFrequencyCh26;
            this.tb_pf27.Text = pwmParams.PwmFrequencyCh27;
            this.tb_pf28.Text = pwmParams.PwmFrequencyCh28;
            this.tb_pf29.Text = pwmParams.PwmFrequencyCh29;
            this.tb_pf30.Text = pwmParams.PwmFrequencyCh30;

            this.tb_pp1.Text = pwmParams.PwmFreqPersentCh1;
            this.tb_pp2.Text = pwmParams.PwmFreqPersentCh2;
            this.tb_pp3.Text = pwmParams.PwmFreqPersentCh3;
            this.tb_pp4.Text = pwmParams.PwmFreqPersentCh4;
            this.tb_pp5.Text = pwmParams.PwmFreqPersentCh5;
            this.tb_pp6.Text = pwmParams.PwmFreqPersentCh6;
            this.tb_pp7.Text = pwmParams.PwmFreqPersentCh7;
            this.tb_pp8.Text = pwmParams.PwmFreqPersentCh8;
            this.tb_pp9.Text = pwmParams.PwmFreqPersentCh9;
            this.tb_pp10.Text = pwmParams.PwmFreqPersentCh10;
            this.tb_pp11.Text = pwmParams.PwmFreqPersentCh11;
            this.tb_pp12.Text = pwmParams.PwmFreqPersentCh12;
            this.tb_pp13.Text = pwmParams.PwmFreqPersentCh13;
            this.tb_pp14.Text = pwmParams.PwmFreqPersentCh14;
            this.tb_pp15.Text = pwmParams.PwmFreqPersentCh15;
            this.tb_pp16.Text = pwmParams.PwmFreqPersentCh16;
            this.tb_pp17.Text = pwmParams.PwmFreqPersentCh17;
            this.tb_pp18.Text = pwmParams.PwmFreqPersentCh18;
            this.tb_pp19.Text = pwmParams.PwmFreqPersentCh19;
            this.tb_pp20.Text = pwmParams.PwmFreqPersentCh20;
            this.tb_pp21.Text = pwmParams.PwmFreqPersentCh21;
            this.tb_pp22.Text = pwmParams.PwmFreqPersentCh22;
            this.tb_pp23.Text = pwmParams.PwmFreqPersentCh23;
            this.tb_pp24.Text = pwmParams.PwmFreqPersentCh24;
            this.tb_pp25.Text = pwmParams.PwmFreqPersentCh25;
            this.tb_pp26.Text = pwmParams.PwmFreqPersentCh26;
            this.tb_pp27.Text = pwmParams.PwmFreqPersentCh27;
            this.tb_pp28.Text = pwmParams.PwmFreqPersentCh28;
            this.tb_pp29.Text = pwmParams.PwmFreqPersentCh29;
            this.tb_pp30.Text = pwmParams.PwmFreqPersentCh30;
        }

        private byte CrcValid(byte[] receiveData)
        {
            //C1 + C0 +len + sid + subid + data + crc(len+sid+subid+data)
            byte sum = 0;
            for (int i = 0 + 2; i < receiveData.Length - 2; i++)
            {
                sum += receiveData[i];
            }
            return sum;
        }

        private async void SaveConfig()
        {
            await Task.Run(()=>
            {
                #region voltage
                INIFile.SetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V1, this.tb_v1.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V2, this.tb_v2.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V3, this.tb_v3.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V4, this.tb_v4.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V5, this.tb_v5.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V6, this.tb_v6.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V7, this.tb_v7.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V8, this.tb_v8.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V9, this.tb_v9.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V10, this.tb_v10.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V11, this.tb_v11.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V12, this.tb_v12.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V13, this.tb_v13.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V14, this.tb_v14.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V15, this.tb_v15.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V16, this.tb_v16.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V17, this.tb_v17.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V18, this.tb_v18.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V19, this.tb_v19.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V20, this.tb_v20.Text.Trim(), configPath);
                #endregion

                #region pvm
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF1, this.tb_pf1.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP1, this.tb_pp1.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF2, this.tb_pf2.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP2, this.tb_pp2.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF3, this.tb_pf3.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP3, this.tb_pp3.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF4, this.tb_pf4.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP4, this.tb_pp4.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF5, this.tb_pf5.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP5, this.tb_pp5.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF6, this.tb_pf6.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP6, this.tb_pp6.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF7, this.tb_pf7.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP7, this.tb_pp7.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF8, this.tb_pf8.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP8, this.tb_pp8.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF9, this.tb_pf9.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP9, this.tb_pp9.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF10, this.tb_pf10.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP10, this.tb_pp10.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF11, this.tb_pf11.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP11, this.tb_pp11.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF12, this.tb_pf12.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP12, this.tb_pp12.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF13, this.tb_pf13.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP13, this.tb_pp13.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF14, this.tb_pf14.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP14, this.tb_pp14.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF15, this.tb_pf15.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP15, this.tb_pp15.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF16, this.tb_pf16.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP16, this.tb_pp16.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF17, this.tb_pf17.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP17, this.tb_pp17.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF18, this.tb_pf18.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP18, this.tb_pp18.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF19, this.tb_pf19.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP19, this.tb_pp19.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF20, this.tb_pf20.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP20, this.tb_pp20.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF21, this.tb_pf21.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP21, this.tb_pp21.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF22, this.tb_pf22.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP22, this.tb_pp22.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF23, this.tb_pf23.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP23, this.tb_pp23.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF24, this.tb_pf24.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP24, this.tb_pp24.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF25, this.tb_pf25.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP25, this.tb_pp25.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF26, this.tb_pf26.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP26, this.tb_pp26.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF27, this.tb_pf27.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP27, this.tb_pp27.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF28, this.tb_pf28.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP28, this.tb_pp28.Text.Trim(), configPath);

                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF29, this.tb_pf29.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP29, this.tb_pp29.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF30, this.tb_pf30.Text.Trim(), configPath);
                INIFile.SetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP30, this.tb_pp30.Text.Trim(), configPath);
                #endregion
            });

            MessageBox.Show("保存成功！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private async void ImportLastConfig()
        {
            await Task.Run(() =>
            {
                if (sendCommand == null)
                    return;
                #region voltage
                sendCommand.SendVoltageParam(1, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V1, configPath));
                sendCommand.SendVoltageParam(2, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V2, configPath));
                sendCommand.SendVoltageParam(3, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V3, configPath));
                sendCommand.SendVoltageParam(4, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V4, configPath));
                sendCommand.SendVoltageParam(5, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V5, configPath));
                sendCommand.SendVoltageParam(6, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V6, configPath));
                sendCommand.SendVoltageParam(7, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V7, configPath));
                sendCommand.SendVoltageParam(8, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V8, configPath));
                sendCommand.SendVoltageParam(9, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V9, configPath));
                sendCommand.SendVoltageParam(10, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V10, configPath));
                sendCommand.SendVoltageParam(11, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V11, configPath));
                sendCommand.SendVoltageParam(12, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V12, configPath));
                sendCommand.SendVoltageParam(13, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V13, configPath));
                sendCommand.SendVoltageParam(14, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V14, configPath));
                sendCommand.SendVoltageParam(15, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V15, configPath));
                sendCommand.SendVoltageParam(16, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V16, configPath));
                sendCommand.SendVoltageParam(17, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V17, configPath));
                sendCommand.SendVoltageParam(18, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V18, configPath));
                sendCommand.SendVoltageParam(19, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V19, configPath));
                sendCommand.SendVoltageParam(20, INIFile.GetValue(IniConfig.CONFIG_SECTION_VOLTAGE, IniConfig.CONFIG_PARAM_V20, configPath));
                #endregion

                #region pvm-pf
                sendCommand.SendFrequencyParam(1, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF1, configPath));
                sendCommand.SendFrequencyParam(2, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF2, configPath));
                sendCommand.SendFrequencyParam(3, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF3, configPath));
                sendCommand.SendFrequencyParam(4, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF4, configPath));
                sendCommand.SendFrequencyParam(5, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF5, configPath));
                sendCommand.SendFrequencyParam(6, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF6, configPath));
                sendCommand.SendFrequencyParam(7, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF7, configPath));
                sendCommand.SendFrequencyParam(8, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF8, configPath));
                sendCommand.SendFrequencyParam(9, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF9, configPath));
                sendCommand.SendFrequencyParam(10, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF10, configPath));
                sendCommand.SendFrequencyParam(11, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF11, configPath));
                sendCommand.SendFrequencyParam(12, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF12, configPath));
                sendCommand.SendFrequencyParam(13, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF13, configPath));
                sendCommand.SendFrequencyParam(14, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF14, configPath));
                sendCommand.SendFrequencyParam(15, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF15, configPath));
                sendCommand.SendFrequencyParam(16, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF16, configPath));
                sendCommand.SendFrequencyParam(17, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF17, configPath));
                sendCommand.SendFrequencyParam(18, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF18, configPath));
                sendCommand.SendFrequencyParam(19, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF19, configPath));
                sendCommand.SendFrequencyParam(20, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF20, configPath));
                sendCommand.SendFrequencyParam(21, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF21, configPath));
                sendCommand.SendFrequencyParam(22, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF22, configPath));
                sendCommand.SendFrequencyParam(23, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF23, configPath));
                sendCommand.SendFrequencyParam(24, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF24, configPath));
                sendCommand.SendFrequencyParam(25, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF25, configPath));
                sendCommand.SendFrequencyParam(26, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF26, configPath));
                sendCommand.SendFrequencyParam(27, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF27, configPath));
                sendCommand.SendFrequencyParam(28, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF28, configPath));
                sendCommand.SendFrequencyParam(29, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF29, configPath));
                sendCommand.SendFrequencyParam(30, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PF30, configPath));
                #endregion

                #region pvm-pp
                sendCommand.SendFreqPersentParam(1, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP1, configPath));
                sendCommand.SendFreqPersentParam(2, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP2, configPath));
                sendCommand.SendFreqPersentParam(3, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP3, configPath));
                sendCommand.SendFreqPersentParam(4, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP4, configPath));
                sendCommand.SendFreqPersentParam(5, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP5, configPath));
                sendCommand.SendFreqPersentParam(6, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP6, configPath));
                sendCommand.SendFreqPersentParam(7, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP7, configPath));
                sendCommand.SendFreqPersentParam(8, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP8, configPath));
                sendCommand.SendFreqPersentParam(9, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP9, configPath));
                sendCommand.SendFreqPersentParam(10, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP10, configPath));
                sendCommand.SendFreqPersentParam(11, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP11, configPath));
                sendCommand.SendFreqPersentParam(12, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP12, configPath));
                sendCommand.SendFreqPersentParam(13, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP13, configPath));
                sendCommand.SendFreqPersentParam(14, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP14, configPath));
                sendCommand.SendFreqPersentParam(15, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP15, configPath));
                sendCommand.SendFreqPersentParam(16, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP16, configPath));
                sendCommand.SendFreqPersentParam(17, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP17, configPath));
                sendCommand.SendFreqPersentParam(18, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP18, configPath));
                sendCommand.SendFreqPersentParam(19, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP19, configPath));
                sendCommand.SendFreqPersentParam(20, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP20, configPath));
                sendCommand.SendFreqPersentParam(21, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP21, configPath));
                sendCommand.SendFreqPersentParam(22, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP22, configPath));
                sendCommand.SendFreqPersentParam(23, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP23, configPath));
                sendCommand.SendFreqPersentParam(24, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP24, configPath));
                sendCommand.SendFreqPersentParam(25, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP25, configPath));
                sendCommand.SendFreqPersentParam(26, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP26, configPath));
                sendCommand.SendFreqPersentParam(27, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP27, configPath));
                sendCommand.SendFreqPersentParam(28, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP28, configPath));
                sendCommand.SendFreqPersentParam(29, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP29, configPath));
                sendCommand.SendFreqPersentParam(30, INIFile.GetValue(IniConfig.CONFIG_SECTION_PVM, IniConfig.CONFIG_PARAM_PP30, configPath));
                #endregion
            });
        }

        private void InitAutoSendParam(bool IsReset)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "config\\lbs.ini";
            if (!IsReset)
            {
                if (File.Exists(path))
                    return;
            }
            var explainVG = "       ;参数格式：(电压,延时时间)，单位：秒";
            var explainPW = "       ;参数格式：(频率,占空比,延时时间)，单位：秒";

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                {
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_V1}=(0,1){explainVG}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_V2}=(0,1){explainVG}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_V3}=(0,1){explainVG}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_V4}=(0,1){explainVG}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_V5}=(0,1){explainVG}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_V6}=(0,1){explainVG}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_V7}=(0,1){explainVG}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_V8}=(0,1){explainVG}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_V9}=(0,1){explainVG}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_V10}=(0,1){explainVG}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_V11}=(0,1){explainVG}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_V12}=(0,1){explainVG}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_V13}=(0,1){explainVG}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_V14}=(0,1){explainVG}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_V15}=(0,1){explainVG}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_V16}=(0,1){explainVG}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_V17}=(0,1){explainVG}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_V18}=(0,1){explainVG}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_V19}=(0,1){explainVG}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_V20}=(0,1){explainVG}");

                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP1}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP2}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP3}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP4}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP5}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP6}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP7}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP8}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP9}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP10}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP11}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP12}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP13}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP14}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP15}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP16}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP17}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP18}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP19}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP20}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP21}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP22}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP23}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP24}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP25}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP26}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP27}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP28}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP29}=(0,0,1){explainPW}");
                    sw.WriteLine($"{IniConfig.CONFIG_PARAM_PF_PP30}=(0,0,1){explainPW}");
                }
            }
        }
    }
}
