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
    public partial class frmAddSubRoof : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();
        string ProjectId, Floor, RoomName, RoofId;
        int max = 0;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtMatName.Text == "")
            {
                MessageBox.Show("กรุณากรอกชื่อวัสดุด้วย", "แจ้งเตือน");
                return;
            }

            if (txtUnitPower.Text == "")
            {
                MessageBox.Show("กรุณากรอก U หน่วยการใช้พลังงานด้วย", "แจ้งเตือน");
                return;
            }

            if (txtPrice.Text == "")
            {
                MessageBox.Show("กรุณากรอก ราคา ด้วย", "แจ้งเตือน");
                return;
            }

            if (txtDensity.Text == "")
            {
                MessageBox.Show("กรุณากรอกค่าความหนาแน่น ด้วย", "แจ้งเตือน");
                return;
            }

            if (txtPower.Text == "")
            {
                MessageBox.Show("กรุณากรอกค่าพลังงานด้วย", "แจ้งเตือน");
                return;
            }

            string strSQL = " INSERT INTO SubRoof ";
            strSQL += " (Project_id, Floor_id, Room_name, Roof_id, Sub_id, Sub_Name, Unit_Power, Density, Power) ";
            strSQL += " VALUES ('" + ProjectId + "', " + Floor + ", '" + RoomName + "', '"+ RoofId +"', '" + max + "', '" + txtMatName.Text + "', " + txtUnitPower.Text + ", " + txtDensity.Text + ", ";
            strSQL += " " + txtPower.Text + ") ";
            if (db.QueryExecuteNonQuery(strSQL))
            {
                MessageBox.Show("ระบบทำการบันทึกข้อมูลห้องเรียบร้อย", "Success!!");
                this.Close();
            }
            else
            {
                MessageBox.Show("ไม่สามารถบันทึกข้อมูลห้องได้", "Success!!");
            }
        }

        public frmAddSubRoof(string sProjectId, string sFloor, string sRoom_name, string sRoofId)
        {
            InitializeComponent();
            ProjectId = sProjectId;
            Floor = sFloor;
            RoomName = sRoom_name;
            RoofId = sRoofId;
        }

        private void Load_Roof()
        {
            DataTable dt;
            string strSQL = " SELECT * FROM SubRoof ";
            strSQL += " WHERE Project_id = '" + ProjectId + "' AND Floor_id = " + Floor + " AND Room_name = '" + RoomName + "' AND Roof_id = "+ RoofId +" ";
            dt = db.QueryDataTable(strSQL);

            max = dt.Rows.Count + 1;

            txtMatName.Text = "CL" + max.ToString();
        }

        private void frmAddSubRoof_Load(object sender, EventArgs e)
        {
            DBClass.DBConnect.Connenct_DB();
            Load_Roof();
        }
    }
}
