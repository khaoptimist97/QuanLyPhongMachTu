using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcAppMain.Models;
using PagedList;
using Newtonsoft.Json;
using System.Collections;
using MvcAppMain.Help;
using MvcAppMain.DTO;
using MvcAppMain.ViewModel;

namespace MvcAppMain.Controllers
{
    [Authorize]
    public class PhieuKhamBenhsController : Controller
    {
        private QLPMContext db = new QLPMContext();

        // GET: PhieuSuaChuas
        public ViewResult Index(string currentFilter, string searchString, int? page)
        {
            
            //Customize dropdownlist for show IDPhieu+TenChuXe
            IQueryable<PhieuTiepNhan> model = from s in db.PhieuTiepNhans
                                              where s.Deleted == false
                                              select s;
            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (var item in model)
            {
                listItems.Add(new SelectListItem
                {
                    Value = item.IDPhieuTN.ToString(),
                    Text = item.HoSoBenhNhan.HoTen + " -  " + item.IDPhieuTN.ToString()
                });
            }
            ViewBag.IDPhieuTN = new SelectList(listItems, "Value", "Text");
            //s
            ViewBag.ID_Thuoc = new SelectList(db.Thuocs, "ID_Thuoc", "TenThuoc");
            ViewBag.ID_Benh = new SelectList(db.Benhs, "ID_Benh", "TenBenh");
            //Search
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var phieuKhamBenh = db.PhieuKhamBenhs.Include(p => p.PhieuTiepNhan).Include(p=>p.Benh);
            if (!String.IsNullOrEmpty(searchString))
            {
                phieuKhamBenh = phieuKhamBenh.Where(s => s.PhieuTiepNhan.HoSoBenhNhan.HoTen.Contains(searchString));
            }
            return View(phieuKhamBenh.Where(x => x.Deleted == false).ToList().ToPagedList(page ?? 1, 10));
        }
        public JsonResult GetSearchValue(string search)
        {
            List<BenhNhan> allsearch = db.PhieuKhamBenhs.Where(s => s.PhieuTiepNhan.HoSoBenhNhan.HoTen.Contains(search)).Select(x => new BenhNhan
            {
                TenBenhNhan = x.PhieuTiepNhan.HoSoBenhNhan.HoTen,
            }).ToList();
            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult GetInfoThuoc(int IDThuoc)
        {
            return Json(db.Thuocs.Where(x => x.ID_Thuoc == IDThuoc).Select(s => new
            {
                TenDV = s.DonVi.TenDonVi,
                Gia = s.DonGia
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetInfoChiTietPhieu(int IDPhieu)
        {
            var phieuKhamBenh = db.PhieuKhamBenhs.Where(x => x.Deleted == false && x.ID_PhieuKham == IDPhieu).SingleOrDefault();
            var chiTiets = db.CT_PhieuKhamBenh.Where(x => x.Deleted == false && x.ID_PhieuKham == IDPhieu).ToList();
            //Tạo List<ChiTietKhamBenh> của ChiTietKhamBenh(IDPhieu)
            List<ChiTietPhieuViewModel> chiTietPhieuViewModels = new List<ChiTietPhieuViewModel>();
            foreach (var ele in chiTiets)
            {
                ChiTietPhieuViewModel models = new ChiTietPhieuViewModel()
                {
                    ID_Thuoc = ele.ID_Thuoc,
                    DonGiaBan = ele.DonGiaBan,
                    SoLuongThuocLay = ele.SoLuongThuocLay,
                    ThanhTien = ele.ThanhTien,
                };
                chiTietPhieuViewModels.Add(models);
            }
            //Tạo PhieuKhamBenh gửi lên view

            PhieuKhamBenhViewModel phieuKham = new PhieuKhamBenhViewModel()
            {
                ID_PhieuKham = phieuKhamBenh.ID_PhieuKham,
                IDPhieuTN = phieuKhamBenh.IDPhieuTN,
                NgayKham = phieuKhamBenh.NgayKham,
                ID_Benh = phieuKhamBenh.ID_Benh,
                TrieuChung = phieuKhamBenh.TrieuChung,
                chiTietPhieuKhamBenh = chiTietPhieuViewModels,
                TenBenhNhan = phieuKhamBenh.PhieuTiepNhan.HoSoBenhNhan.HoTen,
                TenBenh = phieuKhamBenh.Benh.TenBenh
            };

            return Json(phieuKham, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Save(int? IDPhieu, int idPhieuTN, DateTime date, int IDBenh, string TrieuChung, CT_PhieuKhamBenh[] chitietphieukham)
        {
            string result = "Error! Thêm chi tiết không thể hoàn tất!";
            if (IDPhieu == null && idPhieuTN != 0 && date != null && chitietphieukham != null && IDBenh!=0)
            {
                PhieuKhamBenh phieuKham = new PhieuKhamBenh();
                phieuKham.IDPhieuTN = idPhieuTN;
                phieuKham.ID_Benh = IDBenh;
                phieuKham.NgayKham = date;
                phieuKham.TrieuChung = TrieuChung;
                db.PhieuKhamBenhs.Add(phieuKham);
                db.SaveChanges();
                int idPhieuKham = phieuKham.ID_PhieuKham;
                foreach (var ct in chitietphieukham)
                {
                    CT_PhieuKhamBenh C = new CT_PhieuKhamBenh();
                    C.ID_PhieuKham = idPhieuKham;
                    C.ID_Thuoc = ct.ID_Thuoc;
                    C.DonGiaBan = ct.DonGiaBan;
                    C.SoLuongThuocLay = ct.SoLuongThuocLay;
                    C.ThanhTien = ct.ThanhTien;
                    db.CT_PhieuKhamBenh.Add(C);
                }
                db.SaveChanges();
                result = "Thành công! Thêm chi tiết hoàn tất!";
            }
            //else
            //{
            //    try
            //    {
            //        //Trường hợp đã remove 1 hay nhiều ChiTietPhieuSua nào đó ...
            //        ChiTietPhieuSua[] C = db.ChiTietPhieuSuas.Where(x => x.IDPhieu == IDPhieu).ToList().ToArray();

            //        List<int> listC = Helper.GhiIDPhuTungThanhMang(C);
            //        List<int> listChiTiet = Helper.GhiIDPhuTungThanhMang(chitietphieusua);
            //        //Tìm phần tử có trong C mà ko có trong chitietphieusua
            //        var excepts = listC.Except(listChiTiet).ToArray();
            //        if (excepts.Count() != 0)
            //        {
            //            foreach (var e in excepts)
            //            {
            //                ChiTietPhieuSua chiTietPhieuSua = db.ChiTietPhieuSuas.Find(IDPhieu, e);
            //                chiTietPhieuSua.Deleted = true;
            //                //Update lại thành Số lượng tồn, Tổng tiền, Tiền nợ
            //                Helper.UpdateAfterDeleteChiTietPhieu(chiTietPhieuSua);
            //                //Flag
            //                db.Entry(chiTietPhieuSua).State = EntityState.Modified;
            //            }
            //            db.SaveChanges();
            //        }
            //        foreach (ChiTietPhieuSua chiTiet in chitietphieusua)
            //        {
            //            int idPhuTung = chiTiet.IDPhuTung;
            //            //Nếu đã có ChiTietPhieu này thi chỉ thực hiện edit
            //            if (db.ChiTietPhieuSuas.Any(x => x.IDPhieu == IDPhieu && x.IDPhuTung == idPhuTung))
            //            {
            //                ChiTietPhieuSua ct = db.ChiTietPhieuSuas.Find(IDPhieu, idPhuTung);
            //                if (ct.Deleted == false) //Chi Edit vs ChiTiet chua xoa
            //                {
            //                    ct.DonGia = chiTiet.DonGia;
            //                    ct.SoLuongBan = chiTiet.SoLuongBan;
            //                    ct.IDTienCong = chiTiet.IDTienCong;
            //                    ct.ThanhTien = chiTiet.ThanhTien;
            //                    ct.NoiDung = chiTiet.NoiDung;
            //                    db.Entry(ct).State = EntityState.Modified;
            //                }
            //                else //Neu xoa roi (Deleted = true) thi xoa hẳn giá trị đó, va them lai => To fire trigger, update Soluongton, ...
            //                {
            //                    //Xóa
            //                    db.ChiTietPhieuSuas.Remove(ct);
            //                    db.SaveChanges();
            //                    //Gán & Add lại
            //                    ct.DonGia = chiTiet.DonGia;
            //                    ct.SoLuongBan = chiTiet.SoLuongBan;
            //                    ct.IDTienCong = chiTiet.IDTienCong;
            //                    ct.ThanhTien = chiTiet.ThanhTien;
            //                    ct.NoiDung = chiTiet.NoiDung;
            //                    ct.Deleted = false;
            //                    db.ChiTietPhieuSuas.Add(ct);
            //                    db.SaveChanges();
            //                }
            //            }
            //            else //Nếu không có thì thêm mới
            //            {
            //                ChiTietPhieuSua ChiTiet = new ChiTietPhieuSua();
            //                ChiTiet.IDPhieu = (int)IDPhieu;
            //                ChiTiet.IDPhuTung = chiTiet.IDPhuTung;
            //                ChiTiet.DonGia = chiTiet.DonGia;
            //                ChiTiet.SoLuongBan = chiTiet.SoLuongBan;
            //                ChiTiet.IDTienCong = chiTiet.IDTienCong;
            //                ChiTiet.ThanhTien = chiTiet.ThanhTien;
            //                ChiTiet.NoiDung = chiTiet.NoiDung;
            //                db.ChiTietPhieuSuas.Add(ChiTiet);
            //            }
            //            db.SaveChanges();
            //        }
            //        result = "Sửa thành công!!";
            //    }
            //    catch (Exception ex)
            //    {
            //        result = "Sửa không thành công!!";
            //        throw ex;
            //    }
            //}

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: PhieuSuaChuas/Details/5
        //public JsonResult GetDetails(int IDPhieu)
        //{
        //    var chiTiets = db.ChiTietPhieuSuas.Where(x => x.Deleted == false && x.IDPhieu == IDPhieu).ToList();
        //    //Tạo List<ChiTietPhieuSuas> của PhieuSuaChua(IDPhieu)
        //    List<ChiTietPhieuViewModel> chiTietPhieuViewModels = new List<ChiTietPhieuViewModel>();
        //    foreach (var ele in chiTiets)
        //    {
        //        ChiTietPhieuViewModel models = new ChiTietPhieuViewModel()
        //        {
        //            IDPhieu = ele.IDPhieu,
        //            IDPhuTung = ele.IDPhuTung,
        //            SoLuongBan = ele.SoLuongBan,
        //            DonGia = ele.DonGia,
        //            IDTienCong = ele.IDTienCong,
        //            ThanhTien = ele.ThanhTien,
        //            NoiDung = ele.NoiDung
        //        };
        //        chiTietPhieuViewModels.Add(models);
        //    }
        //    return Json(chiTietPhieuViewModels, JsonRequestBehavior.AllowGet);
        //}
        // POST: PhieuSuaChuas/Delete/5
        public JsonResult DeleteConfirmation(int IDPhieu)
        {
            bool result = false;
            PhieuKhamBenh phieuSua = db.PhieuKhamBenhs.Find(IDPhieu);
            if (phieuSua != null)
            {
                phieuSua.Deleted = true;
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
