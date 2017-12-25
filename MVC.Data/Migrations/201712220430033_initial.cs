namespace MVC.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cost",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Description = c.String(maxLength: 200),
                        EstimatedCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Dated = c.DateTime(nullable: false, storeType: "date"),
                        Priority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cost");
        }
    }
}
