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
    public partial class frmAddWall : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();
        string ProjectId, Floor, Room;
        int max = 0;

        public frmAddWall(string sProjectId, string sFloor, string sRoom)
        {
            InitializeComponent();
            ProjectId = sProjectId;
            Floor = sFloor;
            Room = sRoom;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtWallName.Text == "")
            {
                MessageBox.Show("กรุณากรอกชื่อผนังด้วย", "แจ้งเตือน");
                return;
            }

            if (txtArea.Text == "")
            {
                MessageBox.Show("กรุณากรอก ข้อมูลพื้นที่ด้วย", "แจ้งเตือน");
                return;
            }

            if (txtPrice.Text == "")
            {
                MessageBox.Show("กรุณากรอกค่าก่อสร้างด้วย", "แจ้งเตือน");
                return;
            }

            if (txtPower.Text == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลค่าพลังงานด้วย", "แจ้งเตือน");
                return;
            }

            string strSQL = " INSERT INTO WallRoom ";
            strSQL += " (Project_id, Floor_id, Room_name, Wall_id, Wall_Name, WallType, Area, Direction, Price, Power) ";
            strSQL += " VALUES ('" + ProjectId + "', " + Floor + ", '" + Room + "', " + max + ", '" + txtWallName.Text + "', '"+ cboWallType.Text +"', '" + txtArea.Text + "', '" + cboDirection.Text + "', " + txtPrice.Text + ", " + txtPower.Text + ") ";
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

        private void frmAddWall_Load(object sender, EventArgs e)
        {
            DBClass.DBConnect.Connenct_DB();
            Load_Wall();
        }

        private void Load_Wall()
        {
            DataTable dt;
            string strSQL = " SELECT Wall_id, Wall_Name, WallType, Area, Direction, Price, Power FROM WallRoom ";
            strSQL += " WHERE Project_id = '" + ProjectId + "' AND Floor_id = " + Floor + " AND Room_name = '" + Room + "' ";
            dt = db.QueryDataTable(strSQL);

            max = dt.Rows.Count + 1;

            txtWallName.Text = "W" + max.ToString();
        }
    }
}
