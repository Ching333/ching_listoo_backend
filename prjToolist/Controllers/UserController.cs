using prjToolist.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using static prjToolist.Controllers.CommonController;

namespace prjToolist.Controllers
{
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        FUENMLEntities db = new FUENMLEntities();

        [Route("get_user_lists")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage get_user_lists([FromBody] tagString s)
        {
           

            int userlogin = 0;
            int[] tFilterid = tagFactory.tagStringToId(s, db);
            List<int> userList = new List<int>();
            List<int> placesList = new List<int>();
            List<int> tagsList = new List<int>();
            List<int> intersectResult = new List<int>();
            List<placeListInfo> infoList = new List<placeListInfo>();



            var dataForm = new
            {
                list = infoList,
                tags= tagsList,
                places=placesList
            };

            var result = new
            {
                status = 0,
                msg = "fail",
                data = dataForm
            };


            //var currentCookie = Request.Headers.GetCookies("session-id").FirstOrDefault();
            if (Request.Headers.Contains("session-id")) {
            userlogin = int.Parse(Request.Headers.GetValues("session-id").FirstOrDefault());
            }

            //string sessionId = "";

            //CookieHeaderValue currentCookie = Request.Headers.GetCookies("session-id").FirstOrDefault();


            //if (currentCookie != null)
            //{
            //sessionId = currentCookie["session-id"].Value;
            //userlogin = int.Parse(currentCookie.ToString());
            if (userlogin != 0) { 
                userList = db.placeLists.Where(p => p.user_id == userlogin).Select(q => q.id).ToList();//使用者建立的全部清單
               
                if (userList != null) { 
                    foreach (int r in userList)
                    {
                        intersectResult.AddRange( db.placeRelations.Where(p=>p.placeList_id==r).Select(q=>q.place_id).ToList());


                        placeListInfo infoItem = new placeListInfo();
                        var li = db.placeLists.Where(p => p.id == r && p.user_id == userlogin).Select(q => q).FirstOrDefault();
                        if (li != null)
                        {
                            infoItem.userId = li.user_id;
                            infoItem.name = li.name;
                            infoItem.description = li.description;
                            infoItem.privacy = li.privacy;
                            infoItem.createdTime = li.created.ToString();
                            infoItem.updatedTime = li.updated.ToString();
                            //byte[] binaryString = (byte[])place.cover;
                            //info.cover = Encoding.UTF8.GetString(binaryString);
                            infoList.Add(infoItem);
                        }
                    }
                }
                result = new
                {
                    status = 1,
                    msg = "OK",
                    data = dataForm
                };
            }

           
            if (tFilterid != null)
            {
                foreach (int i in tFilterid)
                {
                    var searchplacehastag = db.tagRelations.Where(P => P.tag_id == i).Select(q => q.place_id).ToList();
                    intersectResult = intersectResult.Intersect(searchplacehastag).ToList();
                    


                }
                
            }
            if(intersectResult.Count > 0)
            {
                foreach (int j in intersectResult)
                {  //篩選出有共同標籤地點的清單
                    placesList.AddRange(db.placeRelations.Where(p => p.place_id == j).Select(q => q.placeList_id).ToList());
                    Debug.WriteLine("1");

                    //篩選出這些地點的所有tag
                    tagsList.AddRange(db.tagRelations.Where(p => p.place_id == j).Select(q => q.tag_id).ToList());
                }

                placesList = placesList.Distinct().ToList();//最終清單結果
                tagsList = tagsList.Distinct().ToList();//最終tag結果
                dataForm = new
                {
                    list = infoList,
                    tags = tagsList,
                    places = placesList
                };

            }
            
            if (placesList.Count>0&& userlogin != 0) {
                infoList= new List<placeListInfo>();//初始化原本的結果
                foreach (int r in placesList)
                {   placeListInfo infoItem = new placeListInfo();
                    var li= db.placeLists.Where(p => p.id == r&&p.user_id==userlogin).Select(q => q).FirstOrDefault();
                    if (li != null)
                    { 
                    infoItem.userId = li.user_id;
                    infoItem.name = li.name;
                    infoItem.description = li.description;
                    infoItem.privacy = li.privacy;
                    infoItem.createdTime = li.created.ToString();
                    infoItem.updatedTime = li.updated.ToString();
                    //byte[] binaryString = (byte[])place.cover;
                    //info.cover = Encoding.UTF8.GetString(binaryString);
                    infoList.Add(infoItem);
                    }
                }
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

        [Route("list/{list_id:int}/add_place")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage list_add_place(int list_id, viewModelEditListPlace x)
        {
            var result = new
            {
                status = 0,
                msg = $"fail",

            };
            var hasList = db.placeLists.Where(p => p.id == list_id).Select(r => r).Any();
            if ( x.places.Length > 0 && hasList)
            {
                foreach (int i in x.places)
                {
                    placeRelation newListPlaces = new placeRelation();
                    var q = db.places.Where(p => p.id == i).Select(r => r).Any();
                    var t = db.placeRelations.Where(p => p.place_id==i&&p.placeList_id== list_id).Select(r => r).Any();
                    if (q&&(!t))
                    {
                        newListPlaces.placeList_id = list_id;
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

        [Route("list/{list_id:int}/remove_place")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage list_remove_place(int list_id, viewModelEditListPlace x)
        {
            var result = new
            {
                status = 0,
                msg = $"fail",

            };
            var hasList = db.placeLists.Where(p => p.id == list_id).Select(r => r).Any();
            if (x.places.Length > 0 && hasList)
            {
                foreach (int i in x.places)
                {
                    
                    var t = db.placeRelations.Where(p => p.place_id == i && p.placeList_id == list_id).Select(r => r).FirstOrDefault();
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

        [Route("list/{list_id:int}/list_edit_info")]
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


        //TODO  modify_place_tag

        [Route("modify_place_tag")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage modify_place_tag([FromBody] tagString s)
        {
            var result = new
            {
                status = 0,
                msg = $"fail",
                data = ""
            };
            var resp = Request.CreateResponse(
          HttpStatusCode.OK,
          result
          );
            return resp;

        }

        public class placeId
        {
            public int[] place_id { get; set; }
        }


        public class placeNewName
        {
            public string name { get; set; }
        }

        public class placeNewDescription
        {
            public string description { get; set; }
        }

        

    }
}  

    

