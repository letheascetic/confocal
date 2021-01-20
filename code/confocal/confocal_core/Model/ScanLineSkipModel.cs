using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;

namespace confocal_core.Model
{
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

        public static ScanLineSkipModel LoadDefault()
        {
            return new ScanLineSkipModel() { ID = 0, Text = "2x", Data = 2 };
        }

    }

}
