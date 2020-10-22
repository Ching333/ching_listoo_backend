using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            List<int> unionResult = new List<int>();
            var currentCookie = Request.Headers.GetCookies("session-id").FirstOrDefault();
            if (currentCookie != null)
            {
                userlogin = int.Parse(currentCookie.ToString());
                userList = db.placeLists.Where(p => p.user_id == userlogin).Select(q => q.id).ToList();

            }
            if (tFilterid != null)
            {
                foreach (int i in tFilterid)
                {
                    
                    var placesList = db.tagRelations.Where(p => p.tag_id == i && p.user_id == userlogin).Select(q => q.place_id).ToArray();
                    foreach (int j in placesList)
                    {
                        var tagList = db.placeRelations.Where(p => p.place_id == j).Select(q => q.placeList_id).ToArray();
                        foreach(int k in tagList)
                        {
                            var List = db.placeLists.Where(p => p.id == k).Select(q => q.id);
                            unionResult= userList.Union(List).ToList();
                        }
                        
                        
                    }


                }
            }



            var result = new
            {
                status = 0,
                msg = $"fail",
                data = s
            };

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

    

