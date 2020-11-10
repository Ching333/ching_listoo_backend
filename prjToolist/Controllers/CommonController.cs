using prjToolist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using static prjToolist.Models.tagFactory;
//using static prjToolist.Models.tTagRelation;

namespace prjToolist.Controllers
{
    [RoutePrefix("common")]
    //[JwtAuthActionFilter]
    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    public class CommonController : ApiController
    {
        FUENMLEntities db = new FUENMLEntities();

        [Route("get_recommand_lists")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage getRecommendList(tFilter filter)
        {
            List<tPlaceList> placeList = new List<tPlaceList>();
            List<int> intersectPlaceId = new List<int>();
            List<int> intersectPlaceListId = new List<int>();

            int[] tagIdList = db.tags.Select(t => t.id).ToArray();
            //string[] typeList = db.places.Select(p => p.type).Distinct().ToArray();
            Array.Sort(tagIdList);
            List<tTag> tagList = new List<tTag>();
            List<int> tagIDList = new List<int>();
            List<string> typeList = new List<string>();
            var tagInfo = new
            {
                lists = placeList,
                system_tags = typeList,
                user_tags = tagList
            };
            var result = new
            {
                status = 1,
                data = tagInfo,
                msg = ""
            };

            int userlogin = 0;
            var entirePlaces = db.places.Select(p => p.id).ToList();
            var entirePlaceLists = db.placeLists.Select(p => p.id).ToList();
            int filterCount = filter.filter.Length;
            intersectPlaceId = entirePlaces;
            intersectPlaceListId = entirePlaceLists;

            if (filterCount > 0)
            {
                foreach (int i in filter.filter)
                {
                    intersectPlaceId = tagFactory.searchTag(userlogin, ref intersectPlaceId, i, db);
                }
                intersectPlaceId = intersectPlaceId.Distinct().ToList();
                Array.Sort(intersectPlaceId.ToArray());
                foreach (int i in intersectPlaceId)
                {
                    var listFilter = db.placeRelationships.Where(p => p.place_id == i).Select(p => p.placelist_id).ToList();
                    tagIDList.AddRange(db.tagRelationships.Where(p => p.place_id == i).Select(q => q.tag_id).ToList());
                    intersectPlaceListId = intersectPlaceListId.Intersect(listFilter).ToList();
                }
                tagIDList = tagIDList.Distinct().ToList();
                Array.Sort(tagIDList.ToArray());
                Array.Sort(intersectPlaceListId.Distinct().ToArray());
                foreach(int i in intersectPlaceListId)
                {
                    var placeListModel = db.placeLists.FirstOrDefault(p => p.id == i);
                    tPlaceList placeListItem = new tPlaceList();
                    placeListItem.id = placeListModel.id;
                    placeListItem.creator_id = placeListModel.user_id;
                    placeListItem.name = placeListModel.name;
                    placeListItem.coverImageURL = placeListModel.cover;
                    placeList.Add(placeListItem);
                }
                if (tagIDList.Count > 0)
                {
                    foreach (int i in tagIDList)
                    {
                        var tagModel = db.tags.FirstOrDefault(p => p.id == i && p.type == 2);
                        if (tagModel != null)
                        {
                            tTag tagItem = new tTag();
                            tagItem.id = tagModel.id;
                            tagItem.name = tagModel.name;
                            tagItem.type = tagModel.type;
                            tagList.Add(tagItem);
                        }
                    }
                }

                tagInfo = new
                {
                    lists = placeList,
                    system_tags = typeList,
                    user_tags = tagList
                };
                result = new
                {
                    status = 1,
                    data = tagInfo,
                    msg = ""
                };
            }
            else
            {
                foreach(int i in entirePlaceLists)
                {
                    var placeListModel = db.placeLists.FirstOrDefault(p => p.id == i);
                    if(placeListModel != null)
                    {
                        tPlaceList placeListItem = new tPlaceList();
                        placeListItem.id = placeListModel.id;
                        placeListItem.creator_id = placeListModel.user_id;
                        placeListItem.name = placeListModel.name;
                        placeListItem.coverImageURL = placeListModel.cover;
                        placeList.Add(placeListItem);
                    }
                }
                foreach (int i in tagIdList)
                {
                    var tagModel = db.tags.FirstOrDefault(t => t.id == i);
                    tTag tagItem = new tTag();
                    tagItem.id = i;
                    tagItem.type = tagModel.type;
                    tagItem.name = tagModel.name;
                    tagList.Add(tagItem);
                }
                //typeList = db.places.Select(p => p.type).Distinct().ToArray();
                //typeList = typeList.Distinct();
                tagInfo = new
                {
                    lists = placeList,
                    system_tags = typeList,
                    user_tags = tagList
                };
                result = new
                {
                    status = 1,
                    data = tagInfo,
                    msg = ""
                };
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("get_hot_tags")]
        [HttpGet]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage getHotTag()
        {
            int[] idList = db.tags.Select(t => t.id).ToArray();
            string[] typeList = db.places.Select(p => p.type).Distinct().ToArray();
            Array.Sort(idList); 
            List<tTag> tagList = new List<tTag>();
            var tagInfo = new
            {
                system_tags = typeList,
                user_tags = tagList
            };
            var result = new
            {
                status = 0,
                data = tagInfo,
                msg = "fail"
            };

            if (idList.Count() > 0)
            {
                foreach(int i in idList)
                {
                    var tagModel = db.tags.FirstOrDefault(t => t.id == i);
                    tTag tagItem = new tTag();
                    tagItem.id = i;
                    tagItem.name = tagModel.name;
                    tagItem.type = tagModel.type;
                    tagList.Add(tagItem);
                    tagInfo = new
                    {
                        system_tags = typeList,
                        user_tags = tagList
                    };
                    result = new
                    {
                        status = 1,
                        data = tagInfo,
                        msg = ""
                    };
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("get_list_detail")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage GetListDetail(viewModelGetListPlace getListInfo)
        {   //無論公開或私人都會用此功能 所以要找創建清單的userid非登入者的userid
            List<string> systemTagResult = new List<string>();
            List<int> resultplaceid = new List<int>();
            List<int> searchallplaceinlist = new List<int>();
            List<int> intersectResult = new List<int>();
            List<int> tagsList = new List<int>();
            List<listDetailPlace> resultPlaceInfo = new List<listDetailPlace>();
            List<tTag> resultTagInfo = new List<tTag>();
            placeListInfo infoItem = new placeListInfo();
            int[] tFilterid = getListInfo.filter;
            int list_createrId = 0;
            var dataForm = new
            {
                info = infoItem,
                places = resultPlaceInfo,
                systemtags = systemTagResult,
                user_tags = resultTagInfo
            };
            var result = new
            {
                status = 0,
                msg = "fail",
                data = dataForm
            };
            var listModel = db.placeLists.Where(p => p.id == getListInfo.list_id).FirstOrDefault();
            //此結果為特定清單中全部的地點id
            //無論公開或私人都會用此功能 所以要找創建清單的userid非登入者的userid
            if (listModel != null)
            {
                searchallplaceinlist = db.placeRelationships.Where(p => p.placelist_id == listModel.id).Select(q => q.place_id).ToList();
                list_createrId = db.placeLists.Where(p => p.id == getListInfo.list_id).Select(q => q.user_id).FirstOrDefault();
                var listCreator = db.users.Where(u => u.id == list_createrId).FirstOrDefault();
                infoItem.id = listModel.id;
                infoItem.creator_id = listModel.user_id;
                infoItem.name = listModel.name;
                infoItem.creator_username = listCreator.name;
                infoItem.description = listModel.description;
                infoItem.privacy = listModel.privacy;
                infoItem.createdTime = listModel.created != null ? listModel.created.ToString().Substring(0, 10) : "";
                infoItem.updatedTime = listModel.updated != null ? listModel.updated.ToString().Substring(0, 10) : "";

                dataForm = new
                {
                    info = infoItem,
                    places = resultPlaceInfo,
                    systemtags = systemTagResult,
                    user_tags = resultTagInfo
                };
                result = new
                {
                    status = 1,
                    msg = "success",
                    data = dataForm
                };
            }
            //此結果為特定清單中篩選出有標籤的地點id
            if (tFilterid != null && list_createrId != 0)
            {
                intersectResult = searchallplaceinlist;
                foreach (int i in tFilterid)
                {
                    intersectResult = tagFactory.searchTag(list_createrId, ref intersectResult, i, db);
                    //var searchplacehastag = db.tagRelationships.Where(P => P.tag_id == i).Select(q => q.place_id).ToList();
                    //intersectResult = intersectResult.Intersect(searchplacehastag).ToList();
                    //foreach(int p in unionResult) {
                    //    resultplaceid.Add(p);
                    //}
                }
                intersectResult = intersectResult.Distinct().ToList();
            }
            if (intersectResult.Count > 0)
            {
                foreach (int i in intersectResult)
                {
                    var placeItem = db.places.Where(p => p.id == i).Select(q => q).FirstOrDefault();
                    if (placeItem != null)
                    {
                        if (placeItem.type != null)  systemTagResult.Add(placeItem.type); 

                        listDetailPlace placeDetail = new listDetailPlace();
                        placeDetail.id = placeItem.id;
                        placeDetail.gmap_id = placeItem.gmap_id;
                        placeDetail.name = placeItem.name;
                        placeDetail.phone = placeItem.phone;
                        placeDetail.address = placeItem.address;
                        placeDetail.type = placeItem.type;
                        //placeDetail.photo_url = placeItem.photo.ToString(); // photo type in db is byte[]
                        resultPlaceInfo.Add(placeDetail);
                    }
                    tagsList.AddRange(db.tagRelationships.Where(p => p.place_id == i && p.user_id == list_createrId).Select(q => q.tag_id).ToList());
                    //var test = from p in db.tagRelations where p.place_id == i group p.tag_id by p.user_id == userlogin ?"userTag":"othersTag" into g select new {g.Key } ;
                }
                tagsList = tagsList.Distinct().ToList();
                systemTagResult = systemTagResult.Distinct().ToList();
                dataForm = new
                {
                    info = infoItem,
                    places = resultPlaceInfo,
                    systemtags = systemTagResult,
                    user_tags = resultTagInfo
                };
                result = new
                {
                    status = 1,
                    msg = "success",
                    data = dataForm
                };
            }
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
                        t.type = rtag.type;
                        resultTagInfo.Add(t);
                    }
                }
            }
            //var place = db.placeLists.Where(p => p.id == getListInfo.list_id).FirstOrDefault();
            //var placeSpot = db.placeRelationships.Where(p => p.placelist_id == getListInfo.list_id).Select(p => p.place_id).ToList();
            //List<placeListInfo> infoList = new List<placeListInfo>();
            //List<tPlaceInfo> relationPlace = new List<tPlaceInfo>();
            //List<tagInfo> tagInfoList = new List<tagInfo>();

            //List<int> idList = new List<int>();
            //List<int> tagList = new List<int>();

            //for (int i = 0; i < placeSpot.Count(); i++)
            //{
            //    idList.Add(placeSpot[i]);
            //}
            //int[] terms = idList.ToArray();

            //placeListInfo infoItem = new placeListInfo();
            //infoItem.userId = place.user_id;
            //infoItem.name = place.name;
            //infoItem.description = place.description;
            //infoItem.privacy = place.privacy;
            //infoItem.createdTime = place.created.ToString();
            //infoItem.updatedTime = place.updated.ToString();
            ////byte[] binaryString = (byte[])place.cover;
            ////info.cover = Encoding.UTF8.GetString(binaryString);
            //infoList.Add(infoItem);

            //foreach (var i in terms)
            //{
            //    var tagId = db.tagRelationships.Where(p => p.place_id == i).Select(p => p.tag_id).ToList();
            //    tPlaceInfo exportPlaceInfo = new tPlaceInfo();
            //    var placeModel = db.places.FirstOrDefault(p => p.id == i);
            //    for (int j = 0; j < tagId.Count(); j++)
            //    {
            //        tagList.Add(tagId[j]);
            //    }
            //    exportPlaceInfo.name = placeModel.name;
            //    exportPlaceInfo.longitude = placeModel.longitude;
            //    exportPlaceInfo.latitude = placeModel.latitude;
            //    exportPlaceInfo.phone = placeModel.phone;
            //    exportPlaceInfo.address = placeModel.address;
            //    exportPlaceInfo.type = placeModel.type;
            //    exportPlaceInfo.gmap_id = placeModel.gmap_id;
            //    relationPlace.Add(exportPlaceInfo);
            //}

            //int[] tagArray = tagList.Distinct().ToArray();

            //foreach (int t in tagArray)
            //{
            //    var exportTagInfo = new tagInfo();
            //    var tagInfoModel = db.tags.FirstOrDefault(p => p.id == t);
            //    exportTagInfo.name = tagInfoModel.name;
            //    exportTagInfo.type = tagInfoModel.type;
            //    tagInfoList.Add(exportTagInfo);
            //}
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("tag_relation")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage tagRelation(tTagRelation tTag)
        {
            var verifyAccount = db.users.Where(u => u.id == tTag.user_id).FirstOrDefault();
            var verifyGlePlace = db.places.Where(p => p.gmap_id == tTag.gmap_id).FirstOrDefault();

            var result = new
            {
                status = 0,
                msg = "fail"
            };
            if (verifyAccount != null)
                if (verifyGlePlace != null)
                {
                    foreach (var i in tTag.tag_id)
                    {
                        var newTagRelation = new tagRelationship();
                        newTagRelation.place_id = verifyGlePlace.id;
                        newTagRelation.user_id = verifyAccount.id;
                        newTagRelation.tag_id = i;
                        newTagRelation.created = DateTime.Now;
                        db.tagRelationships.Add(newTagRelation);
                    }

                    db.SaveChanges();
                    result = new
                    {
                        status = 1,
                        msg = ""
                    };
                }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
