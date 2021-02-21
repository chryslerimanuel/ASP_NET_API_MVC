namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigrate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tb_Item",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        Quantity = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tb_M_Supplier", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tb_Item", "SupplierId", "dbo.Tb_M_Supplier");
            DropIndex("dbo.Tb_Item", new[] { "SupplierId" });
            DropTable("dbo.Tb_Item");
        }
    }
}
