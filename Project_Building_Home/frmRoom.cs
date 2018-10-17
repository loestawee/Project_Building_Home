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
            frmAddRoof frm = new frmAddRoof();
            frm.ShowDialog();
            Load_Roof();
        }

        private void Load_Roof()
        {

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
        }

        private void Load_Room()
        {
            DataTable dt;
            string strSQL = " SELECT Project_id, Floor_id, Room_id, Room_type, Room_name, People, Lamp, AC, Air, Area, Direction FROM Room WHERE (Project_id = '" + ProjectId + "') AND (Floor_id = " + Floor + ") AND(Room_name = '" + RoomName + "') ";
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
