using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pogodynka_v3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            StripMenuList defaultController = new StripMenuList( new WinFormChart(ref chart1) );
            RssReporter.InitModels();
            defaultController.BindToMenuAndInit(chooseCityToolStripMenuItem);
        }

    }
}
