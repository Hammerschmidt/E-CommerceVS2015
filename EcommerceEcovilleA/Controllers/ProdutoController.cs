using EcommerceEcovilleA.DAL;
using EcommerceEcovilleA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceEcovilleA.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            ViewBag.Title = "Gerenciamento de Produtos";
            ViewBag.Data = DateTime.Now;
            return View(ProdutoDAO.RetornarProdutos());
        }

        public ActionResult CadastrarProduto()
        {
            ViewBag.Categorias =
                new SelectList(CategoriaDAO.RetornarCategorias(),
                "CategoriaId", "Nome");

            ViewBag.Title = "Cadastrar Produto";
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarProduto(Produto produto, int? Categorias,
            HttpPostedFileBase fupImagem)
        {
            ViewBag.Categorias =
                new SelectList(CategoriaDAO.RetornarCategorias(),
                "CategoriaId", "Nome");
            ViewBag.Title = "Gerenciamento de Produtos";
            if (ModelState.IsValid)
            {
                if (Categorias != null)
                {
                    if (fupImagem != null)
                    {
                        string nomeImagem = Path.GetFileName(fupImagem.FileName);
                        string caminho = Path.Combine(Server.MapPath("~/Imagens/"), nomeImagem);
                        fupImagem.SaveAs(caminho);
                        produto.Imagem = nomeImagem;
                    }
                    else
                    {
                        produto.Imagem = "semimagem.jpeg";
                    }
                    produto.Categoria =
                        CategoriaDAO.BuscarCategoriaPorId(Categorias);
                    if (ProdutoDAO.CadastrarProduto(produto))
                    {
                        return RedirectToAction("Index", "Produto");
                    }
                    ModelState.AddModelError("", "Não é possível adicionar um produto com o mesmo nome");
                    return View(produto);
                }
                ModelState.AddModelError("", "Por favor, selecione uma categoria");
                return View(produto);
            }
            return View(produto);
        }

        public ActionResult RemoverProduto(int id)
        {
            ProdutoDAO.RemoverProduto(id);
            return RedirectToAction("Index", "Produto");
        }

        public ActionResult AlterarProduto(int id)
        {
            ViewBag.Title = "Alterar Produto";
            var p = ProdutoDAO.BuscarProdutoPorId(id);
            var c = CategoriaDAO.RetornarCategorias();
            c.Remove(p.Categoria);
            ViewBag.Categorias = c;
            return View(ProdutoDAO.BuscarProdutoPorId(id));
        }

        [HttpPost]
        public ActionResult AlterarProduto(Produto produto)
        {
            if (produto.Categoria != null && produto.Categoria.CategoriaId != 0)
            {
                produto.Imagem = "semimagem.jpeg";
                ProdutoDAO.AlterarProduto(produto);
                return RedirectToAction("Index", "Produto");
            }
            var c = CategoriaDAO.RetornarCategorias();
            c.Remove(produto.Categoria);
            ViewBag.Categorias = c;
            return View(produto);
        }
    }
}