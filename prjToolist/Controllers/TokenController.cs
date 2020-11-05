using Jose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using static prjToolist.Models.JwtAuthActionFilter;

namespace prjToolist.Controllers
{
    [RoutePrefix("token")]
    public class TokenController : ApiController
    {
        FUENMLEntities db = new FUENMLEntities();

        // POST api/values
        [Route("login")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public object Post(LoginData loginData)
        {
            // TODO: key應該移至config
            var secret = "secreteTokenCreator";
            var verifyCount = db.users.FirstOrDefault(v=>v.email == loginData.account && v.password==loginData.password);
            // TODO: 真實世界檢查帳號密碼
            if (verifyCount != null)
            {
                var payload = new JwtAuthObject()
                {
                    accId = verifyCount.email,
                };

                return new
                {
                    Result = true,
                    user_id = verifyCount.id,
                    token = Jose.JWT.Encode(payload, Encoding.UTF8.GetBytes(secret), JwsAlgorithm.HS256)
                };
            }
            else
            {
                throw new UnauthorizedAccessException("帳號密碼錯誤");
            }
        }

        //上面程式我們會先檢查帳號密碼是否正確，
        //接著使用Jose.JWT.Encode來產生JWT token，可以使用postman來看看產生的結果

        public class LoginData
        {
            public string account { get; set; }
            public string password { get; set; }
        }
    }
}
