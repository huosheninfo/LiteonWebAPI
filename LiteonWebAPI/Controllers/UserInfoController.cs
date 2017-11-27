using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LiteonWebAPI.Models;
using Newtonsoft.Json;
using System.Text;
using System.Web.Http.Cors;
using System.Threading.Tasks;
using LiteonWebAPI.MyClass;
using LiteonWebAPI.ViewModel;

namespace LiteonWebAPI.Controllers
{
    public class UserInfoController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetUserInfoList(int page, int limit)
        {

            var le = EFClass.GetEF();
            var list = le.UserInfo.OrderBy(p => p.RegisterDate).Skip((page - 1) * limit).Take(limit).ToList();
            object o = new { code = 0, msg = "", count = list.Count, data = list };
            return ToJson.toJson(o);
        }

        [HttpPost]
        public HttpResponseMessage GetUserList(dynamic para)
        {
            var le = EFClass.GetEF();
            if (para.type == 1)
            {
                //获得工厂列表
                return ToJson.toJson(le.Dic_Regions.ToList());
            }
            else if (para.type == 2)
            {
                //通过初始化数据
                String guid = Convert.ToString(para.guid);//当前角色的roleid
                String cqid = Convert.ToString(para.cqid);//下拉列表框中的选择项
                String txt = Convert.ToString(para.txt);//用户编号 或者 姓名

                int page = Convert.ToInt32(para.page);
                int pagesize = Convert.ToInt32(para.pagesize);
                //在用户角色表中 获得有guid 角色的用户id ，存在UserIDList中
                List<String> UserIDList = (from ur in le.UserRoles where ur.RoleID == guid select ur.UserID).ToList();
                //通过用户表，厂区表 联合查询，查出 用户id不在UserIDList 中，且厂区编号为cqid 的用户
                var EndList = (from ui in le.UserInfo where ui.Region.Contains(cqid) && !UserIDList.Contains(ui.UserID) && (ui.UserName.Contains(txt) || ui.UserID.Contains(txt)) select ui).ToList();

                return ToJson.toJson(EndList);
            }
            else if (para.type == 3)
            {
                //查询不分页
                String guid = Convert.ToString(para.guid);//当前角色的roleid
                String cqid = Convert.ToString(para.cqid);//下拉列表框中的选择项
                String txt = Convert.ToString(para.txt);//用户编号 或者 姓名

                int page = Convert.ToInt32(para.page);
                int pagesize = Convert.ToInt32(para.pagesize);

                //在用户角色表中 获得有guid 角色的用户id ，存在UserIDList中
                List<String> UserIDList = (from ur in le.UserRoles where ur.RoleID == guid select ur.UserID).ToList();
                //通过用户表，厂区表 联合查询，查出 用户id不在UserIDList 中，且厂区编号为cqid 的用户
                var EndList = (from ui in le.UserInfo where ui.Region.Contains(cqid) && !UserIDList.Contains(ui.UserID) && (ui.UserName.Contains(txt) || ui.UserID.Contains(txt)) select ui).ToList();

                return ToJson.toJson(EndList);
            }
            return ToJson.toJson("-1");
        }
    }

    
}
