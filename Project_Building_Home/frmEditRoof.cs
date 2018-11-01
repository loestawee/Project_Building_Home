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
    public partial class frmEditRoof : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();
        string ProjectId, Floor, Room, Roof_id;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        public frmEditRoof(string sProjectId, string sFloor, string sRoom, string sRoof_id)
        {
            InitializeComponent();
            ProjectId = sProjectId;
            Floor = sFloor;
            Room = sRoom;
            Roof_id = sRoof_id;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (txtRoofName.Text == "")
            {
                MessageBox.Show("กรุณากรอกชื่อหลังคาด้วย", "แจ้งเตือน");
                return;
            }

            if (txtArea.Text == "")
            {
                MessageBox.Show("กรุณากรอก ข้อมูลพื้นที่ด้วย", "แจ้งเตือน");
                return;
            }

            if (txtSlope.Text == "")
            {
                MessageBox.Show("กรุณากรอกค่าความลาดด้วย", "แจ้งเตือน");
                return;
            }

            if (txtPower.Text == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลค่าพลังงานด้วย", "แจ้งเตือน");
                return;
            }

            if (MessageBox.Show("คุณต้องการลบข้อมูลนี้ใช่หรือไม่", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string strSQL = " DELETE FROM Roof ";
                strSQL += " WHERE Project_id = '" + ProjectId + "' AND Floor_id = " + Floor + " AND Room_name = '" + Room + "' and Roof_id = " + Roof_id + " ";

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

        private void frmEditRoof_Load(object sender, EventArgs e)
        {
            DBClass.DBConnect.Connenct_DB();
            Load_Roof();
        }

        private void Load_Roof()
        {
            DataTable dt;
            string strSQL = " SELECT Roof_id, Roof_Name, Area, Direction, Slope, Power FROM Roof ";
            strSQL += " WHERE Project_id = '" + ProjectId + "' AND Floor_id = " + Floor + " AND Room_name = '" + Room + "' and Roof_id = " + Roof_id +" ";
            dt = db.QueryDataTable(strSQL);

            if(dt.Rows.Count > 0)
            {
                txtRoofName.Text = dt.Rows[0]["Roof_Name"].ToString();
                txtArea.Text = dt.Rows[0]["Area"].ToString();
                cboDirection.Text = dt.Rows[0]["Direction"].ToString();
                txtSlope.Text = dt.Rows[0]["Slope"].ToString();
                txtPower.Text = dt.Rows[0]["Power"].ToString();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtRoofName.Text == "")
            {
                MessageBox.Show("กรุณากรอกชื่อหลังคาด้วย", "แจ้งเตือน");
                return;
            }

            if (txtArea.Text == "")
            {
                MessageBox.Show("กรุณากรอก ข้อมูลพื้นที่ด้วย", "แจ้งเตือน");
                return;
            }

            if (txtSlope.Text == "")
            {
                MessageBox.Show("กรุณากรอกค่าความลาดด้วย", "แจ้งเตือน");
                return;
            }

            if (txtPower.Text == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลค่าพลังงานด้วย", "แจ้งเตือน");
                return;
            }
            
            string strSQL = " UPDATE Roof SET ";
            strSQL += " Roof_Name = '"+ txtRoofName.Text +"', ";
            strSQL += " Area = "+ txtArea.Text +", ";
            strSQL += " Direction = '" + cboDirection.Text + "', ";
            strSQL += " Slope = "+ txtSlope.Text + ", ";
            strSQL += " Power = "+ txtPower.Text +" ";
            strSQL += " WHERE Project_id = '" + ProjectId + "' AND Floor_id = " + Floor + " AND Room_name = '" + Room + "' and Roof_id = " + Roof_id + " ";

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
    }
}
