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
    public partial class frmHouse : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();
        string ProjectId, ProjectName, Floor, Area, Air;

        private void frmHouse_Resize(object sender, EventArgs e)
        {
            panel1.Location = new Point(
                this.ClientSize.Width / 2 - panel1.Size.Width / 2,
                this.ClientSize.Height / 2 - panel1.Size.Height / 2);
            panel1.Anchor = AnchorStyles.None;
        }

        private void btnRoof_Click(object sender, EventArgs e)
        {
            Roof.frmRoof frm = new Roof.frmRoof(ProjectId, 1);
            frm.ShowDialog();
            Load_Data();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Roof.frmMirror frm = new Roof.frmMirror(ProjectId);
            frm.ShowDialog();
            Load_Data();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAir_Click(object sender, EventArgs e)
        {
            string strSQL = " UPDATE Build_Select SET UA = '"+ lb701.Text + "' WHERE PID = '" + ProjectId + "'  ";

            if (db.QueryExecuteNonQuery(strSQL))
            {
                MessageBox.Show("บันทึกข้อมูลเรียบร้อย !!", "ผลลัพท์");
                Load_Data();
            }
        }

        private void btnLamp_Click(object sender, EventArgs e)
        {
            string strSQL = " UPDATE Build_Select SET UL = '" + lb601.Text + "' WHERE PID = '" + ProjectId + "'  ";

            if (db.QueryExecuteNonQuery(strSQL))
            {
                MessageBox.Show("บันทึกข้อมูลเรียบร้อย !!", "ผลลัพท์");
                Load_Data();
            }
        }

        private void btnCalculator_Click(object sender, EventArgs e)
        {
            Load_Data();
        }

        private void btnMaterial_Click(object sender, EventArgs e)
        {
            frmMaterial frm = new frmMaterial();
            frm.ShowDialog();
        }

        public frmHouse(string sProjectId, string sProjectName, string sFloor, string sArea, string sAir)
        {
            InitializeComponent();
            ProjectId = sProjectId;
            ProjectName = sProjectName;
            Floor = sFloor;
            Area = sArea;
            Air = sAir;
        }

        private void frmHouse_Load(object sender, EventArgs e)
        {
            lbProjectName.Text = ProjectName;
            Load_Data();
        }

        private void Load_Data()
        {
            DataTable dt;

            string strSQL = " SELECT * FROM Build_Select WHERE PID = '"+ ProjectId +"' ";

            dt = db.QueryDataTable(strSQL);

            double UR, SHGC, UM, UW, UL, UA;
            double stdUR = 0.413, stdSHGC = 0.54, stdUM = 1.04, stdUW = 0.441, stdUL = 1.71, stdUA = 11.6;

            if(dt.Rows.Count > 0)
            {
                UR = Convert.ToDouble(dt.Rows[0]["UR"]);
                SHGC = Convert.ToDouble(dt.Rows[0]["SHGC"]);
                UM = Convert.ToDouble(dt.Rows[0]["UM"]);
                UW = Convert.ToDouble(dt.Rows[0]["UW"]);
                UL = Convert.ToDouble(dt.Rows[0]["UL"]);
                UA = Convert.ToDouble(dt.Rows[0]["UA"]);

                lb101.Text = Convert.ToDouble(dt.Rows[0]["UR"]).ToString("0.00");
                lb301.Text = Convert.ToDouble(dt.Rows[0]["SHGC"]).ToString("0.00"); 
                lb401.Text = Convert.ToDouble(dt.Rows[0]["UR"]).ToString("0.00"); 
                lb501.Text = Convert.ToDouble(dt.Rows[0]["UW"]).ToString("0.00"); 
                lb601.Text = Convert.ToDouble(dt.Rows[0]["UL"]).ToString("0.00"); 
                lb701.Text = Convert.ToDouble(dt.Rows[0]["UA"]).ToString("0.00"); 

                if(UR > stdUR)
                {
                    lb1Pass.Text = "ไม่ผ่าน";
                    lb1Pass.BackColor = Color.Red;
                }
                else
                {
                    lb1Pass.Text = "ผ่าน";
                    lb1Pass.BackColor = Color.Green;
                }

                if (SHGC > stdSHGC)
                {
                    lb3Pass.Text = "ไม่ผ่าน";
                    lb3Pass.BackColor = Color.Red;
                }
                else
                {
                    lb3Pass.Text = "ผ่าน";
                    lb3Pass.BackColor = Color.Green;
                }

                if (UM > stdUM)
                {
                    lb4Pass.Text = "ไม่ผ่าน";
                    lb4Pass.BackColor = Color.Red;
                }
                else
                {
                    lb4Pass.Text = "ผ่าน";
                    lb4Pass.BackColor = Color.Green;
                }

                if (UW > stdUW)
                {
                    lb5Pass.Text = "ไม่ผ่าน";
                    lb5Pass.BackColor = Color.Red;
                }
                else
                {
                    lb5Pass.Text = "ผ่าน";
                    lb5Pass.BackColor = Color.Green;
                }

                if (UL > stdUL)
                {
                    lb6Pass.Text = "ไม่ผ่าน";
                    lb6Pass.BackColor = Color.Red;
                }
                else
                {
                    lb6Pass.Text = "ผ่าน";
                    lb6Pass.BackColor = Color.Green;
                }

                if (UA > stdUA)
                {
                    lb7Pass.Text = "ไม่ผ่าน";
                    lb7Pass.BackColor = Color.Red;
                }
                else
                {
                    lb7Pass.Text = "ผ่าน";
                    lb7Pass.BackColor = Color.Green;
                }
            }


        }

        private void btnPlan_Click(object sender, EventArgs e)
        {
            Roof.frmRoof frm = new Roof.frmRoof(ProjectId, 0);
            frm.ShowDialog();
            Load_Data();
        }
    }
}
