using EcommerceEcovilleA.Models;
using EcommerceEcovilleA.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceEcovilleA.Controllers
{
    public class BaseController : Controller
    {
        public Usuario Usuario;
        public BaseController()
        {
        }

        protected override void OnActionExecuting(ActionExecutingContext ctx)
        {
            base.OnActionExecuting(ctx);
            try
            {
                var usuarioCookie = HttpContext.Request.Cookies.Get("C-USR");
                if (usuarioCookie == null || String.IsNullOrEmpty(usuarioCookie.Value))
                    Usuario = null;
                else
                {
                    try
                    {
                        string obj = Seguranca.DescriptografaCookie(usuarioCookie.Value);
                        Usuario = JsonConvert.DeserializeObject<Usuario>(obj);
                        ViewBag.Usuario = Usuario;
                    }
                    catch
                    {
                        LimpaCookies();
                    }
                }
            }
            catch { }
        }


        protected void LimpaCookies()
        {
            string[] cookies = { "C-USR" };
            foreach (var c in cookies)
            {
                var cookie = HttpContext.Request.Cookies.Get(c);
                if (cookie != null && !String.IsNullOrEmpty(cookie.Value))
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    HttpContext.Response.Cookies.Add(cookie);
                }
            }
        }
    }
}