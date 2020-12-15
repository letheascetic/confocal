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
    public enum SCAN_LINE_OPTION
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
        MICROSECONDS6 = 6,
        MICROSECONDS8 = 8,
        MICROSECONDS10 = 10,
        MICROSECONDS20 = 20,
        MICROSECONDS50 = 50,
        MICROSECONDS100 = 100,
        MICROSECONDS200 = 200,
        MICROSECONDS500 = 500,
        MICROSECONDS1000 = 1000
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

    /// <summary>
    /// 扫描通道
    /// 包括：通道ID、通道状态[开/关]、功率、增益、小孔孔径、像素偏置、伽马、伪彩色
    /// </summary>
    public class Z1ScanChannel
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly double LASER_POWER_DEFAULT = 2.0;
        private static readonly double PMT_HV_DEFAULT = 2.5;
        private static readonly double PIN_HOLE_SIZE_DEFAULT = 50;
        ///////////////////////////////////////////////////////////////////////////////////////////
        private readonly CHAN_ID mChannelId;    // 通道ID[激光波长]
        private readonly string mChannelName;   // 通道名
        private CHAN_SWITCH mChannelSwitch;     // 通道[激光]状态
        private double mLaserPower;             // 通道[激光]功率
        private double mGain;                   // PMT增益
        private double mPinHole;                // 小孔孔径
        private int mOffset;                    // 图像像素补偿[偏置]
        private double mGamma;                  // 伽马校正
        private Color mColor;                   // 伪彩色
        ///////////////////////////////////////////////////////////////////////////////////////////
        public CHAN_ID ChannelId { get { return mChannelId; } }
        public string ChannelName { get { return mChannelName; } }
        public CHAN_SWITCH ChannelSwitch { get { return mChannelSwitch; } set { mChannelSwitch = value; } }
        public double LaserPower { get { return mLaserPower; } set { mLaserPower = value; } }
        public double HV { get { return mGain; } set { mGain = value; } }
        public double PinHole { get { return mPinHole; } set { mPinHole = value; } }
        public int Offset { get { return mOffset; } set { mOffset = value; } }
        public double Gamma { get { return mGamma; } set { mGamma = value; } }
        public Color ColorReference { get { return mColor; } set { mColor = value; } }
        ///////////////////////////////////////////////////////////////////////////////////////////
        public Z1ScanChannel(CHAN_ID channelId, string channelName, Color color)
        {
            mChannelId = channelId;
            mChannelName = channelName;
            mChannelSwitch = CHAN_SWITCH.OFF;
            mLaserPower = LASER_POWER_DEFAULT;
            mGain = PMT_HV_DEFAULT;
            mPinHole = PIN_HOLE_SIZE_DEFAULT;
            mOffset = 0;
            mGamma = 1.0;
            mColor = color;
        }

    }

    /// <summary>
    /// 扫描范围
    /// </summary>
    public class Z1ScanField
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly float FULL_FIELD_DEFAULT = 200.0F;
        private static readonly int EXTEND_LINE_TIME_DEFAULT = 100;
        private static readonly int EXTEND_ROW_COUNT_DEFAULT = 0;
        ///////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 行[X方向]扩展时间[单位：us]
        /// </summary>
        public static int ExtendLineTime { get; set; }
        /// <summary>
        /// [Y方向]扩展的行数
        /// </summary>
        public static int ExtendRowCount { get; set; }
        /// <summary>
        /// 行偏置时间[单位：us]
        /// </summary>
        public static int ExtendLineOffset { get; set; }
        /// <summary>
        /// [Y方向]的偏置
        /// </summary>
        public static int ExtendRowOffset { get; set; }
        /// <summary>
        /// 双向扫描中奇数偶数行错位的像素数
        /// </summary>
        public static int ScanPixelCalibration { get; set; }
        /// <summary>
        /// 行边缘区域除数因子
        /// </summary>
        public static double ExtendLineMarginDiv { get; set; }
        /// <summary>
        /// 全视场范围
        /// </summary>
        public static RectangleF FullRange { get; }
        ///////////////////////////////////////////////////////////////////////////////////////////
        public RectangleF ScanRange { get { return mScanRange; } set { mScanRange = value; } }
        public SCAN_AREA ScanArea { get { return mScanArea; } set { mScanArea = value; } }
        ///////////////////////////////////////////////////////////////////////////////////////////
        private RectangleF mScanRange;
        private SCAN_AREA mScanArea;
        ///////////////////////////////////////////////////////////////////////////////////////////
        static Z1ScanField()
        {
            ExtendLineTime = EXTEND_LINE_TIME_DEFAULT;
            ExtendRowCount = EXTEND_ROW_COUNT_DEFAULT;
            ExtendLineOffset = 0;
            ExtendRowOffset = 0;
            ScanPixelCalibration = 0;
            ExtendLineMarginDiv = 50;
            FullRange = new RectangleF(-FULL_FIELD_DEFAULT / 2, -FULL_FIELD_DEFAULT / 2, FULL_FIELD_DEFAULT, FULL_FIELD_DEFAULT);
        }

        public Z1ScanField(SCAN_AREA scanArea, float x, float y, float width, float height)
        {
            mScanArea = scanArea;
            mScanRange = new RectangleF(x, y, width, height);
        }

        public Z1ScanField(SCAN_AREA scanArea, RectangleF scanRange)
        {
            mScanArea = scanArea;
            mScanRange = scanRange;
        }

        public Z1ScanField(SCAN_AREA scanArea, PointF location, SizeF size)
        {
            mScanArea = scanArea;
            mScanRange = new RectangleF(location, size);
        }

    }

    /// <summary>
    /// 振镜属性
    /// </summary>
    public class Z1GalvanoProperty
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly double GALV_RESPONSE_TIME_DEFAULT = 200.0;              // 振镜响应时间, us
        private static readonly double CALIBRATION_VOLTAGE_DEFAULT = 5.848e-5 * 1000;   // 校准[标定]电压,V/um
        private static readonly double CALIBRATION_FACTOR_DEFAULT = 1.0;                // 校准系数
        private static readonly double XOFFSET_VOLTAGE_DEFAULT = 0;                     // X=0位置对应的偏置电压
        private static readonly double YOFFSET_VOLTAGE_DEFAULT = 0;                     // Y=0位置对应的偏置电压
        ///////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// X轴原点坐标对应的振镜电压偏置
        /// </summary>
        public static double XOffsetVoltage { get; set; }
        /// <summary>
        /// Y轴原点坐标对应的振镜电压偏置
        /// </summary>
        public static double YOffsetVoltage { get; set; }
        /// <summary>
        /// 振镜响应时间
        /// </summary>
        public static double GalvanoResponseTime { get; set; }
        /// <summary>
        /// 振镜校准电压，单位：V/um
        /// </summary>
        public static double GalvanoCalibrationVoltage { get; set; }
        /// <summary>
        /// 振镜校准系数
        /// </summary>
        public static double GalvanoCalibrationFactor { get; set; }

        static Z1GalvanoProperty()
        {
            XOffsetVoltage = XOFFSET_VOLTAGE_DEFAULT;
            YOffsetVoltage = YOFFSET_VOLTAGE_DEFAULT;
            GalvanoResponseTime = GALV_RESPONSE_TIME_DEFAULT;
            GalvanoCalibrationVoltage = CALIBRATION_VOLTAGE_DEFAULT;
            GalvanoCalibrationFactor = CALIBRATION_FACTOR_DEFAULT;
        }

        /// <summary>
        /// X坐标->X振镜电压
        /// </summary>
        /// <param name="xCoordinate"></param>
        /// <returns></returns>
        public static double XCoordinateToVoltage(double xCoordinate)
        {
            return xCoordinate * GalvanoCalibrationVoltage * GalvanoCalibrationFactor + XOffsetVoltage;
        }

        /// <summary>
        /// X坐标序列->X振镜电压序列
        /// </summary>
        /// <param name="xCoordinates"></param>
        /// <returns></returns>
        public static double[] XCoordinateToVoltage(double[] xCoordinates)
        {
            double coff = GalvanoCalibrationVoltage * GalvanoCalibrationFactor;
            double[] xVoltages = new double[xCoordinates.Length];
            for (int i = 0; i < xCoordinates.Length; i++)
            {
                xVoltages[i] = xCoordinates[i] * coff + XOffsetVoltage;
            }
            return xVoltages;
        }

        /// <summary>
        /// Y坐标->Y振镜电压
        /// </summary>
        /// <param name="yCoordinate"></param>
        /// <returns></returns>
        public static double YCoordinateToVoltage(double yCoordinate)
        {
            return yCoordinate * GalvanoCalibrationVoltage * GalvanoCalibrationFactor + YOffsetVoltage;
        }

        /// <summary>
        /// Y坐标序列->Y振镜电压序列
        /// </summary>
        /// <param name="yCoordinates"></param>
        /// <returns></returns>
        public static double[] YCoordinateToVoltage(double[] yCoordinates)
        {
            double coff = GalvanoCalibrationVoltage * GalvanoCalibrationFactor;
            double[] yVoltages = new double[yCoordinates.Length];
            for (int i = 0; i < yCoordinates.Length; i++)
            {
                yVoltages[i] = yCoordinates[i] * coff + YOffsetVoltage;
            }
            return yVoltages;
        }

        /// <summary>
        /// X振镜电压->X坐标
        /// </summary>
        /// <param name="xVoltage"></param>
        /// <returns></returns>
        public static double XVoltageToCoordinate(double xVoltage)
        {
            return (xVoltage - XOffsetVoltage) / GalvanoCalibrationFactor / GalvanoCalibrationVoltage;
        }

        /// <summary>
        /// X振镜电压序列->X坐标序列
        /// </summary>
        /// <param name="xVoltages"></param>
        /// <returns></returns>
        public static double[] XVoltageToCoordinate(double[] xVoltages)
        {
            double coff = GalvanoCalibrationFactor * GalvanoCalibrationVoltage;
            double[] xCoordinates = new double[xVoltages.Length];
            for (int i = 0; i < xVoltages.Length; i++)
            {
                xCoordinates[i] = (xVoltages[i] - XOffsetVoltage) / coff;
            }
            return xCoordinates;
        }

        /// <summary>
        /// Y振镜电压->Y坐标
        /// </summary>
        /// <param name="yVoltage"></param>
        /// <returns></returns>
        public static double YVoltageToCoordinate(double yVoltage)
        {
            return (yVoltage - YOffsetVoltage) / GalvanoCalibrationFactor / GalvanoCalibrationVoltage;
        }

        /// <summary>
        /// Y振镜电压序列->Y坐标序列
        /// </summary>
        /// <param name="yVoltages"></param>
        /// <returns></returns>
        public static double[] YVoltageToCoordinate(double[] yVoltages)
        {
            double coff = GalvanoCalibrationFactor * GalvanoCalibrationVoltage;
            double[] yCoordinates = new double[yVoltages.Length];
            for (int i = 0; i < yVoltages.Length; i++)
            {
                yCoordinates[i] = (yVoltages[i] - YOffsetVoltage) / coff;
            }
            return yCoordinates;
        }

    }

    /// <summary>
    /// 扫描属性
    /// </summary>
    public class Z1ScanProperty
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly double CURVE_COFF_DEFAULT = 1.1;                        // 曲线系数
        private static readonly double PIXEL_SAMPLE_RATE_DEFAULT = 1e6;                 // 像素采样速率
        ///////////////////////////////////////////////////////////////////////////////////////////
        public SCAN_MODE ScanMode { get; set; }                             // 扫描模式
        public SCAN_DIRECTION ScanDirection { get; set; }                   // 扫描方向
        public SCANNER_SYSTEM Scanners { get; set; }                        // 扫描振镜
        public SCAN_ACQUISITION_MODE ScanAcquisitionMode { get; set; }      // 采集模式
        public SCAN_LINE_SKIPPING ScanLineSkipping { get; set; }            // Line Skipping
        public SCAN_LINE_OPTION ScanLineOption { get; set; }                // Line Option
        public SCAN_LINE_OPTION_PARA ScanLineOptionPara { get; set; }       // Line Option 参数
        public SCAN_PIXELS ScanPixels { get; set; }                         // 扫描像素
        public SCAN_PIXEL_DWELL ScanPixelDwell { get; set; }                // 扫描时间
        public SCAN_CHANNEL_SEQUENCE ScanChannelSequence { get; set; }      // 扫描通道序列
        public SCAN_AREA ScanArea { get; set; }                             // 扫描区域类型
        public Z1ScanField[] ScanFields { get; set; }                       // 扫描范围  
        public Z1ScanChannel[] ScanChannels { get; set; }                   // 扫描通道
        public Z1GalvanoProperty GalvanoProperty { get; set; }              // 振镜属性
        public double PixelSampleRate { get; set; }                         // 像素速率[采样速率]
        public double CurveCalibrationFactor { get; set; }                  // 曲线校正因子
        public double ScanLineStartTime { get { return Z1GalvanoProperty.GalvanoResponseTime * 2; } }     // 扫描行起始时间[单双向有效]
        public double ScanLineHoldTime { get { return Z1GalvanoProperty.GalvanoResponseTime; } }          // 扫描行起始时间[只对单向扫描有效]
        public double ScanLineEndTime { get { return Z1GalvanoProperty.GalvanoResponseTime; } }           // 扫描行结束时间[只对单向扫描有效]
        ///////////////////////////////////////////////////////////////////////////////////////////
        public Z1ScanProperty()
        {
            ScanMode = SCAN_MODE.GALVANO;
            ScanDirection = SCAN_DIRECTION.UNIDIRECTION;
            Scanners = SCANNER_SYSTEM.TWO_SCANNERS;
            ScanAcquisitionMode = SCAN_ACQUISITION_MODE.LIVE;
            ScanLineSkipping = SCAN_LINE_SKIPPING.NONE;
            ScanLineOption = SCAN_LINE_OPTION.NONE;
            ScanLineOptionPara = SCAN_LINE_OPTION_PARA.REPEAT2;
            ScanPixels = SCAN_PIXELS.X512;
            ScanPixelDwell = SCAN_PIXEL_DWELL.MICROSECONDS2;
            ScanChannelSequence = SCAN_CHANNEL_SEQUENCE.NONE;
            ScanArea = SCAN_AREA.FULLFIELD;

            float fullRange = Z1ScanField.FullRange.Width;
            ScanFields = new Z1ScanField[]
            {
                new Z1ScanField(SCAN_AREA.FULLFIELD, -fullRange / 2, -fullRange / 2, fullRange, fullRange),
                new Z1ScanField(SCAN_AREA.SQUARE, -fullRange / 2, -fullRange / 2, fullRange, fullRange),
                new Z1ScanField(SCAN_AREA.BANK, -fullRange / 2, -fullRange / 2, fullRange, fullRange / 2),
                new Z1ScanField(SCAN_AREA.LINE, -fullRange / 2, 0, fullRange, fullRange / (int)ScanPixels)
            };

            ScanChannels = new Z1ScanChannel[]
            {
                new Z1ScanChannel(CHAN_ID.WAVELENGTH_405_NM, "405nm", Color.MediumPurple),
                new Z1ScanChannel(CHAN_ID.WAVELENGTH_488_NM, "488nm", Color.DarkCyan),
                new Z1ScanChannel(CHAN_ID.WAVELENGTH_561_NM, "561nm", Color.YellowGreen),
                new Z1ScanChannel(CHAN_ID.WAVELENGTH_640_NM, "640nm", Color.Red)
            };
            GalvanoProperty = new Z1GalvanoProperty();
            CurveCalibrationFactor = CURVE_COFF_DEFAULT;
            PixelSampleRate = PIXEL_SAMPLE_RATE_DEFAULT;
        }

        /// <summary>
        /// 像素尺寸[单位：um/pixel]
        /// </summary>
        /// <returns></returns>
        public float GetPixelSize()
        {
            return GetScanField().ScanRange.Width / (int)ScanPixels;
        }

        /// <summary>
        /// 像素电压[单位：V/pixel]
        /// </summary>
        /// <returns></returns>
        public double GetPixelVoltage()
        {
            return GetPixelSize() * Z1GalvanoProperty.GalvanoCalibrationVoltage * Z1GalvanoProperty.GalvanoCalibrationFactor;
        }

        /// <summary>
        /// 扩展前的扫描范围[单位：um]
        /// </summary>
        /// <returns></returns>
        public Z1ScanField GetScanField()
        {
            return ScanFields[(int)ScanArea];
        }

        /// <summary>
        /// 扩展后的扫描范围[单位：um]
        /// </summary>
        /// <returns></returns>
        public Z1ScanField GetExtendScanField()
        {
            Z1ScanField scanField = GetScanField();
            RectangleF scanRange = scanField.ScanRange;
            float xExtendRange = Z1ScanField.ExtendLineTime * scanRange.Width / ((int)ScanPixelDwell * (int)ScanPixels);
            float yExtendRange = Z1ScanField.ExtendRowCount * GetPixelSize();
            return new Z1ScanField(scanField.ScanArea, scanRange.X - xExtendRange / 2, scanRange.Y - yExtendRange / 2, scanRange.Width + xExtendRange, scanRange.Height + yExtendRange);
        }

        /// <summary>
        /// 扩展后行扫描像素数
        /// </summary>
        /// <returns></returns>
        public int GetExtendScanXPixels()
        {
            return GetScanXPixels() + (Z1ScanField.ExtendLineTime >> 1) / (int)ScanPixelDwell * 2;
        }

        /// <summary>
        /// 默认的扫描行数
        /// </summary>
        /// <returns></returns>
        public int GetScanXPixels()
        {
            return (int)ScanPixels;
        }

        /// <summary>
        /// 默认的扫描列数
        /// </summary>
        /// <returns></returns>
        public int GetScanYPixels()
        {
            return (int)ScanPixels;
        }

        /// <summary>
        /// 扩展后扫描列数
        /// </summary>
        /// <returns></returns>
        public int GetExtendScanYPixels()
        {
            return GetScanYPixels() + Z1ScanField.ExtendRowCount;
        }

    }

    /// <summary>
    /// 配置属性
    /// </summary>
    public class Z1Config
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        private volatile static Z1Config pConfig = null;
        private static readonly object locker = new object();
        ///////////////////////////////////////////////////////////////////////////////////////////
        public static readonly int CHAN_NUM = 4;
        ///////////////////////////////////////////////////////////////////////////////////////////
        public bool Debugging { get; set; }
        public string LaserPortName { get; set; }
        public Z1ScanProperty ScanProperty { get; set; }
        ///////////////////////////////////////////////////////////////////////////////////////////
        public static Z1Config GetConfig()
        {
            if (pConfig == null)
            {
                lock (locker)
                {
                    if (pConfig == null)
                    {
                        pConfig = new Z1Config();
                    }
                }
            }
            return pConfig;
        }
        /// <summary>
        /// 通道数
        /// </summary>
        /// <returns></returns>
        public int GetChannelNum()
        {
            return CHAN_NUM;
        }
        /// <summary>
        /// 当前激活的通道数
        /// </summary>
        /// <returns></returns>
        public int GetActivatedChannelNum()
        {
            int activatedChannelNum = 0;
            for (int i = 0; i < CHAN_NUM; i++)
            {
                CHAN_ID id = (CHAN_ID)Enum.ToObject(typeof(CHAN_ID), i);
                if (ScanProperty.ScanChannels[i].ChannelSwitch == CHAN_SWITCH.ON)
                {
                    activatedChannelNum++;
                }
            }
            return activatedChannelNum;
        }

        private Z1Config()
        {
            Debugging = true;
            LaserPortName = null;
            ScanProperty = new Z1ScanProperty();
        }

    }
}
