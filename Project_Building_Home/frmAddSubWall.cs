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
    public partial class frmAddSubWall : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();
        string ProjectId, Floor, Room, RoofId;
        int max = 0;

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

           

            string strSQL = " INSERT INTO WallSub ";
            strSQL += " (Project_id, Floor_id, Room_name, Wall_id, SubWall_id, SubWall_Name, Area) ";
            strSQL += " VALUES ('" + ProjectId + "', " + Floor + ", '" + Room + "', '" + RoofId + "', '" + max + "', '" + txtSubName.Text + "', " + txtArea.Text + ") ";
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

        public frmAddSubWall(string sProjectId, string sFloor, string sRoom, string sRoofId)
        {
            InitializeComponent();
            ProjectId = sProjectId;
            Floor = sFloor;
            Room = sRoom;
            RoofId = sRoofId;
        }

        private void frmAddSubWall_Load(object sender, EventArgs e)
        {
            DBClass.DBConnect.Connenct_DB();
            Load_SubWall();
        }

        private void Load_SubWall()
        {
            DataTable dt;
            string strSQL = " SELECT * FROM WallSub ";
            strSQL += " WHERE Project_id = '" + ProjectId + "' AND Floor_id = " + Floor + " AND Room_name = '" + Room + "' AND Wall_id = " + RoofId + " ";
            dt = db.QueryDataTable(strSQL);

            max = dt.Rows.Count + 1;

            txtSubName.Text = "ww" + max.ToString();
        }
    }
}
