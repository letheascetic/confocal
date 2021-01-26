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
            tbxOutputSampleCountPerRoundTrip.TextChanged += ScanParasChanged;
        }

        private void SetDataBindings()
        {
            tbxOutputSampleRate.DataBindings.Add("Text", mScanParasVM.Sequence, "OutputSampleRate");
            tbxOutputSampleCountPerRoundTrip.DataBindings.Add("Text", mScanParasVM.Sequence, "OutputSampleCountPerRoundTrip");
            tbxOutputRoundTripPerFrame.DataBindings.Add("Text", mScanParasVM.Sequence, "OutputRoundTripCountPerFrame");
            tbxOutputSampleCountPerFrame.DataBindings.Add("Text", mScanParasVM.Sequence, "OutputSampleCountPerFrame");

            tbxInputSampleRate.DataBindings.Add("Text", mScanParasVM.Sequence, "InputSampleRate");
            tbxInputSampleCountPerRoundTrip.DataBindings.Add("Text", mScanParasVM.Sequence, "InputSampleCountPerRoundTrip");
            tbxInputRoundTripCountPerFrame.DataBindings.Add("Text", mScanParasVM.Sequence, "InputRoundTripCountPerFrame");
            tbxInputSampleCountPerFrame.DataBindings.Add("Text", mScanParasVM.Sequence, "InputSampleCountPerFrame");
            tbxInputSampleCountPerPixel.DataBindings.Add("Text", mScanParasVM.Sequence, "InputSampleCountPerPixel");
            tbxInputSampleCountPerAcquisition.DataBindings.Add("Text", mScanParasVM.Sequence, "InputSampleCountPerAcquisition");
            tbxInputPixelCountPerAcquisition.DataBindings.Add("Text", mScanParasVM.Sequence, "InputPixelCountPerAcquisition");
            tbxInputRoundTripCountPerAcquisition.DataBindings.Add("Text", mScanParasVM.Sequence, "InputRoundTripCountPerAcquisition");
            tbxInputAcquisitionCountPerFrame.DataBindings.Add("Text", mScanParasVM.Sequence, "InputAcquisitionCountPerFrame");

            tbxFPS.DataBindings.Add("Text", mScanParasVM.Sequence, "FPS");
            tbxFrameTime.DataBindings.Add("Text", mScanParasVM.Sequence, "FrameTime");
        }

        private void FormParasLoad(object sender, EventArgs e)
        {
            Initialize();
            RegisterEvents();
            SetDataBindings();
        }

        private void ScanParasChanged(object sender, EventArgs e)
        {
            mScanParasVM.UpdateChartValues();
            chart.Series[0].Points.DataBindXY(mScanParasVM.TimeValues, mScanParasVM.XGalvoValues);
            chart.Series[1].Points.DataBindXY(mScanParasVM.TimeValues, mScanParasVM.TriggerValues);
            chart.Update();
        }
        
    }
}
