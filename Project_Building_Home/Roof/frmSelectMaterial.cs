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
    public partial class frmSelectMaterial : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();

        string CLMID;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex == -1)
            {
                return;
            }

            txtMid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtMName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

            txtPart.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtType.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

            txtK.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtDensity.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString(); 
            txtR.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtCost.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
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
            if (txtThickness.Text == "" || txtThickness.Text == "0")
            {
                MessageBox.Show("กรณาระบุค่า Thickness ด้วย !!", "แจ้งเตือน");
                return;
            }

            if (txtK.Text == "")
            {
                txtK.Text = "0";
            }

            if (txtDensity.Text == "")
            {
                txtDensity.Text = "0";
            }

            if (txtR.Text == "")
            {
                txtR.Text = "0";
            }

            if (txtCost.Text == "")
            {
                txtCost.Text = "0";
            }
            
            string strSQL = " UPDATE Composit SET ";
            strSQL += " MID = "+ txtMid.Text +", ";
            strSQL += " MNAME = '" + txtMName.Text + "', ";
            strSQL += " Thickness = " + txtThickness.Text + ", ";
            strSQL += " K = " + txtK.Text + ", ";
            strSQL += " Density = " + txtDensity.Text + ", ";
            strSQL += " R = " + txtR.Text + ", ";
            strSQL += " Price = " + txtCost.Text + " ";
            strSQL += " WHERE CLMID = "+ CLMID +" ";

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
