using confocal_test.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_test.ViewModel
{
    public class AOIViewModel : ViewModelBase
    {
        private int cid;
        public int CID
        {
            get { return cid; }
            set { cid = value; RaisePropertyChanged(() => CID); }
        }

        private int aid;
        public int AID
        {
            get { return aid; }
            set { aid = value; RaisePropertyChanged(() => AID); }
        }

        public AOIViewModel()
        {
            CID = CommonModel.GetCommonModel().ID;
            AID = AModel.GetAModel().ID;
        }

    }
}
