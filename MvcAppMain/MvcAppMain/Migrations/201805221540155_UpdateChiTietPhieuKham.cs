namespace MvcAppMain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateChiTietPhieuKham : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CT_PhieuKhamBenh", "DonGiaBan", c => c.Int(nullable: false, defaultValue:0));
            AddColumn("dbo.CT_PhieuKhamBenh", "ThanhTien", c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CT_PhieuKhamBenh", "ThanhTien");
            DropColumn("dbo.CT_PhieuKhamBenh", "DonGiaBan");
        }
    }
}
