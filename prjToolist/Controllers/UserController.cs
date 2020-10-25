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
            int[] tFilterid = tagStringToId(s);
            List<int> userList = new List<int>();
            List<int> placesList = new List<int>();
            List<int> tagsList = new List<int>();
            List<int> unionResult = new List<int>();
            List<placeListInfo> infoList = new List<placeListInfo>();



            var dataForm = new
            {
                list = infoList,

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
                userList = db.placeLists.Where(p => p.user_id == userlogin).Select(q => q.id).ToList();
                if (userList != null) { 
                    foreach (int r in userList)
                    {
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
                    msg = "Sucess",
                    data = dataForm
                };
            }

           
            //}
            //    string sessionId = "";

            //    CookieHeaderValue cookie = Request.Headers.GetCookies("session-id").FirstOrDefault();
            //    if (cookie != null)
            //    {

            //        sessionId = cookie["session-id"].Value;
            //    }
            //var result = new
            //{
            //    userList1 = userList
            //};




            //if (userList != null)
            //{
            //    result = new
            //    {
            //        status = 1,
            //        msg = "Sucess",
            //        data = userList[0]
            //    };
            //}
            if (tFilterid != null)
            {
                foreach (int i in tFilterid)
                {
                    placesList.AddRange(db.tagRelations.Where(p => p.tag_id == i).Select(q => q.place_id).ToList());
                    //placesList = db.tagRelations.Where(p => p.tag_id == i).Select(q => q.place_id).ToList();
                    //placesList = db.tagRelations.Where(p => p.tag_id == i && p.user_id == userlogin).Select(q => q.place_id).ToList();
                    //foreach (int j in placesList)
                    //{
                    //    var tagList = db.placeRelations.Where(p => p.place_id == j).Select(q => q.placeList_id).ToArray();
                    //    foreach (int k in tagList)
                    //    {
                    //        var List = db.placeLists.Where(p => p.id == k).Select(q => q.id);
                    //        unionResult = userList.Union(List).ToList();
                    //    }


                    //}


                }
                placesList = placesList.Distinct().ToList();
                foreach (int j in placesList)
                {
                    tagsList.AddRange(db.placeRelations.Where(p => p.place_id == j).Select(q => q.placeList_id).ToList());
                    Debug.WriteLine("1");
                }
                
                tagsList = tagsList.Distinct().ToList();
                //result = new
                //{
                //    userList1 = tagsList
                //    //userList1 = placesList
                //    };
            }
            
            if (tagsList.Count>0&& userlogin != 0) {
                infoList= new List<placeListInfo>();
                foreach (int r in tagsList)
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
                    msg = "Sucess",
                    data = dataForm
                };
            }
            
            

            //if (infoList.Count()>0)
            //{
            //    result = new
            //    {
            //        status = 1,
            //        msg = "Sucess",
            //        data = dataForm
            //    };
            //}
            
           
            
           

            //result ={
            //status: 1,data:{lists: [List]},msg: "",}
            var resp = Request.CreateResponse(
           HttpStatusCode.OK,
           result
           );
            return resp;

        }



        [Route("create_list")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage create_list([FromBody] tagString s)
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

        [Route("list/{list_id:int}/add_place")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage list_add_place(int list_id, placeId s)
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

        [Route("list/{list_id:int}/remove_place")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage list_remove_place(int list_id, placeId s)
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

        [Route("list/{list_id:int}/edit_name")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage list_edit_name(int list_id, placeId s)
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


        [Route("list/{list_id:int}/edit_description")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage list_edit_description(int list_id, placeId s)
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
        [Route("list/{list_id:int}/edit_privacy")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage list_edit_privacy(int list_id, placeId s)
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

        public int[] tagStringToId(tagString s)
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

    }
}  

    

