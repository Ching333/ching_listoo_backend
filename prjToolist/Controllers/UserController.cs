
using prjToolist.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using static prjToolist.Controllers.CommonController;
using static prjToolist.Models.tagFactory;
//using static prjToolist.Models.tagFactory;
using static prjToolist.Models.tTagRelation;
//using static prjToolist.Models.tTagRelation.tagFactory;


namespace prjToolist.Controllers
{
    [RoutePrefix("user")]
    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    //[JwtAuthActionFilter]
    public class UserController : ApiController
    {
        FUENMLEntities db = new FUENMLEntities();

        [Route("get_user_places")]
        [HttpPost]
        public HttpResponseMessage get_user_places(tagFilter tfilter)
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
                places = resultPlaceInfo,
                systemtags = systemTagResult
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
                intersectResult = db.tagRelationships.Where(p => p.user_id == userlogin).Select(q => q.place_id).ToList();
                intersectResult = intersectResult.Distinct().ToList();
            }
            if (userlogin != 0 && tFilterid != null && tFilterid.Length > 0)
            {
                foreach (int i in tFilterid)
                {
                    intersectResult = tagFactory.searchTag(userlogin, ref intersectResult, i, db);
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
                    msg = "",
                    data = dataForm
                };
            }

            if (intersectResult.Count > 0)
            {
                foreach (int i in intersectResult)
                {
                    var placeItem = db.places.Where(p => p.id == i).Select(q => q).FirstOrDefault();
                    if (placeItem != null)
                    {
                        if (placeItem.type != null) { systemTagResult.Add(placeItem.type); }

                        placeInfo rPlace = new placeInfo();
                        rPlace.name = placeItem.name;
                        rPlace.phone = placeItem.phone;
                        rPlace.address = placeItem.address;
                        rPlace.type = placeItem.type;
                        rPlace.longitude = placeItem.longitude;
                        rPlace.latitude = placeItem.latitude;
                        resultPlaceInfo.Add(rPlace);
                    }
                    tagsList.AddRange(db.tagRelationships.Where(p => p.place_id == i && p.user_id == userlogin).Select(q => q.tag_id).ToList());

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
                        msg = "",
                        data = dataForm
                    };
                    //var test = from p in db.tagRelations where p.place_id == i group p.tag_id by p.user_id == userlogin ?"userTag":"othersTag" into g select new {g.Key } ;
                }
                tagsList = tagsList.Distinct().ToList();
                systemTagResult= systemTagResult.Distinct().ToList();
                if (tagsList.Count > 0)
                {
                    foreach (int i in tagsList)
                    {
                        var rtag = db.tags.Where(p => p.id == i && p.type == 2).Select(q => q).FirstOrDefault();
                        if (rtag != null)
                        {
                            tTag t = new tTag();
                            t.id = rtag.id;
                            t.name = rtag.name;
                            //t.type = rtag.type;
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
                        msg = "",
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
        public HttpResponseMessage get_user_lists(tagFilter tfilter)
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
                        intersectResult.AddRange(db.placeRelationships.Where(p => p.placelist_id == r).Select(q => q.place_id).ToList());
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
                    msg = "",
                    data = dataForm
                };
            }

            if (userlogin != 0 && tFilterid != null && tFilterid.Length > 0)
            {
                foreach (int i in tFilterid)
                {
                    intersectResult = tagFactory.searchTag(userlogin, ref intersectResult, i, db);
                    //var searchplacehastag = db.tagRelations.Where(P => P.tag_id == i).Select(q => q.place_id).ToList();
                    //searchplacehastag = searchplacehastag.Distinct().ToList();
                    //intersectResult = intersectResult.Intersect(searchplacehastag).ToList();
                }
                intersectResult = intersectResult.Distinct().ToList();

                if (intersectResult.Count <= 0)
                {
                    userList = new List<int>();
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
                    msg = "",
                    data = dataForm
                };
            }

            if (intersectResult.Count > 0)
            {
                //Debug.WriteLine("有搜尋到交集地點或清單中有地點");
                foreach (int j in intersectResult)
                {
                    //篩選出這些地點的所有tag
                    tagsList.AddRange(db.tagRelationships.Where(p => p.place_id == j).Select(q => q.tag_id).ToList());
                }
                tagsList = tagsList.Distinct().ToList();//最終tag結果

                if (tFilterid != null && tFilterid.Length > 0)
                {
                    //若篩選出有共同標籤地點就更新清單  不然使使用原本清單
                    foreach (int j in intersectResult)
                    {
                        placesList.AddRange(db.placeRelationships.Where(p => p.place_id == j).Select(q => q.placelist_id).ToList());
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
                    msg = "",
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
                        infoItem.createdTime = li.created != null ? li.created.ToString().Substring(0, 10) : "";
                        infoItem.updatedTime = li.updated != null ? li.updated.ToString().Substring(0, 10) : "";
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

        [Route("test_union")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage test_union()
        {
            List<int> searchallplaceinlist = new List<int> { 1, 2, 3, 4, 5, 6, 8, 9, 1, 0, 5, 6 };
            List<int> unionResult = searchallplaceinlist;
            List<int> intersectResult = searchallplaceinlist;
            List<int> searchplacehastag = new List<int> { 1, 2, 3, 0, 100, 99 };
            List<int> searchplacehastag2 = new List<int> { 1, 2, 4, 100 };
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
                status = 1,
                msg = "",
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
            //if (Request.Headers.Contains("session-id"))
            //{
            //    userlogin = int.Parse(Request.Headers.GetValues("session-id").FirstOrDefault());
            //}
            var dataform = new
            {
                id = listId
            };
            var result = new
            {
                status = 0,
                msg = "fail",
                data = dataform
            };
            userlogin = userFactory.userIsLoginSession(userlogin);
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
                if (listId > 0)
                {
                    //回傳newlistId
                    //Debug.WriteLine(listId);
                    dataform = new
                    {
                        id = listId
                    };

                    result = new
                    {
                        status = 1,
                        msg = "",
                        data = dataform
                    };
                }
            }

            if (x.places.Length > 0 && listId > 0)
            {
                foreach (int i in x.places)
                {
                    placeRelationship newListPlaces = new placeRelationship();
                    var q = db.places.Where(p => p.id == i).Select(r => r).Any();
                    if (q)
                    {
                        newListPlaces.placelist_id = listId;
                        newListPlaces.place_id = i;

                        db.placeRelationships.Add(newListPlaces);
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
                msg = "fail",
            };

            var hasList = db.placeLists.Where(p => p.id == x.list_id).Select(r => r).Any();
            if (x.places.Length > 0 && hasList)
            {
                foreach (int i in x.places)
                {
                    placeRelationship newListPlaces = new placeRelationship();
                    var q = db.places.Where(p => p.id == i).Select(r => r).Any();
                    var t = db.placeRelationships.Where(p => p.place_id == i && p.placelist_id == x.list_id).Select(r => r).Any();
                    if (q && (!t))
                    {
                        newListPlaces.placelist_id = x.list_id;
                        newListPlaces.place_id = i;

                        db.placeRelationships.Add(newListPlaces);
                        db.SaveChanges();
                    }
                }

                result = new
                {
                    status = 1,
                    msg = "",
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
                msg = "fail",
            };
            var hasList = db.placeLists.Where(p => p.id == x.list_id).Select(r => r).Any();
            if (x.places.Length > 0 && hasList)
            {
                foreach (int i in x.places)
                {
                    var t = db.placeRelationships.Where(p => p.place_id == i && p.placelist_id == x.list_id).Select(r => r).FirstOrDefault();
                    if (t != null)
                    {
                        db.placeRelationships.Remove(t);
                        db.SaveChanges();
                    }
                }
                result = new
                {
                    status = 1,
                    msg = "",
                };
            }

            var resp = Request.CreateResponse(
            HttpStatusCode.OK,
            result
            );
            return resp;
        }

        [Route("search_user_places")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage search_user_places(queryPocketPlaceInfo vm_pocketPlaceInfo)
        {
            int userlogin = 0;
            List<int> placeInDBResult = new List<int>();
            List<int> intersectResult = new List<int>();
            List<placeInfo> resultPlaceInfo = new List<placeInfo>();
            var dataForm = new
            {
                places = resultPlaceInfo
            };
            var result = new
            {
                status = 0,
                msg = "fail",
                data= dataForm
            };
            placeInDBResult = db.places.Select(p => p.id).ToList();
            userlogin = userFactory.userIsLoginSession(userlogin);
            var hasList = db.placeLists.Where(p => p.id == vm_pocketPlaceInfo.list_id).Select(r => r).Any();
            if (hasList&& userlogin!=0)
            {
                
                if (vm_pocketPlaceInfo.text != "") { 
                   vm_pocketPlaceInfo.text = vm_pocketPlaceInfo.text.Trim();
                }
                intersectResult = db.tagRelationships.Where(p => p.user_id == userlogin).Select(q => q.place_id).ToList();
                intersectResult = intersectResult.Distinct().ToList();
                if (intersectResult.Count > 0)
                {
                    foreach (int i in intersectResult)
                    {
                        var pocketPlace= db.places.Where(p => p.id == i && p.name.Contains(vm_pocketPlaceInfo.text)).Select(q => q).FirstOrDefault();
                        if (vm_pocketPlaceInfo.text != "") {
                            pocketPlace = db.places.Where(p => p.id == i && p.name.Contains(vm_pocketPlaceInfo.text)).Select(q => q).FirstOrDefault();
                        }
                        
                        if (pocketPlace != null)
                        {
                            placeInfo placeinfo = new placeInfo();
                            placeinfo.id = pocketPlace.id;
                            placeinfo.gmap_id = pocketPlace.gmap_id;
                            placeinfo.name = pocketPlace.name;
                            placeinfo.phone = pocketPlace.phone;
                            resultPlaceInfo.Add(placeinfo);
                        }
                    }
                    dataForm = new
                    {
                        places = resultPlaceInfo
                    };
                    result = new
                    {
                        status = 1,
                        msg = "success",
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

        //TODO URL待確認是否有要get
        [Route("list_edit_info/{list_id:int}")]
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

        [Route("modify_place_tag")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage modify_place_tag(viewModelTagChange x)
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
                //若資料庫沒有此地點 要新增一個地點 並回傳新增的placeid
                //if (place == null) {
                //    place newPlace = new place();
                //    newPlace.gmap_id = x.gmap_id;
                //}
                if (x.add.Length > 0)
                {
                    foreach (var i in x.add)
                    {
                        var hastag = db.tags.Where(p => p.id == i).Any();
                        var placehastag = db.tagRelationships.Where(p => p.tag_id == i && p.place_id == place.id).Any();
                        if (hastag&&!placehastag) { 
                        tagRelationship t = new tagRelationship();
                        t.tag_id = i;
                        t.place_id = place.id;
                        t.user_id = userlogin;
                        db.tagRelationships.Add(t);
                        db.SaveChanges();

                        tagEvent newEvent = new tagEvent();
                        newEvent.tag_id = i;
                        newEvent.user_id = userlogin;
                        newEvent.tagEvent1 = 3;
                        newEvent.created = DateTime.Now;
                        db.tagEvents.Add(newEvent);
                        db.SaveChanges();
                        }
                    }
                }
                if (x.remove.Length > 0)
                {
                    foreach (var j in x.remove)
                    {
                        var hastag = db.tags.Where(p => p.id == j).Any();
                        var d = db.tagRelationships.Where(p => p.place_id == place.id && p.tag_id == j).Select(q => q).FirstOrDefault();
                        if (hastag&&d != null)
                        {
                            db.tagRelationships.Remove(d);
                            db.SaveChanges();

                            tagEvent newEvent = new tagEvent();
                            newEvent.tag_id = j;
                            newEvent.user_id = userlogin;
                            newEvent.tagEvent1 = 4;
                            newEvent.created = DateTime.Now;
                            db.tagEvents.Add(newEvent);
                            db.SaveChanges();

                        }
                    }
                }

                if (x.newTags.Length > 0)
                {
                    int[] newTagId = tagFactory.checktagString(new tagString { tag_str = x.newTags }, db);
                    if (newTagId.Length > 0) { 
                    foreach (var i in newTagId)
                    {
                        tagRelationship t = new tagRelationship();
                        t.tag_id = i;
                        t.place_id = place.id;
                        t.user_id = userlogin;
                        t.created = DateTime.Now;
                        db.tagRelationships.Add(t);
                        db.SaveChanges();
                    }
                    }
                }

                result = new
                {
                    status = 1,
                    msg = "",
                };
            }
            var resp = Request.CreateResponse(
          HttpStatusCode.OK,
          result
          );
            return resp;
        }

        [Route("modify_list_photo")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage modify_list_photo()
        {
            var result = new
            {
                status = 0,
                msg = "fail",
            };
            
            var httpRequest = HttpContext.Current.Request;
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    var docfiles = new List<string>();
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        FileInfo uploadfile = new FileInfo(postedFile.FileName);
                        string photoName = Guid.NewGuid().ToString() + uploadfile.Extension;
                        var filePath = HttpContext.Current.Server.MapPath("~/Storage/upload/" + photoName);
                        postedFile.SaveAs(filePath);
                        docfiles.Add(filePath);
                    }
                    result = new
                    {
                        status = 1,
                        msg = "上傳成功",
                    };
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
                result = new
                {
                    status = 0,
                    msg = "檔案大小超過限制",
                };
            }
            
            var resp = Request.CreateResponse(
          HttpStatusCode.OK,
          result
          );
            return resp;
        }

        [Route("save_list_photo")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage save_list_photo(viewModelSaveListCover vm_saveListListCover)
        {
            var result = new
            {
                status = 0,
                msg = "fail",
            };
            var list = db.placeLists.Where(p => p.id == vm_saveListListCover.list_id).Select(q => q).FirstOrDefault();
            if (list!=null&& vm_saveListListCover.coverUrl != "")
            {
                try
                {
                    //list.cover = byte[];
                    db.SaveChanges();
                }catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                    result = new
                    {
                        status = 0,
                        msg = "修改圖片失敗",
                    };
                }

                result = new
                {
                    status = 1,
                    msg = "修改圖片成功",
                };
            }
            var resp = Request.CreateResponse(
            HttpStatusCode.OK,
            result
            );
            return resp;

        }

            private static int userIsLoginSession(int userlogin)
        {
            if (HttpContext.Current.Session["SK_login"] != null)
            {
                user u = HttpContext.Current.Session["SK_login"] as user;
                Debug.WriteLine("userid" + u.id);
                userlogin = u.id;
            };
            return userlogin;
        }

        private int userIsLoginCookie(int userlogin)
        {
            var currentCookie = Request.Headers.GetCookies("session-id").FirstOrDefault();
            if (Request.Headers.Contains("session-id"))
            {
                userlogin = int.Parse(Request.Headers.GetValues("session-id").FirstOrDefault());
            }

            return userlogin;
        }
    }
}