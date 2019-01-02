using EcommerceEcovilleA.DAL;
using EcommerceEcovilleA.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace EcommerceEcovilleA.Controllers
{
    [RoutePrefix("api")]
    public class WsController : ApiController
    {

        [HttpGet]
        [Route("ListaProdutos")]
        public IHttpActionResult ListaProdutos()
        {
            return Ok(ProdutoDAO.RetornarProdutos());
        }

        [HttpGet]
        [Route("ListaProdutos/Categoria")]
        public IHttpActionResult ListaProdutos(int? categoriaId)
        {
            return Ok(ProdutoDAO.BuscarProdutosPorCategoria(categoriaId));
        }

        [HttpGet]
        [Route("ListaProdutos/Categoria")]
        public IHttpActionResult ListaProdutos(string categoria)
        {
            return Ok(ProdutoDAO.BuscarProdutosPorCategoria(categoria));
        }

        [HttpGet]
        [Route("ListaVendas")]
        public IHttpActionResult ListaVendas()
        {
            return Ok(VendaDAO.RetornarVendas());
        }

        [HttpGet]
        [Route("DetalheVenda")]
        public IHttpActionResult Venda(int? id)
        {
            return Ok(VendaDAO.DetalhesVenda(id.Value));
        }

        [HttpGet]
        [Route("PrecoFrete")]
        public IHttpActionResult PrecoFrete(string cep)
        {
            //Criar a URL para requisição
            string url = "https://viacep.com.br/ws/" + cep + "/json/";
            //Criar para fazer o download do JSON
            WebClient client = new WebClient();
            //Fazer o download do JSON
            string resultado = client.DownloadString(url);
            //Criptografar para UTF8
            byte[] bytes = Encoding.Default.GetBytes(resultado);
            resultado = Encoding.UTF8.GetString(bytes);
            //Converter o JSON para o ojeto
            dynamic obj = JsonConvert.DeserializeObject(resultado);
            string UF = obj.uf;
            return Ok(FreteDAO.BuscarFretePorUF(UF));
        }

        [HttpPost]
        [Route("CadastrarProduto")]
        public IHttpActionResult CadastroProduto(Produto produto)
        {
            var retorno = ProdutoDAO.CadastrarProduto(produto);
            return Ok(retorno);
        }
    }
}
