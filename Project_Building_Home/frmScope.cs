using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Building_Home
{
    public partial class frmScope : Form
    {
        public frmScope()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmHome frm = new frmHome();
            frm.Show();
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            frmTitle frm = new frmTitle();
            frm.Show();
            this.Close();
        }
    }
}
