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
    public partial class frmAddPlan : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();
        string ProjectId, Floor;
        int max = 0;

        public frmAddPlan(string sProjectId, string sFloor)
        {
            InitializeComponent();
            ProjectId = sProjectId;
            Floor = sFloor;
        }

        private void frmAddPlan_Load(object sender, EventArgs e)
        {
            DBClass.DBConnect.Connenct_DB();
            Load_Room();
        }

        private void Load_Room()
        {
            DataTable dt;
            string RoomType = "";

            if (cboType.Text == "ใช่")
            {
                RoomType = "A";
            }
            else
            {
                RoomType = "O";
            }

            dt = db.QueryDataTable(" SELECT Room_name FROM Room WHERE (LEFT(Room_name, 1) = '" + RoomType + "') AND Project_id = '"+ ProjectId +"' AND Floor_id = "+ Floor +" ");
            max = dt.Rows.Count + 1;
            txtRoomName.Text = RoomType + max.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtRoomName.Text == "")
            {
                MessageBox.Show("กรุณากรอกชื่อห้องด้วย","แจ้งเตือน");
                return;
            }

            if (txtArea.Text == "")
            {
                MessageBox.Show("กรุณากรอก ข้อมูลพื้นที่ด้วย", "แจ้งเตือน");
                return;
            }

            if (txtPeople.Text == "")
            {
                MessageBox.Show("กรุณากรอก จำนวนคนด้วย", "แจ้งเตือน");
                return;
            }

            if (txtLamp.Text == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลแสงสว่างด้วย", "แจ้งเตือน");
                return;
            }

            if (txtAC.Text == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลอุปกรณ์ด้วย", "แจ้งเตือน");
                return;
            }

            if (txtAir.Text == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลเครื่องปรับอากาศด้วย", "แจ้งเตือน");
                return;
            }


            string strSQL = " INSERT INTO Room ";
            strSQL += " (Project_id, Floor_id, Room_id, Room_type, Room_name, People, Lamp, AC, Air, Area, Direction) ";
            strSQL += " VALUES ('" + ProjectId + "', " + Floor + ", " + max + ", '" + cboType.Text + "', '" + txtRoomName.Text + "', " + txtPeople.Text + ", " + txtLamp.Text + ", ";
            strSQL += " "+ txtAC.Text +", "+ txtAir.Text +", "+ txtArea.Text +", '"+ cboDirection.Text +"') ";
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

        private void cboType_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void cboDirection_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_Room();
        }
    }
}
