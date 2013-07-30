namespace DbBox.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class None : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        List_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StockLists", t => t.List_Id, cascadeDelete: true)
                .Index(t => t.List_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Stocks", new[] { "List_Id" });
            DropForeignKey("dbo.Stocks", "List_Id", "dbo.StockLists");
            DropTable("dbo.Stocks");
        }
    }
}
