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
    public partial class frmAddRoof : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();
        string ProjectId, Floor, Room;
        int max = 0;

        public frmAddRoof(string sProjectId, string sFloor, string sRoom)
        {
            InitializeComponent();
            ProjectId = sProjectId;
            Floor = sFloor;
            Room = sRoom;
        }

        private void frmAddRoof_Load(object sender, EventArgs e)
        {
            DBClass.DBConnect.Connenct_DB();
            Load_Roof();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboDirection_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void Load_Roof()
        {
            DataTable dt;
            string strSQL = " SELECT Roof_id, Roof_Name, Area, Direction, Slope, Power FROM Roof ";
            strSQL += " WHERE Project_id = '" + ProjectId + "' AND Floor_id = " + Floor + " AND Room_name = '" + Room + "' ";
            dt = db.QueryDataTable(strSQL);

            max = dt.Rows.Count + 1;

            txtRoofName.Text = "R" + max.ToString();
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
            
            string strSQL = " INSERT INTO Roof ";
            strSQL += " (Project_id, Floor_id, Room_name, Roof_id, Roof_Name, Area, Direction, Slope, Power) ";
            strSQL += " VALUES ('" + ProjectId + "', " + Floor + ", '"+ Room +"', " + max + ", '" + txtRoofName.Text + "', '" + txtArea.Text + "', '" + cboDirection.Text + "', " + txtSlope.Text + ", " + txtPower.Text + ") ";
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
