using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LiteonWebAPI.Models;
using Newtonsoft.Json;
using LiteonWebAPI.MyClass;

namespace LiteonWebAPI.Controllers
{
    public class RolesController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage GetRolesList(int page, int limit)
        {
            var le = EFClass.GetEF();
            var list = le.Roles.OrderBy(p => p.CreatedDate).Skip((page - 1) * limit).Take(limit).ToList();
            object o = new { code = 0, msg = "", count = list.Count, data = list };
            return ToJson.toJson(o);
        }
        [HttpPut]
        public HttpResponseMessage UpdateRoles(dynamic ro)
        {
            var le = EFClass.GetEF();
            String rn = Convert.ToString( ro.rolename);
            String rd = Convert.ToString(ro.roledes);
            String rid  = Convert.ToString(ro.roleid);
            var roled = le.Roles.Where(p => p.RoleID == rid).FirstOrDefault();
            roled.RoleName = rn;
            roled.RoleDes = rd;
            return ToJson.toJson(le.SaveChanges());
        }
        [HttpPost]
        public HttpResponseMessage InsertRole(dynamic ro)
        {
            var le = EFClass.GetEF();
            String rn = Convert.ToString(ro.rolename);
            String rd = Convert.ToString(ro.roledes);
            Roles roled = new Roles(); 
            roled.RoleName = rn;
            roled.RoleDes = rd;
            roled.RoleID = Guid.NewGuid().ToString().ToUpper() ;
            roled.CreatedDate = DateTime.Now;
            le.Roles.Add(roled);
            return ToJson.toJson(le.SaveChanges());
        }
    }
}

