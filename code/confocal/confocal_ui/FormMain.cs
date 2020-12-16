using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace confocal_ui
{
    public partial class FormMain : Form
    {
        /************************************************************************************/
        private static readonly ILog Logger = log4net.LogManager.GetLogger("info");
        /************************************************************************************/

        public FormMain()
        {
            InitializeComponent();
        }

        private void InitControlers()
        {
            // version
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Text += version;
            Logger.Info(string.Format("get software version: [{0}]", version));
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            InitControlers();
        }
    }
}
