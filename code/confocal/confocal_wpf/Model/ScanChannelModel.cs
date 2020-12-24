using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace confocal_wpf.Model
{
    /// <summary>
    /// 扫描通道
    /// </summary>
    public class ScanChannelModel : ObservableObject
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private int id;                     // 通道ID
        private string name;                // 通道名

        private double laserPower;          // 激光功率
        private Color laserColor;           // 激光颜色
        private string laserWaveLength;     // 激光波长

        private bool activated;             // 通道激活状态
        private double pinHole;             // 小孔孔径
        private double gain;                // 增益
        private int offset;                 // 偏置
        private double gamma;               // 伽马
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

        /// <summary>
        /// 小孔孔径
        /// </summary>
        public double PinHole
        {
            get { return pinHole; }
            set { pinHole = value; RaisePropertyChanged(() => PinHole); }
        }

        /// <summary>
        /// 增益
        /// </summary>
        public double Gain
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
        public double Gamma
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

        public static List<ScanChannelModel> Initialize()
        {
            return new List<ScanChannelModel>()
            {
                new ScanChannelModel(){ID = 0, Name = "405nm",
                    LaserPower = 2.0, LaserColor = Color.MediumPurple, LaserWaveLength = "405nm",
                    Activated = false, PinHole = 2.0, Gain = 50, Offset = 0, Gamma = 1.0, PseudoColor = Color.MediumPurple},
                new ScanChannelModel(){ID = 0, Name = "488nm",
                    LaserPower = 2.0, LaserColor = Color.DarkCyan, LaserWaveLength = "488nm",
                    Activated = false, PinHole = 2.0, Gain = 50, Offset = 0, Gamma = 1.0, PseudoColor = Color.DarkCyan},
                new ScanChannelModel(){ID = 0, Name = "561nm",
                    LaserPower = 2.0, LaserColor = Color.YellowGreen, LaserWaveLength = "561nm",
                    Activated = false, PinHole = 2.0, Gain = 50, Offset = 0, Gamma = 1.0, PseudoColor = Color.YellowGreen},
                new ScanChannelModel(){ID = 0, Name = "640nm",
                    LaserPower = 2.0, LaserColor = Color.MediumVioletRed, LaserWaveLength = "640nm",
                    Activated = false, PinHole = 2.0, Gain = 50, Offset = 0, Gamma = 1.0, PseudoColor = Color.MediumVioletRed},
            };
        }
    }
}
