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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DBClass.DBConnect db = new DBClass.DBConnect();
        string ProjectStatus = "";
        string ProjectId = "";

        private void Form1_Load(object sender, EventArgs e)
        {
            DBClass.DBConnect.Connenct_DB();
            ProjectId = Get_Max();
            Load_Project();
        }

        private void Load_Project()
        {
            DataTable dt;

            dt = db.QueryDataTable(" SELECT * FROM Build_Project WHERE Project_id = '" + ProjectId + "' ");

            if (dt.Rows.Count > 0)
            {
                txtProjectName.Text = dt.Rows[0]["Project_Name"].ToString();
                cboLocation.Text = dt.Rows[0]["Location"].ToString();
                cboType.Text = dt.Rows[0]["Project_Type"].ToString();
                txtFloor.Text = dt.Rows[0]["Floor"].ToString();
                txtArea.Text = dt.Rows[0]["Area"].ToString();
                txtAir.Text = dt.Rows[0]["Aircondition"].ToString();

                //label5.Visible = true;
                //label6.Visible = true;
                //txtArea.Visible = true;
                //txtAir.Visible = true;
                //label8.Visible = true;
                //label9.Visible = true;
            }
            else
            {
                //label5.Visible = false;
                //label6.Visible = false;
                //txtArea.Visible = false;
                //txtAir.Visible = false;
                //label8.Visible = false;
                //label9.Visible = false;
            }
        }

        private string Get_Max()
        {
            string sProjectId = "";
            string mProjectId = "";
            int MaxId = 0;
            string YYMM = DateTime.Now.ToString("yyyyMM");
            DataTable dt;

            dt = db.QueryDataTable(" SELECT TOP 1 Project_id as Id FROM Build_Project Order by Project_id DESC ");

            if (dt.Rows.Count > 0)
            {
                mProjectId = dt.Rows[0]["Id"].ToString().Substring(7, 6);
                MaxId = Convert.ToInt32(mProjectId) + 1;
                sProjectId = "P" + YYMM + MaxId.ToString("000000");
            }
            else
            {
                sProjectId = "P" + YYMM + "000001";
            }

            ProjectStatus = "NEW";
            return sProjectId;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ProjectStatus == "NEW")
            {
                if (txtProjectName.Text == "")
                {
                    MessageBox.Show("กรุณากรอกชื่อโครงการก่อน !!", "แจ้งเตือน");
                    return;
                }

                string strSQL = " INSERT INTO Build_Project ";
                strSQL += " (Project_id, Project_Name, Location, Project_Type, Floor, Area, Aircondition) ";
                strSQL += " VALUES('" + ProjectId + "', '" + txtProjectName.Text + "', '" + cboLocation.Text + "', '" + cboType.Text + "', '" + txtFloor.Text + "', '"+ txtArea.Text +"', '"+ txtAir.Text +"') ";

                if (db.QueryExecuteNonQuery(strSQL) == true)
                {
                    frmPlan frm = new frmPlan(ProjectId, Convert.ToInt32(txtFloor.Text), txtProjectName.Text, txtArea.Text, txtAir.Text);
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("ไม่สามารถบันทึกโครงการได้ กรุณาตรวจสอบอีกครั้ง", "แจ้งเตือน");
                }
            }
            else
            {
                frmPlan frm = new frmPlan(ProjectId, Convert.ToInt32(txtFloor.Text), txtProjectName.Text, txtArea.Text, txtAir.Text);
                frm.Show();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            frmProjectView frm = new frmProjectView();
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {
                string sProject = frm.Exit_Value();
                ProjectId = sProject;
                Load_Project();
                ProjectStatus = "OLD";
            }
        }

        private void cboLocation_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void cboType_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }
    }
}
