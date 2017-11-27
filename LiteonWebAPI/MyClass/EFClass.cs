using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using LiteonWebAPI.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace LiteonWebAPI.MyClass
{
    public class EFClass
    {
        public static TrueLiteonDBEntities2 GetEF() {
            return new TrueLiteonDBEntities2();
            //return new LocalLiteonDBEntities();
        }
    }
    public class ToJson
    {

        public static HttpResponseMessage toJson(Object obj)
        {
            String str;
            if (obj is String || obj is Char)
            {
                str = obj.ToString();
            }
            else
            {
                str = JsonConvert.SerializeObject(obj);
            }
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json") };
            return result;
        }

    }
}