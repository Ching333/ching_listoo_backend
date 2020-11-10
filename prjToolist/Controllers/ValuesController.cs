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
    //[JwtAuthActionFilter]
    public class ValuesController : ApiController
    {
        private readonly FUENMLEntities db = new FUENMLEntities();
        public int str { get; set; }

        [Route("get_user_growth")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage get_user_growth()
        {
            List<vmCountDataValues> vm_userGrowth = new List<vmCountDataValues>();
            var result = new
            {
                status = 0,
                msg = "fail",
                data = vm_userGrowth

            };
            var userGrowthPerDay = (from u in db.users
                                    group u by (u.created.HasValue ? u.created.ToString().Substring(0, 10) : "noDateInfo") into g
                                    select new vmCountDataValues { key = g.Key, count = g.Count() }).ToList();

            if (userGrowthPerDay != null)
            {
                result = new
                {
                    status = 1,
                    msg = "OK",
                    data = userGrowthPerDay

                };
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("get_tag_count")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage get_tag_count()
        {
            List<vmCountDataValues> vm_tagCount = new List<vmCountDataValues>();
            List<vmCountDataValues> vm_tagCountResult = new List<vmCountDataValues>();
            var result = new
            {
                status = 0,
                msg = "fail",
                data = vm_tagCountResult
            };
            var tagCountTop10 = (from topTag in db.tagRelationships
                                 group topTag by (topTag.tag_id.ToString()) into g
                                 select new vmCountDataValues { key = g.Key, count = g.Count() }).OrderByDescending(g1 => g1.count).ToList().Take(10);

            if (tagCountTop10 != null)
            {
                foreach (var tagItem in tagCountTop10)
                {
                    int tagid;
                    int.TryParse(tagItem.key, out tagid);
                    var hasTag = db.tags.Where(p => p.id == tagid).Select(q => q.name).FirstOrDefault();
                    if (hasTag != null)
                    {
                        vmCountDataValues r = new vmCountDataValues();
                        r.key = hasTag;
                        r.count = tagItem.count;
                        vm_tagCountResult.Add(r);
                    }
                }

                result = new
                {
                    status = 1,
                    msg = "OK",
                    data = vm_tagCountResult

                };
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("get_tag_event_count")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage get_tag_event_count()
        {
            List<vmCountDataValues> vm_tagCountResult = new List<vmCountDataValues>();
            List<vmCountDataValues> vm_tagEventCount = new List<vmCountDataValues>();
            var result = new
            {
                status = 0,
                msg = "fail",
                data = vm_tagCountResult

            };
            var tagEventCountTop = (from te in db.tagEvents
                                    where te.tagEvent1 == 1 || te.tagEvent1 == 3
                                    group te by (te.tag_id) into g
                                    select new vmCountDataValues { key = g.Key.ToString(), count = g.Count() }).ToList();


            if (tagEventCountTop != null)
            {
                foreach (var tagItem in tagEventCountTop)
                {
                    int tagid;
                    int.TryParse(tagItem.key, out tagid);
                    var hasTag = db.tags.Where(p => p.id == tagid).Select(q => q.name).FirstOrDefault();
                    if (hasTag != null)
                    {
                        vmCountDataValues r = new vmCountDataValues();
                        r.key = hasTag;
                        r.count = tagItem.count;
                        vm_tagCountResult.Add(r);
                    }
                }
                result = new
                {
                    status = 1,
                    msg = "OK",
                    data = vm_tagCountResult

                };
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("get_place_tag_count")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage get_place_tag_count()
        {
            List<vmCountDataValues> vm_placetagCount = new List<vmCountDataValues>();
            List<vmCountDataValues> vm_placetagCountResult = new List<vmCountDataValues>();
            var result = new
            {
                status = 0,
                msg = "fail",
                data = vm_placetagCountResult
            };
            var tagPlaceTop10 = (from topPlace in db.tagRelationships
                                 group topPlace by (topPlace.place_id.ToString()) into g
                                 select new vmCountDataValues { key = g.Key, count = g.Count() }).OrderByDescending(g1 => g1.count).ToList().Take(10);

            if (tagPlaceTop10 != null)
            {
                foreach (var placeItem in tagPlaceTop10)
                {
                    int placeid;
                    int.TryParse(placeItem.key, out placeid);
                    var hasPlace = db.places.Where(p => p.id == placeid).Select(q => q.name).FirstOrDefault();
                    if (hasPlace != null)
                    {
                        vmCountDataValues r = new vmCountDataValues();
                        r.key = hasPlace;
                        r.count = placeItem.count;
                        vm_placetagCountResult.Add(r);
                    }
                }

                result = new
                {
                    status = 1,
                    msg = "OK",
                    data = vm_placetagCountResult

                };
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("get_place_tag_each")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage get_place_tag_each()
        {
            List<vmCountDataValues> vm_placetagCount = new List<vmCountDataValues>();
            List<vmCountDataValuesTwoKey> vm_placetagCountResult = new List<vmCountDataValuesTwoKey>();
            var result = new
            {
                status = 0,
                msg = "fail",
                data = vm_placetagCountResult
            };
            var tagPlaceTop10each = (from topPlace in db.tagRelationships
                                     group topPlace by new { placeid = topPlace.place_id.ToString(), tagid = topPlace.tag_id.ToString() } into g
                                     select new vmCountDataValuesTwoKey { key = g.Key.placeid, key2 = g.Key.tagid, count = g.Count() }).OrderByDescending(g1 => g1.count).ToList().Take(10);

            if (tagPlaceTop10each != null)
            {
                foreach (var placeItem in tagPlaceTop10each)
                {
                    int placeid;
                    int.TryParse(placeItem.key.ToString(), out placeid);
                    int tagid;
                    int.TryParse(placeItem.key2.ToString(), out tagid);
                    var hasTag = db.tags.Where(p => p.id == tagid).Select(q => q.name).FirstOrDefault();
                    var hasPlace = db.places.Where(p => p.id == placeid).Select(q => q.name).FirstOrDefault();
                    if (hasPlace != null && hasTag != null)
                    {
                        vmCountDataValuesTwoKey r = new vmCountDataValuesTwoKey();
                        r.key = hasPlace;
                        r.key2 = hasTag;
                        r.count = placeItem.count;
                        vm_placetagCountResult.Add(r);
                    }
                }

                result = new
                {
                    status = 1,
                    msg = "OK",
                    data = vm_placetagCountResult

                };
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("get_user_list")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage getUserList()
        {
            var intList = db.users.Select(p => p.id).ToList();
            List<queryUserList> usersList = new List<queryUserList>();
            int[] userList = intList.ToArray();
            Array.Sort(userList);
            int listId = 0;
            foreach(int i in userList)
            {
                listId++;
                var userListItem = db.users.AsEnumerable().FirstOrDefault(p => p.id == i);
                queryUserList listItem = new queryUserList();
                listItem.id = listId;
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
            //int[] tagIdArray = tag_List.ToArray();
            //Array.Sort(tagIdArray);
            List<tTagRelaforTable> tagsRelationList = new List<tTagRelaforTable>();
            for (int i = 0; i < tag_List.Count(); i++)
            {
                var placeItem = db.places.AsEnumerable().FirstOrDefault(p => p.id == tag_List[i].place_id);
                var tagItem = db.tags.AsEnumerable().FirstOrDefault(t => t.id == tag_List[i].tag_id);
                var userItem = db.users.AsEnumerable().FirstOrDefault(u => u.id == tag_List[i].user_id);
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
            var intList = db.placeLists.Select(p => p.id).ToList();
            int[] placeListArray = intList.ToArray();
            Array.Sort(placeListArray);
            List<queryPlaceList> placesList = new List<queryPlaceList>();
            foreach (int i in placeListArray)
            {
                //var placeListItem = db.placeLists.AsEnumerable().FirstOrDefault(p => p.id == i);
                //var userListItem = db.users.AsEnumerable().FirstOrDefault(u => u.id == placeListItem.id);
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

        [Route("get_place_selection")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage getPlaceSelection()
        {
            int[] placeArray = db.places.Select(p => p.id).ToArray();
            List<placeSelection> placesSelectionList = new List<placeSelection>();
            foreach (int i in placeArray)
            {
                var placeModel = db.places.FirstOrDefault(p => p.id == i);
                placeSelection placeItem = new placeSelection();
                placeItem.place_id = placeModel.id;
                placeItem.name = placeModel.name;
                placesSelectionList.Add(placeItem);
            }
            var result = new
            {
                data = placesSelectionList
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("create_list")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage createList(updateMember updateItem)
        {
            var result = new
            {
                //data = placeArray
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("update_member")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage updateMember(updateMember updateItem)
        {
            var userModel = db.users.FirstOrDefault(u => u.email == updateItem.email && u.password == updateItem.password);
            var result = new
            {
                status = 0,
                msg = "fail"
            };
            if (userModel != null)
            {
                userModel.name = updateItem.name;
                userModel.password = updateItem.password;
                userModel.authority = updateItem.authority;
                userModel.updated = DateTime.Now;
                db.SaveChanges();
                result = new
                {
                    status = 1,
                    msg = "OK"
                };
            }
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