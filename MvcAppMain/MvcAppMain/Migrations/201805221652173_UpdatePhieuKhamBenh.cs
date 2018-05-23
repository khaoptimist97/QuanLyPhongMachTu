namespace MvcAppMain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePhieuKhamBenh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhieuKhamBenh", "Deleted", c => c.Boolean(nullable: false, defaultValue: false));
        }

        public override void Down()
        {
        }
    }
}
