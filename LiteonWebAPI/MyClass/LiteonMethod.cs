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
    public class LiteonMethod
    {
        /// <summary>
        /// 通过用户区域id，用户名，身份证后六位，员工id，生日，得到用户的登录id，判断是否为光宝员工
        /// </summary>
        /// <param name="lu"></param>
        /// <returns></returns>
        public static String MobilePortal_NoEmailEmployeeCheck(LiteonUser lu)
        {
            DBHelper.SetConnstr(CObject.Liteon_base);
            SqlParameter[] p = new SqlParameter[] {
                new SqlParameter("@SiteCode",SqlDbType.NVarChar,20){Value= lu.SiteCode },
                new SqlParameter("@UserName",SqlDbType.NVarChar,30){Value= lu.UserName },
                new SqlParameter("@IDNumber",SqlDbType.NVarChar,20){Value= lu.IDNumber },
                new SqlParameter("@EmployeeID",SqlDbType.NVarChar,30){Value= lu.EmployeeID },
                new SqlParameter("@Birthdate",SqlDbType.DateTime){Value= lu.Birthdate },
                new SqlParameter("@EmployeeLoginID",SqlDbType.NVarChar,20) {Direction = ParameterDirection.Output}
            };
            DBHelper.UseBoolProc("MobilePortal_NoEmailEmployeeCheck", p);
            if (p[5].Value == null || p[5].Value is DBNull)
                return null;
            return p[5].Value.ToString();


        }
        /// <summary>
        /// 通过登录id，获取厂区，姓名，工号信息，邮箱
        /// </summary>
        /// <param name="Loginid"></param>
        /// <returns></returns>
        public static ReturnLiteonUser MobilePortal_GetEmpInfoViaAccount(String Loginid)
        {
            DBHelper.SetConnstr(CObject.Liteon_base);
            SqlParameter[] p = new SqlParameter[] {
                new SqlParameter("@LogonID",SqlDbType.NVarChar,20){Value= Loginid },
                new SqlParameter("@SiteCodes",SqlDbType.NVarChar,1000),
                new SqlParameter("@CNameTrad",SqlDbType.NVarChar,30),
                new SqlParameter("@Email",SqlDbType.NVarChar,100),
                new SqlParameter("@EmployeeID",SqlDbType.NVarChar,30)
            };
            p[1].Direction = ParameterDirection.Output;
            p[2].Direction = ParameterDirection.Output;
            p[3].Direction = ParameterDirection.Output;
            p[4].Direction = ParameterDirection.Output;

            DBHelper.UseBoolProc("MobilePortal_GetEmpInfoViaAccount", p);
            ReturnLiteonUser rlu = new ReturnLiteonUser();
            if (p[3].Value == null || p[3].Value is DBNull)
                return null;
            rlu.Email = p[3].Value.ToString();
            rlu.SiteCodes = p[1].Value.ToString();
            rlu.CNameTrad = p[2].Value.ToString();
            rlu.EmployeeID = p[4].Value.ToString();
            return rlu;



        }
        /// <summary>
        /// 域账号登录的时候注册
        /// </summary>
        /// <param name="rlu"></param>
        /// <param name="adid"></param>
        /// <returns></returns>
        public static bool RegsiterAction(ReturnLiteonUser rlu, String adid)
        {
            var le = EFClass.GetEF();
            int rows = le.proc_Regsiter(rlu.SiteCodes, rlu.CNameTrad, rlu.Email, rlu.EmployeeID, adid);
            return rows > 0;

        }

    }
}