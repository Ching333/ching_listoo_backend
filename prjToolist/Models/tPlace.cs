﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjToolist.Models
{
    public class tPlace
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal longitude { get; set; }
        public decimal latitude { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public Nullable<int> type { get; set; }
    }

    public class tPlaceInfo
    {
        public string name { get; set; }
        public decimal longitude { get; set; }
        public decimal latitude { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string type { get; set; }
        public string gmap_id { get; set; }
    }

    public class queryPlaceInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal longitude { get; set; }
        public decimal latitude { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string type { get; set; }
    }
}