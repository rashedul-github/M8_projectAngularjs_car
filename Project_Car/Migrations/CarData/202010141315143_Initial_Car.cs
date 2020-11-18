namespace Project_Car.Migrations.CarData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Car : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarParts",
                c => new
                    {
                        CarPartsId = c.Int(nullable: false, identity: true),
                        PartsName = c.String(nullable: false, maxLength: 40),
                        Stock = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        CarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CarPartsId)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        CarId = c.Int(nullable: false, identity: true),
                        CarModel = c.String(nullable: false, maxLength: 40),
                        Type = c.Int(),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        IsAvailable = c.Boolean(nullable: false),
                        LunchDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CarId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarParts", "CarId", "dbo.Cars");
            DropIndex("dbo.CarParts", new[] { "CarId" });
            DropTable("dbo.Cars");
            DropTable("dbo.CarParts");
        }
    }
}
