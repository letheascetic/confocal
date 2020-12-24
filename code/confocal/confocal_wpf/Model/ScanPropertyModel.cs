using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace confocal_wpf.Model
{
    public class ScanPropModelBase : ObservableObject
    {
        private int id;
        private string text;
        private bool isChecked;

        public int ID
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(() => ID); }
        }

        public string Text
        {
            get { return text; }
            set { text = value; RaisePropertyChanged(() => Text); }
        }
        
        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; RaisePropertyChanged(() => IsChecked); }
        }

    }

    public class ScanPropWithValueModelBase<T> : ScanPropModelBase
    {
        private T data;

        public T Data
        {
            get { return data; }
            set { this.data = value; RaisePropertyChanged(() => Data); }
        }
    }

    /// <summary>
    /// 扫描方向：单向、双向
    /// </summary>
    public class ScanDirectionModel : ScanPropModelBase
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        public static readonly int UNIDIRECTION = 0;
        public static readonly int BIDIRECTION = 1;
        ///////////////////////////////////////////////////////////////////////////////////////////
        
        public static List<ScanDirectionModel> Initialize()
        {
            return new List<ScanDirectionModel>()
            {
                new ScanDirectionModel(){ID = UNIDIRECTION, Text = "单向", IsChecked = true},
                new ScanDirectionModel(){ID = BIDIRECTION, Text = "双向", IsChecked = false}
            };
        }

    }

    /// <summary>
    /// 扫描模式：Galvano 或 Resonant
    /// </summary>
    public class ScanModeModel : ScanPropModelBase
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        public static readonly int RESONANT = 0;
        public static readonly int GALVANO = 1;
        ///////////////////////////////////////////////////////////////////////////////////////////

        public static List<ScanModeModel> Initialize()
        {
            return new List<ScanModeModel>()
            {
                new ScanModeModel(){ID = RESONANT, Text = "Resonant", IsChecked = false},
                new ScanModeModel(){ID = GALVANO, Text = "Galvano", IsChecked = true}
            };
        }

    }

    /// <summary>
    /// 扫描头数量：双镜 or 三镜
    /// </summary>
    public class ScannerHeadModel : ScanPropModelBase
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        public static readonly int TWO_SCANNERS = 0;
        public static readonly int THREE_SCANNERS = 1;
        ///////////////////////////////////////////////////////////////////////////////////////////

        public static List<ScannerHeadModel> Initialize()
        {
            return new List<ScannerHeadModel>()
            {
                new ScannerHeadModel(){ID = TWO_SCANNERS, Text = "双镜"},
                new ScannerHeadModel(){ID = THREE_SCANNERS, Text = "三镜"}
            };
        }

    }

    /// <summary>
    /// 扫描采集模式：实时 捕获 发现
    /// </summary>
    public class ScanAcquisitionModeModel : ScanPropModelBase
    {

    }

    /// <summary>
    /// 跳行扫描
    /// </summary>
    public class ScanLineSkippingModel : ObservableObject
    {
        private int id;
        private string text;
        private int value;

        public int ID
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(() => ID); }
        }

        public string Text
        {
            get { return text; }
            set { text = value; RaisePropertyChanged(() => Text); }
        }

        public int Value 
        {
            get { return value; }
            set { this.value = value; RaisePropertyChanged(() => Value); }
        }

    }

    /// <summary>
    /// 扫描行操作：None Averaging Integrate
    /// </summary>
    public class ScanLineOptionModel : ObservableObject
    {
        private int id;
        private string text;
        private int value;

        public int ID
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(() => ID); }
        }

        public string Text
        {
            get { return text; }
            set { text = value; RaisePropertyChanged(() => Text); }
        }

        public int Value
        {
            get { return value; }
            set { this.value = value; RaisePropertyChanged(() => Value); }
        }
    }

    /// <summary>
    /// 扫描像素
    /// </summary>
    public class ScanPixelsModel : ScanPropWithValueModelBase<int>
    {
        public static List<ScanPixelsModel> Initialize()
        {
            return new List<ScanPixelsModel>()
            {
                //new ScanPixelsModel(){ ID = 0, IsChecked = false, Text = "64", Value = 64 },
                //new ScanPixelsModel(){ ID = 1, IsChecked = false, Text = "128", Value = 128 },
                new ScanPixelsModel(){ ID = 2, IsChecked = false, Text = "256", Data = 256 },
                new ScanPixelsModel(){ ID = 3, IsChecked = true, Text = "512", Data = 512 },
                new ScanPixelsModel(){ ID = 4, IsChecked = false, Text = "1024", Data = 1024 },
                new ScanPixelsModel(){ ID = 5, IsChecked = false, Text = "2048", Data = 2048 },
                new ScanPixelsModel(){ ID = 6, IsChecked = false, Text = "4096", Data = 4096 }
            };
        }
    }

    /// <summary>
    /// 扫描停留时间
    /// </summary>
    public class ScanPixelDwellModel : ScanPropWithValueModelBase<int>
    {
        public static List<ScanPixelDwellModel> Initialize()
        {
            return new List<ScanPixelDwellModel>()
            {
                new ScanPixelDwellModel(){ ID = 0, IsChecked = false, Text = "2", Data = 2 },
                new ScanPixelDwellModel(){ ID = 1, IsChecked = false, Text = "4", Data = 4 },
                new ScanPixelDwellModel(){ ID = 2, IsChecked = false, Text = "6", Data = 6 },
                new ScanPixelDwellModel(){ ID = 3, IsChecked = true, Text = "8", Data = 8 },
                new ScanPixelDwellModel(){ ID = 4, IsChecked = false, Text = "10", Data = 10 },
                new ScanPixelDwellModel(){ ID = 5, IsChecked = false, Text = "20", Data = 20 },
                new ScanPixelDwellModel(){ ID = 6, IsChecked = false, Text = "50", Data = 50 },
                new ScanPixelDwellModel(){ ID = 7, IsChecked = false, Text = "100", Data = 100 }
            };
        }
    }

    public class ScanChannel : ObservableObject
    {
        private int id;
        private string name;
        
        private double laserPower;
        private Color laserColor;
        private string laserWaveLength;

        private bool activated;
        private double pinHole;
        private double gain;
        private int offset;
        private double gamma;
        private Color pseudoColor;

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

    }



}
