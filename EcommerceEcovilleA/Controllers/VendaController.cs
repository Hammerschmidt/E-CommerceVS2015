using EcommerceEcovilleA.DAL;
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

namespace EcommerceEcovilleA.Controllers
{
    public class VendaController : BaseController
    {

        [HttpPost]
        public ActionResult BuscarCep(Venda venda)
        {
            //Criar a URL para requisição
            string url = "https://viacep.com.br/ws/" + venda.Cep + "/json/";
            //Criar para fazer o download do JSON
            WebClient client = new WebClient();
            //Fazer o download do JSON
            string resultado = client.DownloadString(url);
            //Criptografar para UTF8
            byte[] bytes = Encoding.Default.GetBytes(resultado);
            resultado = Encoding.UTF8.GetString(bytes);
            //Converter o JSON para o ojeto
            venda = JsonConvert.DeserializeObject<Venda>(resultado);
            TempData["Venda"] = venda;
            return RedirectToAction("Orcamento", "Venda");
        }

        [Acesso]
        public ActionResult Orcamento()
        {
            string frete = "Buscar pelo CEP";
            var v = TempData["Venda"] as Venda;
            if (v == null) v = new Venda();
            else
            {
                var f = FreteDAO.BuscarFretePorUF(v.Uf);
                if (f != null)
                {
                    frete = f.Valor.ToString("C2");
                    v.Frete = f.Valor;
                }
            }
            ViewBag.Frete = frete;
            v.Items = ItemVendaDAO.RetornarItensVendaPorCarrinho(Usuario.UsuarioId.ToString());
            ViewBag.total = CalculadoraCarrinho.CalcularTotal(v.Items);
            return View(v);
        }

        [Acesso]
        public ActionResult Finalizar(Venda venda)
        {
            var cat = CategoriaDAO.RetornarCategorias().Select(a => a.Nome).ToList();
            venda.Usuario = Usuario;
            venda.Data = DateTime.Now;
            venda.Items = ItemVendaDAO.RetornarItensVendaPorCarrinho(Usuario.UsuarioId.ToString());
            var pr = venda.Items.Select(a => a.Produto.Categoria.Nome).ToList();
            if (cat.Where(a => !pr.Contains(a)).Any())
            {
                TempData["Erro"] = new Erros
                {
                    Erro = true,
                    Data = "Só é possivel finalizar adicionando 1 produto por categoria."
                };
                return RedirectToAction("Index", "Home");
            }
            VendaDAO.CadastrarVenda(venda);

            ItemVendaDAO.LimparSessao(Usuario.UsuarioId.ToString());

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Index()
        {
            ViewBag.Data = DateTime.Now;
            return View(VendaDAO.RetornarVendas());
        }

        public ActionResult DetalhesVenda(int id)
        {
            var venda = VendaDAO.DetalhesVenda(id);
            ViewBag.Valor = venda.Items.Sum(x => x.Preco).ToString("C2");
            return View(venda);
        }
    }
}