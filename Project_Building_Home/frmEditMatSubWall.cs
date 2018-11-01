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
    public partial class frmEditMatSubWall : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();
        string ProjectId, Floor, RoomName, WallId, WallSubId, SubId;

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (txtMatName.Text == "")
            {
                MessageBox.Show("กรุณากรอกชื่อด้วย", "แจ้งเตือน");
                return;
            }

            if (txtPower.Text == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลค่าพลังงานด้วย", "แจ้งเตือน");
                return;
            }

            if (MessageBox.Show("คุณต้องการลบข้อมูลนี้ใช่หรือไม่", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string strSQL = " DELETE FROM WallSubMat ";
                strSQL += " WHERE Project_id = '" + ProjectId + "' AND Floor_id = " + Floor + " AND Room_name = '" + RoomName + "' and Wall_id = " + WallId + " AND SubWall_id = " + WallSubId + " AND Sub_id = " + SubId + " ";

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtMatName.Text == "")
            {
                MessageBox.Show("กรุณากรอกชื่อด้วย", "แจ้งเตือน");
                return;
            }
            
            if (txtPower.Text == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลค่าพลังงานด้วย", "แจ้งเตือน");
                return;
            }

            string strSQL = " UPDATE WallSubMat SET ";
            strSQL += " Sub_Name = '" + txtMatName.Text + "', ";
            strSQL += " Unit_Power = " + txtUnitPower.Text + ", ";
            strSQL += " Price = '" + txtPrice.Text + "', ";
            strSQL += " Density = " + txtDensity.Text + ", ";
            strSQL += " Power = " + txtPower.Text + " ";
            strSQL += " WHERE Project_id = '" + ProjectId + "' AND Floor_id = " + Floor + " AND Room_name = '" + RoomName + "' and Wall_id = " + WallId + " AND SubWall_id = "+ WallSubId + " AND Sub_id = "+ SubId +" ";

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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public frmEditMatSubWall(string sProjectId, string sFloor, string sRoom_name, string sWallId, string sWallSubId, string sSubId)
        {
            InitializeComponent();
            ProjectId = sProjectId;
            Floor = sFloor;
            RoomName = sRoom_name;
            WallId = sWallId;
            WallSubId = sWallSubId;
            SubId = sSubId;
        }

        private void frmEditMatSubWall_Load(object sender, EventArgs e)
        {
            DBClass.DBConnect.Connenct_DB();
            Load_Data();
        }

        private void Load_Data()
        {
            DataTable dt;
            string strSQL = " SELECT * FROM WallSubMat ";
            strSQL += " WHERE Project_id = '" + ProjectId + "' AND Floor_id = " + Floor + " AND Room_name = '" + RoomName + "' AND Wall_id = " + WallId + " AND SubWall_id = " + WallSubId + " AND Sub_id = "+ SubId +" ";
            dt = db.QueryDataTable(strSQL);

            if (dt.Rows.Count > 0)
            {
                txtMatName.Text = dt.Rows[0]["Sub_Name"].ToString();
                txtDensity.Text = dt.Rows[0]["Density"].ToString();
                txtPower.Text = dt.Rows[0]["Power"].ToString();
                txtPrice.Text = dt.Rows[0]["Price"].ToString();
                txtUnitPower.Text = dt.Rows[0]["Unit_Power"].ToString();
            }
        }
    }
}
