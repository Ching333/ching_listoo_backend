using prjToolist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

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
            var resultUsername = new
            {
                username = loginUser.account
            };
            var result = new
            {
                status = 0,
                msg = $"fail, {loginUser.account} doesn't exist",
                data= resultUsername
            };
            
            if (verifyAccount != null)
            {   
                result = new
                {
                    status = 1,
                    msg = "",
                    data = resultUsername
                };
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("logout")]
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage logoutPost()
        {
            var result = new
            {
                status = 1,
                msg = ""
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);
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
