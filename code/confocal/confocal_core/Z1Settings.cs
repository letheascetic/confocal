using log4net;
using NationalInstruments.DAQmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core
{
    /// <summary>
    /// 采集设备：PMT or APD
    /// </summary>
    public enum ACQ_DEVICE
    {
        PMT = 0x00,
        APD = 0x01
    }

    /// <summary>
    /// 采集卡
    /// </summary>
    public enum ACQ_BOARD
    {
        NIUSB6363 = 0x00,
        NIPCIE6363 = 0x01,
        ATS9440 = 0x02
    }

    public class Z1Settings
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        private volatile static Z1Settings pSettings = null;
        private static readonly object locker = new object();
        ///////////////////////////////////////////////////////////////////////////////////////////
        private ACQ_BOARD m_acqBoard;
        private ACQ_DEVICE m_acqDevice;
        ///////////////////////////////////////////////////////////////////////////////////////////
        private string m_xGalvoAoChannel;   // X振镜控制电压 - AO输出
        private string m_yGalvoAoChannel;   // Y振镜控制电压 - AO输出
        private string m_y2GalvoAoChannel;  // Y补偿镜控制电压 - AO输出

        private string m_acqTriggerDoLine;      // 采集触发输出信号[行触发]，APD和PMT模式共用 - DO输出
        private string m_acqStartSyncSignal;    // 采集启动同步信号[Start Trigger]，使AO、DO同时工作

        private string[] m_pmtAiChannels;   // PMT输出电压信号 - AI输入
        private string m_pmtTriggerInPfi;   // PMT采集触发输入信号 - PFI

        private string[] m_apdCiChannels;   // APD脉冲信号 - CI计数[CI计数器是虚拟的，需要指定具体的PFI端口]
        private string[] m_apdCiSrcPfis;    // APD脉冲计数器[CI]使用的PFI端口
        private string m_apdTriggerInPfi;   // APD时钟信号（作为触发信号）使用的PFI端口，一个时钟完成一次计数
        ///////////////////////////////////////////////////////////////////////////////////////////
        public static string[] GetDeviceNames()
        {
            return DaqSystem.Local.Devices;
        }

        public static string GetDeviceType(string device)
        {
            return DaqSystem.Local.LoadDevice(device).ProductType;
        }

        public static string[] GetAoChannels()
        {
            return DaqSystem.Local.GetPhysicalChannels(PhysicalChannelTypes.AO, PhysicalChannelAccess.External);
        }

        public static string[] GetAiChannels()
        {
            return DaqSystem.Local.GetPhysicalChannels(PhysicalChannelTypes.AI, PhysicalChannelAccess.External);
        }

        public static string[] GetDoLines()
        {
            return DaqSystem.Local.GetPhysicalChannels(PhysicalChannelTypes.DOLine, PhysicalChannelAccess.External);
        }

        public static string[] GetCiChannels()
        {
            return DaqSystem.Local.GetPhysicalChannels(PhysicalChannelTypes.CI, PhysicalChannelAccess.External);
        }

        public static string[] GetPFIs()
        {
            string[] terminals = DaqSystem.Local.GetTerminals(TerminalTypes.Basic);
            List<string> pfis = new List<string>();

            foreach (string terminal in terminals)
            {
                if (terminal.Contains("/PFI"))
                {
                    pfis.Add(terminal);
                }
            }
            return pfis.ToArray();
        }

        public static string[] GetStartSyncSignals()
        {
            string[] terminals = DaqSystem.Local.GetTerminals(TerminalTypes.Basic);
            List<string> pfis = new List<string>();

            foreach (string terminal in terminals)
            {
                if (terminal.Contains("/StartTrigger"))
                {
                    pfis.Add(terminal);
                }
            }
            return pfis.ToArray();
        }

        public static Z1Settings GetSettings()
        {
            if (pSettings == null)
            {
                lock (locker)
                {
                    if (pSettings == null)
                    {
                        pSettings = new Z1Settings();
                    }
                }
            }
            return pSettings;
        }

        public ACQ_DEVICE GetAcqDevice()
        {
            return m_acqDevice;
        }

        public API_RETURN_CODE SetAcqDevice(ACQ_DEVICE acqDevice)
        {
            Logger.Info(string.Format("set acq device: [{0}].", acqDevice));
            m_acqDevice = acqDevice;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public ACQ_BOARD GetAcqBoard()
        {
            return m_acqBoard;
        }

        public API_RETURN_CODE SetAcqBoard(ACQ_BOARD acqBoard)
        {
            Logger.Info(string.Format("set acq board: [{0}].", acqBoard));
            m_acqBoard = acqBoard;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public string GetXGalvoAoChannel()
        {
            return m_xGalvoAoChannel;
        }

        public API_RETURN_CODE SetXGalvoAoChannel(string aoChannel)
        {
            m_xGalvoAoChannel = aoChannel;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public string GetYGalvoAoChannel()
        {
            return m_yGalvoAoChannel;
        }

        public API_RETURN_CODE SetYGalvoAoChannel(string aoChannel)
        {
            m_yGalvoAoChannel = aoChannel;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public string GetY2GalvoAoChannel()
        {
            return m_y2GalvoAoChannel;
        }

        public API_RETURN_CODE SetY2GalvoAoChannel(string aoChannel)
        {
            m_y2GalvoAoChannel = aoChannel;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public string GetAcqTriggerDoLine()
        {
            return m_acqTriggerDoLine;
        }

        public API_RETURN_CODE SetAcqTriggerDoLine(string doLine)
        {
            m_acqTriggerDoLine = doLine;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public string GetPmtAiChannel(CHAN_ID id)
        {
            return m_pmtAiChannels[(int)id];
        }

        public API_RETURN_CODE SetPmtAiChannel(CHAN_ID id, string aiChannel)
        {
            m_pmtAiChannels[(int)id] = aiChannel;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public string GetPmtTriggerInPfi()
        {
            return m_pmtTriggerInPfi;
        }

        public API_RETURN_CODE SetPmtTriggerInPfi(string pfi)
        {
            m_pmtTriggerInPfi = pfi;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public string GetApdCiChannel(CHAN_ID id)
        {
            return m_apdCiChannels[(int)id];
        }

        public API_RETURN_CODE SetApdCiChannel(CHAN_ID id, string ciChannel)
        {
            m_apdCiChannels[(int)id] = ciChannel;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public string GetApdCiSrcPfi(CHAN_ID id)
        {
            return m_apdCiSrcPfis[(int)id];
        }

        public API_RETURN_CODE SetApdCiSrcPfi(CHAN_ID id, string pfi)
        {
            m_apdCiSrcPfis[(int)id] = pfi;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public string GetApdTriggerInPfi()
        {
            return m_apdTriggerInPfi;
        }

        public API_RETURN_CODE SetApdTriggerInPfi(string pfi)
        {
            m_apdTriggerInPfi = pfi;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public string GetStartSyncSignal()
        {
            return m_acqStartSyncSignal;
        }

        public API_RETURN_CODE SetStartSyncSignal(string startSyncSignal)
        {
            m_acqStartSyncSignal = startSyncSignal;
            return API_RETURN_CODE.API_SUCCESS;
        }

        private Z1Settings()
        {
            m_acqBoard = ACQ_BOARD.NIUSB6363;
            m_acqDevice = ACQ_DEVICE.PMT;

            string[] devices = GetDeviceNames();
            string deviceName = devices.Length > 0 ? devices[0] : "Dev1";

            string[] aoChannels = GetAoChannels();
            m_xGalvoAoChannel = string.Concat(deviceName, "/ao0");
            m_yGalvoAoChannel = string.Concat(deviceName, "/ao1");
            m_y2GalvoAoChannel = string.Concat(deviceName, "/ao2");

            m_acqTriggerDoLine = string.Concat(deviceName, "/port0/line0");
            m_acqStartSyncSignal = string.Concat("/", deviceName, "/ao/StartTrigger");

            string[] aiChannels = GetAiChannels();
            m_pmtAiChannels = new string[] {
                string.Concat(deviceName, "/ai0"),
                string.Concat(deviceName, "/ai1"),
                string.Concat(deviceName, "/ai2"),
                string.Concat(deviceName, "/ai3"),
            };

            m_pmtTriggerInPfi = string.Concat("/", deviceName, "/PFI10");

            string[] ciChannels = GetCiChannels();
            m_apdCiChannels = new string[] {
                string.Concat(deviceName, "/ctr0"),
                string.Concat(deviceName, "/ctr1"),
                string.Concat(deviceName, "/ctr2"),
                string.Concat(deviceName, "/ctr3"),
            };

            m_apdCiSrcPfis = new string[] {
                string.Concat("/", deviceName, "/PFI8"),
                string.Concat("/", deviceName, "/PFI3"),
                string.Concat("/", deviceName, "/PFI0"),
                string.Concat("/", deviceName, "/PFI5"),
            };

            m_apdTriggerInPfi = string.Concat("/", deviceName, "/PFI9");
        }
    }
}
