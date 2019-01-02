using EcommerceEcovilleA.Models;
using EcommerceEcovilleA.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EcommerceEcovilleA.Controllers
{
    public class UsuarioController : BaseController
    {
        private Context db = new Context();

        [HttpPost]
        public ActionResult BuscarCep(Usuario usuario)
        {
            //Criar a URL para requisição
            string url = "https://viacep.com.br/ws/" + usuario.Cep + "/json/";
            //Criar para fazer o download do JSON
            WebClient client = new WebClient();
            //Fazer o download do JSON
            string resultado = client.DownloadString(url);
            //Criptografar para UTF8
            byte[] bytes = Encoding.Default.GetBytes(resultado);
            resultado = Encoding.UTF8.GetString(bytes);
            //Converter o JSON para o ojeto
            usuario = JsonConvert.DeserializeObject<Usuario>(resultado);
            TempData["Usuario"] = usuario;
            return RedirectToAction("CadastrarUsuario", "Usuario");
        }

        public ActionResult Login()
        {
            if (Usuario != null)
                return RedirectToAction("Index", "Home");
            else
                return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            usuario = db.Usuarios.FirstOrDefault(x =>
                x.Email.Equals(usuario.Email) &&
                x.Senha.Equals(usuario.Senha));
            if (usuario != null)
            {
                var json = Seguranca.CriptografaCookie(JsonConvert.SerializeObject(usuario));

                HttpCookie usuarioCookie = new HttpCookie("C-USR", json);
                usuarioCookie.Expires = DateTime.MinValue;
                usuarioCookie.Path = "/";
                this.ControllerContext.HttpContext.Response.Cookies.Add(usuarioCookie);
                return RedirectToAction("Index", "Home");
            }
            ModelState.Clear();
            ModelState.AddModelError("", "E-mail ou senha inválidos!");
            return View();
        }

        // GET: Usuario
        public ActionResult Index()
        {
            return View(db.Usuarios.ToList());
        }

        // GET: Usuario/Create
        public ActionResult CadastrarUsuario()
        {
            return View(TempData["Usuario"]);
        }

        public ActionResult Logout()
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
            return RedirectToAction("Index", "Home");
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarUsuario([Bind(Include = "UsuarioId,Nome,Endereco,Telefone,Email,Senha,ConfirmacaoSenha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}