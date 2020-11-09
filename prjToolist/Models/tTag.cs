using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjToolist.Models
{
    // for common/get_recommend_lists
    public class tTag
    {
        public int id { get; set; }
        public int type { get; set; }
        public string name { get; set; }
    }

    public class tagString
    {
        public string[] tag_str { get; set; }
    }

    public class tagFilter
    {
        public int[] filter { get; set; }
    }

    public class tTagEvent
    {
        //public int user_id { get; set; }
        public int tag_id { get; set; }
        public int action { get; set; }
    }

    public class tagInfo
    {
        public string name { get; set; }
        public int type { get; set; }
    }

    public class tTagRelation
    {
        public int user_id { get; set; }
        public string gmap_id { get; set; }
        public int[] tag_id { get; set; }
    }

    public class viewModelSerachTag
    {
        public string gmap_id { get; set; }
        public string text { get; set; }
    }

    public class viewModelTagChange
    {
        public int place_id { get; set; }
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

    public class updateTagRelation
    {
        public string place_name { get; set; }
        public string tag_name { get; set; }
        public string user_name { get; set; }
    }

    public static class tagFactory
    {

        public static int[] tagStringToId(string s, FUENMLEntities db)
        {
            //用於搜尋TAG
            List<int> tag_id = new List<int>();

            if (s != "" && (db.tags.Where(q => q.name.Contains(s))).Any())
            {

                var tagid = from p in db.tags
                            where (p.name.Contains(s))
                            select p;
                foreach (tag t in tagid)
                {
                    tag_id.Add(t.id);
                }
            }

            return tag_id.Distinct().ToArray();
        }

        public static int[] checktagString(tagString s, FUENMLEntities db)
        {   //用於新增TAG
            List<int> tag_id = new List<int>();
            if (s.tag_str.Length > 0)
            {
                foreach (string item in s.tag_str)
                {
                    string trimString = item.Trim();
                    if (!(db.tags.Where(q => q.name == trimString)).Any())
                    {
                        tag newtag = new tag();
                        newtag.name = trimString;
                        newtag.type = 2;
                        db.tags.Add(newtag);
                        db.SaveChanges();
                    }
                    tag_id.AddRange(db.tags.Where(p => p.name == trimString).Select(q => q.id).ToList());
                }
            }
            return tag_id.Distinct().ToArray();
        }


        public static List<int> searchTag(int userlogin, ref List<int> intersectResult, int i, FUENMLEntities db)
        {
            //用於自動完成 回傳相關tag autocomplete
            var searchplacehastag = db.tagRelationships.Where(P => P.tag_id == i).Select(q => q.place_id).ToList();
            if (userlogin != 0)
            {
                searchplacehastag = db.tagRelationships.Where(P => P.tag_id == i && P.user_id == userlogin).Select(q => q.place_id).ToList();
            }
            //searchplacehastag = searchplacehastag.Distinct().ToList();
            intersectResult = intersectResult.Intersect(searchplacehastag).ToList();
            return intersectResult;
        }

        //public static List<int> listFilter(ref List<int> intersectPlaceList, int i, FUENMLEntities db)
        //{
        //    var 
        //}
    }
}