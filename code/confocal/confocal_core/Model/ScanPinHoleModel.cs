using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Model
{
    /// <summary>
    /// 小孔孔径
    /// </summary>
    public class ScanPinHoleModel : ObservableObject
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private int id;                     // 通道ID
        private string name;                // 通道名
        private int size;                   // 孔径

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
        /// 孔径
        /// </summary>
        public int Size
        {
            get { return size; }
            set { size = value; RaisePropertyChanged(() => Size); }
        }

        public static List<ScanPinHoleModel> Initialize()
        {
            return new List<ScanPinHoleModel>()
            {
                new ScanPinHoleModel(){ ID = 0, Name = "405nm", Size = 1},
                new ScanPinHoleModel(){ ID = 1, Name = "488nm", Size = 1},
                new ScanPinHoleModel(){ ID = 2, Name = "561nm", Size = 1},
                new ScanPinHoleModel(){ ID = 3, Name = "640nm", Size = 1},
            };
        }

    }
}
