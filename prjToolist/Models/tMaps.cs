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

    public class placeInfo
    {
        public string name { get; set; }
        public decimal longitude { get; set; }
        public decimal latitude { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string type { get; set; }
        public string gmap_id { get; set; }
    }

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

    public class candidatePlacePara
    {
        public string gmap_id { get; set; }
        public string type { get; set; }
        public int limit { get; set; }
    }

    //public List<tTag> placeTags(tGMapId gMapId)
    //{
    //    FUENMLEntities db = new FUENMLEntities();
    //    var placeId = db.places.FirstOrDefault(p=>p.gmap_id == gMapId.gmap_id).id;
    //    var tagIdList = db.tagRelations.Where(t => t.place_id == placeId).ToList().Distinct();
    //    List<tTag> tags = new List<tTag>();
    //    for (int i= 0; i < tagIdList.Count();i++)
    //    {
    //        tTag tagItem = new tTag();
    //        var tag = db.tags.FirstOrDefault(t => t.id == i);
    //        tagItem.id = tag.id;
    //        tagItem.name = tag.name;
    //        tagItem.type = tag.type;
    //        tags.Add(tagItem);
    //    }
    //    return tags;
    //}

}