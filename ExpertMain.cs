using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPQR_Session4_1_9
{
    public partial class ExpertMain : Form
    {
        public ExpertMain()
        {
            InitializeComponent();
        }

        private void btnUpdateExpertRecords_Click(object sender, EventArgs e)
        {
            Hide();
            (new UpdateExpert()).ShowDialog();
            Close();
        }
    }
}
