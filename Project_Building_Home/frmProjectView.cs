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
    public partial class frmProjectView : Form
    {
        public frmProjectView()
        {
            InitializeComponent();
        }

        DBClass.DBConnect db = new DBClass.DBConnect();
        string sProjectId = "";

        private void frmProjectView_Load(object sender, EventArgs e)
        {
            DBClass.DBConnect.Connenct_DB();
            Load_Project();
        }

        public string Exit_Value()
        {
            return sProjectId;
        }

        private void Load_Project()
        {
            DataTable dt;
            dt = db.QueryDataTable(" SELECT Project_id as [Project ID], Project_Name as [Project Name] FROM Build_Project ");

            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Exit_Value();
            this.Close();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            sProjectId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }
    }
}
