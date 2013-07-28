namespace DbBox.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Morebs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Stocks", "List_Id", "dbo.StockLists");
            DropForeignKey("dbo.Stocks", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Stocks", new[] { "List_Id" });
            DropIndex("dbo.Stocks", new[] { "Country_Id" });
            AlterColumn("dbo.Stocks", "List_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Stocks", "Country_Id", c => c.String(nullable: false, maxLength: 128));
            AddForeignKey("dbo.Stocks", "List_Id", "dbo.StockLists", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Stocks", "Country_Id", "dbo.Countries", "Id", cascadeDelete: true);
            CreateIndex("dbo.Stocks", "List_Id");
            CreateIndex("dbo.Stocks", "Country_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Stocks", new[] { "Country_Id" });
            DropIndex("dbo.Stocks", new[] { "List_Id" });
            DropForeignKey("dbo.Stocks", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.Stocks", "List_Id", "dbo.StockLists");
            AlterColumn("dbo.Stocks", "Country_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Stocks", "List_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Stocks", "Country_Id");
            CreateIndex("dbo.Stocks", "List_Id");
            AddForeignKey("dbo.Stocks", "Country_Id", "dbo.Countries", "Id");
            AddForeignKey("dbo.Stocks", "List_Id", "dbo.StockLists", "Id");
        }
    }
}
