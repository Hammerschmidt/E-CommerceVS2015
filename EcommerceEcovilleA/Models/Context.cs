using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EcommerceEcovilleA.Models
{
    public class Context : DbContext
    {
        public Context() : base("DbEcommerceEcovilleA")
        {

        }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<ItemVenda> ItensVenda { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Frete> Fretes { get; set; }
        public DbSet<UF> UFs { get; set; }


    }
}