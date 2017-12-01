using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LiteonWebAPI.MyClass;
using LiteonWebAPI.Models;
using System.Threading;

namespace LiteonWebAPI.Controllers
{
    public class RealmController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetRealmList(int page, int limit)
        {
            var le = EFClass.GetEF();
            var list = le.Realms.OrderBy(p => p.CreatedDate).Skip((page - 1) * limit).Take(limit).ToList();
            object o = new { code = 0, msg = "", count = list.Count, data = list };
            return ToJson.toJson(o);
        }

        [HttpPut]
        public HttpResponseMessage UpdateRealm(dynamic para)
        {
            var le = EFClass.GetEF();
            String RealmID = Convert.ToString(para.RealmID);
            String RealmDes = Convert.ToString(para.RealmDes);
            long Sequence = Convert.ToInt64(para.Sequence);
            String Uri = Convert.ToString(para.Uri);
            //String ParentID = Convert.ToString(para.ParentID);
            var r = le.Realms.Where(p => p.RealmID == RealmID).FirstOrDefault();
            r.Sequence = Sequence;
            r.RealmDes = RealmDes;
            r.Uri = Uri;
            return ToJson.toJson(le.SaveChanges());
        }


        [HttpPost]
        public HttpResponseMessage InsertRealm(dynamic para)
        {
            var le = EFClass.GetEF();
            int type = Convert.ToInt32(para.type);
            if (type == 1)
            {
                
                String RealmDes = Convert.ToString(para.RealmDes);
                long Sequence = Convert.ToInt64(para.Sequence);
                String Uri = Convert.ToString(para.Uri);
                //String ParentID = Convert.ToString(para.ParentID);
                String AppID = Guid.NewGuid().ToString();
                String Security = Guid.NewGuid().ToString();
                Realms r = new Realms();
                r.RealmID = Guid.NewGuid().ToString();
                r.RealmDes = RealmDes;
                r.Uri = Uri;
                r.AppID = AppID;
                r.Sequence = Sequence;
                r.Security = Security;
                r.CreatedDate = DateTime.Now;
                le.Realms.Add(r);
                return ToJson.toJson(le.SaveChanges());
            }
            else if (type == 2)
            {
                Thread.Sleep(2000);
                String RealmID = Convert.ToString(para.id);
                var r = le.Realms.Where(p => p.RealmID == RealmID).FirstOrDefault();
                r.Forzen = 1;
                return ToJson.toJson(le.SaveChanges());
            }
            return ToJson.toJson("");
        }


    }
}
