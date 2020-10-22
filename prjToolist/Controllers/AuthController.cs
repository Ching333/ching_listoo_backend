﻿using prjToolist.Models;
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
    // POST: api/Auth
    [RoutePrefix("auth")]
    public class AuthController : ApiController
    {
        FUENMLEntities db = new FUENMLEntities();
        
        [Route("login")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage loginPost([FromBody] memberLogin loginUser)
        {
            var verifyAccount = db.users.FirstOrDefault(P => P.email == loginUser.account && P.password == loginUser.password);
            var cookie = new CookieHeaderValue("session-id", verifyAccount.id.ToString());
            cookie.Expires = DateTimeOffset.Now.AddDays(1);
            //cookie.Domain = Request.RequestUri.Host;
            //cookie.Path = "/";
            var resultUsername = new
            {
                username = verifyAccount.name
            };
            var result = new
            {
                status = 0,
                msg = $"fail, {verifyAccount.name} doesn't exist",
                data = resultUsername
            };
            var resp = Request.CreateResponse(
            HttpStatusCode.OK,
            result
            );

            if (verifyAccount != null)
            {
                result = new
                {
                    status = 1,
                    msg = "",
                    data = resultUsername
                };
                resp = Request.CreateResponse(
                    HttpStatusCode.OK,
                    result
                );
                resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });
                //resp.RequestMessage.Content = result;
                //var reqResult = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            //return Request.CreateResponse(HttpStatusCode.OK, resp);
            return resp;
        }

        [Route("logout")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage logoutPost() { 
        //{
        //    string sessionId = "";
          
        //    CookieHeaderValue cookie = Request.Headers.GetCookies("session-id").FirstOrDefault();
        //    if (cookie != null)
        //    {
                
        //        sessionId = cookie["session-id"].Value;
        //    }
           
            //============================================================

            var currentCookie = Request.Headers.GetCookies("session-id").FirstOrDefault();
            var result = new
            {
                status = 1,
                msg = ""
            };
            var resp = Request.CreateResponse(
                HttpStatusCode.OK,
                result
            );
            if (currentCookie != null)
            {
                var cookie = new CookieHeaderValue("session-id", "")
                {
                    Expires = DateTimeOffset.Now.AddDays(-1),
                    Domain = currentCookie.Domain,
                    Path = currentCookie.Path
                };
                resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            }

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
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage createUser(createMember x)
        {
            var isnullormember = db.users.Where(p => p.email == x.email).FirstOrDefault();
            var result = new
            {
                status = 0,
                msg = "fail",
            };
            if (isnullormember == null)
            {
                user newmember = new user();
                newmember.name =x.name;
                newmember.password = x.password;
                newmember.email = x.email;
                newmember.created = DateTime.Now;
                newmember.updated = DateTime.Now;
                newmember.authority = 1;
                db.users.Add(newmember);
                db.SaveChanges();
                result = new
                {
                    status = 1,
                    msg = "",
                };
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}