namespace MvcAppMain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotAlowedNullGender : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HoSoBenhNhan", "GioiTinh", c => c.Boolean(nullable: false,defaultValue:false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HoSoBenhNhan", "GioiTinh", c => c.Boolean());
        }
    }
}
