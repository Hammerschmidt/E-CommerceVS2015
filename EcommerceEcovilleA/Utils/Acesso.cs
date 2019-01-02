using EcommerceEcovilleA.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EcommerceEcovilleA.Utils
{
    public class Acesso : ActionFilterAttribute
    {
        protected string usuario;
        protected Usuario Usuario;
        public string Filtros;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var usuarioCookie = HttpContext.Current.Request.Cookies["C-USR"];

            if (usuarioCookie == null || String.IsNullOrEmpty(usuarioCookie.Value))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Usuario",
                    action = "Login"
                }));
                LimpaCookies(filterContext);
            }
            else
            {
                try
                {
                    string obj = Seguranca.DescriptografaCookie(usuarioCookie.Value);
                    Usuario = JsonConvert.DeserializeObject<Usuario>(obj);
                }
                catch
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Usuario",
                        action = "Login"
                    }));
                    LimpaCookies(filterContext);
                    return;
                }
            }

        }

        protected void LimpaCookies(ActionExecutingContext filterContext)
        {
            string[] cookies = { "C-USR" };
            foreach (var c in cookies)
            {
                var cookie = filterContext.HttpContext.Request.Cookies.Get(c);
                if (cookie != null && !String.IsNullOrEmpty(cookie.Value))
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    filterContext.HttpContext.Response.Cookies.Add(cookie);
                }
            }
        }

    }
}