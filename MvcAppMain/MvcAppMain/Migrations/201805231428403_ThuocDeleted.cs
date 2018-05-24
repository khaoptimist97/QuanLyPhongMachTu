namespace MvcAppMain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThuocDeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CachDung", "Deleted", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.Thuoc", "Deleted", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.DonVi", "Deleted", c => c.Boolean(nullable: false, defaultValue: false));
        }

        public override void Down()
        {
        }
    }
}
