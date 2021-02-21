namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doubletofloat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tb_Item", "Price", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tb_Item", "Price", c => c.Double(nullable: false));
        }
    }
}
