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
    public partial class frmHomeSelect : Form
    {
        string ProjectId, ProjectName, Floor, Area, Air;

        private void button1_Click(object sender, EventArgs e)
        {
            frmMain frm = new frmMain();
            frm.Show();
            this.Close();
        }

        private void frmHomeSelect_Resize(object sender, EventArgs e)
        {
            panel6.Location = new Point(
               this.ClientSize.Width / 2 - panel6.Size.Width / 2,
               this.ClientSize.Height / 2 - panel6.Size.Height / 2);
            panel6.Anchor = AnchorStyles.None;
        }

        public frmHomeSelect(string sProjectId, string sProjectName, string sFloor, string sArea, string sAir)
        {
            InitializeComponent();
            ProjectId = sProjectId;
            ProjectName = sProjectName;
            Floor = sFloor;
            Area = sArea;
            Air = sAir;
        }

        private void btnSelectPlan_Click(object sender, EventArgs e)
        {
            frmHouse frm = new frmHouse(ProjectId, ProjectName, Floor, Area, Air);
            frm.Show();
        }

        private void btnCalculator_Click(object sender, EventArgs e)
        {
            Calculator.frmAddRoom frm = new Calculator.frmAddRoom(ProjectId, ProjectName, Floor, Area, Air);
            frm.Show();
        }

        private void frmHomeSelect_Load(object sender, EventArgs e)
        {

        }
    }
}
