using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcAppMain.Models;
namespace MvcAppMain.Help
{
    public static class Helper
    {
        //static QuanLyGaraOtoContext db = new QuanLyGaraOtoContext();
        //public static List<int> GhiIDPhuTungThanhMang(ChiTietPhieuSua[] ct)
        //{
        //    List<int> list = new List<int>();
        //    foreach(ChiTietPhieuSua c in ct)
        //    {
        //        list.Add(c.IDPhuTung);
        //    }
        //    return list;
        //}
        //public static void UpdateAfterDeleteChiTietPhieu(ChiTietPhieuSua ct)
        //{
        //    QuanLyGaraOtoContext db = new QuanLyGaraOtoContext();
        //    PhieuSuaChua phieuSua = db.PhieuSuaChuas.Find(ct.IDPhieu);
        //    PhuTung phuTung = db.PhuTungs.Find(ct.IDPhuTung);
        //    Xe xe = db.Xes.Find(phieuSua.PhieuTiepNhan.Xe.IDBienSo);
        //    //Xu ly
        //    //Update so luong ton
        //    phuTung.SoLuong += (int)ct.SoLuongBan;
        //    //Update tong tien
        //    int tongTien = (int)phieuSua.TongTien;
        //    tongTien -= (int) ct.ThanhTien;
        //    if(tongTien<0)
        //    {
        //        phieuSua.TongTien = 0;
        //    }
        //    else
        //    {
        //        phieuSua.TongTien = tongTien;
        //    }
        //    //Update tien no
        //    int tienNo = xe.TienNo;
        //    tienNo -= (int)ct.ThanhTien;
        //    if (tienNo < 0)
        //    {
        //        xe.TienNo = 0;
        //    }
        //    else
        //    {
        //        xe.TienNo = tienNo;
        //    }
        //    //Save
        //    db.SaveChanges();
        //}
        //public static void UpdateAfterPhieuThuTienUpdating(PhieuThuTien phieuThu, int soTienThuCu)
        //{
        //    Xe xe = db.Xes.Find(phieuThu.IDBienSo);
        //    xe.TienNo += soTienThuCu;
        //    xe.TienNo -= phieuThu.SoTienThu;
        //    db.SaveChanges();
        //}
    }
}