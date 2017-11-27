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

namespace LiteonWebAPI.Controllers
{
    public class LoginController : ApiController
    {
        ReturnMessage rm = new ReturnMessage();
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Login(dynamic para)
        {
            String userid = para.userid;
            String inputPwd = para.pwd;
            var le = EFClass.GetEF();
            var userList = le.Users.Where(p => p.UserID == userid && p.Password.ToUpper() == inputPwd.ToUpper()).ToList();
            if (userList.Count == 1)
            {
                rm.ResponseState = ResponseState.Successed;
                rm.ResponseMessage = "登录成功";
                rm.ResponseData = JsonConvert.SerializeObject(userList[0]);
            }
            else
            {
                rm.ResponseState = ResponseState.Failed;
                rm.ResponseMessage = "用户名或密码错误";
            }

        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}