using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class ScanPropWithValueModelBase : ScanPropModelBase
    {
        private int value;

        public int Value
        {
            get { return value; }
            set { this.value = value; RaisePropertyChanged(() => Value); }
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
    public class ScanPixelsMode : ObservableObject
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
    /// 扫描停留时间
    /// </summary>
    public class ScanPixelDwellMode : ObservableObject
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

}
