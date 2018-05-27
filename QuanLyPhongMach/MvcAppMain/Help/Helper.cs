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
        static QLPMContext db = new QLPMContext();
        public static List<int> GhiIDThuocThanhMang(CT_PhieuKhamBenh[] ct)
        {
            List<int> list = new List<int>();
            foreach (CT_PhieuKhamBenh c in ct)
            {
                list.Add(c.ID_Thuoc);
            }
            return list;
        }
        public static void UpdateAfterDeleteChiTietPhieu(CT_PhieuKhamBenh ct)
        {
            QLPMContext db = new QLPMContext();            
            Thuoc thuoc = db.Thuocs.Find(ct.ID_Thuoc);
            //Xu ly
            //Update so luong ton
            thuoc.SoLuong += ct.SoLuongThuocLay;
            //Save
            db.SaveChanges();
        }

    }
}