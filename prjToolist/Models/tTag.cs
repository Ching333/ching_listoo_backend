using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjToolist.Models
{
   
    public class tTag
    {
        public int id { get; set; }
        public string name { get; set; }
        public int type { get; set; }
    }

    public class tagString
    {
        public string[] tag_str { get; set; }

    }
    public class tagFilter
    {
        public int[] filter { get; set; }
    }

    public class tagInfo
    {
        public string name { get; set; }
        public int type { get; set; }
    }

    public class viewModelTagChange
    {
        
        public string gmap_id { get; set; }
        public int[] add { get; set; }
        public int[] remove { get; set; }
        public string[] newTags { get; set; }
    }

    public class tTagRelaforTable
    {
        public int id { get; set; }
        public string place_name { get; set; }
        public string tag_name { get; set; }
        public string user_name { get; set; }
    }

    public class viewModelSerachTag
    {

    }

    public static class tagFactory {
        
        public static int[] tagStringToId(tagString s, FUENMLEntities db)
    {
        //用於搜尋TAG
        List<int> tag_id = new List<int>();
        foreach (string item in s.tag_str)
        {
            //if (!(db.tags.Where(q => q.name == item)).Any())
            //{

            //    tag newtag = new tag();
            //    newtag.name = item;
            //    newtag.type = 1;
            //    db.tags.Add(newtag);

            //}
            if ((db.tags.Where(q => q.name.Contains(item))).Any())
            {

                var tagid = from p in db.tags
                            where (p.name.Contains(item))
                            select p;
                foreach (tag t in tagid)
                {
                    tag_id.Add(t.id);
                }

            }
        }
        return tag_id.Distinct().ToArray();
    }

        public static int[] checktagString(tagString s, FUENMLEntities db)
        {   //用於新增TAG
            List<int> tag_id = new List<int>();
            foreach (string item in s.tag_str)
            {
                if (!(db.tags.Where(q => q.name == item)).Any())
                {

                    tag newtag = new tag();
                    newtag.name = item;
                    newtag.type = 2;
                    db.tags.Add(newtag);
                    db.SaveChanges();
                }
                tag_id.AddRange(db.tags.Where(p => p.name == item).Select(q => q.id).ToList());



            }
            return tag_id.Distinct().ToArray();
        }
    }

}