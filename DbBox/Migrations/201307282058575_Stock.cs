namespace DbBox.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Stock : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        List_Id = c.String(maxLength: 128),
                        Country_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StockLists", t => t.List_Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .Index(t => t.List_Id)
                .Index(t => t.Country_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Stocks", new[] { "Country_Id" });
            DropIndex("dbo.Stocks", new[] { "List_Id" });
            DropForeignKey("dbo.Stocks", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.Stocks", "List_Id", "dbo.StockLists");
            DropTable("dbo.Stocks");
        }
    }
}
