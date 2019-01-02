using EcommerceEcovilleA.DAL;
using EcommerceEcovilleA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceEcovilleA.Controllers
{
    public class UFsController : Controller
    {
        private Context db = new Context();

        // GET: UFs
        public ActionResult Index()
        {
            return View(UFDAO.ListaEstados().ToList());
        }

        // GET: UFs/Cadastro
        public ActionResult Cadastro()
        {
            return View();
        }

        // POST: UFs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro([Bind(Include = "UfId,Descricao")] UF uF)
        {
            if (ModelState.IsValid)
            {
                UFDAO.CadastrarUF(uF);
                return RedirectToAction("Index");
            }

            return View(uF);
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