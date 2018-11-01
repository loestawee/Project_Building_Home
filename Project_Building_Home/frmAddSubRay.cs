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
    public partial class frmAddSubRay : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();
        string ProjectId, Floor, Room, RoofId;
        int max = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtRayName.Text == "")
            {
                MessageBox.Show("กรุณากรอกชื่อด้วย", "แจ้งเตือน");
                return;
            }

            if (txtArea.Text == "")
            {
                MessageBox.Show("กรุณากรอกขนาดพื่นที่ด้วย", "แจ้งเตือน");
                return;
            }

            if (txtOject.Text == "")
            {
                MessageBox.Show("กรุณากรอก วัสดุด้วย", "แจ้งเตือน");
                return;
            }

            if (txtSC.Text == "")
            {
                MessageBox.Show("กรุณากรอก SC ด้วย", "แจ้งเตือน");
                return;
            }

            if (txtPrice.Text == "")
            {
                MessageBox.Show("กรุณากรอก ราคา ด้วย", "แจ้งเตือน");
                return;
            }

            if (txtPower.Text == "")
            {
                MessageBox.Show("กรุณากรอกหน่วยพลังงานด้วย", "แจ้งเตือน");
                return;
            }



            string strSQL = " INSERT INTO WallRaySun ";
            strSQL += " (Project_id, Floor_id, Room_name, Wall_id, RaySun_id, RaySun_Name, Area, Material, SC, Price, Power) ";
            strSQL += " VALUES ('" + ProjectId + "', " + Floor + ", '" + Room + "', '" + RoofId + "', '" + max + "', '" + txtRayName.Text + "', " + txtArea.Text + ", '"+ txtOject.Text +"', "+ txtSC.Text +", " + txtPrice.Text + ", "+ txtPower.Text +") ";
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

        public frmAddSubRay(string sProjectId, string sFloor, string sRoom, string sRoofId)
        {
            InitializeComponent();
            ProjectId = sProjectId;
            Floor = sFloor;
            Room = sRoom;
            RoofId = sRoofId;
        }

        private void frmAddSubRay_Load(object sender, EventArgs e)
        {
            DBClass.DBConnect.Connenct_DB();
            Load_SubRay();
        }

        private void Load_SubRay()
        {
            DataTable dt;
            string strSQL = " SELECT * FROM WallRaySun ";
            strSQL += " WHERE Project_id = '" + ProjectId + "' AND Floor_id = " + Floor + " AND Room_name = '" + Room + "' AND Wall_id = " + RoofId + " ";
            dt = db.QueryDataTable(strSQL);

            max = dt.Rows.Count + 1;

            txtRayName.Text = "v" + max.ToString();
        }
    }
}
