using EcommerceEcovilleA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EcommerceEcovilleA.DAL
{
    public class ProdutoDAO
    {

        public static List<Produto> RetornarProdutos()
        {
            using (var ctx = new Context())
            {
                return ctx.Produtos.AsNoTracking().Include("Categoria").ToList();
            }
        }
        public static bool CadastrarProduto(Produto produto)
        {
            if (BuscarProdutoPorNome(produto) == null)
            {
                using (var ctx = new Context())
                {
                    ctx.Categorias.Attach(produto.Categoria);
                    ctx.Produtos.Add(produto);
                    ctx.SaveChanges();
                }
                return true;
            }
            return false;
        }
        public static List<Produto> BuscarProdutosPorCategoria(int? id)
        {
            using (var ctx = new Context())
            {
                return ctx.Produtos.AsNoTracking().Include("Categoria").
                Where(x => x.Categoria.CategoriaId == id).ToList();
            }
        }

        public static List<Produto> BuscarProdutosPorCategoria(string categoria)
        {
            using (var ctx = new Context())
            {
                return ctx.Produtos.AsNoTracking().Include("Categoria").
                Where(x => x.Categoria.Nome == categoria).ToList();
            }
        }

        public static Produto BuscarProdutoPorId(int id)
        {
            using (var ctx = new Context())
            {
                return ctx.Produtos.Include("Categoria").AsNoTracking().Where(a => a.ProdutoId == id).FirstOrDefault();
            }
        }
        public static void RemoverProduto(int id)
        {
            using (var ctx = new Context())
            {
                ctx.Produtos.Remove(BuscarProdutoPorId(id));
            }
        }
        public static void AlterarProduto(Produto produto)
        {
            using (var cx = new Context())
            {
                cx.Produtos.Attach(produto);
                cx.Entry(produto).State = EntityState.Modified;
                cx.SaveChanges();
            }
        }
        public static Produto BuscarProdutoPorNome(Produto produto)
        {
            using (var ctx = new Context())
            {
                return ctx.Produtos.AsNoTracking().
                FirstOrDefault(x => x.Nome.Equals(produto.Nome));
            }
        }
    }
}