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
    public partial class frmRoof : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();
        string ProjectId, Floor, Room, RoofId;

        public frmRoof(string sProjectId, string sFloor, string sRoom, string sRoofId)
        {
            InitializeComponent();
            ProjectId = sProjectId;
            Floor = sFloor;
            Room = sRoom;
            RoofId = sRoofId;
        }
        
        private void frmRoof_Load(object sender, EventArgs e)
        {
            DBClass.DBConnect.Connenct_DB();
            Load_Data();
            Load_Roof();
        }

        private void btnAddRoof_Click(object sender, EventArgs e)
        {
            frmAddSubRoof frm = new frmAddSubRoof(ProjectId, Floor, Room, RoofId);
            frm.ShowDialog();
            Load_Roof();
        }

        private void Load_Roof()
        {
            DataTable dt;
            string strSQL = " SELECT * FROM SubRoof ";
            strSQL += " WHERE Project_id = '" + ProjectId + "' and Floor_id = " + Floor + " and Room_name = '" + Room + "' ";
            strSQL += " and Roof_id = " + RoofId + " ";

            dt = db.QueryDataTable(strSQL);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
        }

        private void Load_Data()
        {
            DataTable dt;
            string strSQL = " SELECT Roof_id, Roof_Name, Area, Direction, Slope, Power FROM Roof ";
            strSQL += " WHERE Project_id = '"+ ProjectId +"' and Floor_id = "+ Floor +" and Room_name = '"+ Room +"' ";
            strSQL += " and Roof_id = "+ RoofId +" ";
            dt = db.QueryDataTable(strSQL);

            if(dt.Rows.Count > 0)
            {
                txtRoofName.Text = dt.Rows[0]["Roof_Name"].ToString();
                txtArea.Text = dt.Rows[0]["Area"].ToString();
                txtSlope.Text = dt.Rows[0]["Slope"].ToString();
                cboDirection.Text = dt.Rows[0]["Direction"].ToString();
                txtPower.Text = dt.Rows[0]["Power"].ToString();
            }


        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
    }
}
