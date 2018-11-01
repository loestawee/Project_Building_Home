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
    public partial class frmRoom : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();
        string ProjectId, Floor, Roomid, RoomName;

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            frmAddRoof frm = new frmAddRoof(ProjectId, Floor, txtRoomName.Text);
            frm.ShowDialog();
            Load_Roof();
            Load_Wall();
        }

        private void Load_Roof()
        {
            DataTable dt;
            string strSQL = " SELECT Roof_id, Roof_Name, Area, Direction, Slope, Power FROM Roof ";
            strSQL += " WHERE Project_id = '"+ ProjectId +"' AND Floor_id = "+ Floor +" AND Room_name = '"+ RoomName +"' ";
            dt = db.QueryDataTable(strSQL);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
        }

        private void Load_Wall()
        {
            DataTable dt;
            string strSQL = " SELECT Wall_id, Wall_Name, WallType, Area, Direction, Price, Power FROM WallRoom ";
            strSQL += " WHERE Project_id = '" + ProjectId + "' AND Floor_id = " + Floor + " AND Room_name = '" + RoomName + "' ";
            dt = db.QueryDataTable(strSQL);
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddRoof frm = new frmAddRoof(ProjectId, Floor, txtRoomName.Text);
            frm.ShowDialog();
            Load_Roof();
        }

        private void btnAddWall_Click(object sender, EventArgs e)
        {
            frmAddWall frm = new frmAddWall(ProjectId, Floor, txtRoomName.Text);
            frm.ShowDialog();
            Load_Wall();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex == -1)
            {
                return;
            }

            string Roof_id = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            string Roof_name = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

            if (e.ColumnIndex == 1)
            {
                frmEditRoof frm = new frmEditRoof(ProjectId, Floor, RoomName, Roof_id);
                frm.ShowDialog();
                Load_Roof();
            }
            else if (e.ColumnIndex == 0)
            {
                frmRoof frm = new frmRoof(ProjectId, Floor, RoomName, Roof_id);
                frm.ShowDialog();
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            string Roof_id = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
            string Roof_name = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();

            if (e.ColumnIndex == 1)
            {
                frmEditWall frm = new frmEditWall(ProjectId, Floor, RoomName, Roof_id);
                frm.ShowDialog();
                Load_Wall();
            }
            else if (e.ColumnIndex == 0)
            {
                frmSubWall frm = new frmSubWall(ProjectId, Floor, RoomName, Roof_id);
                frm.ShowDialog();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        public frmRoom(string sProjectId, string sFloor, string sId, string sName)
        {
            InitializeComponent();

            ProjectId = sProjectId;
            Floor = sFloor;
            Roomid = sId;
            RoomName = sName;
        }

        private void frmRoom_Load(object sender, EventArgs e)
        {
            DBClass.DBConnect.Connenct_DB();
            Load_Room();
            Load_Roof();
            Load_Wall();
        }

        private void Load_Room()
        {
            DataTable dt;
            string strSQL = " SELECT Project_id, Floor_id, Room_id, Room_type, Room_name, People, Lamp, AC, Air, Area, Direction FROM Room WHERE (Project_id = '" + ProjectId + "') AND (Floor_id = " + Floor + ") AND (Room_name = '" + RoomName + "') ";
            dt = db.QueryDataTable(strSQL);

            if (dt.Rows.Count > 0)
            {
                cboType.Text = dt.Rows[0]["Room_type"].ToString();
                txtRoomName.Text = dt.Rows[0]["Room_name"].ToString();
                txtArea.Text = dt.Rows[0]["Area"].ToString();
                txtPeople.Text = dt.Rows[0]["People"].ToString();
                txtLamp.Text = dt.Rows[0]["Lamp"].ToString();
                txtAC.Text = dt.Rows[0]["AC"].ToString();
                txtAir.Text = dt.Rows[0]["Air"].ToString();

                cboType.Enabled = false;
                txtRoomName.Enabled = false;
            }
        }
    }
}
