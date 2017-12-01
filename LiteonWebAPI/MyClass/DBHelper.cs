using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using LiteonWebAPI.ViewModel;
using LiteonWebAPI.Models;
namespace LiteonWebAPI.MyClass
{

    public static class DBHelper
    {



        private static String connstr = "";
        private static void CheckedConn()
        {
            if (connstr == "")
            {
                connstr = ConfigurationManager.ConnectionStrings["LocalLiteonBaseDBEntities"].ConnectionString;
            }
        }

        public static void SetConnstrToBase()
        {
            connstr = ConfigurationManager.ConnectionStrings["LocalLiteonBaseDBEntities"].ConnectionString;
        }
        public static void SetConnstr(CObject oc)
        {
            switch (oc)
            {
                case CObject.Liteon_base:
                    connstr = ConfigurationManager.ConnectionStrings["LocalLiteonBaseDBEntities"].ConnectionString;
                    break;
                case CObject.Liteon_action:
                    connstr = ConfigurationManager.ConnectionStrings["LocalLiteonActionDBEntities"].ConnectionString;
                    break;
                default:
                    break;
            }
        }
        public static Boolean UseBoolProc(String sql, SqlParameter[] p)
        {
            CheckedConn();
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            foreach (var sp in p)
            {
                cmd.Parameters.Add(sp);
            }
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            return rows > 0;
        }

        public static DataTable UseDTProc(String sql, SqlParameter[] p)
        {
            CheckedConn();
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            foreach (var sp in p)
            {
                cmd.Parameters.Add(sp);
            }

            DataTable dt = new DataTable();
            SqlDataAdapter dap = new SqlDataAdapter(cmd);
            dap.Fill(dt);
            conn.Close();
            return dt;


        }
    }
}