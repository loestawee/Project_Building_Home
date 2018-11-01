using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Building_Home.Calculator
{
    public partial class frmAddSub : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();
        string RoomId, RoomName;
        public frmAddSub(string sRoomId, string sRoomName)
        {
            InitializeComponent();
            RoomId = sRoomId;
            RoomName = sRoomName;
        }

        private void frmAddSub_Load(object sender, EventArgs e)
        {
            txtRID.Text = RoomId;
            txtRName.Text = RoomName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(cboDirection.Text == "")
            {
                MessageBox.Show("กรุณาเลือก ทิศ ด้วย !!", "แจ้งเตือน");
                return;
            }

            if (cboType.Text == "")
            {
                MessageBox.Show("กรุณาเลือก ประเภท ด้วย !!", "แจ้งเตือน");
                return;
            }

            if (txtArea.Text == "")
            {
                MessageBox.Show("กรุณากรอกพื้นที่ ด้วย !!", "แจ้งเตือน");
                return;
            }

            string strSQL = " INSERT INTO Envelop (RID, Direction, Type, Area) VALUES ('"+ txtRID.Text +"', '"+ cboDirection.Text +"', '"+ cboType.Text +"', '"+ txtArea.Text +"'); ";

            if (db.QueryExecuteNonQuery(strSQL))
            {
                MessageBox.Show("ระบบบันทึกข้อมูลเรียบร้อย !!", "แจ้งเตือน");
                this.Close();
            }
        }
    }
}
