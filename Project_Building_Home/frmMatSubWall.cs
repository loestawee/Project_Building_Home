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
    public partial class frmMatSubWall : Form
    {
        string ProjectId, Floor, Room, WallId, SubWallId, SubName, Area;

        private void btnAddRoof_Click(object sender, EventArgs e)
        {
            frmAddSubWallMat frm = new frmAddSubWallMat(ProjectId, Floor, Room, WallId, SubWallId);
            frm.ShowDialog();
            Load_SubMat();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex == -1)
            {
                return;
            }

            string Sub_id = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            string Sub_name = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            string Area = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

            if (e.ColumnIndex == 1)
            {
                frmEditMatSubWall frm = new frmEditMatSubWall(ProjectId, Floor, Room, WallId, SubWallId, Sub_id);
                frm.ShowDialog();
                Load_SubMat();
            }
            else if (e.ColumnIndex == 0)
            {
                //frmMatSubWall frm = new frmMatSubWall(ProjectId, Floor, Room, RoofId, SubWall_id, SubWall_name, Area);
                //frm.ShowDialog();
                //Load_SubMat();
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        DBClass.DBConnect db = new DBClass.DBConnect();
        public frmMatSubWall(string sProjectId, string sFloor, string sRoom, string sWallId, string sSubWallId, string sSubWallName, string sArea)
        {
            InitializeComponent();
            ProjectId = sProjectId;
            Floor = sFloor;
            Room = sRoom;
            WallId = sWallId;
            SubName = sSubWallName;
            Area = sArea;
            SubWallId = sSubWallId;
        }

        private void frmMatSubWall_Load(object sender, EventArgs e)
        {
            DBClass.DBConnect.Connenct_DB();

            txtSubWallName.Text = SubName;
            txtArea.Text = Area;
            Load_SubMat();
        }

        private void Load_SubMat()
        {
            DataTable dt;
            string strSQL = " SELECT * FROM WallSubMat ";
            strSQL += " WHERE Project_id = '" + ProjectId + "' and Floor_id = " + Floor + " and Room_name = '" + Room + "' ";
            strSQL += " and Wall_id = " + WallId + " AND SubWall_id = "+ SubWallId +" ";

            dt = db.QueryDataTable(strSQL);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
        }
        
    }
}
