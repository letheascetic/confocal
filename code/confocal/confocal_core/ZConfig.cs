using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace confocal_core
{
    /// <summary>
    /// 代码执行结果
    /// </summary>
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

    /// <summary>
    /// 通道ID
    /// </summary>
    public enum CHAN_ID : int
    {
        WAVELENGTH_405_NM = 0,
        WAVELENGTH_488_NM = 1,
        WAVELENGTH_561_NM = 2,
        WAVELENGTH_640_NM = 3
    }

    /// <summary>
    /// 通道开关
    /// </summary>
    public enum CHAN_SWITCH : int
    {
        OFF = 0,
        ON = 1
    }

    /// <summary>
    /// 扫描模式：Galvano 或 Resonant
    /// </summary>
    public enum SCAN_MODE : int
    {
        RESONANT = 0,
        GALVANO = 1
    }

    /// <summary>
    /// 扫描方向：单向 or 双向
    /// </summary>
    public enum SCAN_DIRECTION : int
    {
        BIDIRECTION = 0,          // 双向
        UNIDIRECTION = 1          // 单向
    }

    /// <summary>
    /// 扫描振镜：双镜 or 三镜
    /// </summary>
    public enum SCANNER_SYSTEM
    {
        TWO_SCANNERS = 2,
        THREE_SCANNERS = 3
    };

    /// <summary>
    /// 扫描采集模式：实时 捕获 发现
    /// </summary>
    public enum SCAN_ACQUISITION_MODE
    {
        LIVE = 0,
        CAPTURE = 1,
        FIND = 2
    };

    /// <summary>
    /// 扫描线跳过
    /// </summary>
    public enum SCAN_LINE_SKIPPING
    {
        NONE = 0,
        EVERY2 = 2,
        EVERY4 = 4,
        EVERY8 = 8,
        EVERY16 = 16,
    };

    /// <summary>
    /// 扫描线操作模式
    /// </summary>
    public enum SCAN_LINE_OPTION_MODE
    {
        NONE = 0,
        AVERAGE = 1,
        INTEGRATE = 2
    };

    /// <summary>
    /// 扫描线选项参数
    /// </summary>
    public enum SCAN_LINE_OPTION_PARA
    {
        REPEAT2 = 2,
        REPEAT4 = 4,
        REPEAT8 = 8,
        REPEAT16 = 16
    };

    /// <summary>
    /// 扫描像素
    /// </summary>
    public enum SCAN_PIXELS
    { 
        X64 = 64,
        X128 = 128,
        X256 = 256,
        X512 = 512,
        X1024 = 1024,
        X2048 = 2048,
        X4096 = 4096
    };

    /// <summary>
    /// 扫描像素时间
    /// </summary>
    public enum SCAN_PIXEL_DWELL
    {
        MICROSECONDS2 = 2,
        MICROSECONDS4 = 4,
        MICROSECONDS8 = 8,
        MICROSECONDS16 = 16,
        MICROSECONDS32 = 32,
        MICROSECONDS64 = 64,
        MICROSECONDS128 = 128,
        MICROSECONDS256 = 256,
        MICROSECONDS512 = 512,
        MICROSECONDS1024 = 1024
    };

    /// <summary>
    /// 扫描通道序列
    /// </summary>
    public enum SCAN_CHANNEL_SEQUENCE
    {
        NONE = 0,
        CH1TO4 = 1,
        CH4TO1 = 2
    };

    /// <summary>
    /// 扫描区域
    /// </summary>
    public enum SCAN_AREA
    {
        FULLFIELD = 0,
        SQUARE = 1,
        BANK = 2,
        LINE = 3
    };

    public class ChannelProperty
    {
        private readonly CHAN_ID mChannelId;    // 通道ID[激光波长]
        private readonly string mChannelName;   // 通道名
        private CHAN_SWITCH mChannelSwitch;     // 通道[激光]状态
        private double mLaserPower;             // 通道[激光]功率
        private double mGain;                   // PMT增益
        private double mPinHole;                // 小孔孔径
        private int mOffset;                    // 图像像素补偿[偏移]
        private double mGamma;                  // 伽马校正
        private Color mColor;                   // 伪彩色

        public CHAN_ID ChannelId { get { return mChannelId; } }
        public string ChannelName { get { return mChannelName; } }
        public CHAN_SWITCH ChannelSwitch { get { return mChannelSwitch; } set { mChannelSwitch = value; } }
        public double LaserPower { get { return mLaserPower; } set { mLaserPower = value; } }
        public double HV { get { return mGain; } set { mGain = value; } }
        public double PinHole { get { return mPinHole; } set { mPinHole = value; } }
        public int Offset { get { return mOffset; } set { mOffset = value; } }
        public double Gamma { get { return mGamma; } set { mGamma = value; } }
        public Color ColorReference { get { return mColor; } set { mColor = value; } }

        public ChannelProperty(CHAN_ID channelId, string channelName)
        {
            mChannelId = channelId;
            mChannelName = channelName;
        }

    }






    public class ZConfig
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        private volatile static ZConfig pConfig = null;
        private static readonly object locker = new object();
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly int CHAN_NUM = 4;
        private static readonly double LASER_POWER_DEFAULT = 2.0;
        private static readonly double PMT_HV_DEFAULT = 2.5;
        private static readonly double CRS_AMPLITUDE_DEFAULT = 3.3;
        private static readonly int SCAN_PIXELS_DEFAULT = 512;              // 默认扫描像素
        private static readonly double SCAN_PIXEL_DWELL_DEFAULT = 4.0;      // 像素时间, us
        private static readonly double PIN_HOLE_SIZE_DEFAULT = 50;          // 小孔尺寸
        private static readonly double GALV_RESPONSE_TIME_DEFAULT = 200.0;  // 振镜响应时间, us
        private static readonly double FIELD_SIZE_DEFAULT = 200.0;          // 视场大小, um
        private static readonly double CALIBRATION_VOLTAGE_DEFAULT = 5.848e-5;      // 校准[标定]电压,V
        private static readonly double CURVE_COFF_DEFAULT = 10.0;           // 曲线系数
        private static readonly int SCAN_PIXEL_COMPENSATION = 64;           // Z形扫描中有效像素补偿
        ///////////////////////////////////////////////////////////////////////////////////////////



    }
}
