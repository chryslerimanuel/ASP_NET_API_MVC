namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeQuantityTypeData : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tb_Item", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tb_Item", "Quantity", c => c.Double(nullable: false));
        }
    }
}
