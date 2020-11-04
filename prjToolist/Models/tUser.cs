﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace prjToolist.Models
{
    public class tUser
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int authority { get; set; }
        public Nullable<System.DateTime> updated { get; set; }
        public Nullable<System.DateTime> created { get; set; }
    }
    public class memberLogin
    {
        public string account { get; set; }
        public string password { get; set; }
    }
    public class createMember
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }

    public class queryUserList
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int authority { get; set; }
        public string createdTime { get; set; }
        public string updatedTime { get; set; }
    }
    public static class userFactory
    {
        public static int userIsLoginSession(int userlogin)
        {
            if (HttpContext.Current.Session["SK_login"] != null)
            {
                user u = HttpContext.Current.Session["SK_login"] as user;
                //Debug.WriteLine("userid" + u.id);
                userlogin = u.id;

            };
            return userlogin;
        }

    }
    }