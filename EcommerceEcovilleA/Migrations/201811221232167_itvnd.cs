namespace EcommerceEcovilleA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itvnd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItensVenda", "Carrinho", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItensVenda", "Carrinho");
        }
    }
}
