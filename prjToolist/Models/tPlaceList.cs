using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjToolist.Models
{
    //for common/get_recommend_lists
    public class tFilter
    {
        public int[] filter { get; set; }
    }

    // for common/get_recommend_lists
    public class tPlaceList
    {
        public int id { get; set; }
        public int creator_id { get; set; }
        public string name { get; set; }
        //public string description { get; set; }
        //public int privacy { get; set; }
        //public string createdTime { get; set; }
        //public string updatedTime { get; set; }
        public string coverImageURL { get; set; }
    }

    // for user/get_list_info
    public class getPlaceListbyId
    {
        public int list_id { get; set; }
    }

    // for user/get_list_info
    // for common/get_list_detail 
    public class placeListInfo
    {
        public int id { get; set; }
        public int creator_id { get; set; }
        public string name { get; set; }
        public string coverImageURL { get; set; }
        public string creator_username { get; set; }
        public int privacy { get; set; }
        public string description { get; set; }
        public string createdTime { get; set; }
        public string updatedTime { get; set; }
    }

    //for user/create_list
    public class viewModelPlaceList
    {
        public string name { get; set; }
        public string description { get; set; }
        public int privacy { get; set; }
        //public string coverImageURL { get; set; }
        public int[] places { get; set; }
    }

    //for user/add_list_places
    //for user/remove_list_places
    public class viewModelEditListPlace
    {
        public int[] places { get; set; }
        public int list_id { get; set; }
    }
    public class viewModelEditListInfo
    {
        public int list_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int privacy { get; set; }
    }

    //  for common/get_list_detail
    public class viewModelGetListPlace
    {
        public int list_id { get; set; }
        public int[] filter { get; set; }
    }

    public class viewModelSaveListCover
    {
        public int list_id { get; set; }
        public string coverUrl { get; set; }
    }

    //for user/set_list_photo
    public class viewModelSetListCover
    {
        public int list_id { get; set; }
        public string cover_image_url { get; set; }
    }

    //for query/get_place_list
    public class queryPlaceList
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string listName { get; set; }
        public string description { get; set; }
        public int privacy { get; set; }
        public string createdTime { get; set; }
        public string updatedTime { get; set; }
    }
}