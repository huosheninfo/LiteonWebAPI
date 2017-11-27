using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LiteonWebAPI.MyClass;
using LiteonWebAPI.Models;
using LiteonWebAPI.ViewModel;
namespace LiteonWebAPI.Controllers
{
    public class RoleToUserController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetUserInfoList(dynamic other)
        {
            var le = EFClass.GetEF();
            if (other.type == 1)
            {
                //初次加载或者空条件查询用户列表 
                //不需要条件 有分页
                //String text = Convert.ToString(other.txt).Trim();
                String guid = Convert.ToString(other.guid);
                int page = Convert.ToInt32(other.page);
                int limit = Convert.ToInt32(other.limit);

                var ss = from ui in le.UserInfo join ur in le.UserRoles on ui.UserID equals ur.UserID where ur.RoleID == guid select new UserToRoles { UserName = ui.UserName, UserID = ui.UserID, GUID = ur.GUID };
                List<UserToRoles> list = new List<UserToRoles>(ss);

                var list1 = list.OrderBy(p => p.GUID).Skip((page - 1) * limit).Take(limit).Select(p => new { GUID = p.GUID, UserID = p.UserID, UserName = p.UserName }).ToList();

                return ToJson.toJson(list1);
            }
            else if (other.type == 2)
            {
                //获得有该角色的用户总数
                String text = Convert.ToString(other.txt).Trim();
                String guid = Convert.ToString(other.guid);

                var userIDList = le.UserRoles.Where(p => p.RoleID == guid).ToList();
                List<UserInfo> userInfoList = new List<UserInfo>();

                var ss = from ui in le.UserInfo join ur in le.UserRoles on ui.UserID equals ur.UserID where ur.RoleID == guid && (ui.UserID.Contains(text) || ui.UserName.Contains(text)) select new { UserName = ui.UserName, UserID = ui.UserID, GUID = ur.GUID };

                List<object> list = new List<object>(ss);

                return ToJson.toJson(list.Count);


            }
            else if (other.type == 4)
            {
                //根据条件查询 无分页
                String text = Convert.ToString(other.txt).Trim();
                String guid = Convert.ToString(other.guid);

                var ss = from ui in le.UserInfo join ur in le.UserRoles on ui.UserID equals ur.UserID where ur.RoleID == guid && (ui.UserID.Contains(text) || ui.UserName.Contains(text)) select new UserToRoles { UserName = ui.UserName, UserID = ui.UserID, GUID = ur.GUID };
                List<UserToRoles> list = new List<UserToRoles>(ss);

                var list1 = list.Select(p => new { GUID = p.GUID, UserID = p.UserID, UserName = p.UserName }).ToList();

                return ToJson.toJson(list1);
            }
            else if (other.type == 3)
            {
                //添加用户给该角色
                String userids = Convert.ToString(other.userids);
                String guid = Convert.ToString(other.guid);
                String[] idList = userids.Trim(',').Split(',');
                for (int i = 0; i < idList.Length; i++)
                {
                    UserRoles ur = new UserRoles();
                    ur.GUID = Guid.NewGuid().ToString().ToUpper();
                    ur.CreatedDate = DateTime.Now;
                    ur.RoleID = guid;
                    ur.UserID = idList[i];
                    le.UserRoles.Add(ur);
                }
                return ToJson.toJson(le.SaveChanges());
            }
            return ToJson.toJson("");

        }

        [HttpGet]
        public HttpResponseMessage DeleteUserToRole(String guid)
        {
            //从用户角色表中删除改用户与改角色的联系
            var le = EFClass.GetEF();
            UserRoles ur = le.UserRoles.Where(p => p.GUID == guid).FirstOrDefault();
            if (ur != null)
            {
                le.UserRoles.Remove(ur);
                return ToJson.toJson(le.SaveChanges());
            }
            return ToJson.toJson("-1");
        }
    }
}
