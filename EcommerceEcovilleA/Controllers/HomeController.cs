using EcommerceEcovilleA.DAL;
using EcommerceEcovilleA.Models;
using EcommerceEcovilleA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceEcovilleA.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        [Acesso]
        public ActionResult Index(int? id)
        {
            if (TempData["Erro"] != null)
                ViewBag.Erro = TempData["Erro"];
            ViewBag.Title = "Ecoville A";
            ViewBag.Categorias = CategoriaDAO.RetornarCategorias();
            if (id == null)
            {
                return View(ProdutoDAO.RetornarProdutos());
            }
            return View(ProdutoDAO.BuscarProdutosPorCategoria(id));
        }

        [Acesso]
        public ActionResult AdicionarAoCarrinho(int id)
        {
            ViewBag.Title = "Carrinho Compras";
            Produto produto = ProdutoDAO.BuscarProdutoPorId(id);
            var itens = ItemVendaDAO.RetornarItensVendaPorCarrinho(Usuario.UsuarioId.ToString());
            if (!itens.Where(a => a.Produto.Categoria.Nome == produto.Categoria.Nome).Any())
            {

                ItemVenda item = new ItemVenda
                {
                    Produto = produto,
                    Preco = produto.Preco,
                    Quantidade = 1,
                    Data = DateTime.Now,
                    CarrinhoId = Usuario.UsuarioId.ToString(),
                    Carrinho = true
                };
                ItemVendaDAO.CadastrarItemVenda(item);
                return RedirectToAction("CarrinhoCompras");
            }
            else
            {
                TempData["Erro"] = new Erros { Erro = true, Data = "Só é possível adicionar 1 produto por categoria." };
                return RedirectToAction("Index");
            }
        }

        [Acesso]
        public ActionResult CarrinhoCompras()
        {
            ViewBag.Total = ItemVendaDAO.RetornarTotalCarrinho(Usuario.UsuarioId.ToString());
            return View(ItemVendaDAO.
                BuscarItensVendaPorCarrinhoId(Usuario.UsuarioId.ToString()));
        }

        [Acesso]
        public ActionResult RemoverItem(int id)
        {
            ItemVendaDAO.RemoverItem(id);
            return RedirectToAction("CarrinhoCompras", "Home");
        }

        [Acesso]
        public ActionResult AdicionarItem(int id)
        {
            ItemVendaDAO.AdicionarItem(id);
            return RedirectToAction("CarrinhoCompras", "Home");
        }

        [Acesso]
        public ActionResult DiminuirItem(int id)
        {
            ItemVendaDAO.DiminuirItem(id);
            return RedirectToAction("CarrinhoCompras", "Home");
        }
    }
}