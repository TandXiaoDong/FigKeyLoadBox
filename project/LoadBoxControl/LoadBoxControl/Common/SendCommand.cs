using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtils.Logger;
using System.IO.Ports;
using System.Windows.Forms;
using LoadBoxControl.Model;

namespace LoadBoxControl.Common
{
    public class SendCommand
    {
        #region 上位机发送参数常量
        private const string PAGE_DAC_VOLTAGE_BEFORE = "page pageDAC";
        private const string PAGE_DAC_VOLTAGE_BACK = "pageDAC";
        private const string PAGE_DAC_PWM_BEFORE = "page pagePWM";
        private const string PAGE_DAC_PWM_BACK = "pagePWM";
        private const string FF_END = "FF FF FF";
        #endregion

        private SerialPort serialPort;

        public SendCommand(SerialPort serialPort)
        {
            this.serialPort = serialPort;
        }

        public byte[] SendVoltageString(int index, double value)
        {
            var part1 = "";
            var part2 = "";
            var part3 = "";
            var part4 = "";
            if (index <= 10)
            {
                part1 = PAGE_DAC_VOLTAGE_BEFORE;
                part2 = FF_END;
                part3 = PAGE_DAC_VOLTAGE_BACK + ".x" + (index - 1) + ".val=" + value * 10;
                part4 = FF_END;
            }
            else
            {
                part1 = PAGE_DAC_VOLTAGE_BEFORE + "1";
                part2 = FF_END;
                part3 = PAGE_DAC_VOLTAGE_BACK + "1" + ".x" + (index - 1 - 10) + ".val=" + value * 10;
                part4 = FF_END;
            }
            return ConvertSendByte(part1, part2, part3, part4);
        }

        /// <summary>
        /// PWM-频率
        /// </summary>
        /// <param name="index">当前传入的序号，起始位置为1</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public byte[] SendPwmFreqString(int index, double value)
        {
            var part1 = "";
            var part2 = "";
            var part3 = "";
            var part4 = "";
            if (index <= 10 && index > 0)
            {
                part1 = PAGE_DAC_PWM_BEFORE;
                part2 = FF_END;
                part3 = PAGE_DAC_PWM_BACK + ".n" + (index + 10 - 1) + ".val=" + value;
                part4 = FF_END;
            }
            else if (index <= 20 && index > 10)
            {
                part1 = PAGE_DAC_PWM_BEFORE + "1";
                part2 = FF_END;
                part3 = PAGE_DAC_PWM_BACK + "1" + ".n" + (index - 1) + ".val=" + value;
                part4 = FF_END;
            }
            else if (index <= 30 && index > 20)
            {
                part1 = PAGE_DAC_PWM_BEFORE + "2";
                part2 = FF_END;
                part3 = PAGE_DAC_PWM_BACK + "2" + ".n" + (index - 10 - 1) + ".val=" + value;
                part4 = FF_END;
            }
            return ConvertSendByte(part1, part2, part3, part4);
        }

        /// <summary>
        /// PVM-频率占空比
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public byte[] SendPwmFreqPersentString(int index, double value)
        {
            var part1 = "";
            var part2 = "";
            var part3 = "";
            var part4 = "";
            if (index <= 10 && index > 0)
            {
                part1 = PAGE_DAC_PWM_BEFORE;
                part2 = FF_END;
                part3 = PAGE_DAC_PWM_BACK + ".n" + (index - 1) + ".val=" + value;
                part4 = FF_END;
            }
            else if (index <= 20 && index > 10)
            {
                part1 = PAGE_DAC_PWM_BEFORE + "1";
                part2 = FF_END;
                part3 = PAGE_DAC_PWM_BACK + "1" + ".n" + (index - 10 - 1) + ".val=" + value;
                part4 = FF_END;
            }
            else if (index <= 30 && index > 20)
            {
                part1 = PAGE_DAC_PWM_BEFORE + "2";
                part2 = FF_END;
                part3 = PAGE_DAC_PWM_BACK + "2" + ".n" + (index - 20 - 1) + ".val=" + value;
                part4 = FF_END;
            }
            return ConvertSendByte(part1, part2, part3, part4);
        }

