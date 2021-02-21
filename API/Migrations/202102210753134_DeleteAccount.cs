namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteAccount : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Tb_Account");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Tb_Account",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
