﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceEcovilleA.Utils
{
    public class Sessao
    {
        public static string RetornarCarrinhoId()
        {
            if(HttpContext.Current.Session["CarrinhoId"] == null)
            {
                Guid guid = Guid.NewGuid();
                HttpContext.Current.Session["CarrinhoId"] = guid.ToString();
            }
            return HttpContext.Current.Session["CarrinhoId"].ToString();
        }
        public static void LimparSessao()
        {
            HttpContext.Current.Session["CarrinhoId"] = null;
        }
    }
}