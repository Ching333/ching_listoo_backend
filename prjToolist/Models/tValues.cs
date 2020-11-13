using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjToolist.Models
{
    public class vmCountDataValues
    {
        public int count { get; set; }
        public string key { get; set; }
    }

    public class vmCountDataValuesTwoKey
    {
        public int count { get; set; }
        public string key { get; set; }
        public string key2 { get; set; }
    }

    public class tagSelection
    {
        public int tagId { get; set; }
        public string tagName { get; set; }
    }
}