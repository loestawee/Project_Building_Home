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
    public partial class frmSelectMaterial : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();

        string CLMID;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            txtMid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtMName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

            txtPart.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtType.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

            txtSHGC.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            txtSC.Text = "";
            txtU.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
        }

        public frmSelectMaterial(string sCLMID)
        {
            InitializeComponent();
            CLMID = sCLMID;
        }

        private void frmSelectMaterial_Load(object sender, EventArgs e)
        {
            Load_Material();
        }

        private void Load_Material()
        {
            DataTable dt;
            string strSQL = " SELECT * FROM Material ";
            dt = db.QueryDataTable(strSQL);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
        }

        private void btnWallSave_Click(object sender, EventArgs e)
        {
            if (txtU.Text == "")
            {
                txtU.Text = "0";
            }

            if (txtSHGC.Text == "")
            {
                txtSHGC.Text = "0";
            }

            if (txtSC.Text == "")
            {
                MessageBox.Show("กรณาระบุค่า SC ด้วย !!", "แจ้งเตือน");
                return;
            }
            
            string strSQL = " UPDATE Envelop SET ";
            strSQL += " Ref_id = " + txtMid.Text + ", ";
            strSQL += " Name = '" + txtMName.Text + "', ";
            strSQL += " U = " + txtU.Text + ", ";
            strSQL += " SHGC = " + txtSHGC.Text + ", ";
            strSQL += " SC = " + txtSC.Text + " ";
            strSQL += " WHERE EID = " + CLMID + " ";

            if (db.QueryExecuteNonQuery(strSQL))
            {
                MessageBox.Show("ระบบบันทึกข้อมูลเรียบร้อย !!", "Alert");
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            frmMaterial frm = new frmMaterial();
            frm.ShowDialog();
        }
    }
}