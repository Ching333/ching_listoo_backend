using prjToolist.Models;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
//using System.Web.Mvc;



namespace prjToolist.Controllers
{
    //[EnableCors(origins: "http://localhost:9527",headers:"*",methods:"*", SupportsCredentials = true)]
    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    [RoutePrefix("auth")]
    //[JwtAuthActionFilter]
    public class AuthController : ApiController
    {
        FUENMLEntities db = new FUENMLEntities();

        [Route("login")]
        [HttpPost]
        public HttpResponseMessage loginPost(memberLogin loginUser)
        {
            var verifyAccount = db.users.FirstOrDefault(P => P.email == loginUser.account && P.password == loginUser.password);
            //var cookie = new CookieHeaderValue("session-id", verifyAccount.id.ToString());
            //cookie.Expires = DateTimeOffset.Now.AddDays(1);
            //cookie.Domain = Request.RequestUri.Host;
            //cookie.Path = "/";
            var resultUsername = new
            {
                username = ""
            };
            var result = new
            {
                status = 0,
                msg = $"fail, {loginUser.account} doesn't exist or password is incorrect",
                data = resultUsername
            };
            if (verifyAccount != null)
            {
                HttpContext.Current.Session["SK_login"] = verifyAccount;

                resultUsername = new
                {
                    username = verifyAccount.name
                };
                result = new
                {
                    status = 1,
                    msg = "",
                    data = resultUsername
                };
                //resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });
                //resp.RequestMessage.Content = result;
                //var reqResult = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            var resp = Request.CreateResponse(
               HttpStatusCode.OK,
               result
               );
            return resp;
        }

        [Route("logout")]
        [HttpPost]
        public HttpResponseMessage logoutPost()
        {
            //{
            //    string sessionId = "";
            //    CookieHeaderValue cookie = Request.Headers.GetCookies("session-id").FirstOrDefault();
            //    if (cookie != null)
            //    {
            //        sessionId = cookie["session-id"].Value;
            //    }
            //============================================================
            //var currentCookie = Request.Headers.GetCookies("session-id").FirstOrDefault();
            var result = new
            {
                status = 0,
                msg = "fail"
            };
            //if (currentCookie != null)
            //{
            //    var cookie = new CookieHeaderValue("session-id", "")
            //    {
            //        Expires = DateTimeOffset.Now.AddDays(-1),
            //        //Domain = currentCookie.Domain,
            //        //Path = currentCookie.Path
            //    };
            //    resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            //}
            if (HttpContext.Current.Session["SK_login"] != null)
            {
                HttpContext.Current.Session["SK_login"] = null;
                result = new
                {
                    status = 1,
                    msg = "logout success"
                };
            }

            var resp = Request.CreateResponse(
                HttpStatusCode.OK,
                result
            );
            //var result = new
            //{
            //    status = 1,
            //    msg = ""
            //};
            //return Request.CreateResponse(HttpStatusCode.OK, result);
            return resp;
        }

        [Route("register")]
        [HttpPost]
        public HttpResponseMessage createUser(createMember createMemberModel)
        {
            var isnullormember = db.users.Where(p => p.email == createMemberModel.email).FirstOrDefault();
            var result = new
            {
                status = 0,
                msg = "fail,email exist",
            };
            if (isnullormember == null)
            {
                user newmember = new user();
                newmember.name = createMemberModel.name;
                newmember.password = createMemberModel.password;
                newmember.email = createMemberModel.email;
                newmember.created = DateTime.Now;
                newmember.updated = DateTime.Now;
                newmember.authority = 1;
                db.users.Add(newmember);
                db.SaveChanges();
                result = new
                {
                    status = 1,
                    msg = "Register success",
                };
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        [Route("test")]
        [HttpPost]
        public HttpResponseMessage testSession()
        {
            //int userlogin = 0;
            var result = new
            {
                status = 0,
                msg = "fail",
            };
            //userlogin = userFactory.userIsLoginSession(userlogin);
            //userlogin = (new UserController()).userIsLoginCookie(userlogin);
            if (HttpContext.Current.Session["SK_login"] != null)
            {
                user x = HttpContext.Current.Session["SK_login"] as user;
                Debug.WriteLine(x.id);
                result = new
                {
                    status = 1,
                    msg = "Success" + " " + x.id,
                };
            }
            var resp = Request.CreateResponse(
            HttpStatusCode.OK,
            result
            );
            return resp;
        }
    }
}