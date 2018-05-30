namespace MvcAppMain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBKTables : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.BK_HoaDon");
            AddColumn("dbo.BK_CT_PhieuKhamBenh", "DonGiaBan", c => c.Int(nullable: false));
            AddColumn("dbo.BK_CT_PhieuKhamBenh", "ThanhTien", c => c.Int(nullable: false));
            AddColumn("dbo.BK_HoaDon", "TongTien", c => c.Int(nullable: false));
            AddColumn("dbo.BK_PhieuKhamBenh", "IDPhieuTN", c => c.Int(nullable: false));
            AlterColumn("dbo.BK_CT_PhieuKhamBenh", "SoLuongThuocLay", c => c.Int(nullable: false));
            AlterColumn("dbo.BK_HoaDon", "ID_HoaDon", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.BK_HoaDon", "TienKham", c => c.Int(nullable: false));
            AlterColumn("dbo.BK_HoaDon", "TienThuoc", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.BK_HoaDon", "ID_HoaDon");
            DropColumn("dbo.BK_CT_PhieuKhamBenh", "ID_CachDung");
            DropColumn("dbo.BK_HoaDon", "DoanhThu");
            DropColumn("dbo.BK_HoaDon", "GhiChu");
            DropColumn("dbo.BK_PhieuKhamBenh", "ID_BenhNhan");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BK_PhieuKhamBenh", "ID_BenhNhan", c => c.Int(nullable: false));
            AddColumn("dbo.BK_HoaDon", "GhiChu", c => c.String(maxLength: 255, unicode: false));
            AddColumn("dbo.BK_HoaDon", "DoanhThu", c => c.Int());
            AddColumn("dbo.BK_CT_PhieuKhamBenh", "ID_CachDung", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.BK_HoaDon");
            AlterColumn("dbo.BK_HoaDon", "TienThuoc", c => c.Int());
            AlterColumn("dbo.BK_HoaDon", "TienKham", c => c.Int());
            AlterColumn("dbo.BK_HoaDon", "ID_HoaDon", c => c.Int(nullable: false));
            AlterColumn("dbo.BK_CT_PhieuKhamBenh", "SoLuongThuocLay", c => c.Int());
            DropColumn("dbo.BK_PhieuKhamBenh", "IDPhieuTN");
            DropColumn("dbo.BK_HoaDon", "TongTien");
            DropColumn("dbo.BK_CT_PhieuKhamBenh", "ThanhTien");
            DropColumn("dbo.BK_CT_PhieuKhamBenh", "DonGiaBan");
            AddPrimaryKey("dbo.BK_HoaDon", new[] { "ID_HoaDon", "ID_PhieuKham" });
        }
    }
}
