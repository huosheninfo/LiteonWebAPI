using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LiteonWebAPI.MyClass;
using LiteonWebAPI.ViewModel;
using LiteonWebAPI.Models;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Web;

namespace LiteonWebAPI.Controllers
{
    public class LoginController : ApiController
    {
        ReturnMessage rm = new ReturnMessage();


        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Login(dynamic para)
        {

            String userid = para.userid;
            String inputPwd = para.pwd;
            //trevor.chang@liteon.com 模式只取@前面的不带点的
            if (userid.Contains("@"))
            {

                int @index = userid.IndexOf('@');
                userid = userid.Remove(@index);
                userid = userid.Replace(".", "");
                ReturnLiteonUser rlu = LiteonMethod.MobilePortal_GetEmpInfoViaAccount(userid);

                if (rlu != null)
                {
                    //用户号验证成功
                    LiteonMethod.RegsiterAction(rlu, userid);
                    rm.ResponseState = ResponseState.Successed;
                    rm.ResponseMessage = "登录成功";
                    rm.ResponseData = JsonConvert.SerializeObject(rlu);
                    return ToJson.toJson(rm);

                }
                else
                {
                    //用户不存在
                    rm.ResponseState = ResponseState.Failed;
                    rm.ResponseMessage = "登录失败";
                    return ToJson.toJson(rm);
                }

            }
            else if (userid.Contains("\\"))
            {
                //liteon\trevorchang 模式只取\后面的
                int @index = userid.IndexOf('\\');
                userid = userid.Remove(0, @index + 1);

                ReturnLiteonUser rlu = LiteonMethod.MobilePortal_GetEmpInfoViaAccount(userid);
                if (rlu != null)
                {
                    rm.ResponseState = ResponseState.Successed;
                    rm.ResponseMessage = "登录成功";
                    rm.ResponseData = JsonConvert.SerializeObject(rlu);
                    return ToJson.toJson(rm);
                }
                else
                {
                    rm.ResponseState = ResponseState.Failed;
                    rm.ResponseMessage = "登录失败";
                    return ToJson.toJson(rm);
                }
            }
            else
            {
                var le = EFClass.GetEF();
                
                var userList = le.Users.Where(p => p.UserID == userid && p.Password.ToUpper() == inputPwd.ToUpper()).ToList();
                if (userList.Count == 1)
                {
                    var context = HttpContext.Current;

                    Users us = userList[0];
                    var urList = le.UserRoles.Where(p => p.UserID == us.UserID).ToList();
                    context.Session["roleid"] = urList;
                    rm.ResponseState = ResponseState.Successed;
                    rm.ResponseMessage = "登录成功";
                    rm.ResponseData = JsonConvert.SerializeObject(us.UserID);
                }
                else
                {
                    rm.ResponseState = ResponseState.Failed;
                    rm.ResponseMessage = "用户名或密码错误";
                }
                return ToJson.toJson(rm);
            }
        }
    }
}