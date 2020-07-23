using log4net;
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

        public CHAN_ID Id { get { return id; } set { id = value; } }
        public Color ColorReference { get { return colorReference; } set { colorReference = value; } }
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
        private int bScanPixelComp;             // Z形双向扫描中有效像素补偿
        private int bScanPixelOffset;           // Z形双向扫描中像素错位
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
        public int BScanPixelComp
        { get { return bScanPixelComp; } set { bScanPixelComp = value; } }
        public int BScanPixelOffset
        { get { return bScanPixelOffset; } set { bScanPixelOffset = value; } }
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
        private static readonly double FIELD_SIZE_DEFAULT = 100.0;          // 视场大小, um
        private static readonly double CALIBRATION_VOLTAGE_DEFAULT = 4.09855e-5;  // 校准[标定]电压,V
        private static readonly double CURVE_COFF_DEFAULT = 10.0;           // 曲线系数
        private static readonly int BIDIRECTION_SCAN_PIXEL_COMPENSATION = 64;    // Z形双向扫描中有效像素补偿
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

        public API_RETURN_CODE SetBScanPixelCompensation(int bScanPixelComp)
        {
            Logger.Info(string.Format("set bidirection scan pixel compensation: [{0}].", bScanPixelComp));
            m_scan.BScanPixelComp = bScanPixelComp;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public int GetBScanPixelCompensation()
        {
            return m_scan.BScanPixelComp;
        }

        public API_RETURN_CODE SetBScanPixelOffset(int bScanPixelOffset)
        {
            Logger.Info(string.Format("set bidirection scan pixel offset: [{0}].", bScanPixelOffset));
            m_scan.BScanPixelOffset = bScanPixelOffset;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public int GetBScanPixelOffset()
        {
            return m_scan.BScanPixelOffset;
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
            Logger.Info(string.Format("set channel color reference: [id:{0}], [power:{1}].", id, colorReference));
            GetPropChannel(id).ColorReference = colorReference;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public Color GetChannelColorReference(CHAN_ID id)
        {
            return GetPropChannel(id).ColorReference;
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
                BScanPixelComp = BIDIRECTION_SCAN_PIXEL_COMPENSATION,
                BScanPixelOffset = 0,
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
                    ColorReference = clolrs[i]
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
}
