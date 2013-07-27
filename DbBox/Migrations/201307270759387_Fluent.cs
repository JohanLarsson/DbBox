namespace DbBox.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fluent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StockLists", "Country_Id", "dbo.Countries");
            DropIndex("dbo.StockLists", new[] { "Country_Id" });
            AlterColumn("dbo.StockLists", "Country_Id", c => c.String(nullable: false, maxLength: 128));
            AddForeignKey("dbo.StockLists", "Country_Id", "dbo.Countries", "Id", cascadeDelete: true);
            CreateIndex("dbo.StockLists", "Country_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.StockLists", new[] { "Country_Id" });
            DropForeignKey("dbo.StockLists", "Country_Id", "dbo.Countries");
            AlterColumn("dbo.StockLists", "Country_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.StockLists", "Country_Id");
            AddForeignKey("dbo.StockLists", "Country_Id", "dbo.Countries", "Id");
        }
    }
}
