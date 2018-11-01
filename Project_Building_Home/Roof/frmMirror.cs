using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Building_Home.Roof
{
    public partial class frmMirror : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();
        string ProjectId;
        string MirrorId;
        public frmMirror(string sProjectId)
        {
            InitializeComponent();
            ProjectId = sProjectId;
        }

        private void frmMirror_Load(object sender, EventArgs e)
        {
            Load_Data();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtMatId.Text == "")
            {
                MessageBox.Show("กรุณากรอกรหัสวัสดุด้วย !!", "แจ้งเตือน");
                return;
            }

            if(txtMatName.Text == "")
            {
                MessageBox.Show("กรุณากรอกชื่อวัสดุด้วย !!", "แจ้งเตือน");
                return;
            }

            if(txtMass.Text == "")
            {
                txtMass.Text = "0";
            }

            if (txtUValue.Text == "")
            {
                txtUValue.Text = "0";
            }

            if (txtSHGC.Text == "")
            {
                txtSHGC.Text = "0";
            }

            if (txtPrice.Text == "")
            {
                txtPrice.Text = "0";
            }

            double um = Convert.ToDouble(txtUValue.Text) / 5.68;

            string strSQL = " UPDATE Build_Select SET SHGC = '" + txtSHGC.Text + "', ";
            strSQL += " UM = '" + um.ToString("0.00") +"' ";
            strSQL += " WHERE PID = '" + ProjectId + "'  ";

            //if (MirrorId == "")
            //{
            //    strSQL = " INSERT INTO Mirror ";
            //    strSQL += " (ProjectId, MatId, MatName, Mass, UValue, SHGC, Price) ";
            //    strSQL += " VALUES('" + ProjectId + "', '" + txtMatId.Text + "', '" + txtMatName.Text + "', '" + txtMass.Text + "', '" + txtUValue.Text + "', '" + txtSHGC.Text + "', '" + txtPrice.Text + "') ";
            //}
            //else
            //{
            //    strSQL = " UPDATE Mirror SET ";
            //    strSQL += " MatId = '" + txtMatId.Text + "', ";
            //    strSQL += " MatName = '" + txtMatName.Text + "', ";
            //    strSQL += " Mass = '" + txtMass.Text + "', ";
            //    strSQL += " UValue = '" + txtUValue.Text + "', ";
            //    strSQL += " SHGC = '" + txtSHGC.Text + "', ";
            //    strSQL += " Price = '" + txtPrice.Text + "' ";
            //    strSQL += " WHERE id = "+ MirrorId + " ";
            //}

            if (db.QueryExecuteNonQuery(strSQL))
            {
                MessageBox.Show("ระบบบันทึกข้อมูลเรียบร้อย !!", "แจ้งเตือน");
                this.Close();
            }
        }

        private void Load_Data()
        {
            DataTable dt;
            string strSQL = " SELECT MID, MNAME, Density / 1000 AS Mass, [U-value], SHGC, Cost FROM Material WHERE Material_type = 'วัสดุกระจก' ";

            dt = db.QueryDataTable(strSQL);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex == -1)
            {
                return;
            }
            
            txtMatId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtMatName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtMass.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtUValue.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtSHGC.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear_Data();
        }

        private void Clear_Data()
        {
            txtMatId.Text = "";
            txtMatName.Text = "";
            txtMass.Text = "";
            txtUValue.Text = "";
            txtSHGC.Text = "";
            txtPrice.Text = "";
            MirrorId = "";
        }
    }
}
