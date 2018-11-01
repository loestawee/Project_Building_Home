using DevExpress.XtraCharts;
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
    public partial class frmCalculator : Form
    {
        DBClass.DBConnect db = new DBClass.DBConnect();
        string ProjectId;
        public frmCalculator(string sProjectId)
        {
            InitializeComponent();
            ProjectId = sProjectId;
        }

        private void frmCalculator_Load(object sender, EventArgs e)
        {
            Load_Data();
            Load_Pie1();
            Load_Pie2();
        }

        private void Load_Data()
        {
            DataTable dt;

            string strSQL = " SELECT * FROM 4_Energy_All WHERE Project_id = '" + ProjectId + "' ";

            dt = db.QueryDataTable(strSQL);

            if(dt.Rows.Count > 0)
            {
                double all = Convert.ToDouble(dt.Rows[0]["AC"].ToString()) + Convert.ToDouble(dt.Rows[0]["Lighting"].ToString()) + Convert.ToDouble(dt.Rows[0]["Equip"].ToString());
                txtAir.Text = dt.Rows[0]["AC"].ToString();
                txtLamp.Text = dt.Rows[0]["Lighting"].ToString();
                txtEquip.Text = dt.Rows[0]["Equip"].ToString();
                txtAll.Text = all.ToString();
                txtYear.Text = (all / 150).ToString();
            }
        }

        private void Load_Pie1()
        {
            DataTable dt;

            string strSQL = " SELECT * FROM Pie_Chart_Energy WHERE Project_id = '" + ProjectId + "' ";

            dt = db.QueryDataTable(strSQL);

            Series series1 = new Series("Total Electrical Energy", ViewType.Pie);

            for(int i = 0; i<dt.Rows.Count; i++)
            {
                
                series1.Points.Add(new SeriesPoint(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString()));
                chartControl1.Series.Add(series1);
            }

            series1.Label.TextPattern = "{A}: {VP:p0}";
            chartControl1.DataSource = dt;

        }

        private void Load_Pie2()
        {
            DataTable dt;

            string strSQL = " SELECT * FROM Pie_Chart_Parts WHERE Project_id = '" + ProjectId + "' ";
            
            dt = db.QueryDataTable(strSQL);

            Series series1 = new Series("Total Electrical Energy", ViewType.Pie);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
                series1.Points.Add(new SeriesPoint(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString()));
                chartControl2.Series.Add(series1);

                series1.Label.TextPattern = "{A}: {VP:p0}";
            }

            chartControl2.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
