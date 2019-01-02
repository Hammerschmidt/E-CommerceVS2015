using EcommerceEcovilleA.DAL;
using EcommerceEcovilleA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EcommerceEcovilleA.Controllers
{
    public class FretesController : Controller
    {
        private Context db = new Context();

        // GET: Fretes
        public ActionResult Index()
        {
            return View(FreteDAO.ListaFretes().ToList());
        }

        // GET: Fretes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Frete frete = db.Fretes.Find(id);
            if (frete == null)
            {
                return HttpNotFound();
            }
            return View(frete);
        }

        // GET: Fretes/Create
        public ActionResult Cadastro()
        {
            ViewBag.Estados = UFDAO.ListaEstados();
            return View();
        }

        // POST: Fretes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro([Bind(Include = "FreteId,Valor,UnidadeFederativa")] Frete frete)
        {
            if (ModelState.IsValid)
            {
                FreteDAO.CadastrarFrete(frete);
                return RedirectToAction("Index");
            }

            return View(frete);
        }

        // GET: Fretes/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Frete frete = new Frete();
            frete.FreteId = id;
            frete = FreteDAO.BuscarFrete(frete);
            if (frete == null)
            {
                return HttpNotFound();
            }
            return View(frete);
        }

        // POST: Fretes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FreteId,Valor,UnidadeFederativa")] Frete frete)
        {
            if (ModelState.IsValid)
            {
                FreteDAO.AtualizarFrete(frete);
                return RedirectToAction("Index");
            }
            return View(frete);
        }

        // GET: Fretes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Frete frete = db.Fretes.Find(id);
            if (frete == null)
            {
                return HttpNotFound();
            }
            return View(frete);
        }

        // POST: Fretes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Frete frete = new Frete();
            frete.FreteId = id;
            frete = FreteDAO.BuscarFrete(frete);
            FreteDAO.Remover(frete);
            return RedirectToAction("Index");
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