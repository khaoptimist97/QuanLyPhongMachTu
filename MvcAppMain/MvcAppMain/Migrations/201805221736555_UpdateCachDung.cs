namespace MvcAppMain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCachDung : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CachDung", "SoLanDung");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CachDung", "SoLanDung", c => c.Int(nullable: false));
        }
    }
}
