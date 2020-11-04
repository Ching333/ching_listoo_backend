using prjToolist.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using static prjToolist.Controllers.CommonController;

namespace prjToolist.Controllers
{
    [RoutePrefix("user")]
    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    public class UserController : ApiController
    {
        FUENMLEntities db = new FUENMLEntities();

        [Route("get_user_places")]
        [HttpPost]
        public HttpResponseMessage get_user_places([FromBody] tagFilter tfilter)
        {
            int userlogin = 0;
            int[] tFilterid = tfilter.filter;
            List<int> intersectResult = new List<int>();
            List<string> systemTagResult = new List<string>();
            List<int> tagsList = new List<int>();
            List<tTag> resultTagInfo = new List<tTag>();
            List<placeInfo> resultPlaceInfo = new List<placeInfo>();
            var dataForm = new
            {
                alltags = tagsList,
                tags = resultTagInfo,
                placesId = intersectResult,
                places=resultPlaceInfo,
                systemtags= systemTagResult
            };
            var result = new
            {
                status = 0,
                msg = "fail",
                data = dataForm
            };
            userlogin = userFactory.userIsLoginSession(userlogin);
            if (userlogin != 0)
            {
                intersectResult = db.tagRelations.Where(p => p.user_id == userlogin).Select(q => q.place_id).ToList();
                intersectResult = intersectResult.Distinct().ToList();
            }
            if (userlogin != 0 && tFilterid != null && tFilterid.Length > 0)
            {

                foreach (int i in tFilterid)
                {
                    intersectResult = tagFactory.searchTag(userlogin, ref intersectResult, i,db);
                        
                }
                intersectResult = intersectResult.Distinct().ToList();

                dataForm = new
                {
                    alltags = tagsList,
                    tags = resultTagInfo,
                    placesId = intersectResult,
                    places = resultPlaceInfo,
                    systemtags = systemTagResult
                };
                result = new
                {
                    status = 1,
                    msg = "Success",
                    data = dataForm
                };
            }
            if (intersectResult.Count > 0)
            {
                foreach (int i in intersectResult)
                {   
                    var placeItem = db.places.Where(p => p.id == i).Select(q => q).FirstOrDefault();
                    if (placeItem != null) {
                        if(placeItem.type != null) {systemTagResult.Add(placeItem.type);}
                   

                    placeInfo rPlace = new placeInfo();
                    rPlace.name = placeItem.name;
                    rPlace.phone = placeItem.phone;
                    rPlace.address = placeItem.address;
                    rPlace.type = placeItem.type;
                    rPlace.longitude = placeItem.longitude;
                    rPlace.latitude = placeItem.latitude;
                    resultPlaceInfo.Add(rPlace);
                    }
                    tagsList.AddRange(db.tagRelations.Where(p => p.place_id == i && p.user_id == userlogin).Select(q => q.tag_id).ToList());
                    dataForm = new
                    {
                        alltags = tagsList,
                        tags = resultTagInfo,
                        placesId = intersectResult,
                        places = resultPlaceInfo,
                        systemtags = systemTagResult
                    };
                    result = new
                    {
                        status = 1,
                        msg = "Success",
                        data = dataForm
                    };
                    //var test = from p in db.tagRelations where p.place_id == i group p.tag_id by p.user_id == userlogin ?"userTag":"othersTag" into g select new {g.Key } ;
                }
                tagsList = tagsList.Distinct().ToList();

                if (tagsList.Count > 0)
                {
                    foreach (int i in tagsList)
                    {
                        var rtag = db.tags.Where(p => p.id == i && p.type == 1).Select(q => q).FirstOrDefault();
                        if (rtag != null)
                        {
                            tTag t = new tTag();
                            t.id = rtag.id;
                            t.name = rtag.name;
                            t.type = rtag.type;
                            resultTagInfo.Add(t);
                        }
                    }
                    dataForm = new
                    {
                        alltags = tagsList,
                        tags = resultTagInfo,
                        placesId = intersectResult,
                        places = resultPlaceInfo,
                        systemtags = systemTagResult
                    };
                    result = new
                    {
                        status = 1,
                        msg = "Success",
                        data = dataForm
                    };
                }
               
            }
            var resp = Request.CreateResponse(
              HttpStatusCode.OK,
              result
              );
            return resp;
        }

        [Route("get_user_lists")]
        [HttpPost]
        public HttpResponseMessage get_user_lists([FromBody] tagFilter tfilter)
        {
            int userlogin = 0;
            //int[] tFilterid = tagFactory.tagStringToId(s, db);
            int[] tFilterid = tfilter.filter;
            List<int> userList = new List<int>();
            List<int> placesList = new List<int>();
            List<int> tagsList = new List<int>();
            List<int> intersectResult = new List<int>();
            List<placeListInfo> infoList = new List<placeListInfo>();
            //if (HttpContext.Current.Session["SK_login"] != null)
            //{
            //    user x = HttpContext.Current.Session["SK_login"] as user;
            //    Debug.WriteLine("userid"+x.id);
            //    userlogin = x.id;

            //};
            userlogin = userFactory.userIsLoginSession(userlogin);
            //userlogin = userIsLoginCookie(userlogin);
            var dataForm = new
            {
                list = infoList,
                tags = tagsList,
                places = intersectResult
            };
            var result = new
            {
                status = 0,
                msg = "fail",
                data = dataForm
            };
            if (userlogin != 0)
            {
                userList = db.placeLists.Where(p => p.user_id == userlogin).Select(q => q.id).ToList();//使用者建立的全部清單

                if (userList != null)
                {
                    foreach (int r in userList)
                    {
                        //placeListInfo infoItem = new placeListInfo();
                        //var li = db.placeLists.Where(p => p.id == r && p.user_id == userlogin).Select(q => q).FirstOrDefault();
                        //if (li != null)
                        //{
                        //    infoItem.id = li.id;
                        //    infoItem.userId = li.user_id;
                        //    infoItem.name = li.name;
                        //    infoItem.description = li.description;
                        //    infoItem.privacy = li.privacy;
                        //    infoItem.createdTime = li.created.ToString();
                        //    infoItem.updatedTime = li.updated.ToString();
                        //    //byte[] binaryString = (byte[])place.cover;
                        //    //info.cover = Encoding.UTF8.GetString(binaryString);
                        //    infoList.Add(infoItem);
                        //}
                        intersectResult.AddRange(db.placeRelations.Where(p => p.placeList_id == r).Select(q => q.place_id).ToList());
                    }
                    
                    intersectResult = intersectResult.Distinct().ToList();
                }
                dataForm = new
                {
                    list = infoList,
                    tags = tagsList,
                    places = intersectResult
                };
                result = new
                {
                    status = 1,
                    msg = "OK",
                    data = dataForm
                };
            }
            if (userlogin != 0&&tFilterid !=null&& tFilterid.Length>0)
            {
                foreach (int i in tFilterid)
                {
                    intersectResult = tagFactory.searchTag(userlogin, ref intersectResult, i,db);

                    //var searchplacehastag = db.tagRelations.Where(P => P.tag_id == i).Select(q => q.place_id).ToList();
                    //searchplacehastag = searchplacehastag.Distinct().ToList();
                    //intersectResult = intersectResult.Intersect(searchplacehastag).ToList();
                }
                intersectResult = intersectResult.Distinct().ToList();
                if (intersectResult.Count <=0)
                {
                    userList=new List<int>();
                }
                dataForm = new
                {
                    list = infoList,
                    tags = tagsList,
                    places = intersectResult//地點編號
                };
                result = new
                {
                    status = 1,
                    msg = "OK",
                    data = dataForm
                };
            }
            if (intersectResult.Count > 0)
            {
                Debug.WriteLine("有搜尋到交集地點或清單中有地點");
                foreach (int j in intersectResult)
                {  
                    //篩選出這些地點的所有tag
                    tagsList.AddRange(db.tagRelations.Where(p => p.place_id == j).Select(q => q.tag_id).ToList());
                }
                tagsList = tagsList.Distinct().ToList();//最終tag結果

                if (tFilterid != null && tFilterid.Length > 0) {
                    //若篩選出有共同標籤地點就更新清單  不然使使用原本清單
                    foreach (int j in intersectResult)
                    {
                        placesList.AddRange(db.placeRelations.Where(p => p.place_id == j).Select(q => q.placeList_id).ToList());
                        userList = userList.Intersect(placesList).ToList();
                    }
                userList = userList.Distinct().ToList();//最終清單結果
                }
                dataForm = new
                {
                    list = infoList,
                    tags = tagsList,
                    places = intersectResult//地點編號
                };
                result = new
                {
                    status = 1,
                    msg = "OK",
                    data = dataForm
                };
            }

            if (userList.Count > 0 && userlogin != 0)
            {  
                infoList = new List<placeListInfo>();//初始化原本的結果
                foreach (int r in userList)
                {
                    placeListInfo infoItem = new placeListInfo();
                    var li = db.placeLists.Where(p => p.id == r && p.user_id == userlogin).Select(q => q).FirstOrDefault();
                    if (li != null)
                    {
                        infoItem.id = li.id;
                        infoItem.userId = li.user_id;
                        infoItem.name = li.name;
                        infoItem.description = li.description;
                        infoItem.privacy = li.privacy;
                        infoItem.createdTime = li.created!= null?li.created.ToString().Substring(0,10):"";
                        infoItem.updatedTime = li.updated!=null?li.updated.ToString().Substring(0,10):"";
                        //byte[] binaryString = (byte[])place.cover;
                        //info.cover = Encoding.UTF8.GetString(binaryString);
                        infoList.Add(infoItem);
                    }
                }
                dataForm = new
                {
                    list = infoList,
                    tags = tagsList,
                    places = intersectResult//地點編號
                };
                result = new
                {
                    status = 1,
                    msg = "OK",
                    data = dataForm
                };
            }
           var resp = Request.CreateResponse(
           HttpStatusCode.OK,
           result
           );
            return resp;

        }

        //public List<int> searchTag(int userlogin, ref List<int> intersectResult, int i)
        //{

        //    var searchplacehastag = db.tagRelations.Where(P => P.tag_id == i).Select(q => q.place_id).ToList();
        //    if (userlogin != 0)
        //    {   
        //        searchplacehastag = db.tagRelations.Where(P => P.tag_id == i && P.user_id == userlogin).Select(q => q.place_id).ToList();
        //    }
        //    //searchplacehastag = searchplacehastag.Distinct().ToList();
        //    intersectResult = intersectResult.Intersect(searchplacehastag).ToList();
        //    return intersectResult;
        //}


        [Route("test_union")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage test_union()
        {
            List<int> searchallplaceinlist = new List<int> { 1, 2, 3, 4, 5, 6, 8, 9, 1, 0, 5, 6 };
            List<int> unionResult = searchallplaceinlist;
            List<int> intersectResult = searchallplaceinlist;
            List<int> searchplacehastag=new List<int> { 1, 2, 3, 0,100,99};
            List<int> searchplacehastag2 = new List<int> { 1, 2,4,100 };
            //交集
            intersectResult = intersectResult.Intersect(searchplacehastag).ToList();
            intersectResult = intersectResult.Intersect(searchplacehastag2).ToList();
            //聯集
            unionResult = unionResult.Union(searchplacehastag).ToList();
            //var result = new
            //{
            //    status = 0,
            //    msg = $"fail",
            //    data = searchallplaceinlist.ToList()
            //};

            var result = new
            {
                status = 0,
                msg = "OK",
                data = intersectResult
            };
            var resp = Request.CreateResponse(
          HttpStatusCode.OK,
          result
          );
            return resp;

        }

        [Route("create_list")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage create_list([FromBody]viewModelPlaceList x)
        {
            int listId = 0;
            int userlogin = 0;
            if (Request.Headers.Contains("session-id"))
            {
                userlogin = int.Parse(Request.Headers.GetValues("session-id").FirstOrDefault());
            }
            var dataform = new
            {
                id= listId
            };
            var result = new
            {
                status = 0,
                msg = $"fail",
                data = dataform
            };
            if (userlogin != 0)
            {
                placeList newList = new placeList();
                newList.user_id = userlogin;
                newList.name = x.name;
                newList.description = x.description;
                newList.privacy = x.privacy;
                newList.created = DateTime.Now;

                db.placeLists.Add(newList);
                db.SaveChanges();
                //listId = int.Parse(db.placeLists.OrderByDescending(p => p.id).Take(1).ToString());
                //var listId1 = (from p in db.placeLists.AsEnumerable()
                //               orderby p.id descending
                //               select p.id).Take(1).ToArray();
               listId = int.Parse((from p in db.placeLists.AsEnumerable()
                               
                               select p.id).Last().ToString());
               
                //listId = listId1[0];
                //Debug.WriteLine(listId);
                if (listId > 0) {
                    //回傳newlistId
                    Debug.WriteLine(listId);
                    dataform = new
                {
                    id = listId
                };
                result = new
                    {
                        status = 1,
                        msg = "OK",
                        data = dataform
                    };
                }

            }
            if (x.places.Length > 0&& listId>0)
            {
                foreach(int i in x.places)
                {
                    placeRelation newListPlaces = new placeRelation();
                    var q = db.places.Where(p => p.id == i).Select(r => r).Any();
                    if (q) {
                    newListPlaces.placeList_id = listId;
                    newListPlaces.place_id = i;

                    db.placeRelations.Add(newListPlaces);
                    db.SaveChanges();
                    }
                }
            }
            var resp = Request.CreateResponse(
            HttpStatusCode.OK,
            result
            );
            return resp;
        }

        [Route("add_list_places")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage list_add_place(viewModelEditListPlace x)
        {
            var result = new
            {
                status = 0,
                msg = $"fail",
            };
            var hasList = db.placeLists.Where(p => p.id == x.list_id).Select(r => r).Any();
            if ( x.places.Length > 0 && hasList)
            {
                foreach (int i in x.places)
                {
                    placeRelation newListPlaces = new placeRelation();
                    var q = db.places.Where(p => p.id == i).Select(r => r).Any();
                    var t = db.placeRelations.Where(p => p.place_id==i&&p.placeList_id== x.list_id).Select(r => r).Any();
                    if (q&&(!t))
                    {
                        newListPlaces.placeList_id = x.list_id;
                        newListPlaces.place_id = i;

                        db.placeRelations.Add(newListPlaces);
                        db.SaveChanges();
                    }
                }
                result = new
                {
                    status = 1,
                    msg = "OK",

                };
            }

            var resp = Request.CreateResponse(
            HttpStatusCode.OK,
            result
            );
            return resp;
        }

        [Route("remove_list_places")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage list_remove_place(viewModelEditListPlace x)
        {
            var result = new
            {
                status = 0,
                msg = $"fail",

            };
            var hasList = db.placeLists.Where(p => p.id == x.list_id).Select(r => r).Any();
            if (x.places.Length > 0 && hasList)
            {
                foreach (int i in x.places)
                {
                    
                    var t = db.placeRelations.Where(p => p.place_id == i && p.placeList_id == x.list_id).Select(r => r).FirstOrDefault();
                    if (t!=null)
                    {
                        db.placeRelations.Remove(t);
                 
                        db.SaveChanges();
                    }
                }
                result = new
                {
                    status = 1,
                    msg = "OK",

                };
            }
          var resp = Request.CreateResponse(
          HttpStatusCode.OK,
          result
          );
            return resp;
        }

        //TODO URL待確認是否有要get
        [Route("edit_list_info/{list_id:int}")]
        [HttpGet]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage list_edit_info(int list_id)
        {
            var result = new
            {
                status = 0,
                msg = new viewModelEditListInfo(),

            };
            var list = db.placeLists.Where(p => p.id == list_id).Select(q => q).FirstOrDefault();
            if (list != null)
            {
              viewModelEditListInfo x = new viewModelEditListInfo();
                x.name = list.name;
                x.description = list.description;
                x.privacy = list.privacy;

                result = new
                {
                    status = 1,
                    msg = x,

                };
            }
            var resp = Request.CreateResponse(
         HttpStatusCode.OK,
         result
         );
            return resp;
        }
        
        //TODO URL待確認是否有list_id 或包在BOdy
        [Route("list_edit_info/{list_id:int}")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage list_edit_info(int list_id, viewModelEditListInfo x)
        {
            var result = new
            {
                status = 0,
                msg = $"fail",

            };
            var list = db.placeLists.Where(p => p.id == list_id).Select(q => q).FirstOrDefault();

            if (list != null)
            {
                list.name = x.name;
                list.description = x.description;
                list.privacy = x.privacy;
                list.updated = DateTime.Now;
                db.SaveChanges();
                result = new
                {
                    status = 1,
                    msg = "OK",

                };
            }

            var resp = Request.CreateResponse(
          HttpStatusCode.OK,
          result
          );
            return resp;
        }

        //TODO 後續add和remove記錄存起來
        [Route("modify_place_tag")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage modify_place_tag([FromBody] viewModelTagChange x)
        {
            int userlogin = 0;
            var result = new
            {
                status = 0,
                msg = "fail",

            };
            userlogin = userFactory.userIsLoginSession(userlogin);

            var place = db.places.Where(p => p.gmap_id == x.gmap_id).Select(q => q).FirstOrDefault();
            if (place != null && userlogin != 0)
            {
                //若資料可沒有此地點要新增一個地點 並回傳新增的placeid
                //if (place == null) {
                //    place newPlace = new place();
                //    newPlace.gmap_id = x.gmap_id;
                //}

                if (x.add.Length > 0)
                {
                    foreach (var i in x.add)
                    {
                        tagRelation t = new tagRelation();
                        t.tag_id = i;
                        t.place_id = place.id;
                        t.user_id = userlogin;
                        t.created = DateTime.Now;
                        db.tagRelations.Add(t);
                        db.SaveChanges();

                    }
                }
                if (x.remove.Length > 0)
                {
                    foreach (var j in x.remove)
                    {
                        var d = db.tagRelations.Where(p => p.place_id == place.id && p.tag_id == j).Select(q => q).FirstOrDefault();
                        if (d != null) { 
                        db.tagRelations.Remove(d);
                        db.SaveChanges();
                        }
                    }
                }
                if (x.newTags.Length > 0)
                {

                    int[] newTagId = tagFactory.checktagString(new tagString { tag_str = x.newTags }, db);
                    foreach (var i in newTagId)
                    {
                        tagRelation t = new tagRelation();
                        t.tag_id = i;
                        t.place_id = place.id;
                        t.user_id = userlogin;
                        t.created = DateTime.Now;
                        db.tagRelations.Add(t);
                        db.SaveChanges();

                    }
                }

                result = new
                {
                    status = 1,
                    msg = "Success",

                };
            }
            var resp = Request.CreateResponse(
          HttpStatusCode.OK,
          result
          );
            return resp;

        }

        [NonAction]
        public int userIsLoginCookie(int userlogin)
        {
            var currentCookie = Request.Headers.GetCookies("session-id").FirstOrDefault();
            if (Request.Headers.Contains("session-id"))
            {
                userlogin = int.Parse(Request.Headers.GetValues("session-id").FirstOrDefault());
            }
            //user_id
            return userlogin;
        }

    }
}  

    

