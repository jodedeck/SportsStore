using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.WebUI.Models
{
    public static class UserSession
    {
       

        public static string Name
        {
            get { return (string)HttpContext.Current.Session["Name"]; }
            set { HttpContext.Current.Session["Name"] = value; }
        }

    }
}