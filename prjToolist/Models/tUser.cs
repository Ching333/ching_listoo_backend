using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjToolist.Models
{
    public class tUser
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int authority { get; set; }
        public Nullable<System.DateTime> updated { get; set; }
        public Nullable<System.DateTime> created { get; set; }
    }
    public class memberLogin
    {
        public string account { get; set; }
        public string password { get; set; }
    }
    public class createMember
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }

}