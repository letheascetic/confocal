using log4net;
using NationalInstruments.DAQmx;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace confocal_core
{
    public enum API_RETURN_CODE : int
    {
        API_SUCCESS = 0x00000000,
        API_FAILED = 0x00000001,
        /* error for config */
        API_FAILED_CONFIG_HANDLE_INVALID = 0x00000100,
        API_FAILED_CONFIG_SET_LASER_SWITCH_FAILED,
        API_FAILED_CONFIG_SET_LASER_POWER_FAILED,
        /* error for ats9440 */
        API_FAILED_ATS_HANDLE_INVALID = 0x00000200,
        API_FAILED_ATS_GET_BOARD_VERSION_FAILED,
        API_FAILED_ATS_GET_SDK_VERSION_FAILED,
        API_FIALED_ATS_GET_DRIVER_VERSION_FAILED,
        API_FIALED_ATS_GET_CPLD_VERSION_FAILED,
        API_FAILED_ATS_INFO_INSTANCE_INVALID,
        API_FAILED_ATS_SET_CLOCK_FAILED,
        API_FAILED_ATS_SET_INPUT_CHANNEL_FAILED,
        API_FAILED_ATS_SET_TRIGGER_OPERATION_FAILED,
        API_FAILED_ATS_SET_EXTERNAL_TRIGGER_FAILED,
        API_FAILED_ATS_SET_TRIGGER_TIMEOUT_FAILED,
        API_FAILED_ATS_SET_TRIGGER_DELAY_FAILED,
        API_FAILED_ATS_SET_AUX_IO_FAILED,
        API_FAILED_ATS_GET_CHANNEL_INFO_FAILED,
        API_FAILED_ATS_MALLOC_BUFFER_FAILED,
        API_FAILED_ATS_SET_RECORD_SIZE_FAILED,
        API_FAILED_ATS_CONFIG_ASYNC_READ_FAILED,
        API_FAILED_ATS_START_CAPTURE_FAILED,
        API_FAILED_ATS_ENABLE_CRS_FAILED,
        API_FAILED_ATS_ABORT_ASYNC_READ_FAILED,
        API_FAILED_ATS_FORCE_TRIGGER_ENABLE_FAILED,
        /* error for laser */
        API_FAILED_LASER_LOAD_DLL_FAILED = 0x00000400,
        API_FAILED_LASER_CONNECT_FAILED,
        API_FAILED_LASER_RELEASE_FAILED,
        API_FAILED_LASER_OPEN_CHANNEL_FAILED,
        API_FAILED_LASER_CLOSE_CHANNEL_FAILED,
        API_FAILED_LASER_SET_POWER_FAILED,
        API_FAILED_LASER_UP_POWER_FAILED,
        API_FAILED_LASER_DOWN_POWER_FAILED,
        /* error for ni card */
        API_FAILED_NI_CONFIG_AO_TASK_EXCEPTION = 0x00000800,
        API_FAILED_NI_CONFIG_DO_TASK_EXCEPTION,
        API_FAILED_NI_CONFIG_AI_TASK_EXCEPTION,
        API_FAILED_NI_CONFIG_CI_TASK_EXCEPTION,
        API_FAILED_NI_NO_AI_CHANNEL_ACTIVATED,
        API_FAILED_NI_START_TASK_EXCEPTION,
        /* error for scan task */
        API_FAILED_SCAN_TASK_INVALID = 0x00001000,
        API_FAILED_SCAN_TASK_NOT_FOUND,
        API_FAILED_SCAN_TASK_START_FAILED,
        API_FAILED_SCAN_TASK_STOP_FAILED,
        /* error for usb dac */
        API_FAILED_USB_DAC_OPEN_FAILED = 0x00002000,
        API_FAILED_USB_DAC_RELEASE_FAILED,
        API_FAILED_USB_DAC_SET_ALL_OUT_FAILED,
        API_FAILED_USB_DAC_SET_CHANNEL_OUT_FAILED,
        API_FAILED_USB_DAC_SET_ZERO_CALIBRATION_FAILED,
        API_FAILED_USB_DAC_SET_GAIN_CALIBRATION_FAILED,
        API_FAILED_USB_DAC_WRITE_CONFIG_FAILED,
        API_FAILED_USB_DAC_READ_CONFIG_FAILED,
        API_FAILED_USB_DAC_SET_REGISTER_FAILED
    }

    public enum CHAN_ID : int
    {
        WAVELENGTH_405_NM = 0,
        WAVELENGTH_488_NM = 1,
        WAVELENGTH_561_NM = 2,
        WAVELENGTH_640_NM = 3
    }

    public enum LASER_CHAN_SWITCH : int
    {
        OFF = 0,
        ON = 1
    }

    public enum SCAN_MODE : int
    {
        RESONANT = 0,
        GALVANOMETER = 1
    }

    public enum SCAN_STRATEGY : int
    {
        Z_BIDIRECTION = 0,          // Z行双向
        Z_UNIDIRECTION = 1          // Z行单向
    }

    public enum SCAN_MIRROR_NUM
    {
        TWO = 0,
        THREEE
    };

    public enum SCAN_ACQUISITION_MODE
    {
        STANDARD = 0,
        SUM,
        AVARAGE
    };

    public class PropChannel
    {
        private CHAN_ID id;
        private Color colorReference;       // 各通道显示的颜色基准
        private short backgroundNoiseLevel;   // 背景噪声水平

        public CHAN_ID Id { get { return id; } set { id = value; } }
        public Color ColorReference { get { return colorReference; } set { colorReference = value; } }
        public short BackgroundNoiseLevel { get { return backgroundNoiseLevel; } set { backgroundNoiseLevel = value; } }
    }

    public class Properties
    {
        private PropChannel[] channels;

        public PropChannel[] Channels
        { get { return channels; } set { channels = value; } }
    }

    public class LaserChannel
    {
        private CHAN_ID id;
        private LASER_CHAN_SWITCH status;
        private double power;

        public CHAN_ID Id
        { get { return id; } set{ id = value; } }
        public LASER_CHAN_SWITCH Status
        { get { return status; } set { status = value; } }
        public double Power
        { get { return power; } set { power = value; } }
    }

    public class Laser
    {
        private string portName;
        private LaserChannel[] channels;

        public string PortName
        { get { return portName; } set { portName = value; } }
        public LaserChannel[] Channels
        { get { return channels; } set { channels = value; } }

    }

    public class PmtChannel
    {
        private CHAN_ID id;
        private double gain;

        public CHAN_ID Id
        { get { return id; } set { id = value; } }
        public double Gain
        { get { return gain; } set { gain = value; } }
    }

    public class Pmt
    {
        private PmtChannel[] channels;

        public PmtChannel[] Channels
        { get { return channels; } set { channels = value; } }
    }

    public class Crs
    {
        private double amplitude;

        public double Amplitude
        { get { return amplitude; } set { amplitude = value; } }
    }

    public class PinHole
    {
        private double size;

        public double Size
        { get{ return size; }set { size = value; } }
    }

    public class Scan
    {
        private SCAN_MODE mode;             // 扫描模式, Galv or Res
        private SCAN_STRATEGY strategy;     // 扫描策略, Z形单向/Z形双向
        private SCAN_MIRROR_NUM mirrorNum;  // 三振镜 or 两振镜
        private SCAN_ACQUISITION_MODE auquisitionMode;  // 扫描采集模式[标准|平均|求和]
        private int flag;                   // 扫描功能标志位
        private double galvResponseTime;        // 振镜响应时间,us
        private double fieldSize;               // 视场大小,um
        private double dwellTime;               // 停留时间
        private int xPoints;                    // x扫描像素
        private int yPoints;                    // y扫描像素
        private double calibrationVoltage;      // 校准[标定]电压,V
        private double curveCoff;               // 曲线系数,%
        private int scanPixelComp;              // Z形扫描中补偿的有效像素
        private int scanPixelOffset;            // Z形扫描中有效像素起始位
        private int scanPixelCalibration;       // Z形双向扫描中奇数偶数行错位校准
        private int acquisitionModeNum;         // 扫描采集模式为平均和求和时的平均数|求和数

        public SCAN_MODE Mode
        { get { return mode; } set { mode = value; } }
        public SCAN_STRATEGY Strategy
        { get { return strategy; } set { strategy = value; } }
        public SCAN_MIRROR_NUM MirrorNum
        { get { return mirrorNum; } set { mirrorNum = value; } }
        public SCAN_ACQUISITION_MODE AcquisitionMode
        { get { return auquisitionMode; } set { auquisitionMode = value; } }
        public int Flag
        { get { return flag; } set { flag = value; } }
        public double GalvResponseTime
        { get { return galvResponseTime; } set { galvResponseTime = value; } }
        public double FieldSize
        { get { return fieldSize; } set { fieldSize = value; } }
        public double DwellTime
        { get { return dwellTime; } set { dwellTime = value; } }
        public int XPoints
        { get { return xPoints; } set { xPoints = value; } }
        public int YPoints
        { get{ return yPoints; } set { yPoints = value; } }
        public double CalibrationVoltage
        { get { return calibrationVoltage; }set { calibrationVoltage = value; } }
        public double CurveCoff
        { get { return curveCoff; } set { curveCoff = value; } }
        public int ScanPixelComp
        { get { return scanPixelComp; } set { scanPixelComp = value; } }
        public int ScanPixelOffset
        { get { return scanPixelOffset; } set { scanPixelOffset = value; } }
        public int ScanPixelCalibration
        { get { return scanPixelCalibration; }set { scanPixelCalibration = value; } }
        public int AcquisitionModeNum
        { get { return acquisitionModeNum; } set { acquisitionModeNum = value; } }
    }

    public delegate void ConfigEventHandler(Config config, Object paras);

    public class Config
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        private volatile static Config pConfig = null;
        private static readonly object locker = new object();
        ///////////////////////////////////////////////////////////////////////////////////////////
        //public event ConfigEventHandler ScanChangedEvent;        // scan参数变化事件
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly int CHAN_NUM = 4;
        //private static readonly int LASER_CHAN_NUM = CHAN_NUM;
        //private static readonly int PMT_CHAN_NUM = CHAN_NUM;
        private static readonly double LASER_POWER_DEFAULT = 2.0;
        private static readonly double PMT_GAIN_DEFAULT = 50.0;
        private static readonly double CRS_AMPLITUDE_DEFAULT = 3.3;
        private static readonly int SCAN_POINTS_DEFAULT = 512;
        private static readonly double SCAN_PIXEL_TIME_DEFAULT = 4.0;       // 像素时间, us
        private static readonly double PIN_HOLE_SIZE_DEFAULT = 50;          // 小孔尺寸
        private static readonly double GALV_RESPONSE_TIME_DEFAULT = 200.0;  // 振镜响应时间, us
        private static readonly double FIELD_SIZE_DEFAULT = 200.0;          // 视场大小, um
        private static readonly double CALIBRATION_VOLTAGE_DEFAULT = 4.09855e-5;  // 校准[标定]电压,V
        private static readonly double CURVE_COFF_DEFAULT = 10.0;           // 曲线系数
        private static readonly int SCAN_PIXEL_COMPENSATION = 128;          // Z形扫描中有效像素补偿
        private static readonly int SCAN_ACQUISITION_MODE_NUM_DEFAULT = 4;
        ///////////////////////////////////////////////////////////////////////////////////////////
        private Laser m_laser;              // 激光参数
        private Pmt m_pmt;                  // PMT参数
        private Crs m_crs;                  // CRS
        private PinHole m_hole;             // 小孔
        private Scan m_scan;                // 扫描参数
        private Properties m_properties;    // 属性参数
        ///////////////////////////////////////////////////////////////////////////////////////////
        public bool Debugging { get; set; } 
        ///////////////////////////////////////////////////////////////////////////////////////////
        #region public apis

        public static Config GetConfig()
        {
            if (pConfig == null)
            {
                lock (locker)
                {
                    if (pConfig == null)
                    {
                        pConfig = new Config();
                    }
                }
            }
            return pConfig;
        }

        public int GetChannelNum()
        {
            return CHAN_NUM;
        }

        public int GetActivatedChannelNum()
        {
            int activatedChannelNum = 0;
            for (int i = 0; i < CHAN_NUM; i++)
            {
                CHAN_ID id = (CHAN_ID)Enum.ToObject(typeof(CHAN_ID), i);
                if (GetLaserSwitch(id) == LASER_CHAN_SWITCH.ON)
                {
                    activatedChannelNum++;
                }
            }
            return activatedChannelNum;
        }

        public API_RETURN_CODE SetLaserPortName(string portName)
        {
            Logger.Info(string.Format("set laser portname: [{0}].", portName));
            m_laser.PortName = portName;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public string GetLaserPortName()
        {
            return m_laser.PortName;
        }

        public API_RETURN_CODE SetLaserSwitch(CHAN_ID id, LASER_CHAN_SWITCH status)
        {
            Logger.Info(string.Format("set laser swicth: [id:{0}], [status:{1}].", id, status));
            GetLaserChannel(id).Status = status;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public LASER_CHAN_SWITCH GetLaserSwitch(CHAN_ID id)
        {
            return GetLaserChannel(id).Status;
        }

        public API_RETURN_CODE SetLaserPower(CHAN_ID id, double power)
        {
            Logger.Info(string.Format("set laser power: [id:{0}], [power:{1}].", id, power));
            GetLaserChannel(id).Power = power;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public double GetLaserPower(CHAN_ID id)
        {
            return GetLaserChannel(id).Power;
        }

        public API_RETURN_CODE SetPmtGain(CHAN_ID id, double gain)
        {
            Logger.Info(string.Format("set pmt gain: [id:{0}], [gain:{1}].", id, gain));
            GetPmtChannel(id).Gain = gain;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public double GetPmtGain(CHAN_ID id)
        {
            return GetPmtChannel(id).Gain;
        }

        public API_RETURN_CODE SetCrsAmplitude(double amplitude)
        {
            Logger.Info(string.Format("set crs amplitude: [{0}].", amplitude));
            m_crs.Amplitude = amplitude;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public double GetCrsAmplitude()
        {
            return m_crs.Amplitude;
        }

        public API_RETURN_CODE SetPinHoleSize(double size)
        {
            Logger.Info(string.Format("set pin hole size: [{0}].", size));
            m_hole.Size = size;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public double GetPinHoleSize()
        {
            return m_hole.Size;
        }

        public API_RETURN_CODE SetScanMode(SCAN_MODE mode)
        {
            Logger.Info(string.Format("set scan mode: [{0}].", mode));
            m_scan.Mode = mode;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public SCAN_MODE GetScanMode()
        {
            return m_scan.Mode;
        }

        public API_RETURN_CODE SetScanStartegy(SCAN_STRATEGY strategy)
        {
            Logger.Info(string.Format("set scan strategy: [{0}].", strategy));
            m_scan.Strategy = strategy;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public SCAN_STRATEGY GetScanStrategy()
        {
            return m_scan.Strategy;
        }

        public API_RETURN_CODE SetScanMirrorNum(SCAN_MIRROR_NUM mirrorNum)
        {
            Logger.Info(string.Format("set scan mirror num: [{0}].", mirrorNum));
            m_scan.MirrorNum = mirrorNum;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public SCAN_MIRROR_NUM GetScanMirrorNum()
        {
            return m_scan.MirrorNum;
        }

        public API_RETURN_CODE SetScanFlag(int flag)
        {
            Logger.Info(string.Format("set scan flag: [{0}].", flag));
            m_scan.Flag = flag;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public int GetScanFlag()
        {
            return m_scan.Flag;
        }

        public API_RETURN_CODE SetGalvResponseTime(double responeTime)
        {
            Logger.Info(string.Format("set galv response time: [{0}].", responeTime));
            m_scan.GalvResponseTime = responeTime;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public double GetGalvResponseTime()
        {
            return m_scan.GalvResponseTime;
        }

        public API_RETURN_CODE SetScanFieldSize(double fieldSize)
        {
            Logger.Info(string.Format("set scan field size: [{0}].", fieldSize));
            m_scan.FieldSize = fieldSize;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public double GetScanFieldSize()
        {
            return m_scan.FieldSize;
        }

        public API_RETURN_CODE SetScanDwellTime(double dwellTime)
        {
            Logger.Info(string.Format("set scan dwell time: [{0}].", dwellTime));
            m_scan.DwellTime = dwellTime;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public double GetScanDwellTime()
        {
            return m_scan.DwellTime;
        }

        public API_RETURN_CODE SetScanXPoints(int xPoints)
        {
            Logger.Info(string.Format("set scan x points: [{0}].", xPoints));
            m_scan.XPoints = xPoints;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public int GetScanXPoints()
        {
            return m_scan.XPoints;
        }

        public API_RETURN_CODE SetScanYPoints(int yPoints)
        {
            Logger.Info(string.Format("set scan y points: [{0}].", yPoints));
            m_scan.YPoints = yPoints;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public int GetScanYPoints()
        {
            return m_scan.YPoints;
        }

        public API_RETURN_CODE SetScanCalibrationVoltage(double calibrationVoltage)
        {
            Logger.Info(string.Format("set calibration voltage: [{0}].", calibrationVoltage));
            m_scan.CalibrationVoltage = calibrationVoltage;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public double GetScanCalibrationVoltage()
        {
            return m_scan.CalibrationVoltage;
        }

        public API_RETURN_CODE SetScanCurveCoff(double curveCoff)
        {
            Logger.Info(string.Format("set scan curve coff: [{0}].", curveCoff));
            m_scan.CurveCoff = curveCoff;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public double GetScanCurveCoff()
        {
            return m_scan.CurveCoff;
        }

        public API_RETURN_CODE SetScanPixelCompensation(int scanPixelComp)
        {
            Logger.Info(string.Format("set scan pixel compensation: [{0}].", scanPixelComp));
            m_scan.ScanPixelComp = scanPixelComp;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public int GetScanPixelCompensation()
        {
            return m_scan.ScanPixelComp;
        }

        public API_RETURN_CODE SetScanPixelOffset(int scanPixelOffset)
        {
            Logger.Info(string.Format("set scan pixel offset: [{0}].", scanPixelOffset));
            m_scan.ScanPixelOffset = scanPixelOffset;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public int GetScanPixelOffset()
        {
            return m_scan.ScanPixelOffset;
        }

        public API_RETURN_CODE SetScanPixelCalibration(int scanPixelCalibration)
        {
            Logger.Info(string.Format("set scan pixel caliration: [{0}].", scanPixelCalibration));
            m_scan.ScanPixelCalibration = scanPixelCalibration;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public int GetScanPixelCalibration()
        {
            return m_scan.ScanPixelCalibration;
        }

        public API_RETURN_CODE SetScanAcquisitionMode(SCAN_ACQUISITION_MODE acquisitionMode)
        {
            Logger.Info(string.Format("set scan acquisition mode: [{0}].", acquisitionMode));
            m_scan.AcquisitionMode = acquisitionMode;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public SCAN_ACQUISITION_MODE GetScanAcquisitionMode()
        {
            return m_scan.AcquisitionMode;
        }

        public API_RETURN_CODE SetScanAcquisitionModeNum(int acquisitionModeNum)
        {
            Logger.Info(string.Format("set scan acquisition mode num: [{0}].", acquisitionModeNum));
            m_scan.AcquisitionModeNum = acquisitionModeNum;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public int GetScanAcquisitionModeNum()
        {
            return m_scan.AcquisitionModeNum;
        }
        
        public API_RETURN_CODE SetChannelColorReference(CHAN_ID id, Color colorReference)
        {
            Logger.Info(string.Format("set channel color reference: [id:{0}], [color:{1}].", id, colorReference));
            GetPropChannel(id).ColorReference = colorReference;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public Color GetChannelColorReference(CHAN_ID id)
        {
            return GetPropChannel(id).ColorReference;
        }

        public API_RETURN_CODE SetChannelBackgroundNoiseLevel(CHAN_ID id, short noiseLevel)
        {
            Logger.Info(string.Format("set channel background noise level: [id:{0}], [level:{1}].", id, noiseLevel));
            GetPropChannel(id).BackgroundNoiseLevel = noiseLevel;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public short GetChannelBackgroundNoiseLevel(CHAN_ID id)
        {
            return GetPropChannel(id).BackgroundNoiseLevel;
        }

        #endregion

        #region private apis

        private Config()
        {
            m_laser = new Laser
            {
                PortName = null,
                Channels = new LaserChannel[CHAN_NUM]
            };

            for (int i = 0; i < CHAN_NUM; i++)
            {
                m_laser.Channels[i] = new LaserChannel
                {
                    Id = (CHAN_ID)Enum.ToObject(typeof(CHAN_ID), i),
                    Power = LASER_POWER_DEFAULT,
                    Status = LASER_CHAN_SWITCH.OFF
                };
            }

            m_pmt = new Pmt
            {
                Channels = new PmtChannel[CHAN_NUM]
            };

            for (int i = 0; i < CHAN_NUM; i++)
            {
                m_pmt.Channels[i] = new PmtChannel
                {
                    Id = (CHAN_ID)Enum.ToObject(typeof(CHAN_ID), i),
                    Gain = PMT_GAIN_DEFAULT
                };
            }

            m_crs = new Crs
            {
                Amplitude = CRS_AMPLITUDE_DEFAULT
            };
            m_hole = new PinHole
            {
                Size = PIN_HOLE_SIZE_DEFAULT
            };

            m_scan = new Scan
            {
                Mode = SCAN_MODE.GALVANOMETER,
                Strategy = SCAN_STRATEGY.Z_UNIDIRECTION,
                MirrorNum = SCAN_MIRROR_NUM.TWO,
                XPoints = SCAN_POINTS_DEFAULT,
                YPoints = SCAN_POINTS_DEFAULT,
                Flag = 0,
                GalvResponseTime = GALV_RESPONSE_TIME_DEFAULT,
                FieldSize = FIELD_SIZE_DEFAULT,
                DwellTime = SCAN_PIXEL_TIME_DEFAULT,
                CalibrationVoltage = CALIBRATION_VOLTAGE_DEFAULT,
                CurveCoff = CURVE_COFF_DEFAULT,
                ScanPixelComp = SCAN_PIXEL_COMPENSATION,
                ScanPixelOffset = 0,
                ScanPixelCalibration = 0,
                AcquisitionMode = SCAN_ACQUISITION_MODE.STANDARD,
                AcquisitionModeNum = SCAN_ACQUISITION_MODE_NUM_DEFAULT
            };

            m_properties = new Properties
            {
                Channels = new PropChannel[CHAN_NUM]
            };

            Color[] clolrs = new Color[] { Color.Purple, Color.Cyan, Color.YellowGreen, Color.Red };

            for (int i = 0; i < CHAN_NUM; i++)
            {
                m_properties.Channels[i] = new PropChannel
                {
                    Id = (CHAN_ID)Enum.ToObject(typeof(CHAN_ID), i),
                    ColorReference = clolrs[i],
                    BackgroundNoiseLevel = 0
                };
            }

            Debugging = true;
        }

        private LaserChannel GetLaserChannel(CHAN_ID id)
        {
            for (int i = 0; i < CHAN_NUM; i++)
            {
                if (m_laser.Channels[i].Id == id)
                {
                    return m_laser.Channels[i];
                }
            }
            return null;
        }

        private PmtChannel GetPmtChannel(CHAN_ID id)
        {
            for (int i = 0; i < CHAN_NUM; i++)
            {
                if (m_pmt.Channels[i].Id == id)
                {
                    return m_pmt.Channels[i];
                }
            }
            return null;
        }

        private PropChannel GetPropChannel(CHAN_ID id)
        {
            for (int i = 0; i < CHAN_NUM; i++)
            {
                if (m_properties.Channels[i].Id == id)
                {
                    return m_properties.Channels[i];
                }
            }
            return null;
        }

        #endregion

    }

    public enum ACQ_DEVICE
    {
        PMT = 0x00,
        APD = 0x01
    }

    public enum ACQ_BOARD
    {
        NICARD = 0x00,
        ATS9440 = 0x01
    }

    public class SysConfig
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        private volatile static SysConfig pConfig = null;
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
        private string[] m_apdCiGatePfis;   // APD门信号[Pause Trigger]使用的PFI端口，用于使能/禁能计数器计数
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

            foreach(string terminal in terminals)
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

        public static SysConfig GetSysConfig()
        {
            if (pConfig == null)
            {
                lock (locker)
                {
                    if (pConfig == null)
                    {
                        pConfig = new SysConfig();
                    }
                }
            }
            return pConfig;
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

        public string GetApdCiGatePfi(CHAN_ID id)
        {
            return m_apdCiGatePfis[(int)id];
        }

        public API_RETURN_CODE SetApdCiGatePfi(CHAN_ID id, string pfi)
        {
            m_apdCiGatePfis[(int)id] = pfi;
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

        private SysConfig()
        {
            m_acqBoard = ACQ_BOARD.NICARD;
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

            m_apdCiGatePfis = new string[] {
                string.Concat("/", deviceName, "/PFI9"),
                string.Concat("/", deviceName, "/PFI4"),
                string.Concat("/", deviceName, "/PFI1"),
                string.Concat("/", deviceName, "/PFI6"),
            };
        }

    }
}
