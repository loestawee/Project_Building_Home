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
    public partial class frmMaterial : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();

        public frmMaterial()
        {
            InitializeComponent();
        }

        private void frmMaterial_Load(object sender, EventArgs e)
        {
            Clear_Data();
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex == -1)
            {
                return;
            }

            txtMid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtMName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPart.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            cboType.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtR.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtK.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtU.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtDensity.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtHeat.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            txtSHGC.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            txtCost.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
            btnDel.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(txtMid.Text == "")
            {
                MessageBox.Show("กรุณาเลือกรายการก่อน !!", "แจ้งเตือน");
                return;
            }

            string strSQL = " DELETE FROM Material WHERE MID = "+  txtMid.Text +"  ";
            if (db.QueryExecuteNonQuery(strSQL))
            {
                MessageBox.Show("ระบบทำการลบข้อมูลเรียบร้อย !!", "แจ้งเตือน");
                Load_Material();
                Clear_Data();
            }
        }

        private void Clear_Data()
        {
            txtMid.Text = "";
            txtMName.Text = "";
            txtPart.Text = "";
            cboType.Text = "---เลือกประเภท--";
            txtR.Text = "";
            txtK.Text = "";
            txtU.Text = "";
            txtDensity.Text = "";
            txtHeat.Text = "";
            txtSHGC.Text = "";
            txtCost.Text = "";

            btnDel.Visible = false;
        }

        private void btnWallSave_Click(object sender, EventArgs e)
        {
            if(txtMName.Text == "")
            {
                MessageBox.Show("กรณากรอกชื่อวัสดุด้วย !!", "แจ้งเตือน");
                return;
            }

            if (txtPart.Text == "")
            {
                MessageBox.Show("กรณากรอก Part ด้วย !!", "แจ้งเตือน");
                return;
            }

            if (cboType.Text == "---เลือกประเภท--")
            {
                MessageBox.Show("กรณากรอกเลือกประเภทด้วย !!", "แจ้งเตือน");
                return;
            }

            if(txtR.Text == "")
            {
                txtR.Text = "0";
            }

            if (txtK.Text == "")
            {
                txtK.Text = "0";
            }

            if (txtU.Text == "")
            {
                txtU.Text = "0";
            }

            if (txtR.Text == "")
            {
                txtR.Text = "0";
            }

            if (txtHeat.Text == "")
            {
                txtHeat.Text = "0";
            }

            if (txtSHGC.Text == "")
            {
                txtSHGC.Text = "0";
            }

            if (txtCost.Text == "")
            {
                txtCost.Text = "0";
            }

            string strSQL = "";
            if(txtMid.Text == "")
            {
                strSQL = " INSERT INTO Material  (MNAME, part, Material_type, [R-value], [k-value], [U-value], Density, [Heat Capacity], SHGC, Cost) ";
                strSQL += " VALUES ('" + txtMName.Text + "', '" + txtPart.Text + "', '" + cboType.Text + "', ";
                strSQL += " '" + txtR.Text + "', '" + txtK.Text + "', '" + txtU.Text + "', '" + txtDensity.Text + "', ";
                strSQL += " '"+ txtHeat.Text +"', '"+ txtSHGC.Text +"', '"+ txtCost.Text +"') ";
            }
            else
            {
                strSQL = " UPDATE Material ";
                strSQL += " SET MNAME = '" + txtMName.Text + "', part = '" + txtPart.Text + "', ";
                strSQL += " Material_type = '" + cboType.Text + "', [R-value] = '" + txtR.Text + "', ";
                strSQL += " [k-value] = '" + txtK.Text + "', [U-value] = '" + txtU.Text + "', ";
                strSQL += " Density = '" + txtDensity.Text + "', ";
                strSQL += " [Heat Capacity] = '" + txtHeat.Text +"', SHGC = '"+ txtSHGC.Text +"', Cost = '"+ txtCost.Text +"' ";
                strSQL += " WHERE MID = "+ txtMid.Text +" ";
            }

            if (db.QueryExecuteNonQuery(strSQL))
            {
                MessageBox.Show("ระบบบันทึกข้อมูลเรียบร้อย !!", "Alert");
                Load_Material();
                Clear_Data();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
