using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LiteonWebAPI.MyClass;
using LiteonWebAPI.ViewModel;
using LiteonWebAPI.Models;

namespace LiteonWebAPI.Controllers
{
    public class RegsiterController : ApiController
    {
        ReturnMessage rm = new ReturnMessage();
        [HttpPost]
        public HttpResponseMessage Regsiters(dynamic para)
        {
            LiteonUser lu = new LiteonUser();


            String cq = para.cq;
            String xm = para.xm;
            String sfz = para.sfz;
            String gh = para.gh;
            String sr = para.sr;
            String mm = para.mm;
            String userid = cq.ToUpper() + gh;
            lu.Birthdate = Convert.ToDateTime(sr);
            lu.IDNumber = sfz;
            lu.SiteCode = cq;
            lu.UserName = xm;
            lu.EmployeeID = gh;
            String loginid = LiteonMethod.MobilePortal_NoEmailEmployeeCheck(lu);
            if (!String.IsNullOrEmpty(loginid))
            {
                var le = EFClass.GetEF();
                var uilist = le.UserInfo.Where(p => p.UserID == userid).ToList();
                if (uilist.Count == 0)
                {

                    //该帐号没有被注册
                    UserInfo ui = new UserInfo();
                    ui.UserID = userid;
                    ui.Region = cq;
                    ui.UserName = xm;
                    ui.RegisterDate = DateTime.Now;
                    ui.GUID = Guid.NewGuid().ToString();
                    ui.EmployeeID = gh;
                    le.UserInfo.Add(ui);
                    Users u = new Users();
                    u.Password = mm;
                    u.GUID = Guid.NewGuid().ToString();
                    u.UserID = userid;
                    u.LoginDate = DateTime.Now;
                    le.Users.Add(u);
                    int result = le.SaveChanges();

                    if (result > 0)
                    {

                        rm.ResponseState = ResponseState.Successed;
                        rm.ResponseMessage = "添加成功！";
                        rm.ResponseData = userid;
                    }
                    else
                    {
                        rm.ResponseState = ResponseState.Failed;
                        rm.ResponseMessage = "添加失败！";
                    }
                    return ToJson.toJson(rm);
                }
                else
                {
                    rm.ResponseState = ResponseState.Failed;
                    rm.ResponseMessage = "该帐号已被注册";
                    return ToJson.toJson(rm);
                }

            }
            else
            {
                rm.ResponseState = ResponseState.Failed;
                rm.ResponseMessage = "您不是光宝员工！";
                return ToJson.toJson(rm);
            }
        }
    }
}
