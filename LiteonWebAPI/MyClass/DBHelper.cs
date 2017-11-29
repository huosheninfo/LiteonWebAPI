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
    public class DBHelper
    {

        public enum ConnectionObject
        {
            Liteon_base = 1,
            Liteon_action = 2,
        }

        private static String connstr = ConfigurationManager.ConnectionStrings["LocalLiteon_BaseDBEntities"].ConnectionString;

        public static void SetConnstr(ConnectionObject oc)
        {
            switch (oc)
            {
                case ConnectionObject.Liteon_base:
                    connstr = ConfigurationManager.ConnectionStrings["AzureLiteonBaseEntities"].ConnectionString;
                    break;
                case ConnectionObject.Liteon_action:
                    connstr = ConfigurationManager.ConnectionStrings["AzureLiteonActionEntities"].ConnectionString;
                    break;
                default:
                    break;
            }
        }
        public static Boolean UseBoolProc(String sql, SqlParameter[] p)
        {
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