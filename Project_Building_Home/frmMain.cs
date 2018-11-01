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
    public partial class frmMain : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();
        string ProjectStatus = "";
        string ProjectId = "";

        public frmMain()
        {
            InitializeComponent();
        }
        
        private void frmMain_Load(object sender, EventArgs e)
        {
            DBClass.DBConnect.Connenct_DB();
            ProjectId = Get_Max();
            Load_Province();
            Load_Project();
        }

        private void Load_Province()
        {
            DataTable dt;

            dt = db.QueryDataTable(" SELECT Province FROM Province ");

            cboLocation.BeginUpdate();
            cboLocation.DisplayMember = "Province";
            cboLocation.ValueMember = "";
            cboLocation.DataSource = dt;
            cboLocation.EndUpdate();
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
                txtPeople.Text = dt.Rows[0]["People"].ToString();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ProjectStatus == "NEW")
            {
                if (txtProjectName.Text == "")
                {
                    MessageBox.Show("กรุณากรอกชื่อโครงการก่อน !!", "แจ้งเตือน");
                    return;
                }

                string strSQL = " INSERT INTO Build_Project ";
                strSQL += " (Project_id, Project_Name, Location, Project_Type, Floor, Area, Aircondition, People) ";
                strSQL += " VALUES('" + ProjectId + "', '" + txtProjectName.Text + "', '" + cboLocation.Text + "', '" + cboType.Text + "', '" + txtFloor.Text + "', '" + txtArea.Text + "', '" + txtAir.Text + "', '"+ txtPeople.Text +"') ";

                if (db.QueryExecuteNonQuery(strSQL) == true)
                {
                    db.QueryExecuteNonQuery(" INSERT INTO Build_Select (PID, UR, SHGC, UM, UW, UL, UA) VALUES('"+ ProjectId +"', 0, 0, 0, 0, 0, 0); ");


                    MessageBox.Show("บันทึกข้อมูลโครงการเรียบร้อย", "แจ้งเตือน");
                }
                else
                {
                    MessageBox.Show("ไม่สามารถบันทึกโครงการได้ กรุณาตรวจสอบอีกครั้ง", "แจ้งเตือน");
                }
            }
            else
            {
                if (txtProjectName.Text == "")
                {
                    MessageBox.Show("กรุณากรอกชื่อโครงการก่อน !!", "แจ้งเตือน");
                    return;
                }

                string strSQL = " UPDATE Build_Project SET ";
                strSQL += " Project_Name = '"+ txtProjectName.Text +"', ";
                strSQL += " Location = '" + cboLocation.Text + "', ";
                strSQL += " Project_Type = '" + cboType.Text + "', ";
                strSQL += " Floor = '" + txtFloor.Text + "', ";
                strSQL += " Area = '" + txtArea.Text + "', ";
                strSQL += " Aircondition = '" + txtAir.Text + "', ";
                strSQL += " People = '" + txtPeople.Text + "' ";
                strSQL += " WHERE Project_id = '"+ ProjectId + "' ";

                if (db.QueryExecuteNonQuery(strSQL) == true)
                {
                    MessageBox.Show("บันทึกข้อมูลโครงการเรียบร้อย", "แจ้งเตือน");
                }
                else
                {
                    MessageBox.Show("ไม่สามารถบันทึกโครงการได้ กรุณาตรวจสอบอีกครั้ง", "แจ้งเตือน");
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(txtProjectName.Text == "")
            {
                MessageBox.Show("กรุณากรอกชื่อโครงการก่อนครับ !!", "แจ้งเตือน");
                return;
            }

            if(ProjectStatus == "")
            {
                MessageBox.Show("กรุณาบันทึกโครงการก่อนครับ !!", "แจ้งเตือน");
                return;
            }
            
            frmHomeSelect frm = new frmHomeSelect(ProjectId, txtProjectName.Text, txtFloor.Text, txtArea.Text, txtAir.Text);
            frm.Show();
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmHome frm = new frmHome();
            frm.Show();
            this.Close();
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
