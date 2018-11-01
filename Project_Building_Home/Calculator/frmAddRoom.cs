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
    public partial class frmAddRoom : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();
        string ProjectId, ProjectName, Floor, Area, Air;

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtRName.Text == "")
            {
                MessageBox.Show("กรุณากรอกชื่อด้วย !!", "แจ้งเตือน");
                return;
            }

            if (txtArea.Text == "")
            {
                MessageBox.Show("กรุณากรอกพื้นที่ด้วย !!", "แจ้งเตือน");
                return;
            }

            if (txtUnit.Text == "")
            {
                MessageBox.Show("กรุณากรอกจำนวนผู้ใช้ด้วย !!", "แจ้งเตือน");
                return;
            }

            if (txtLamp.Text == "")
            {
                MessageBox.Show("กรุณากรอก อุปกรณ์แสงสว่าง ด้วย !!", "แจ้งเตือน");
                return;
            }

            if (txtAir.Text == "")
            {
                MessageBox.Show("กรุณากรอก เครื่องปรับอากาศ ด้วย !!", "แจ้งเตือน");
                return;
            }

            string strSQL = "";
            if(txtRID.Text == "")
            {
                strSQL = " INSERT INTO Room (RNAME, PID, AC, Area, Unit, Light, Aircon) ";
                strSQL += " VALUES('"+ txtRName.Text +"', '"+ ProjectId +"', "+ ck1.Checked +", '"+ txtArea.Text +"', '"+ txtUnit.Text +"', '"+ txtLamp.Text +"', '"+ txtAir.Text +"') ";
            }
            else
            {
                strSQL = " UPDATE Room ";
                strSQL += " SET RNAME = '" + txtRName.Text + "', ";
                strSQL += " AC = '+ ck1.Checked +', Area = '" + txtArea.Text + "', Unit = '" + txtUnit.Text + "', ";
                strSQL += " Light = '" + txtLamp.Text + "', Aircon = '" + txtAir.Text + "' ";
                strSQL += " WHERE  PID = '" + ProjectId + "' AND RID = "+ txtRID.Text +" ";
            }

            if (db.QueryExecuteNonQuery(strSQL))
            {
                MessageBox.Show("ระบบบันทึกข้อมูลเรียบร้อย !!", "แจ้งเตือน");
                Load_Data();
            }
        }

        private void Clear_Data()
        {
            txtRID.Text = "";
            txtRName.Text = "";
            txtArea.Text = "";
            txtAir.Text = "";
            txtLamp.Text = "";
            txtUnit.Text = "";
            btnDel.Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex == -1)
            {
                return;
            }

            btnDel.Visible = true;

            string RoomId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            string RoomName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            bool ck = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[2].Value);

            if (e.ColumnIndex == 7)
            {
                if (ck == true)
                {
                    Calculator.frmAddSubRoom frm = new Calculator.frmAddSubRoom(ProjectId, RoomId, RoomName);
                    frm.ShowDialog();
                }
                Load_Data();
            }
            else
            {
                txtRID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtRName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                ck1.Checked = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                txtArea.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtUnit.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtLamp.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtAir.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Clear_Data();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if(txtRID.Text  == "")
            {
                MessageBox.Show("กรณาเลือกรายการก่อน !!", "แจ้งเตือน");
                return;
            }

            string strSQL = " DELETE FROM Room WHERE RID = "+ txtRID.Text +" ";

            if (db.QueryExecuteNonQuery(strSQL))
            {
                db.QueryExecuteNonQuery(" DELETE FROM Envelop WHERE RID = '"+ txtRID.Text +"' ");
                MessageBox.Show("ระบบลบข้อมูลเรียบร้อย !!", "แจ้งเตือน");
                Load_Data();
            }

        }

        private void btnCalculator_Click(object sender, EventArgs e)
        {
            frmCalculator frm = new frmCalculator(ProjectId);
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public frmAddRoom(string sProjectId, string sProjectName, string sFloor, string sArea, string sAir)
        {
            InitializeComponent();
            ProjectId = sProjectId;
            ProjectName = sProjectName;
            Floor = sFloor;
            Area = sArea;
            Air = sAir;
        }

        private void frmAddRoom_Load(object sender, EventArgs e)
        {
            Load_Data();
        }

        private void Load_Data()
        {
            DataTable dt;

            string strSQL = " SELECT * FROM Room WHERE PID = '"+ ProjectId +"' ";
            dt= db.QueryDataTable(strSQL);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
        }
    }
}
