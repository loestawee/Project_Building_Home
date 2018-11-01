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
    public partial class frmPlan : Form
    {
        string ProjectId = "";
        int gFloor = 1;
        string ProjectName, Area, Air;

        DBClass.DBConnect db = new DBClass.DBConnect();

        public frmPlan(string sProject, int sFloor, string sProjectName, string sArea, string sAir)
        {
            InitializeComponent();
            ProjectId = sProject;
            gFloor = sFloor;
            ProjectName = sProjectName;
            Area = sArea;
            Air = sAir;
        }

        private void frmPlan_Load(object sender, EventArgs e)
        {
            DBClass.DBConnect.Connenct_DB();
            txtProjectName.Text = ProjectName;
            txtArea.Text = Area;
            txtAir.Text = Air;
            Load_Floor();
            Load_Data();
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            frmAddPlan frm = new frmAddPlan(ProjectId, cboFloor.Text.ToString());
            frm.ShowDialog();
            Load_Data();
        }

        private void cboFloor_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void cboDirection_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex == -1)
            {
                return;
            }

            string Room_id = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            string Room_name = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            string Room_type = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();

            if (e.ColumnIndex == 1)
            {
                frmEditPlan frm = new frmEditPlan(ProjectId, cboFloor.Text, Room_id, Room_name);
                frm.ShowDialog();
                Load_Data();
            }else if(e.ColumnIndex == 0)
            {
                if (Room_type != "ไม่ใช่")
                {
                    frmRoom frm = new frmRoom(ProjectId, cboFloor.Text, Room_id, Room_name);
                    frm.ShowDialog();
                    Load_Data();
                }
            }
        }

        private void cboFloor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_Data();
        }

        private void Load_Data()
        {
            DataTable dt;
            string strSQL = " SELECT Room_id, Room_type, Room_name, People, Lamp, AC, Air, Area, Direction FROM Room ";
            strSQL += " WHERE (Project_id = '"+ ProjectId +"') AND (Floor_id = "+ cboFloor.Text +") ";

            dt = db.QueryDataTable(strSQL);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
        }

        private void Load_Floor()
        {
            int floor = 1;
            for (int i = 0; i < gFloor; i++)
            {
                cboFloor.Items.Insert(i, floor.ToString());
                floor++;
            }
            cboFloor.SelectedIndex = 0;
        }
    }
}
