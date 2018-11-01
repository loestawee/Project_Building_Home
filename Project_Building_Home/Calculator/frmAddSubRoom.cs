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
    public partial class frmAddSubRoom : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();
        string ProjectId, RoomId, RoomName;

        public frmAddSubRoom(string sProjectId, string sRoomId, string sRoomName)
        {
            InitializeComponent();
            ProjectId = sProjectId;
            RoomId = sRoomId;
            RoomName = sRoomName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            frmAddSub frm = new frmAddSub(RoomId, RoomName);
            frm.ShowDialog();
            Load_Data();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex == -1)
            {
                return;
            }

            string id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            if(e.ColumnIndex == 8)
            {
                Calculator.frmSelectMaterial frm = new Calculator.frmSelectMaterial(id);
                frm.ShowDialog();
                Load_Data();
            }
        }

        private void frmAddSubRoom_Load(object sender, EventArgs e)
        {
            txtRID.Text = RoomId;
            txtRName.Text = RoomName;
            Load_Data();
        }
        
        private void Load_Data()
        {
            DataTable dt;

            string strSQL = " SELECT * FROM Envelop WHERE RID = "+ RoomId +" ";
            dt = db.QueryDataTable(strSQL);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
        }
    }
}
