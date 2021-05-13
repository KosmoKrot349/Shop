namespace kr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.desctModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        desc = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.desctModels");
        }
    }
}
