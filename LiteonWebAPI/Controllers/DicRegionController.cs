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
    public class DicRegionController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetDicRegionList(int page, int limit)
        {
            var le = EFClass.GetEF();
            var list = le.Dic_Regions.OrderBy(p => p.UpdatedDate).Skip((page - 1) * limit).Take(limit).ToList();
            object o = new { code = 0, msg = "", count = list.Count, data = list };
            return ToJson.toJson(o);
        }

        [HttpPut]
        public HttpResponseMessage UpdateRegion(dynamic ro)
        {
            var le = EFClass.GetEF();
            String RegionName = Convert.ToString(ro.RegionName);
            String RegionDes = Convert.ToString(ro.RegionDes);
            String GUID = Convert.ToString(ro.GUID);
            String Alias = Convert.ToString(ro.Alias);

            var roled = le.Dic_Regions.Where(p => p.GUID == GUID).FirstOrDefault();
            roled.RegionDes = RegionDes;
            roled.RegionName = RegionName;
            roled.Alias = Alias;
            roled.UpdatedDate = DateTime.Now;
            return ToJson.toJson(le.SaveChanges());
        }

        [HttpPost]
        public HttpResponseMessage InsertRegion(dynamic para)
        {
            var le = EFClass.GetEF();
            String RegionID = Convert.ToString(para.RegionID);
            String RegionName = Convert.ToString(para.RegionName);
            String RegionDes = Convert.ToString(para.RegionDes);
            String Alias = Convert.ToString(para.Alias);
            var re = le.Dic_Regions.Where(p => p.RegionID == RegionID).FirstOrDefault();
            if (re != null)
                return ToJson.toJson("-9999");
            Dic_Regions dr = new Dic_Regions();
            dr.RegionDes = RegionDes;
            dr.GUID = Guid.NewGuid().ToString();
            dr.RegionName = RegionName;
            dr.UpdatedDate = DateTime.Now;
            dr.Alias = Alias;
            dr.RegionID = RegionID;
            le.Dic_Regions.Add(dr);
            return ToJson.toJson(le.SaveChanges());
        }
    }
}
