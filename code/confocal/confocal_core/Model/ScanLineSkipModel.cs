using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using confocal_core.Common;
using GalaSoft.MvvmLight;

namespace confocal_core.Model
{
    /// <summary>
    /// 跳行扫描使能更新事件委托
    /// </summary>
    /// <param name="lineSkipEnabled"></param>
    /// <returns></returns>
    public delegate API_RETURN_CODE LineSkipEnableChangedEventHandler(bool lineSkipEnabled);

    /// <summary>
    /// 跳行扫描参数更新事件委托
    /// </summary>
    /// <param name="lineSkip"></param>
    /// <returns></returns>
    public delegate API_RETURN_CODE LineSkipChangedEventHandler(ScanLineSkipModel lineSkip);

    /// <summary>
    /// 跳行扫描
    /// </summary>
    public class ScanLineSkipModel : ObservableObject
    {
        private int id;
        private string text;
        private int data;

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

        public int Data
        {
            get { return data; }
            set { this.data = value; RaisePropertyChanged(() => Data); }
        }

        public static List<ScanLineSkipModel> Initialize()
        {
            return new List<ScanLineSkipModel>()
            {
                new ScanLineSkipModel(){ ID = 0, Text = "2x", Data = 2 },
                new ScanLineSkipModel(){ ID = 1, Text = "4x", Data = 4 },
                new ScanLineSkipModel(){ ID = 2, Text = "8x", Data = 8 },
                new ScanLineSkipModel(){ ID = 3, Text = "16x", Data = 16 },
            };
        }

        public static ScanLineSkipModel Initialize(int id)
        {
            return Initialize().Where(p => p.ID == id).First();
        }

    }

}
