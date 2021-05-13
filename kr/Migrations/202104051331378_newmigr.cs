namespace kr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmigr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "countOfSels", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "isCompleet", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "isCompleet");
            DropColumn("dbo.Products", "countOfSels");
        }
    }
}
