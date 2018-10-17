using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Building_Home.DBClass
{
    class DBConnect
    {
        public static string strConn;
        public static OleDbConnection Conn = new OleDbConnection();
        public static OleDbCommand objCmd;

        public DataTable QueryDataTable(String strSQL)
        {
            Close_Con();
            OleDbDataAdapter dtAdapter;
            DataTable dt = new DataTable();
            Conn_Open();
            dtAdapter = new OleDbDataAdapter(strSQL, Conn);
            dtAdapter.Fill(dt);
            Close_Con();
            return dt; //*** Return DataTable ***//
        }

        public Boolean QueryExecuteNonQuery(String strSQL)
        {
            Conn_Open();

            try
            {
                objCmd = new OleDbCommand();
                objCmd.Connection = Conn;
                objCmd.CommandType = CommandType.Text;
                objCmd.CommandText = strSQL;

                objCmd.ExecuteNonQuery();
                Close_Con();
                return true; //*** Return True ***//
            }
            catch (Exception)
            {
                Close_Con();
                return false; //*** Return False ***//
            }
        }

        public Boolean QueryExecuteNonQuery(String strSQL, OleDbParameterCollection parameters)
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand(strSQL, Conn);
                foreach (OleDbParameter param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.ParameterName, param.OleDbType).Value = param.Value;
                }
                Conn_Open();
                cmd.ExecuteNonQuery();

                Close_Con();
                return true; //*** Return True ***//
            }
            catch (Exception)
            {
                Close_Con();
                return false; //*** Return False ***//
            }
        }

        public static void Conn_Open()
        {
            if (Conn.State == ConnectionState.Closed)
            {
                Conn.Open();
            }
        }

        public static void Close_Con()
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }

        public static void Connenct_DB()
        {
            try
            {
                string ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source=Home.mdb";
                Conn = new OleDbConnection(ConnectionString);
                Conn.Open();
            }
            catch
            {

            }
        }
    }
}