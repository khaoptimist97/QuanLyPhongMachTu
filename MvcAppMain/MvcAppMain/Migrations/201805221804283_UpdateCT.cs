namespace MvcAppMain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCT : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CT_PhieuKhamBenh", "Deleted", c => c.Boolean(nullable: false, defaultValue: false));
        }

        public override void Down()
        {
        }
    }
}
