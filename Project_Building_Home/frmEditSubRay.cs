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
    public partial class frmEditSubRay : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();
        string ProjectId, Floor, Room, RoofId, RaySubId;

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (txtRayName.Text == "")
            {
                MessageBox.Show("กรุณากรอกชื่อด้วย", "แจ้งเตือน");
                return;
            }

            if (txtArea.Text == "")
            {
                MessageBox.Show("กรุณากรอก ข้อมูลพื้นที่ด้วย", "แจ้งเตือน");
                return;
            }

            if (txtPrice.Text == "")
            {
                MessageBox.Show("กรุณากรอกราคาด้วย", "แจ้งเตือน");
                return;
            }

            if (txtPower.Text == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลค่าพลังงานด้วย", "แจ้งเตือน");
                return;
            }

            if (MessageBox.Show("คุณต้องการลบข้อมูลนี้ใช่หรือไม่", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string strSQL = " DELETE FROM WallRaySun ";
                strSQL += " WHERE Project_id = '" + ProjectId + "' AND Floor_id = " + Floor + " AND Room_name = '" + Room + "' and Wall_id = " + RoofId + " AND RaySun_id = "+ RaySubId +" ";

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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtRayName.Text == "")
            {
                MessageBox.Show("กรุณากรอกชื่อด้วย", "แจ้งเตือน");
                return;
            }

            if (txtArea.Text == "")
            {
                MessageBox.Show("กรุณากรอก ข้อมูลพื้นที่ด้วย", "แจ้งเตือน");
                return;
            }

            if (txtPrice.Text == "")
            {
                MessageBox.Show("กรุณากรอกราคาด้วย", "แจ้งเตือน");
                return;
            }

            if (txtPower.Text == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลค่าพลังงานด้วย", "แจ้งเตือน");
                return;
            }

            string strSQL = " UPDATE WallRaySun SET ";
            strSQL += " RaySun_name = '" + txtRayName.Text + "', ";
            strSQL += " Area = " + txtArea.Text + ", ";
            strSQL += " Material = '" + txtOject.Text + "', ";
            strSQL += " SC = " + txtSC.Text + ", ";
            strSQL += " Price = " + txtPrice.Text + ", ";
            strSQL += " Power = " + txtPower.Text + " ";
            strSQL += " WHERE Project_id = '" + ProjectId + "' AND Floor_id = " + Floor + " AND Room_name = '" + Room + "' and Wall_id = " + RoofId + " AND RaySun_id = "+ RaySubId +" ";

            if (db.QueryExecuteNonQuery(strSQL))
            {
                MessageBox.Show("ระบบทำการบันทึกข้อมูลเรียบร้อย", "Success!!");
                this.Close();
            }
            else
            {
                MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้", "Success!!");
            }
        }

        public frmEditSubRay(string sProjectId, string sFloor, string sRoom, string sRoofId, string sRaySun_id)
        {
            InitializeComponent();
            ProjectId = sProjectId;
            Floor = sFloor;
            Room = sRoom;
            RoofId = sRoofId;
            RaySubId = sRaySun_id;
        }

        private void frmEditSubRay_Load(object sender, EventArgs e)
        {
            DBClass.DBConnect.Connenct_DB();
            Load_SubRay();
        }

        private void Load_SubRay()
        {
            DataTable dt;
            string strSQL = " SELECT * FROM WallRaySun ";
            strSQL += " WHERE Project_id = '" + ProjectId + "' AND Floor_id = " + Floor + " AND Room_name = '" + Room + "' AND Wall_id = " + RoofId + " and RaySun_id = " + RaySubId +" ";
            dt = db.QueryDataTable(strSQL);
                
            if(dt.Rows.Count > 0)
            {
                txtRayName.Text = dt.Rows[0]["RaySun_name"].ToString();
                txtArea.Text = dt.Rows[0]["Area"].ToString();
                txtOject.Text = dt.Rows[0]["Material"].ToString();
                txtPower.Text = dt.Rows[0]["Power"].ToString();
                txtPrice.Text = dt.Rows[0]["Price"].ToString();
                txtSC.Text = dt.Rows[0]["SC"].ToString();
            }
           
        }
    }
}
