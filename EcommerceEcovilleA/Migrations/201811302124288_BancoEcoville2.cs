namespace EcommerceEcovilleA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BancoEcoville2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produtos", "Tamanho", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Produtos", "Tamanho");
        }
    }
}
