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
    public partial class frmEditSubWall : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();
        string ProjectId, Floor, Room, RoofId, SubWallid;

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (txtSubName.Text == "")
            {
                MessageBox.Show("กรุณากรอกชื่อหลังคาด้วย", "แจ้งเตือน");
                return;
            }

            if (txtArea.Text == "")
            {
                MessageBox.Show("กรุณากรอก ข้อมูลพื้นที่ด้วย", "แจ้งเตือน");
                return;
            }

            
            if (MessageBox.Show("คุณต้องการลบข้อมูลนี้ใช่หรือไม่", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string strSQL = " DELETE FROM WallSub ";
                strSQL += " WHERE Project_id = '" + ProjectId + "' AND Floor_id = " + Floor + " AND Room_name = '" + Room + "' and Wall_id = " + RoofId + " and SubWall_id = "+ SubWallid +" ";

                if (db.QueryExecuteNonQuery(strSQL))
                {
                    MessageBox.Show("ระบบทำการลบข้อมูลเรียบร้อย", "Success!!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("ไม่สามารถลบได้", "Success!!");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtSubName.Text == "")
            {
                MessageBox.Show("กรุณากรอกชื่อด้วย", "แจ้งเตือน");
                return;
            }

            if (txtArea.Text == "")
            {
                MessageBox.Show("กรุณากรอกขนาดพื่นที่ด้วย", "แจ้งเตือน");
                return;
            }
            
            string strSQL = " UPDATE WallSub SET ";
            strSQL += " SubWall_Name = '" + txtSubName.Text + "', ";
            strSQL += " Area = " + txtArea.Text + " ";
            strSQL += " WHERE Project_id = '" + ProjectId + "' AND Floor_id = " + Floor + " AND Room_name = '" + Room + "' and Wall_id = " + RoofId + " and SubWall_id = "+ SubWallid +" ";

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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public frmEditSubWall(string sProjectId, string sFloor, string sRoom, string sRoofId, string sSubWallId)
        {
            InitializeComponent();
            ProjectId = sProjectId;
            Floor = sFloor;
            Room = sRoom;
            RoofId = sRoofId;
            SubWallid = sSubWallId;
        }


        private void frmEditSubWall_Load(object sender, EventArgs e)
        {
            DBClass.DBConnect.Connenct_DB();
            Load_SubWall();
        }

        private void Load_SubWall()
        {
            DataTable dt;
            string strSQL = " SELECT * FROM WallSub ";
            strSQL += " WHERE Project_id = '" + ProjectId + "' AND Floor_id = " + Floor + " AND Room_name = '" + Room + "' ";
            strSQL += " AND Wall_id = " + RoofId + " AND SubWall_id = "+ SubWallid +" ";
            dt = db.QueryDataTable(strSQL);

            if(dt.Rows.Count >0)
            {
                txtSubName.Text = dt.Rows[0]["SubWall_Name"].ToString();
                txtArea.Text = dt.Rows[0]["Area"].ToString();
            }
           
        }
    }
}
