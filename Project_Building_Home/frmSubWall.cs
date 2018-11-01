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
    public partial class frmSubWall : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();
        string ProjectId, Floor, Room, RoofId;

        public frmSubWall(string sProjectId, string sFloor, string sRoom, string sRoofId)
        {
            InitializeComponent();
            ProjectId = sProjectId;
            Floor = sFloor;
            Room = sRoom;
            RoofId = sRoofId;
        }

        private void frmSubWall_Load(object sender, EventArgs e)
        {
            DBClass.DBConnect.Connenct_DB();
            Load_Data();
            Load_WallSub();
            Load_WallRay();
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            frmAddSubWall frm = new frmAddSubWall(ProjectId, Floor, Room, RoofId);
            frm.ShowDialog();
            Load_WallSub();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex == -1)
            {
                return;
            }

            string SubWall_id = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            string SubWall_name = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            string Area = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

            if (e.ColumnIndex == 1)
            {
                frmEditSubWall frm = new frmEditSubWall(ProjectId, Floor, Room, RoofId, SubWall_id);
                frm.ShowDialog();
                Load_WallSub();
            }
            else if (e.ColumnIndex == 0)
            {
                frmMatSubWall frm = new frmMatSubWall(ProjectId, Floor, Room, RoofId, SubWall_id, SubWall_name, Area);
                frm.ShowDialog();
                Load_WallSub();
            }


        }

        private void btnAddWall_Click(object sender, EventArgs e)
        {
            frmAddSubRay frm = new frmAddSubRay(ProjectId, Floor, Room, RoofId);
            frm.ShowDialog();
            Load_WallRay();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            string SubWall_id = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
            string SubWall_name = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();

            if (e.ColumnIndex == 1)
            {
                frmEditSubRay frm = new frmEditSubRay(ProjectId,Floor,Room, RoofId, SubWall_id);
                frm.ShowDialog();
                Load_WallRay();
            }
            else if (e.ColumnIndex == 0)
            {
                frmAddRayMat frm = new frmAddRayMat();
                frm.ShowDialog();
                Load_WallRay();
            }
        }

        private void Load_Data()
        {
            DataTable dt;
            string strSQL = " SELECT * FROM WallRoom  ";
            strSQL += " WHERE (Project_id = '" + ProjectId + "') AND (Floor_id = " + Floor + ") AND (Room_name = '" + Room + "') ";
            strSQL += " AND (Wall_id = "+ RoofId +") ";
            dt = db.QueryDataTable(strSQL);

            if (dt.Rows.Count > 0)
            {
                txtWallName.Text = dt.Rows[0]["Wall_Name"].ToString();
                cboType.Text = dt.Rows[0]["WallType"].ToString();
                txtArea.Text = dt.Rows[0]["Area"].ToString();
                txtDirection.Text = dt.Rows[0]["Direction"].ToString();
                txtPrice.Text = dt.Rows[0]["Price"].ToString();
                txtPower.Text = dt.Rows[0]["Power"].ToString();
            }

        }

        private void Load_WallSub()
        {
            DataTable dt;
            string strSQL = " SELECT * FROM WallSub ";
            strSQL += " WHERE (Project_id = '" + ProjectId + "') AND (Floor_id = " + Floor + ") AND (Room_name = '" + Room + "') ";
            strSQL += " AND Wall_id = "+ RoofId + " ";

            dt = db.QueryDataTable(strSQL);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
        }

        private void Load_WallRay()
        {
            DataTable dt;
            string strSQL = " SELECT * FROM WallRaySun ";
            strSQL += " WHERE (Project_id = '" + ProjectId + "') AND (Floor_id = " + Floor + ") AND (Room_name = '" + Room + "') ";
            strSQL += " AND Wall_id = " + RoofId + " ";

            dt = db.QueryDataTable(strSQL);
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = dt;
        }
    }
}
