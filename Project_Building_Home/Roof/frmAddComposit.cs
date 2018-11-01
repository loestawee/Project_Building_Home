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
    public partial class frmAddComposit : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();
        int TabSelect;
        string EditStatus = "";

        public frmAddComposit(int sTab)
        {
            InitializeComponent();
            TabSelect = sTab;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddComposit_Load(object sender, EventArgs e)
        {
            EditStatus = "";
            btnWallDel.Visible = false;
            btnRoofDel.Visible = false;

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

        private void btnWallSave_Click(object sender, EventArgs e)
        {
            if (txtWallName.Text == "")
            {
                MessageBox.Show("กรุณากรอกวัสดุด้วย !!", "แจ้งเตือน");
                return;
            }

            string strSQL = "";
            if (EditStatus == "")
            {
                strSQL = " INSERT INTO Composit_List (CLNAME, Type) VALUES ('" + txtWallName.Text + "', 'ผนัง'); ";
            }
            else
            {
                strSQL = " UPDATE Composit_List SET CLNAME = '" + txtWallName.Text + "' WHERE CLID = '" + txtWallID.Text + "' ";
            }

            if (db.QueryExecuteNonQuery(strSQL))
            {
                MessageBox.Show("ระบบบันทึกข้อมูลเรียบร้อย !!", "แจ้งเตือน");

                EditStatus = "";
                btnWallDel.Visible = false;
                btnRoofDel.Visible = false;
                Load_List_Wall();
                Load_List_Roof();
            }
        }

        private void btnRoofSave_Click(object sender, EventArgs e)
        {
            if (txtRoofName.Text == "")
            {
                MessageBox.Show("กรุณากรอกวัสดุด้วย !!", "แจ้งเตือน");
                return;
            }

            string strSQL = "";
            if(EditStatus == "")
            {
                strSQL = " INSERT INTO Composit_List (CLNAME, Type) VALUES ('"+ txtRoofName.Text + "', 'หลังคา'); ";
            }
            else
            {
                strSQL = " UPDATE Composit_List SET CLNAME = '"+ txtRoofName.Text +"' WHERE CLID = '"+ txtRoofId.Text +"' ";
            }

            if (db.QueryExecuteNonQuery(strSQL))
            {
                MessageBox.Show("ระบบบันทึกข้อมูลเรียบร้อย !!", "แจ้งเตือน");

                EditStatus = "";
                btnWallDel.Visible = false;
                btnRoofDel.Visible = false;
                Load_List_Wall();
                Load_List_Roof();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            string CLID = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            string CNAME = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

            if (e.ColumnIndex == 5)
            {
                Roof.frmAddSubComposit frm = new Roof.frmAddSubComposit(CLID, CNAME, 0);
                frm.ShowDialog();
            }
            else
            {
                txtWallID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtWallName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                EditStatus = "Edit";
                btnWallDel.Visible = true;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            string CLID = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            string CNAME = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();

            if (e.ColumnIndex == 5)
            {
                Roof.frmAddSubComposit frm = new Roof.frmAddSubComposit(CLID, CNAME, 1);
                frm.ShowDialog();
            }
            else
            {
                txtRoofId.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtRoofName.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                EditStatus = "Edit";
                btnRoofDel.Visible = true;
            }
        }

        private void btnWallDel_Click(object sender, EventArgs e)
        {
            if (txtWallID.Text == "")
            {
                MessageBox.Show("กรุณากรอกวัสดุด้วย !!", "แจ้งเตือน");
                return;
            }

            string strSQL = " DELETE FROM Composit_List WHERE CLID = "+ txtWallID.Text +" ";

            if (db.QueryExecuteNonQuery(strSQL))
            {
                MessageBox.Show("ระบบลบข้อมูลเรียบร้อย !!", "แจ้งเตือน");

                EditStatus = "";
                btnWallDel.Visible = false;
                btnRoofDel.Visible = false;
                Load_List_Wall();
                Load_List_Roof();
            }
        }

        private void btnRoofDel_Click(object sender, EventArgs e)
        {
            if (txtRoofId.Text == "")
            {
                MessageBox.Show("กรุณากรอกวัสดุด้วย !!", "แจ้งเตือน");
                return;
            }

            string strSQL = " DELETE FROM Composit_List WHERE CLID = " + txtRoofId.Text + " ";

            if (db.QueryExecuteNonQuery(strSQL))
            {
                MessageBox.Show("ระบบลบข้อมูลเรียบร้อย !!", "แจ้งเตือน");

                EditStatus = "";
                btnWallDel.Visible = false;
                btnRoofDel.Visible = false;
                Load_List_Wall();
                Load_List_Roof();
            }
        }

        private void btnWallNew_Click(object sender, EventArgs e)
        {
            EditStatus = "";
            txtWallID.Text = "";
            txtWallName.Text = "";
            btnWallDel.Visible = false;
            btnRoofDel.Visible = false;
            Load_List_Wall();
            Load_List_Roof();
        }

        private void btnRoofNew_Click(object sender, EventArgs e)
        {
            EditStatus = "";
            txtRoofId.Text = "";
            txtRoofName.Text = "";
            btnWallDel.Visible = false;
            btnRoofDel.Visible = false;
            Load_List_Wall();
            Load_List_Roof();
        }
    }
}
