using prjToolist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace prjToolist.Controllers
{
    [RoutePrefix("common")]
    public class CommonController : ApiController
    {
        FUENMLEntities db = new FUENMLEntities();

        [Route("get_list/{list_id:int}")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage GetList(int list_id, tagString s)
        {
            //獲取某特定清單中含有標籤的地點id
            List<int> resultplaceid= new List<int>();
            int[] tFilterid=tagFactory.checktagString(s, db);
            var searchallplaceinlist = db.placeRelations.Where(p => p.placeList_id == list_id)
                .Select(q => q.place_id).ToList();
            //此結果為特定清單中篩選出的地點id
            List<int> intersectResult = searchallplaceinlist;
            if (tFilterid != null) { 
            foreach (int i in tFilterid) {
                var searchplacehastag = db.tagRelations.Where(P => P.tag_id == i).Select(q => q.place_id).ToList();
                intersectResult = intersectResult.Intersect(searchplacehastag).ToList();
                //foreach(int p in unionResult) {
                //    resultplaceid.Add(p);
                //}
              }
            }
            //============================================

            var place = db.placeLists.Where(p => p.id == list_id).FirstOrDefault();
            var placeSpot = db.placeRelations.Where(p => p.placeList_id == list_id).Select(p => p.place_id).ToList();
            List <placeListInfo> infoList = new List<placeListInfo>();
            List<placeInfo> relationPlace = new List<placeInfo>();
            List<tagInfo> tagInfoList = new List<tagInfo>();
            
            List<int> idList = new List<int>();
            List<int> tagList = new List<int>();

            for (int i =0;i<placeSpot.Count();i++)
            {
                idList.Add(placeSpot[i]);
            }
            int[] terms = idList.ToArray();

            placeListInfo infoItem = new placeListInfo();
            infoItem.userId = place.user_id;
            infoItem.name = place.name;
            infoItem.description = place.description;
            infoItem.privacy = place.privacy;
            infoItem.createdTime = place.created.ToString();
            infoItem.updatedTime = place.updated.ToString();
            //byte[] binaryString = (byte[])place.cover;
            //info.cover = Encoding.UTF8.GetString(binaryString);
            infoList.Add(infoItem);

            foreach(var i in terms)
            {
                var tagId = db.tagRelations.Where(p => p.place_id == i).Select(p => p.tag_id).ToList();
                var exportPlaceInfo = new placeInfo();
                var placeModel = db.places.FirstOrDefault(p => p.id == i);
                for (int j = 0; j < tagId.Count(); j++)
                {
                    tagList.Add(tagId[j]);
                }
                exportPlaceInfo.name = placeModel.name;
                exportPlaceInfo.longitude = placeModel.longitude;
                exportPlaceInfo.latitude = placeModel.latitude;
                exportPlaceInfo.phone = placeModel.phone;
                exportPlaceInfo.address = placeModel.address;
                exportPlaceInfo.type = placeModel.type;
                exportPlaceInfo.gmap_id = placeModel.gmap_id;
                relationPlace.Add(exportPlaceInfo);
            }

            int[] tagArray = tagList.Distinct().ToArray();

            foreach(int t in tagArray)
            {
                var exportTagInfo = new tagInfo();
                var tagInfoModel = db.tags.FirstOrDefault(p => p.id == t);
                exportTagInfo.name = tagInfoModel.name;
                exportTagInfo.type = tagInfoModel.type;
                tagInfoList.Add(exportTagInfo);
            }

            var dataForm = new
            {
                info = infoList,
                places = relationPlace,
                tags = tagInfoList
            };

            var result = new
            {
                status = 1,
                data = dataForm,
                msg = "",
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

       

       


        
        

        
    }
}
