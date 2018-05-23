namespace MvcAppMain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Benh",
                c => new
                    {
                        ID_Benh = c.Int(nullable: false, identity: true),
                        TenBenh = c.String(maxLength: 255, unicode: false),
                        Deleted = c.Boolean(nullable: false, defaultValue: false),
                })
                .PrimaryKey(t => t.ID_Benh);
            
            CreateTable(
                "dbo.PhieuKhamBenh",
                c => new
                    {
                        ID_PhieuKham = c.Int(nullable: false, identity: true),
                        IDPhieuTN = c.Int(nullable: false),
                        ID_Benh = c.Int(nullable: false),
                        NgayKham = c.DateTime(storeType: "date",defaultValueSql:"GetDate()"),
                        TrieuChung = c.String(maxLength: 255, unicode: false),
                        HoSoBenhNhan_ID_BenhNhan = c.Int(),
                    })
                .PrimaryKey(t => t.ID_PhieuKham)
                .ForeignKey("dbo.PhieuTiepNhan", t => t.IDPhieuTN, cascadeDelete: true)
                .ForeignKey("dbo.HoSoBenhNhan", t => t.HoSoBenhNhan_ID_BenhNhan)
                .ForeignKey("dbo.Benh", t => t.ID_Benh)
                .Index(t => t.IDPhieuTN)
                .Index(t => t.ID_Benh)
                .Index(t => t.HoSoBenhNhan_ID_BenhNhan);
            
            CreateTable(
                "dbo.CT_PhieuKhamBenh",
                c => new
                    {
                        ID_PhieuKham = c.Int(nullable: false),
                        ID_Thuoc = c.Int(nullable: false),
                        ID_CachDung = c.Int(nullable: false),
                        SoLuongThuocLay = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_PhieuKham, t.ID_Thuoc })
                .ForeignKey("dbo.Thuoc", t => t.ID_Thuoc, cascadeDelete: true)
                .ForeignKey("dbo.CachDung", t => t.ID_CachDung, cascadeDelete: true)
                .ForeignKey("dbo.PhieuKhamBenh", t => t.ID_PhieuKham, cascadeDelete: true)
                .Index(t => t.ID_PhieuKham)
                .Index(t => t.ID_Thuoc)
                .Index(t => t.ID_CachDung);
            
            CreateTable(
                "dbo.CachDung",
                c => new
                    {
                        ID_CachDung = c.Int(nullable: false, identity: true),
                        TenCachDung = c.String(maxLength: 255, unicode: false),
                        SoLanDung = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID_CachDung);
            
            CreateTable(
                "dbo.Thuoc",
                c => new
                    {
                        ID_Thuoc = c.Int(nullable: false, identity: true),
                        TenThuoc = c.String(maxLength: 255, unicode: false),
                        SoLuong = c.Int(nullable: false),
                        DonGia = c.Int(nullable: false),
                        ID_DonVi = c.Int(nullable: false),
                        ID_CachDung = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false, defaultValue: false),
                })
                .PrimaryKey(t => t.ID_Thuoc)
                .ForeignKey("dbo.CachDung", t => t.ID_CachDung)
                .ForeignKey("dbo.DonVi", t => t.ID_DonVi)
                .Index(t => t.ID_DonVi)
                .Index(t => t.ID_CachDung);
            
            CreateTable(
                "dbo.DonVi",
                c => new
                    {
                        ID_DonVi = c.Int(nullable: false, identity: true),
                        TenDonVi = c.String(maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.ID_DonVi);
            
            CreateTable(
                "dbo.HoaDon",
                c => new
                    {
                        ID_HoaDon = c.Int(nullable: false, identity: true),
                        ID_PhieuKham = c.Int(nullable: false),
                        TienKham = c.Int(nullable: false, defaultValue: 0),
                        TienThuoc = c.Int(nullable: false, defaultValue: 0),
                        TongTien = c.Int(nullable: false,defaultValue: 0),
                        GhiChu = c.String(maxLength: 255, unicode: false),
                        Deleted = c.Boolean(nullable: false, defaultValue: false),
                })
                .PrimaryKey(t => t.ID_HoaDon)
                .ForeignKey("dbo.PhieuKhamBenh", t => t.ID_PhieuKham, cascadeDelete: true)
                .Index(t => t.ID_PhieuKham);
            
            CreateTable(
                "dbo.HoSoBenhNhan",
                c => new
                    {
                        ID_BenhNhan = c.Int(nullable: false, identity: true),
                        HoTen = c.String(maxLength: 255, unicode: false),
                        GioiTinh = c.Boolean(),
                        NamSinh = c.DateTime(storeType: "date"),
                        DiaChi = c.String(maxLength: 255, unicode: false),
                        Deleted = c.Boolean(nullable: false, defaultValue: false),
                    })
                .PrimaryKey(t => t.ID_BenhNhan);
            
            CreateTable(
                "dbo.PhieuTiepNhan",
                c => new
                    {
                        IDPhieuTN = c.Int(nullable: false, identity: true),
                        ID_BenhNhan = c.Int(nullable: false),
                        NgayTiepNhan = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false, defaultValue: false),
                })
                .PrimaryKey(t => t.IDPhieuTN)
                .ForeignKey("dbo.HoSoBenhNhan", t => t.ID_BenhNhan, cascadeDelete: true)
                .Index(t => t.ID_BenhNhan);

            CreateTable(
                "dbo.BK_CT_PhieuKhamBenh",
                c => new
                {
                    ID_PhieuKham = c.Int(nullable: false),
                    ID_Thuoc = c.Int(nullable: false),
                    ID_CachDung = c.Int(nullable: false),
                    SoLuongThuocLay = c.Int(),
                })
                .PrimaryKey(t => new { t.ID_PhieuKham, t.ID_Thuoc });



            CreateTable(
                "dbo.BK_HoaDon",
                c => new
                {
                    ID_HoaDon = c.Int(nullable: false),
                    ID_PhieuKham = c.Int(nullable: false),
                    TienKham = c.Int(),
                    TienThuoc = c.Int(),
                    DoanhThu = c.Int(),
                    GhiChu = c.String(maxLength: 255, unicode: false),
                })
                .PrimaryKey(t => new { t.ID_HoaDon, t.ID_PhieuKham });


            CreateTable(
                "dbo.BK_PhieuKhamBenh",
                c => new
                {
                    ID_PhieuKham = c.Int(nullable: false),
                    ID_BenhNhan = c.Int(nullable: false),
                    ID_Benh = c.Int(nullable: false),
                    NgayKham = c.DateTime(storeType: "date"),
                    TrieuChung = c.String(maxLength: 255, unicode: false),
                })
                .PrimaryKey(t => t.ID_PhieuKham);
                
            
            CreateTable(
                "dbo.ThamSo",
                c => new
                    {
                        ID_ThamSo = c.Int(nullable: false, identity: true),
                        GiaTri = c.Int(),
                        GhiChu = c.String(maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.ID_ThamSo);
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 7),
                        Password = c.String(),
                        UserTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserName)
                .ForeignKey("dbo.UserTypes", t => t.UserTypeID, cascadeDelete: true)
                .Index(t => t.UserTypeID);
            
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDetails", "UserTypeID", "dbo.UserTypes");
            DropForeignKey("dbo.BK_PhieuKhamBenh", "ID_BenhNhan", "dbo.HoSoBenhNhan");
            DropForeignKey("dbo.BK_PhieuKhamBenh", "ID_Benh", "dbo.Benh");
            DropForeignKey("dbo.BK_HoaDon", "ID_PhieuKham", "dbo.PhieuKhamBenh");
            DropForeignKey("dbo.BK_CT_PhieuKhamBenh", "ID_Thuoc", "dbo.Thuoc");
            DropForeignKey("dbo.BK_CT_PhieuKhamBenh", "ID_PhieuKham", "dbo.PhieuKhamBenh");
            DropForeignKey("dbo.BK_CT_PhieuKhamBenh", "ID_CachDung", "dbo.CachDung");
            DropForeignKey("dbo.PhieuKhamBenh", "ID_Benh", "dbo.Benh");
            DropForeignKey("dbo.PhieuKhamBenh", "HoSoBenhNhan_ID_BenhNhan", "dbo.HoSoBenhNhan");
            DropForeignKey("dbo.PhieuKhamBenh", "IDPhieuTN", "dbo.PhieuTiepNhan");
            DropForeignKey("dbo.PhieuTiepNhan", "ID_BenhNhan", "dbo.HoSoBenhNhan");
            DropForeignKey("dbo.HoaDon", "ID_PhieuKham", "dbo.PhieuKhamBenh");
            DropForeignKey("dbo.CT_PhieuKhamBenh", "ID_PhieuKham", "dbo.PhieuKhamBenh");
            DropForeignKey("dbo.CT_PhieuKhamBenh", "ID_CachDung", "dbo.CachDung");
            DropForeignKey("dbo.Thuoc", "ID_DonVi", "dbo.DonVi");
            DropForeignKey("dbo.CT_PhieuKhamBenh", "ID_Thuoc", "dbo.Thuoc");
            DropForeignKey("dbo.Thuoc", "ID_CachDung", "dbo.CachDung");
            DropIndex("dbo.UserDetails", new[] { "UserTypeID" });
            DropIndex("dbo.BK_PhieuKhamBenh", new[] { "ID_Benh" });
            DropIndex("dbo.BK_PhieuKhamBenh", new[] { "ID_BenhNhan" });
            DropIndex("dbo.BK_HoaDon", new[] { "ID_PhieuKham" });
            DropIndex("dbo.BK_CT_PhieuKhamBenh", new[] { "ID_CachDung" });
            DropIndex("dbo.BK_CT_PhieuKhamBenh", new[] { "ID_Thuoc" });
            DropIndex("dbo.BK_CT_PhieuKhamBenh", new[] { "ID_PhieuKham" });
            DropIndex("dbo.PhieuTiepNhan", new[] { "ID_BenhNhan" });
            DropIndex("dbo.HoaDon", new[] { "ID_PhieuKham" });
            DropIndex("dbo.Thuoc", new[] { "ID_CachDung" });
            DropIndex("dbo.Thuoc", new[] { "ID_DonVi" });
            DropIndex("dbo.CT_PhieuKhamBenh", new[] { "ID_CachDung" });
            DropIndex("dbo.CT_PhieuKhamBenh", new[] { "ID_Thuoc" });
            DropIndex("dbo.CT_PhieuKhamBenh", new[] { "ID_PhieuKham" });
            DropIndex("dbo.PhieuKhamBenh", new[] { "HoSoBenhNhan_ID_BenhNhan" });
            DropIndex("dbo.PhieuKhamBenh", new[] { "ID_Benh" });
            DropIndex("dbo.PhieuKhamBenh", new[] { "IDPhieuTN" });
            DropTable("dbo.UserTypes");
            DropTable("dbo.UserDetails");
            DropTable("dbo.ThamSo");
            DropTable("dbo.BK_PhieuKhamBenh");
            DropTable("dbo.BK_HoaDon");
            DropTable("dbo.BK_CT_PhieuKhamBenh");
            DropTable("dbo.PhieuTiepNhan");
            DropTable("dbo.HoSoBenhNhan");
            DropTable("dbo.HoaDon");
            DropTable("dbo.DonVi");
            DropTable("dbo.Thuoc");
            DropTable("dbo.CachDung");
            DropTable("dbo.CT_PhieuKhamBenh");
            DropTable("dbo.PhieuKhamBenh");
            DropTable("dbo.Benh");
        }
    }
}
