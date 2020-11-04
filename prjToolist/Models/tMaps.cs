using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjToolist.Models
{
    public class tStartPosition
    {
        public float lon { get; set; }
        public float lat { get; set; }
    }

    public class tEndPosition
    {
        public float lon { get; set; }
        public float lat { get; set; }
    }

    public class tGmap
    {
        public tStartPosition from { get; set; }
        public tEndPosition to { get; set; }
        public int[] filter { get; set; }
    }

    public class tGMapId
    {
        public string gmap_id { get; set; }
    }

    //public class placeInfo
    //{
    //    public string name { get; set; }
    //    public string phone { get; set; }
    //    public string address { get; set; }
    //    public string type { get; set; }
    //}

    public class location
    {
        public float lon { get; set; }
        public float lat { get; set; }
    }

    public class tMapMark
    {
        public string gmap_id { get; set; }
        public location location { get; set; }
    }
}