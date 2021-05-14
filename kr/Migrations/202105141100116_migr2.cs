namespace kr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "title", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "title", c => c.String());
            AlterColumn("dbo.Categories", "title", c => c.String());
        }
    }
}
