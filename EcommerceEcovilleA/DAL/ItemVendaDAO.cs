using EcommerceEcovilleA.Models;
using EcommerceEcovilleA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceEcovilleA.DAL
{
    public class ItemVendaDAO
    {
        public static void CadastrarItemVenda(ItemVenda itemVenda)
        {
            using (var ctx = new Context())
            {
                ctx.Produtos.Attach(itemVenda.Produto);
                ctx.Entry(itemVenda).State = System.Data.Entity.EntityState.Added;
                ctx.SaveChanges();
            }
        }

        public static List<ItemVenda> RetornarItensPorcArrinhoId(string carrinhoId)
        {
            using (var ctx = new Context())
            {
                return ctx.ItensVenda.
                Include("Produto.Categoria").AsNoTracking().
                Where(x => x.CarrinhoId.
                    Equals(carrinhoId) && x.Carrinho == true).ToList();
            }
        }

        public static List<ItemVenda> BuscarItensVendaPorCarrinhoId(string carrinhoId)
        {
            using (var ctx = new Context())
            {
                return ctx.ItensVenda.
                Include("Produto").AsNoTracking().
                Where(x => x.CarrinhoId == carrinhoId && x.Carrinho == true).ToList();
            }
        }

        public static void RemoverItem(int id)
        {
            using (var ctx = new Context())
            {
                ItemVenda item = ctx.ItensVenda.Find(id);

                if (item.Quantidade > 1)
                {
                    item.Quantidade--;
                }
                else
                {
                    ctx.ItensVenda.Remove(item);
                }
                ctx.SaveChanges();
            }
        }
        public static void AdicionarItem(int id)
        {
            using (var ctx = new Context())
            {
                ItemVenda item = ctx.ItensVenda.Find(id);
                item.Quantidade++;
                ctx.SaveChanges();
            }
        }
        public static void DiminuirItem(int id)
        {
            using (var ctx = new Context())
            {
                ItemVenda item = ctx.ItensVenda.Find(id);
                if (item.Quantidade > 1)
                {
                    item.Quantidade--;
                    ctx.SaveChanges();
                }
            }
        }

        public static double RetornarTotalCarrinho(string carrinhoId)
        {
            return BuscarItensVendaPorCarrinhoId(carrinhoId).Sum(x => x.Quantidade * x.Preco);
        }
        public static double RetornarQuantidadeItensCarrinho(string carrinhoId)
        {
            return BuscarItensVendaPorCarrinhoId(carrinhoId).Sum(x => x.Quantidade);
        }

        public static List<ItemVenda> RetornarItensVendaPorCarrinho(string carrinhoId)
        {
            using (var ctx = new Context())
            {
                return ctx.ItensVenda.Include("Produto.Categoria").AsNoTracking().
                Where(x => x.CarrinhoId == carrinhoId && x.Carrinho == true).
                ToList();
            }
        }

        public static bool LimparSessao(string carrinhoId)
        {
            try
            {
                using (var ctx = new Context())
                {
                    var lt = ctx.ItensVenda.Where(x => x.CarrinhoId == carrinhoId).ToList();
                    lt.ForEach(a => a.Carrinho = false);
                    ctx.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
