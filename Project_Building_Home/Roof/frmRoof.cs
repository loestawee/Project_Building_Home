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
    public partial class frmRoof : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();
        int TabSelect;
        string ProjectId;

        public frmRoof(string sProjectId,int sTab)
        {
            InitializeComponent();
            TabSelect = sTab;
            ProjectId = sProjectId;
        }
        
        private void frmRoof_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = TabSelect;
            Load_List_Wall();
            Load_List_Roof();
        }

        private void Load_List_Wall()
        {
            DataTable dt;
            string strSQL = " SELECT * FROM Composit_List_Wall ";
            dt = db.QueryDataTable(strSQL);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
        }

        private void Load_List_Roof()
        {
            DataTable dt;
            string strSQL = " SELECT * FROM Composit_List_Roof ";
            dt = db.QueryDataTable(strSQL);
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = dt;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex == -1)
            {
                return;
            }

            txtWallID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtWallName.Text  = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtWallUValue.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtWallMass.Text  = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtWallPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnWallSave_Click(object sender, EventArgs e)
        {

            if (txtWallName.Text == "")
            {
                MessageBox.Show("กรุณาเลือกวัสดุประกอบด้วย !!", "แจ้งเตือน");
                return;
            }

            
            double uw = Convert.ToDouble(txtWallUValue.Text) / 5.68;

            string strSQL = " UPDATE Build_Select SET UW = '" + uw + "' ";
            strSQL += " WHERE PID = '" + ProjectId + "'  ";

            if (db.QueryExecuteNonQuery(strSQL))
            {
                MessageBox.Show("ระบบบันทึกข้อมูลเรียบร้อย !!", "แจ้งเตือน");
                this.Close();
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            txtRoofId.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtRoofName.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtRoofUValue.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtRoofMass.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtRoofPrice.Text = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnRoofSave_Click(object sender, EventArgs e)
        {

            if (txtRoofName.Text == "")
            {
                MessageBox.Show("กรุณาเลือกวัสดุประกอบด้วย !!", "แจ้งเตือน");
                return;
            }


            double ur = Convert.ToDouble(txtRoofUValue.Text) / 5.68;

            string strSQL = " UPDATE Build_Select SET ur = '" + ur + "' ";
            strSQL += " WHERE PID = '" + ProjectId + "'  ";

            if (db.QueryExecuteNonQuery(strSQL))
            {
                MessageBox.Show("ระบบบันทึกข้อมูลเรียบร้อย !!", "แจ้งเตือน");
                this.Close();
            }
        }

        private void btnMaterial_Click(object sender, EventArgs e)
        {
            Roof.frmAddComposit frm = new Roof.frmAddComposit(tabControl1.SelectedIndex);
            frm.ShowDialog();
        }
    }
}
