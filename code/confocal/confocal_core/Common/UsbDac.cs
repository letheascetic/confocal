using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace confocal_core.Common
{
    public class UsbDac
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("USB_DAC_V40X64.dll")]
        static extern int OpenUsbV40();
        [DllImport("USB_DAC_V40X64.dll")]
        static extern int CloseUsbV40();
        [DllImport("USB_DAC_V40X64.dll")]
        static extern int ResetUsbV40(int dev);
        [DllImport("USB_DAC_V40X64.dll")]
        static extern int GetDeviceCountV40();
        [DllImport("USB_DAC_V40X64.dll")]
        static extern int senddataV40(int dev, int cs, uint data, int update);
        [DllImport("USB_DAC_V40X64.dll")]
        static extern int readdataV40(int dev, int cs, uint address, ref uint data);
        [DllImport("USB_DAC_V40X64.dll")]
        static extern int SetDac_RegisterV40(int dev, int cs, uint register);
        [DllImport("USB_DAC_V40X64.dll")]
        static extern int SetDac_AllOutV40(int dev, int cs, short value);
        [DllImport("USB_DAC_V40X64.dll")]
        static extern int SetDac_OutV40(int dev, uint ch, short value, uint update);
        [DllImport("USB_DAC_V40X64.dll")]
        static extern int SetDac_ZeroCalibrationV40(int dev, uint ch, short zero, uint update);
        [DllImport("USB_DAC_V40X64.dll")]
        static extern int SetDac_GainCalibrationV40(int dev, uint ch, ushort gain, uint update);
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        // private static readonly int USB_DAC_CHANNEL_NUM_TOTAL = 16;
        private static readonly int USB_DAC_CHANNEL_NUM_USED = 4;
        private static readonly uint USB_DAC_REGISTER_VALUE_DEFAULT = 0x8000;
        private static readonly ushort USB_DAC_GAIN_VALUE_DEFAULT = 32768;
        private static readonly float USB_DAC_VOUT_VALUE_DEFAULT = 2.5f;
        private static readonly short USB_DAC_ZERO_VALUE_DEFAULT = 0;
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly int API_RETURN_SUCCESS = 0;
        private static bool m_connected;
        private static uint m_register;         // DAC寄存器值，两个DAC共用，配置为相同
        private static ushort[] m_gainList;     // 各通道增益
        private static short[] m_zeroList;      // 各通道零点
        private static float[] m_voutList;      // 各通道输出

        static UsbDac()
        {
            m_connected = false;
            m_register = USB_DAC_REGISTER_VALUE_DEFAULT;
            m_gainList = Enumerable.Repeat<ushort>(USB_DAC_GAIN_VALUE_DEFAULT, USB_DAC_CHANNEL_NUM_USED).ToArray();
            m_zeroList = Enumerable.Repeat<short>(USB_DAC_ZERO_VALUE_DEFAULT, USB_DAC_CHANNEL_NUM_USED).ToArray();
            m_voutList = Enumerable.Repeat<float>(USB_DAC_VOUT_VALUE_DEFAULT, USB_DAC_CHANNEL_NUM_USED).ToArray();
        }

        public static bool IsConnected()
        {
            return m_connected;
        }

        public static API_RETURN_CODE Connect()
        {
            if (m_connected)
            {
                Logger.Info(string.Format("Usb Dac already connected."));
                return API_RETURN_CODE.API_SUCCESS;
            }

            // 打开设备
            if (OpenUsbV40() != API_RETURN_SUCCESS)
            {
                Logger.Info(string.Format("Usb dac connect failed: [{0}].", API_RETURN_CODE.API_FAILED_USB_DAC_OPEN_FAILED));
                return API_RETURN_CODE.API_FAILED_USB_DAC_OPEN_FAILED;
            }

            // 设置寄存器初始值
            // 量程: -15V/15V 不启动零点和满量程校准
            API_RETURN_CODE code = SetDacRegister(0, m_register);
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                return code;
            }
            code = SetDacRegister(1, m_register);
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                return code;
            }

            // 设置被使用通道的零点 & 增益[不开启，不需要设置]
            //for (int i = 0; i < USB_DAC_CHANNEL_NUM_USED; i++)
            //{
            //    code |= SetZeroCalibration((uint)i, m_zeroList[i]);
            //    code |= SetGainCalibration((uint)i, m_gainList[i]);
            //}
            //if (code != API_RETURN_CODE.API_SUCCESS)
            //{
            //    return code;
            //}

            // 设置各通道电压输出值
            for (int i = 0; i < USB_DAC_CHANNEL_NUM_USED; i++)
            {
                short value = 0;
                VoutToWriteValue((uint)i, m_voutList[i], ref value);
                code |= SetDacOut((uint)i, value);
            }
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                return code;
            }

            m_connected = true;
            Logger.Info(string.Format("Usb dac connect success: [{0}].", API_RETURN_CODE.API_SUCCESS));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public static API_RETURN_CODE Release()
        {
            if (!m_connected)
            {
                Logger.Info(string.Format("Usb Dac already released."));
                return API_RETURN_CODE.API_SUCCESS;
            }

            if (CloseUsbV40() == API_RETURN_SUCCESS)
            {
                m_connected = false;
                Logger.Info(string.Format("Usb dac release success: [{0}].", API_RETURN_CODE.API_SUCCESS));
                return API_RETURN_CODE.API_SUCCESS;
            }

            Logger.Info(string.Format("Usb dac release failed: [{0}].", API_RETURN_CODE.API_FAILED_USB_DAC_OPEN_FAILED));
            return API_RETURN_CODE.API_FAILED_USB_DAC_OPEN_FAILED;
        }

        public static int GetDeviceCount()
        {
            int count = GetDeviceCountV40();
            Logger.Info(string.Format("Usb Dac device count: [{0}].", count));
            return count;
        }

        /// <summary>
        /// 写DAC指定寄存器
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static API_RETURN_CODE WriteConfig(int cs, uint data)
        {
            if (senddataV40(0, cs, data, 1) == API_RETURN_SUCCESS)
            {
                Logger.Info(string.Format("Usb dac[{0}] write config[{1}] success: [{2}].", cs, data, API_RETURN_CODE.API_SUCCESS));
                return API_RETURN_CODE.API_SUCCESS;
            }

            Logger.Info(string.Format("Usb dac[{0}] write config[{1}] failed: [{2}].", cs, data, API_RETURN_CODE.API_FAILED_USB_DAC_WRITE_CONFIG_FAILED));
            return API_RETURN_CODE.API_FAILED_USB_DAC_WRITE_CONFIG_FAILED;
        }

        /// <summary>
        /// 读DAC寄存器
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="addr"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static API_RETURN_CODE ReadConfig(int cs, uint addr, ref uint data)
        {
            if (readdataV40(0, cs, addr, ref data) == API_RETURN_SUCCESS)
            {
                Logger.Info(string.Format("Usb dac[{0}] read config[{1}] success: [{2}].", cs, data, API_RETURN_CODE.API_SUCCESS));
                return API_RETURN_CODE.API_SUCCESS;
            }
            Logger.Info(string.Format("Usb dac[{0}] read config[{1}] failed: [{2}].", cs, data, API_RETURN_CODE.API_FAILED_USB_DAC_READ_CONFIG_FAILED));
            return API_RETURN_CODE.API_FAILED_USB_DAC_READ_CONFIG_FAILED;
        }

        /// <summary>
        /// 写寄存器
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="register"></param>
        /// <returns></returns>
        public static API_RETURN_CODE SetDacRegister(int cs, uint register)
        {
            if (SetDac_RegisterV40(0, cs, register) == API_RETURN_SUCCESS)
            {
                m_register = register;      // 写寄存器成功，才赋值存储
                Logger.Info(string.Format("Usb dac[{0}] set register[{1}] success: [{2}].", cs, register, API_RETURN_CODE.API_SUCCESS));
                return API_RETURN_CODE.API_SUCCESS;
            }
            Logger.Info(string.Format("Usb dac[{0}] set register[{1}] failed: [{2}].", cs, register, API_RETURN_CODE.API_FAILED_USB_DAC_SET_REGISTER_FAILED));
            return API_RETURN_CODE.API_FAILED_USB_DAC_SET_REGISTER_FAILED;
        }

        /// <summary>
        /// 设置所有通道的输出值
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static API_RETURN_CODE SetDacAllOut(int cs, short value)
        {
            if (SetDac_AllOutV40(0, cs, value) == API_RETURN_SUCCESS)
            {
                Logger.Info(string.Format("Usb dac set all out[{0}] success: [{1}].", value, API_RETURN_CODE.API_SUCCESS));
                return API_RETURN_CODE.API_SUCCESS;
            }

            Logger.Info(string.Format("Usb dac set all out[{0}] failed: [{1}].", value, API_RETURN_CODE.API_FAILED_USB_DAC_SET_ALL_OUT_FAILED));
            return API_RETURN_CODE.API_FAILED_USB_DAC_SET_ALL_OUT_FAILED;
        }

        /// <summary>
        /// 设置单个通道的输出值
        /// </summary>
        /// <param name="ch">通道</param>
        /// <param name="value">输出值</param>
        /// <returns></returns>
        public static API_RETURN_CODE SetDacOut(uint ch, short value)
        {
            if (SetDac_OutV40(0, ch, value, 1) == API_RETURN_SUCCESS)
            {
                Logger.Info(string.Format("Usb dac set channel[{0}] out[{1}] success: [{2}].", ch, value, API_RETURN_CODE.API_SUCCESS));
                return API_RETURN_CODE.API_SUCCESS;
            }

            Logger.Info(string.Format("Usb dac set channel[{0}] out[{1}] failed: [{2}].", ch, value, API_RETURN_CODE.API_FAILED_USB_DAC_SET_CHANNEL_OUT_FAILED));
            return API_RETURN_CODE.API_FAILED_USB_DAC_SET_CHANNEL_OUT_FAILED;
        }

        public static API_RETURN_CODE SetDacOut(uint ch, float vout)
        {
            short value = 0;
            VoutToWriteValue(ch, vout, ref value);
            if (SetDac_OutV40(0, ch, value, 1) == API_RETURN_SUCCESS)
            {
                Logger.Info(string.Format("Usb dac set channel[{0}] out[{1}] success: [{2}].", ch, vout, API_RETURN_CODE.API_SUCCESS));
                return API_RETURN_CODE.API_SUCCESS;
            }

            Logger.Info(string.Format("Usb dac set channel[{0}] out[{1}] failed: [{2}].", ch, vout, API_RETURN_CODE.API_FAILED_USB_DAC_SET_CHANNEL_OUT_FAILED));
            return API_RETURN_CODE.API_FAILED_USB_DAC_SET_CHANNEL_OUT_FAILED;
        }

        /// <summary>
        /// 设置指定通道的零点值
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="zeroValue"></param>
        /// <returns></returns>
        public static API_RETURN_CODE SetZeroCalibration(uint ch, short zeroValue)
        {
            if (SetDac_ZeroCalibrationV40(0, ch, zeroValue, 1) == API_RETURN_SUCCESS)
            {
                Logger.Info(string.Format("Usb dac[{0}] set zero calibration[{1}] success: [{2}].", ch, zeroValue, API_RETURN_CODE.API_SUCCESS));
                return API_RETURN_CODE.API_SUCCESS;
            }

            Logger.Info(string.Format("Usb dac[{0}] set zero calibration[{1}] failed: [{2}].",
                ch, zeroValue, API_RETURN_CODE.API_FAILED_USB_DAC_SET_ZERO_CALIBRATION_FAILED));
            return API_RETURN_CODE.API_FAILED_USB_DAC_SET_ZERO_CALIBRATION_FAILED;
        }

        /// <summary>
        /// 设置指定通道的增益
        /// </summary>
        /// <param name="ch">通道</param>
        /// <param name="gain">增益</param>
        /// <returns></returns>
        public static API_RETURN_CODE SetGainCalibration(uint ch, ushort gain)
        {
            if (SetDac_GainCalibrationV40(0, ch, gain, 1) == API_RETURN_SUCCESS)
            {
                m_gainList[ch] = gain;
                Logger.Info(string.Format("Usb dac[{0}] set gain calibration[{1}] success: [{2}].", ch, gain, API_RETURN_CODE.API_SUCCESS));
                return API_RETURN_CODE.API_SUCCESS;
            }

            Logger.Info(string.Format("Usb dac[{0}] set gain calibration[{1}] failed: [{2}].",
                ch, gain, API_RETURN_CODE.API_FAILED_USB_DAC_SET_GAIN_CALIBRATION_FAILED));
            return API_RETURN_CODE.API_FAILED_USB_DAC_SET_GAIN_CALIBRATION_FAILED;
        }

        /// <summary>
        /// 输出值与写入值转换
        /// Vout=VScale* DAC_DATA/32768;
        /// DAC_DATA= value*(GAIN+32767)/65536+ Zero;
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="vout"></param>
        /// <param name="value"></param>
        public static void VoutToWriteValue(uint ch, float vout, ref short value)
        {
            uint gainFlag = (m_register >> 7) & 0x03;
            float voutScale = gainFlag == 0x00 ? 15.0f : 10.0f;
            value = (short)(vout / voutScale * 32768);
        }

        /// <summary>
        /// 将Config中配置的增益值（configValue）转换成实际写入USBDAC模块的增益值
        /// </summary>
        /// <param name="configValue">0.1%-100.0%</param>
        /// <returns></returns>
        public static ushort ConfigValueToGain(float configValue)
        {
            return (ushort)(ushort.MaxValue / 100.0f * configValue);
        }

        /// <summary>
        /// 将USBDAC中的增益值转换成Config中的配置值
        /// </summary>
        /// <param name="gain">1-65536</param>
        /// <returns></returns>
        public static float GainToConfigValue(ushort gain)
        {
            return 100.0f * gain / ushort.MaxValue;
        }

        public static float ConfigValueToVout(float configValue)
        {
            return 5.0f * configValue / 100;
        }

        public static float VoutToConfigValue(float vout)
        {
            return 100.0f * vout / 5.0f;
        }

    }
}
