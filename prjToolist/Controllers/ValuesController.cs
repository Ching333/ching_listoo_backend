using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json.Linq;
using prjToolist.Models;

namespace prjToolist.Controllers {

    [RoutePrefix("query")]
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
                listItem.id = i + 1;
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
            var tag_List = db.tagRelations.ToList();
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
                //TODO會出錯
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

        [Route("update_placelist")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage updatePlaceList(queryPlaceList updateItem)
        {
            var placeListItem = db.placeLists.FirstOrDefault(p => p.id == updateItem.id);
            var userListItem = db.users.FirstOrDefault(u => u.id == updateItem.user_id);
            placeListItem.name = updateItem.listName;
            placeListItem.description = updateItem.description;
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






    //[RoutePrefix("test")]
    //public class ValuesController : ApiController {
    //    private readonly FUENMLEntities db = new FUENMLEntities();
    //    public int str { get; set; }


    //    [Route("tag_relation")]
    //    [HttpPost]
    //    [EnableCors("*", "*", "*")]
    //    public HttpResponseMessage tagRelation([FromBody] tTag tTag) {
    //        var verifyAccount = db.users.Where(u => u.id == tTag.user_id).FirstOrDefault();
    //        var verifyGlePlace = db.places.Where(p => p.gmap_id == tTag.gmap_id).FirstOrDefault();

    //        var result = new {
    //            status = 0,
    //            msg = "fail"
    //        };
    //        if (verifyAccount != null)
    //            if (verifyGlePlace != null) {
    //                foreach (var i in tTag.tag_id) {
    //                    var newTagRelation = new tagRelation();
    //                    newTagRelation.place_id = verifyGlePlace.id;
    //                    newTagRelation.user_id = verifyAccount.id;
    //                    newTagRelation.tag_id = i;
    //                    newTagRelation.created = DateTime.Now;
    //                    db.tagRelations.Add(newTagRelation);
    //                }

    //                db.SaveChanges();
    //                result = new {
    //                    status = 1,
    //                    msg = ""
    //                };
    //            }

    //        return Request.CreateResponse(HttpStatusCode.OK, result);
    //    }


    //    [HttpPost]
    //    [Route("listPost")]
    //    [EnableCors("*", "*", "*")]
    //    public IEnumerable<user> ttt() {
    //        //public List<Student> Get() {
    //        var api = from p in db.users
    //            select p;
    //        //user.Add*()
    //        return api.ToList();
    //    }


    //    /// <summary>
    //    ///     查詢USER
    //    /// </summary>
    //    /// <returns></returns>
    //    // GET api/values
    //    [HttpGet]
    //    [Route("listGet")]
    //    public IEnumerable<user> Get() {
    //        //public List<Student> Get() {

    //        var api = from p in db.users
    //            select p;

    //        //user.Add*()
    //        return api.ToList();

    //        /*
    //        return new List<Student> {
    //            new Student {
    //                Id = 100,
    //                Name = "小AAA明"
    //            },
    //            new Student {
    //                Id = 101,
    //                Name = "小華"
    //            }
    //        };
    //        */
    //    }

    //    // GET api/values/5
    //    public IEnumerable<user> Get(int id) {
    //        var api = from p in db.users
    //            where p.id == id
    //            select p;
    //        return api;
    //    }

    //    [HttpPost]
    //    // POST api/values
    //    public HttpResponseMessage Post([FromBody] string createUser) {
    //        var controllerName = ControllerContext.RouteData.Values["controller"].ToString();
    //        var jo = JObject.Parse(createUser);
    //        var name = jo["name"].ToString();
    //        var email = jo["email"].ToString();
    //        var pwd = jo["password"].ToString();
    //        var createdTime = jo["created"].ToString();
    //        //string updatedTime = jo["updated"].ToString();
    //        var authorityNew = jo["authority"].ToString();
    //        var createMember = new user();
    //        createMember.name = name;
    //        createMember.email = email;
    //        createMember.password = pwd;
    //        createMember.created = DateTime.Parse(createdTime);
    //        //createMember.updated = DateTime.Parse(updatedTime);
    //        createMember.authority = int.Parse(authorityNew);
    //        db.users.Add(createMember);
    //        db.SaveChanges();
    //        var result = new {
    //            STATUS = true,
    //            MSG = "成功"
    //        };

    //        return Request.CreateResponse(HttpStatusCode.OK, result);
    //    }

    //    // PUT api/values/5
    //    public HttpResponseMessage Put(int id, [FromBody] string updateUser) {
    //        var controllerName = ControllerContext.RouteData.Values["controller"].ToString();
    //        var jo = JObject.Parse(updateUser);
    //        //string memberId = jo["id"].ToString();
    //        var name = jo["name"].ToString();
    //        var email = jo["email"].ToString();
    //        var pwd = jo["password"].ToString();
    //        var updatedTime = jo["updated"].ToString();
    //        var authorityNew = jo["authority"].ToString();
    //        var updateMember = db.users.FirstOrDefault(p => p.id == id);

    //        //user createMember = new user();
    //        updateMember.name = name;
    //        updateMember.email = email;
    //        updateMember.password = pwd;
    //        updateMember.updated = DateTime.Parse(updatedTime);
    //        updateMember.authority = int.Parse(authorityNew);
    //        db.SaveChanges();
    //        var result = new {
    //            STATUS = true,
    //            MSG = "成功"
    //        };

    //        return Request.CreateResponse(HttpStatusCode.OK, result);
    //    }

    //    // DELETE api/values/5
    //    public void Delete(int id) {
    //    }

    //    public class tTag {
    //        public int user_id { get; set; }
    //        public string gmap_id { get; set; }
    //        public int[] tag_id { get; set; }
    //    }


    //    public class Student {
    //        public int Id { get; set; }
    //        public string Name { get; set; }
    //    }
    //}
}