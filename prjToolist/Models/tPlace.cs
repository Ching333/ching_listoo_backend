using System;
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

    // for user/get_user_places
    // for user/search_user_places
    public class userPlace
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal longitude { get; set; }
        public decimal latitude { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string type { get; set; }
        public string gmap_id { get; set; }
        public string photo_url { get; set; }
    }

    // for user/get_user_lists
    public class userListInfo
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int privacy { get; set; }
        public string createdTime { get; set; }
        public string updatedTime { get; set; }
        public string coverImageURL { get; set; }
    }

    //for common/get_list_detail
    public class listDetailPlace
    {
        public int id { get; set; }
        public string gmap_id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string type { get; set; }
        public string photo_url { get; set; }
    }
    
    //for query/get_place_info
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
    
    //for query/get_place_selectoin
    public class placeSelection
    {
        public int place_id { get; set; }
        public string name { get; set; }
    }

    //for user/search_user_places
    public class queryPocketPlaceInfo
    {
        public int list_id { get; set; }
        public string text { get; set; }
    }
}