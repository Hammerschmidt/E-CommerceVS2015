namespace EcommerceEcovilleA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usr : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Vendas", "Cliente_ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.Vendas", "uf_UfId", "dbo.UF");
            DropIndex("dbo.Vendas", new[] { "Cliente_ClienteId" });
            DropIndex("dbo.Vendas", new[] { "uf_UfId" });
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Endereco = c.String(),
                        Telefone = c.String(),
                        Email = c.String(),
                        Senha = c.String(),
                        Cep = c.String(),
                        Logradouro = c.String(),
                        Uf = c.String(),
                    })
                .PrimaryKey(t => t.UsuarioId);
            
            AddColumn("dbo.Vendas", "Cep", c => c.String());
            AddColumn("dbo.Vendas", "Logradouro", c => c.String());
            AddColumn("dbo.Vendas", "Uf", c => c.String());
            AddColumn("dbo.Vendas", "Frete", c => c.Double(nullable: false));
            AddColumn("dbo.Vendas", "Usuario_UsuarioId", c => c.Int());
            CreateIndex("dbo.Vendas", "Usuario_UsuarioId");
            AddForeignKey("dbo.Vendas", "Usuario_UsuarioId", "dbo.Usuarios", "UsuarioId");
            DropColumn("dbo.Vendas", "Cliente_ClienteId");
            DropColumn("dbo.Vendas", "uf_UfId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vendas", "uf_UfId", c => c.Int());
            AddColumn("dbo.Vendas", "Cliente_ClienteId", c => c.Int());
            DropForeignKey("dbo.Vendas", "Usuario_UsuarioId", "dbo.Usuarios");
            DropIndex("dbo.Vendas", new[] { "Usuario_UsuarioId" });
            DropColumn("dbo.Vendas", "Usuario_UsuarioId");
            DropColumn("dbo.Vendas", "Frete");
            DropColumn("dbo.Vendas", "Uf");
            DropColumn("dbo.Vendas", "Logradouro");
            DropColumn("dbo.Vendas", "Cep");
            DropTable("dbo.Usuarios");
            CreateIndex("dbo.Vendas", "uf_UfId");
            CreateIndex("dbo.Vendas", "Cliente_ClienteId");
            AddForeignKey("dbo.Vendas", "uf_UfId", "dbo.UF", "UfId");
            AddForeignKey("dbo.Vendas", "Cliente_ClienteId", "dbo.Clientes", "ClienteId");
        }
    }
}
