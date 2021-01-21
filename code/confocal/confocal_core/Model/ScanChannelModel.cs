using confocal_core.Common;
using confocal_core.Properties;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace confocal_core.Model
{
    /// <summary>
    /// 通道增益更新事件委托
    /// </summary>
    /// <param name="id"></param>
    /// <param name="gain"></param>
    /// <returns></returns>
    public delegate API_RETURN_CODE ChannelGainChangedEventHandler(int id, int gain);
    /// <summary>
    /// 通道偏置更新事件委托
    /// </summary>
    /// <param name="id"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public delegate API_RETURN_CODE ChannelOffsetChangedEventHandler(int id, int offset);
    /// <summary>
    /// 通道功率更新事件委托
    /// </summary>
    /// <param name="id"></param>
    /// <param name="power"></param>
    /// <returns></returns>
    public delegate API_RETURN_CODE ChannelPowerChangedEventHandler(int id, int power);
    /// <summary>
    /// 通道激活状态更新事件委托
    /// </summary>
    /// <param name="id"></param>
    /// <param name="activated"></param>
    /// <returns></returns>
    public delegate API_RETURN_CODE ChannelActivateChangedEventHandler(int id, bool activated);

    /// <summary>
    /// 扫描通道
    /// </summary>
    public class ScanChannelModel : ObservableObject
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        public static readonly int CHANNEL405 = 0;
        public static readonly int CHANNEL488 = 1;
        public static readonly int CHANNEL561 = 2;
        public static readonly int CHANNEL640 = 3;
        ///////////////////////////////////////////////////////////////////////////////////////////
        private int id;                     // 通道ID
        private bool collapesd;             // 折叠
        private string name;                // 通道名

        private double laserPower;          // 激光功率
        private Color laserColor;           // 激光颜色
        private string laserWaveLength;     // 激光波长

        private bool activated;             // 通道激活状态
        private int gain;                   // 增益
        private int offset;                 // 偏置
        private int gamma;                  // 伽马
        private Color pseudoColor;          // 伪彩色
        ///////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 通道ID
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(() => ID); }
        }

        /// <summary>
        /// 是否可见
        /// </summary>
        public bool Collapsed
        {
            get { return collapesd; }
            set { collapesd = value; RaisePropertyChanged(() => Collapsed); }
        }

        /// <summary>
        /// 通道名
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(() => Name); }
        }

        /// <summary>
        /// 激光功率
        /// </summary>
        public double LaserPower
        {
            get { return laserPower; }
            set { laserPower = value; RaisePropertyChanged(() => LaserPower); }
        }

        /// <summary>
        /// 激光颜色
        /// </summary>
        public Color LaserColor
        {
            get { return laserColor; }
            set { laserColor = value; RaisePropertyChanged(() => LaserColor); }
        }

        /// <summary>
        /// 激光波长
        /// </summary>
        public string LaserWaveLength
        {
            get { return laserWaveLength; }
            set { laserWaveLength = value; RaisePropertyChanged(() => LaserWaveLength); }
        }

        /// <summary>
        /// 通道状态：激活 or 关闭
        /// </summary>
        public bool Activated
        {
            get { return activated; }
            set { activated = value; RaisePropertyChanged(() => Activated); }
        }

        ///// <summary>
        ///// 小孔孔径
        ///// </summary>
        //public double PinHole
        //{
        //    get { return pinHole; }
        //    set { pinHole = value; RaisePropertyChanged(() => PinHole); }
        //}

        /// <summary>
        /// 增益
        /// </summary>
        public int Gain
        {
            get { return gain; }
            set { gain = value; RaisePropertyChanged(() => Gain); }
        }

        /// <summary>
        /// 偏置
        /// </summary>
        public int Offset
        {
            get { return offset; }
            set { offset = value; RaisePropertyChanged(() => Offset); }
        }

        /// <summary>
        /// 伽马校正
        /// </summary>
        public int Gamma
        {
            get { return gamma; }
            set { gamma = value; RaisePropertyChanged(() => gamma); }
        }

        /// <summary>
        /// 伪彩色
        /// </summary>
        public Color PseudoColor
        {
            get { return pseudoColor; }
            set { pseudoColor = value; RaisePropertyChanged(() => PseudoColor); }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ScanChannelModel Initialize(int id)
        {
            switch (id)
            {
                case 0:
                    return new ScanChannelModel()
                    {
                        ID = CHANNEL405,
                        Collapsed = false,
                        Name = "通道1",
                        LaserPower = Settings.Default.ScanChannel405LaserPower,
                        LaserColor = Settings.Default.ScanChannel561LaserColor,
                        LaserWaveLength = "405nm",
                        Activated = Settings.Default.ScanChannel405Activated,
                        Gain = Settings.Default.ScanChannel405Gain,
                        Offset = Settings.Default.ScanChannel405Offset,
                        Gamma = Settings.Default.ScanChannel405Gamma,
                        PseudoColor = Settings.Default.ScanChannel405PseudoColor
                    };
                case 1:
                    return new ScanChannelModel()
                    {
                        ID = CHANNEL488,
                        Collapsed = false,
                        Name = "通道2",
                        LaserPower = Settings.Default.ScanChannel488LaserPower,
                        LaserColor = Settings.Default.ScanChannel488LaserColor,
                        LaserWaveLength = "488nm",
                        Activated = Settings.Default.ScanChannel488Activated,
                        Gain = Settings.Default.ScanChannel488Gain,
                        Offset = Settings.Default.ScanChannel488Offset,
                        Gamma = Settings.Default.ScanChannel488Gamma,
                        PseudoColor = Settings.Default.ScanChannel488PseudoColor
                    };
                case 2:
                    return new ScanChannelModel()
                    {
                        ID = CHANNEL561,
                        Collapsed = false,
                        Name = "通道3",
                        LaserPower = Settings.Default.ScanChannel561LaserPower,
                        LaserColor = Settings.Default.ScanChannel561LaserColor,
                        LaserWaveLength = "561nm",
                        Activated = Settings.Default.ScanChannel561Activated,
                        Gain = Settings.Default.ScanChannel561Gain,
                        Offset = Settings.Default.ScanChannel561Offset,
                        Gamma = Settings.Default.ScanChannel561Gamma,
                        PseudoColor = Settings.Default.ScanChannel561PseudoColor
                    };
                case 3:
                    return new ScanChannelModel()
                    {
                        ID = CHANNEL640,
                        Collapsed = false,
                        Name = "通道4",
                        LaserPower = Settings.Default.ScanChannel640LaserPower,
                        LaserColor = Settings.Default.ScanChannel640LaserColor,
                        LaserWaveLength = "640nm",
                        Activated = Settings.Default.ScanChannel640Activated,
                        Gain = Settings.Default.ScanChannel640Gain,
                        Offset = Settings.Default.ScanChannel640Offset,
                        Gamma = Settings.Default.ScanChannel640Gamma,
                        PseudoColor = Settings.Default.ScanChannel640PseudoColor
                    };
                default:
                    throw new ArgumentOutOfRangeException("ID Exception");
            }
        }

    }
}
