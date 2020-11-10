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
            try
            {
                var verifyAccount = db.users.FirstOrDefault(v => v.email == loginUser.email && v.password == loginUser.password);
                //var cookie = new CookieHeaderValue("session-id", verifyAccount.id.ToString());
                //cookie.Expires = DateTimeOffset.Now.AddDays(1);
                //cookie.Domain = Request.RequestUri.Host;
                //cookie.Path = "/";
                var resultUsername = new
                {
                    username = "",
                    user_id = ""
                };
                var result = new
                {
                    status = 0,
                    msg = $"fail, {loginUser.email} doesn't exist or password is incorrect",
                    data = resultUsername
                };
                if (verifyAccount != null)
                {
                    HttpContext.Current.Session["SK_login"] = verifyAccount;

                    resultUsername = new
                    {
                        username = verifyAccount.name,
                        user_id = verifyAccount.id.ToString()
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("logout")]
        [HttpPost]
        public HttpResponseMessage logoutPost()
        {
            try
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
                int userlogin = 0;
                // userlogin = (new UserController()).userIsLoginCookie(userlogin);
                if (userlogin != 0)
                {
                    // HttpContext.Current.Session["SK_login"] = null;
                    Debug.WriteLine("logout success");
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("register")]
        [HttpPost]
        public HttpResponseMessage createUser(createMember createMemberModel)
        {
            try
            {
                var isnullormember = db.users.Where(p => p.email == createMemberModel.email).FirstOrDefault();
                var result = new
                {
                    status = 0,
                    msg = "fail,email exist",
                };
                if (isnullormember == null)
                {
                    if (createMemberModel.name != null && createMemberModel.password != null)
                    {
                        user newmember = new user();
                        newmember.name = createMemberModel.name;
                        newmember.password = createMemberModel.password;
                        newmember.email = createMemberModel.email;
                        newmember.created = DateTime.Now;
                        //newmember.updated = DateTime.Now; // -->register need updated time?
                        newmember.authority = 1;
                        db.users.Add(newmember);
                        db.SaveChanges();
                        result = new
                        {
                            status = 1,
                            msg = "Register success",
                        };
                    }
                    result = new
                    {
                        status = 1,
                        msg = "註冊會員資料不完整",
                    };
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
         }

        //TODO: remove befor final released
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