using C1.Win.C1Ribbon;
using confocal_core.Common;
using confocal_core.ViewModel;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace confocal_ui.View
{
    public partial class FormScanParas : C1RibbonForm
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////

        private ScanParasViewModel mScanParasVM;

        public FormScanParas()
        {
            InitializeComponent();
        }

        private void Initialize()
        {
            mScanParasVM = new ScanParasViewModel();


        }

        private void RegisterEvents()
        {
            
        }

        private void SetDataBindings()
        {
            // tbxOutputSampleRate.DataBindings.Add("Text", Sequence, "OutputSampleRate");
            // tbxOutputRoundTripPerFrame.DataBindings.Add("Text", mScanParasVM, "OutputSampleCountPerRoundTrip");
        }

        private void FormParasLoad(object sender, EventArgs e)
        {
            Initialize();
            SetDataBindings();
            RegisterEvents();
        }
    }
}
