using confocal_core.Common;
using confocal_core.Properties;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Model
{
    public class ScanPropertyBaseModel : ObservableObject
    {
        private int id;
        private string text;
        private bool isEnabled;

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

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; RaisePropertyChanged(() => IsEnabled); }
        }
    }

    public class ScanPropertyWithValueBaseModel<T> : ScanPropertyBaseModel
    {
        private T data;

        public T Data
        {
            get { return data; }
            set { this.data = value; RaisePropertyChanged(() => Data); }
        }
    }

}
