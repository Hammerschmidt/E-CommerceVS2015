namespace EcommerceEcovilleA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class criarbancodeDados : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        CategoriaId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Endereco = c.String(),
                        Telefone = c.String(),
                    })
                .PrimaryKey(t => t.ClienteId);
            
            CreateTable(
                "dbo.Fretes",
                c => new
                    {
                        FreteId = c.Int(nullable: false, identity: true),
                        Valor = c.Double(nullable: false),
                        UnidadeFederativa_UfId = c.Int(),
                    })
                .PrimaryKey(t => t.FreteId)
                .ForeignKey("dbo.UF", t => t.UnidadeFederativa_UfId)
                .Index(t => t.UnidadeFederativa_UfId);
            
            CreateTable(
                "dbo.UF",
                c => new
                    {
                        UfId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UfId);
            
            CreateTable(
                "dbo.ItensVenda",
                c => new
                    {
                        ItemVendaId = c.Int(nullable: false, identity: true),
                        Quantidade = c.Int(nullable: false),
                        Preco = c.Double(nullable: false),
                        Data = c.DateTime(nullable: false),
                        CarrinhoId = c.String(),
                        Produto_ProdutoId = c.Int(),
                        Venda_VendaId = c.Int(),
                    })
                .PrimaryKey(t => t.ItemVendaId)
                .ForeignKey("dbo.Produtos", t => t.Produto_ProdutoId)
                .ForeignKey("dbo.Vendas", t => t.Venda_VendaId)
                .Index(t => t.Produto_ProdutoId)
                .Index(t => t.Venda_VendaId);
            
            CreateTable(
                "dbo.Produtos",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        Descricao = c.String(),
                        Imagem = c.String(),
                        Preco = c.Double(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        Categoria_CategoriaId = c.Int(),
                    })
                .PrimaryKey(t => t.ProdutoId)
                .ForeignKey("dbo.Categorias", t => t.Categoria_CategoriaId)
                .Index(t => t.Categoria_CategoriaId);
            
            CreateTable(
                "dbo.Vendas",
                c => new
                    {
                        VendaId = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        Cliente_ClienteId = c.Int(),
                        uf_UfId = c.Int(),
                    })
                .PrimaryKey(t => t.VendaId)
                .ForeignKey("dbo.Clientes", t => t.Cliente_ClienteId)
                .ForeignKey("dbo.UF", t => t.uf_UfId)
                .Index(t => t.Cliente_ClienteId)
                .Index(t => t.uf_UfId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vendas", "uf_UfId", "dbo.UF");
            DropForeignKey("dbo.ItensVenda", "Venda_VendaId", "dbo.Vendas");
            DropForeignKey("dbo.Vendas", "Cliente_ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.ItensVenda", "Produto_ProdutoId", "dbo.Produtos");
            DropForeignKey("dbo.Produtos", "Categoria_CategoriaId", "dbo.Categorias");
            DropForeignKey("dbo.Fretes", "UnidadeFederativa_UfId", "dbo.UF");
            DropIndex("dbo.Vendas", new[] { "uf_UfId" });
            DropIndex("dbo.Vendas", new[] { "Cliente_ClienteId" });
            DropIndex("dbo.Produtos", new[] { "Categoria_CategoriaId" });
            DropIndex("dbo.ItensVenda", new[] { "Venda_VendaId" });
            DropIndex("dbo.ItensVenda", new[] { "Produto_ProdutoId" });
            DropIndex("dbo.Fretes", new[] { "UnidadeFederativa_UfId" });
            DropTable("dbo.Vendas");
            DropTable("dbo.Produtos");
            DropTable("dbo.ItensVenda");
            DropTable("dbo.UF");
            DropTable("dbo.Fretes");
            DropTable("dbo.Clientes");
            DropTable("dbo.Categorias");
        }
    }
}
