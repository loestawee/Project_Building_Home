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
    public partial class frmEditPlan : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();
        string ProjectId, Floor, Roomid, RoomName;

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtRoomName.Text == "")
            {
                MessageBox.Show("กรุณากรอกชื่อห้องด้วย", "แจ้งเตือน");
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


            string strSQL = " UPDATE Room ";
            strSQL += " SET People = " + txtPeople.Text + ", Lamp = " + txtLamp.Text + ", ";
            strSQL += " AC = "+ txtAC.Text +", Air = "+ txtAir.Text +", Area = "+ txtArea.Text +", Direction = '"+ cboDirection.Text +"' ";
            strSQL += " WHERE (Room.Project_id = '"+ ProjectId +"') ";
            strSQL += " AND (Room.Floor_id = "+ Floor +") AND (Room.Room_name = '"+ txtRoomName.Text +"') ";
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

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (txtRoomName.Text == "")
            {
                MessageBox.Show("กรุณากรอกชื่อห้องด้วย", "แจ้งเตือน");
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

            if (MessageBox.Show("คุณต้องการลบข้อมูลห้องนี้ใช่หรือไม่", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                string strSQL = " DELETE FROM Room ";
                strSQL += " WHERE (Room.Project_id = '" + ProjectId + "') ";
                strSQL += " AND (Room.Floor_id = " + Floor + ") AND (Room.Room_name = '" + txtRoomName.Text + "') ";


                if (db.QueryExecuteNonQuery(strSQL))
                {
                    MessageBox.Show("ระบบทำการ ลบ ข้อมูลห้องเรียบร้อย", "Success!!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("ไม่สามารถลบข้อมูลห้องได้", "Success!!");
                }
            }
            else
            {
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public frmEditPlan(string sProjectId, string sFloor, string sId, string sName)
        {
            InitializeComponent();
            ProjectId = sProjectId;
            Floor = sFloor;
            Roomid = sId;
            RoomName = sName;
        }

        private void frmEditPlan_Load(object sender, EventArgs e)
        {
            DBClass.DBConnect.Connenct_DB();
            Load_Room();
        }

        private void Load_Room()
        {
            DataTable dt;
            string strSQL = " SELECT Project_id, Floor_id, Room_id, Room_type, Room_name, People, Lamp, AC, Air, Area, Direction FROM Room WHERE (Project_id = '" + ProjectId + "') AND (Floor_id = " + Floor + ") AND(Room_name = '" + RoomName + "') ";
            dt = db.QueryDataTable(strSQL);

            if(dt.Rows.Count > 0)
            {
                cboType.Text = dt.Rows[0]["Room_type"].ToString();
                txtRoomName.Text = dt.Rows[0]["Room_name"].ToString();
                cboDirection.Text = dt.Rows[0]["Direction"].ToString();
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
