using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteonWebAPI.ViewModel
{
    public class LiteonUser
    {
        public String SiteCode { get; set; }
        public String IDNumber { get; set; }
        public String UserName { get; set; }
        public String EmployeeID { get; set; }
        public DateTime Birthdate { get; set; }
    }
}