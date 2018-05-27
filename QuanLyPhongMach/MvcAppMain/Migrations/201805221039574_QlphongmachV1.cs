namespace MvcAppMain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QlphongmachV1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CT_PhieuKhamBenh", "ID_CachDung", "dbo.CachDung");
            DropForeignKey("dbo.PhieuKhamBenh", "HoSoBenhNhan_ID_BenhNhan", "dbo.HoSoBenhNhan");
            DropIndex("dbo.PhieuKhamBenh", new[] { "HoSoBenhNhan_ID_BenhNhan" });
            DropIndex("dbo.CT_PhieuKhamBenh", new[] { "ID_CachDung" });
            DropColumn("dbo.PhieuKhamBenh", "HoSoBenhNhan_ID_BenhNhan");
            DropColumn("dbo.CT_PhieuKhamBenh", "ID_CachDung");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CT_PhieuKhamBenh", "ID_CachDung", c => c.Int(nullable: false));
            AddColumn("dbo.PhieuKhamBenh", "HoSoBenhNhan_ID_BenhNhan", c => c.Int());
            CreateIndex("dbo.CT_PhieuKhamBenh", "ID_CachDung");
            CreateIndex("dbo.PhieuKhamBenh", "HoSoBenhNhan_ID_BenhNhan");
            AddForeignKey("dbo.PhieuKhamBenh", "HoSoBenhNhan_ID_BenhNhan", "dbo.HoSoBenhNhan", "ID_BenhNhan");
            AddForeignKey("dbo.CT_PhieuKhamBenh", "ID_CachDung", "dbo.CachDung", "ID_CachDung", cascadeDelete: true);
        }
    }
}