        private byte[] ConvertSendByte(string part1String, string hexString1, string part2String, string hexString2)
        {
            var part1Byte = CharToByte(part1String);
            var part2Byte = CharToByte(part2String);
            var joinPart1Byte = JoinToByte(part1Byte, hexString1);
            byte[] bothPart1AndPart2 = new byte[joinPart1Byte.Length + part2Byte.Length];
            joinPart1Byte.CopyTo(bothPart1AndPart2, 0);
            Array.Copy(part2Byte, 0, bothPart1AndPart2, joinPart1Byte.Length, part2Byte.Length);
            var joinPart2Byte = JoinToByte(bothPart1AndPart2, hexString2);
            return joinPart2Byte;
        }

        public SendResult SendVoltageParam(int startIndex, string voltage)
        {
            SendResult sendResult = new SendResult();
            double value = 0;
            double.TryParse(voltage, out value);
            var sendByte = SendVoltageString(startIndex, value);
            LogHelper.Log.Info($"【电压】index={startIndex} " + BitConverter.ToString(sendByte));
            sendResult.sendString = BitConverter.ToString(sendByte);
            if (SendDevConfigMsg(sendByte))
            {
                sendResult.IsSendSuccess = true;
            }
            else
                sendResult.IsSendSuccess = false;
            return sendResult;
        }

        public SendResult SendFrequencyParam(int startIndex, string freq)
        {
            SendResult sendResult = new SendResult();
            double value = 0;
            double.TryParse(freq, out value);
            var sendByte = SendPwmFreqString(startIndex, value);
            LogHelper.Log.Info($"【频率】index={startIndex} " + BitConverter.ToString(sendByte));
            sendResult.sendString = BitConverter.ToString(sendByte);
            if (SendDevConfigMsg(sendByte))
            {
                sendResult.IsSendSuccess = true;
            }
            else
                sendResult.IsSendSuccess = false;
            return sendResult;
        }

        public SendResult SendFreqPersentParam(int startIndex, string freqPersent)
        {
            SendResult sendResult = new SendResult();
            double value = 0;
            double.TryParse(freqPersent, out value);
            var sendByte = SendPwmFreqPersentString(startIndex, value);
            LogHelper.Log.Info($"【占空比】index={startIndex} " + BitConverter.ToString(sendByte));
            sendResult.sendString = BitConverter.ToString(sendByte);
            if (SendDevConfigMsg(sendByte))
            {
                sendResult.IsSendSuccess = true;
            }
            else
                sendResult.IsSendSuccess = false;
            return sendResult;
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="sendContent"></param>
        public bool SendDevConfigMsg(byte[] sendContent)
        {
            ///发送hex格式 
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Write(sendContent, 0, sendContent.Length);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误");
                return false;
            }
        }

        private byte[] CharToByte(string inputString)
        {
            char[] strArray = inputString.Replace(" ", "").ToCharArray();
            byte[] btArray = new byte[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                btArray[i] = Convert.ToByte(strArray[i]);
            }
            return btArray;
        }

        /// <summary>
        /// byte连接16进制字符串，以空格隔开
        /// </summary>
        /// <param name="sourchByte"></param>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private byte[] JoinToByte(byte[] sourchByte, string hexString)
        {
            string[] stringArray = hexString.Split(' ');
            byte[] unionByte = new byte[sourchByte.Length + stringArray.Length];
            byte[] hexByte = new byte[stringArray.Length];
            for (int i = 0; i < stringArray.Length; i++)
            {
                hexByte[i] = Convert.ToByte(stringArray[i], 16);
            }
            sourchByte.CopyTo(unionByte, 0);
            Array.Copy(hexByte, 0, unionByte, sourchByte.Length, hexByte.Length);
            return unionByte;
        }
    }
}
