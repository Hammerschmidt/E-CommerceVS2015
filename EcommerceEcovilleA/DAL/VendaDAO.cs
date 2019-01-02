using EcommerceEcovilleA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;



namespace EcommerceEcovilleA.DAL
{
    public class VendaDAO
    {

        public static void CadastrarVenda(Venda venda)
        {
            using (var ctx = new Context())
            {
                ctx.Usuarios.Attach(venda.Usuario);
                venda.Items.ForEach(a =>
                {
                    ctx.ItensVenda.Attach(a);
                });
                ctx.Vendas.Add(venda);
                ctx.SaveChanges();
            }
        }

        public static List<Venda> RetornarVendas()
        {
            using (var ctx = new Context())
            {
                return ctx.Vendas.AsNoTracking().Include("Usuario").ToList();
            }
        }

        public static Venda DetalhesVenda(int id)
        {
            using (var ctx = new Context())
            {
                return ctx.Vendas.Include(x => x.Items)
                                 .Include(x => x.Items.Select(a => a.Produto)).AsNoTracking()
                                 .FirstOrDefault(x => x.VendaId==id);
            }
        }
    }
}