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
    public partial class frmAddSubComposit : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();
        string CLID, CNAME, CLMID;
        int TabSelect;
        public frmAddSubComposit(string sCLID, string sCNAME, int sTab)
        {
            InitializeComponent();
            CLID = sCLID;
            CNAME = sCNAME;
            TabSelect = sTab;
        }

        private void frmAddSubComposit_Load(object sender, EventArgs e)
        {
            Load_List_Wall();
            btnDel.Visible = false;
            
        }

        private void btnWallSave_Click(object sender, EventArgs e)
        {
            string strSQL = " INSERT INTO Composit (CLID, Name_Composit) VALUES ('"+ CLID +"', '"+ CNAME +"'); ";
            if (db.QueryExecuteNonQuery(strSQL))
            {
                MessageBox.Show("ระบบบันทึกข้อมูลเรียบร้อย !!", "Alert");
                Load_List_Wall();
                btnDel.Visible = false;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            string strSQL = " INSERT INTO Composit (CLID, Name_Composit) VALUES ('" + CLID + "', '" + CNAME + "'); ";
            if (db.QueryExecuteNonQuery(strSQL))
            {
                MessageBox.Show("ระบบบันทึกข้อมูลเรียบร้อย !!", "Alert");
                Load_List_Wall();
                btnDel.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex == -1)
            {
                return;
            }

            CLMID = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            btnDel.Visible = true;

            if(e.ColumnIndex == 6)
            {
                Roof.frmSelectMaterial frm = new Roof.frmSelectMaterial(CLMID);
                frm.ShowDialog();
                Load_List_Wall();
            }
        }

        private void Load_List_Wall()
        {
            DataTable dt;
            string strSQL = " SELECT * FROM Composit WHERE CLID = "+ CLID +" ";
            dt = db.QueryDataTable(strSQL);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
        }
    }
}
