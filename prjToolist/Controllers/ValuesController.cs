using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json.Linq;
using prjToolist.Models;
using static prjToolist.Models.tagFactory;
//using static prjToolist.Models.tagFactory;
//using static prjToolist.Models.tTagRelation.tagFactory;

namespace prjToolist.Controllers
{
    [RoutePrefix("query")]
    [JwtAuthActionFilter]
    public class ValuesController : ApiController
    {
        private readonly FUENMLEntities db = new FUENMLEntities();
        public int str { get; set; }

        [Route("get_user_list")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage getUserList()
        {
            var intList = db.users.Select(p => p.id).ToList();
            List<queryUserList> usersList = new List<queryUserList>();
            for (int i = 0; i < intList.Count(); i++)
            {
                var userListItem = db.users.AsEnumerable().FirstOrDefault(p => p.id == intList[i]);
                queryUserList listItem = new queryUserList();
                listItem.id = i+1;
                listItem.name = userListItem.name;
                listItem.email = userListItem.email;
                listItem.authority = userListItem.authority;
                listItem.password = userListItem.password;
                listItem.createdTime = userListItem.created.ToString();
                listItem.updatedTime = userListItem.updated.ToString();
                usersList.Add(listItem);
            }
            var result = new
            {
                data = usersList,
                total = usersList.Count()
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("get_tag_list")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage getTagList()
        {
            var tag_List = db.tagRelationships.ToList();
            List<tTagRelaforTable> tagsRelationList = new List<tTagRelaforTable>();
            for (int i = 0; i < tag_List.Count(); i++)
            {
                var placeItem = db.places.AsEnumerable().Where(p => p.id == tag_List[i].place_id).FirstOrDefault();
                var tagItem = db.tags.AsEnumerable().Where(t => t.id == tag_List[i].tag_id).FirstOrDefault();
                var userItem = db.users.AsEnumerable().Where(u => u.id == tag_List[i].user_id).FirstOrDefault();
                string placeName = placeItem.name;
                string tagName = tagItem.name;
                string userName = userItem.name;

                tTagRelaforTable listItem = new tTagRelaforTable();
                listItem.id = i + 1;
                listItem.place_name = placeName;
                listItem.tag_name = tagName;
                listItem.user_name = userName;

                tagsRelationList.Add(listItem);
            }
            var result = new
            {
                data = tagsRelationList,
                total = tagsRelationList.Count()
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("get_place_list")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage getPlaceList()
        {
            var intList = db.places.Select(p => p.id).ToList();
            List<queryPlaceList> placesList = new List<queryPlaceList>();
            foreach (int i in intList)
            {
                var placeListItem = db.placeLists.FirstOrDefault(p => p.id == i);
                var userListItem = db.users.FirstOrDefault(u => u.id == placeListItem.user_id);
                queryPlaceList listItem = new queryPlaceList();
                listItem.id = placeListItem.id;
                listItem.listName = placeListItem.name;
                listItem.description = placeListItem.description;
                listItem.privacy = placeListItem.privacy;
                listItem.user_id = placeListItem.user_id;
                listItem.user_name = userListItem.name;
                listItem.createdTime = placeListItem.created.ToString();
                listItem.updatedTime = placeListItem.updated.ToString();
                placesList.Add(listItem);
            }
            var result = new
            {
                data = placesList,
                total = placesList.Count()
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("get_place_info")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage getPlaceInfo()
        {
            var intList = db.places.Select(p => p.id).ToList();
            List<queryPlaceInfo> placesInfoList = new List<queryPlaceInfo>();
            foreach (int i in intList)
            {
                var placeInfoItem = db.places.FirstOrDefault(p => p.id == i);
                queryPlaceInfo listItem = new queryPlaceInfo();
                listItem.id = placeInfoItem.id;
                listItem.name = placeInfoItem.name;
                listItem.type = placeInfoItem.type;
                listItem.phone = placeInfoItem.phone;
                listItem.address = placeInfoItem.address;
                listItem.longitude = placeInfoItem.longitude;
                listItem.latitude = placeInfoItem.latitude;
                placesInfoList.Add(listItem);
            }
            var result = new
            {
                data = placesInfoList,
                total = placesInfoList.Count()
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("update_placelist")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage updatePlaceList(queryPlaceList updateItem)
        {
            var placeListItem = db.placeLists.FirstOrDefault(p => p.id == updateItem.id);
            var userListItem = db.users.FirstOrDefault(u => u.id == updateItem.user_id);
            placeListItem.name = updateItem.listName;
            placeListItem.description = updateItem.description;
            placeListItem.privacy = updateItem.privacy;
            placeListItem.updated = DateTime.Now;
            //placeListItem.cover = updateItem.cover;
            userListItem.name = updateItem.user_name;
            db.SaveChanges();
            var result = new
            {
                msg = "success"
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("update_tagRelation")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage updateTagRelation(updateTagRelation updateItem)
        {
            var placeListItem = db.placeLists.FirstOrDefault(p => p.name == updateItem.place_name);
            var userListItem = db.users.FirstOrDefault(u => u.name == updateItem.user_name);
            var tagListItem = db.tags.FirstOrDefault(t=>t.name == updateItem.tag_name);
            var tagRelationItem = db.tagRelationships.Where(r => r.place_id == placeListItem.id
                                                              && r.tag_id == tagListItem.id
                                                              && r.user_id == userListItem.id)
                                                           .FirstOrDefault();
            var result = new
            {
                msg = "Error"
            };
            if (placeListItem != null && userListItem != null && tagListItem != null && tagRelationItem != null)
            {
                tagRelationItem.place_id = placeListItem.id;
                tagRelationItem.tag_id = tagListItem.id;
                tagRelationItem.user_id = userListItem.id;
                db.SaveChanges();
                result = new
                {
                    msg = ""
                };
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("listPost")]
        [EnableCors("*", "*", "*")]
        public IEnumerable<user> ttt()
        {
            //public List<Student> Get() {
            var api = from p in db.users
                      select p;
            //user.Add*()
            return api.ToList();
        }

        [HttpPost]
        // POST api/values
        public HttpResponseMessage Post([FromBody] string createUser)
        {
            var controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            var jo = JObject.Parse(createUser);
            var name = jo["name"].ToString();
            var email = jo["email"].ToString();
            var pwd = jo["password"].ToString();
            var createdTime = jo["created"].ToString();
            //string updatedTime = jo["updated"].ToString();
            var authorityNew = jo["authority"].ToString();
            var createMember = new user();
            createMember.name = name;
            createMember.email = email;
            createMember.password = pwd;
            createMember.created = DateTime.Parse(createdTime);
            //createMember.updated = DateTime.Parse(updatedTime);
            createMember.authority = int.Parse(authorityNew);
            db.users.Add(createMember);
            db.SaveChanges();
            var result = new
            {
                STATUS = true,
                MSG = "成功"
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // PUT api/values/5
        public HttpResponseMessage Put(int id, [FromBody] string updateUser)
        {
            var controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            var jo = JObject.Parse(updateUser);
            //string memberId = jo["id"].ToString();
            var name = jo["name"].ToString();
            var email = jo["email"].ToString();
            var pwd = jo["password"].ToString();
            var updatedTime = jo["updated"].ToString();
            var authorityNew = jo["authority"].ToString();
            var updateMember = db.users.FirstOrDefault(p => p.id == id);

            //user createMember = new user();
            updateMember.name = name;
            updateMember.email = email;
            updateMember.password = pwd;
            updateMember.updated = DateTime.Parse(updatedTime);
            updateMember.authority = int.Parse(authorityNew);
            db.SaveChanges();
            var result = new
            {
                STATUS = true,
                MSG = "成功"
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        public class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}