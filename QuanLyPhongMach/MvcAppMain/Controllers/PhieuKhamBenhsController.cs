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
                
                int tienThuoc = 0;
                phieuKham.IDPhieuTN = idPhieuTN;
                phieuKham.ID_Benh = IDBenh;
                phieuKham.NgayKham = date;
                phieuKham.TrieuChung = TrieuChung;
                db.PhieuKhamBenhs.Add(phieuKham);
                db.SaveChanges();
                int idPhieuKham = phieuKham.ID_PhieuKham;
                HoaDon hoaDon = new HoaDon();
                hoaDon.ID_PhieuKham = idPhieuKham;
                int tienKham = Convert.ToInt32(db.ThamSoes.Where(x => x.GhiChu.Contains("Tien kham")).SingleOrDefault().GiaTri); ;
                hoaDon.TienKham = tienKham;
                foreach (var ct in chitietphieukham)
                {
                    CT_PhieuKhamBenh C = new CT_PhieuKhamBenh();
                    C.ID_PhieuKham = idPhieuKham;
                    C.ID_Thuoc = ct.ID_Thuoc;
                    C.DonGiaBan = ct.DonGiaBan;
                    C.SoLuongThuocLay = ct.SoLuongThuocLay;
                    C.ThanhTien = ct.ThanhTien;
                    db.CT_PhieuKhamBenh.Add(C);
                    tienThuoc += ct.ThanhTien;
                }
                hoaDon.TienThuoc = tienThuoc;
                hoaDon.TongTien = tienKham + tienThuoc;
                db.HoaDons.Add(hoaDon);
                db.SaveChanges();
                Session["ID_PhieuKham"] = idPhieuKham;
                result = "Thành công! Thêm chi tiết hoàn tất!";
            }
            else
            {
                try
                {
                    int tienThuoc = 0;
                    //Trường hợp đã remove 1 hay nhiều ChiTietKhamBenh nào đó ...
                    CT_PhieuKhamBenh[] C = db.CT_PhieuKhamBenh.Where(x => x.ID_PhieuKham == IDPhieu && x.Deleted == false).ToList().ToArray();

                    List<int> listC = Helper.GhiIDThuocThanhMang(C);
                    List<int> listChiTiet = Helper.GhiIDThuocThanhMang(chitietphieukham);
                    //Tìm phần tử có trong C mà ko có trong chitietphieusua
                    var excepts = listC.Except(listChiTiet).ToArray();
                    if (excepts.Count() != 0)
                    {
                        foreach (var e in excepts)
                        {
                            CT_PhieuKhamBenh chiTietPhieuKham = db.CT_PhieuKhamBenh.Find(IDPhieu, e);
                            chiTietPhieuKham.Deleted = true;
                            //Update lại thành Số lượng tồn
                            Helper.UpdateAfterDeleteChiTietPhieu(chiTietPhieuKham);
                            //Flag
                            db.Entry(chiTietPhieuKham).State = EntityState.Modified;
                        }
                      
                        db.SaveChanges();
                    }
                    foreach (CT_PhieuKhamBenh chiTiet in chitietphieukham)
                    {
                        int idThuoc = chiTiet.ID_Thuoc;
                        
                        //Nếu đã có ChiTietPhieu này thi chỉ thực hiện edit
                        if (db.CT_PhieuKhamBenh.Any(x => x.ID_PhieuKham == IDPhieu && x.ID_Thuoc == idThuoc))
                        {
                            CT_PhieuKhamBenh ct = db.CT_PhieuKhamBenh.Find(IDPhieu, idThuoc);
                            if (ct.Deleted == false) //Chi Edit vs ChiTiet chua xoa
                            {
                                ct.DonGiaBan = chiTiet.DonGiaBan;
                                ct.SoLuongThuocLay = chiTiet.SoLuongThuocLay;
                                ct.ThanhTien = chiTiet.ThanhTien;
                                db.Entry(ct).State = EntityState.Modified;
                            }
                            else //Neu xoa roi (Deleted = true) thi xoa hẳn giá trị đó, va them lai => To fire trigger, update Soluongton, ...
                            {
                                //Xóa
                                db.CT_PhieuKhamBenh.Remove(ct);
                                db.SaveChanges();
                                //Gán & Add lại
                                ct.DonGiaBan = chiTiet.DonGiaBan;
                                ct.SoLuongThuocLay = chiTiet.SoLuongThuocLay;
                                ct.ThanhTien = chiTiet.ThanhTien;
                                ct.Deleted = false;
                                db.CT_PhieuKhamBenh.Add(ct);
                                db.SaveChanges();
                            }
                        }
                        else //Nếu không có thì thêm mới
                        {
                            CT_PhieuKhamBenh ChiTiet = new CT_PhieuKhamBenh();
                            ChiTiet.ID_PhieuKham = (int)IDPhieu;
                            ChiTiet.ID_Thuoc = chiTiet.ID_Thuoc;
                            ChiTiet.DonGiaBan = chiTiet.DonGiaBan;
                            ChiTiet.SoLuongThuocLay = chiTiet.SoLuongThuocLay;
                            ChiTiet.ThanhTien = chiTiet.ThanhTien;
                            db.CT_PhieuKhamBenh.Add(ChiTiet);
                        }
                        db.SaveChanges();
                    }
                    HoaDon phieuThuTien = db.HoaDons.Where(x=>x.ID_PhieuKham == IDPhieu).SingleOrDefault();
                    var chiTietKham = db.CT_PhieuKhamBenh.Where(x=>x.ID_PhieuKham == IDPhieu && x.Deleted == false);
                    foreach(var ele in chiTietKham)
                    {
                        tienThuoc += ele.ThanhTien;
                    }
                    phieuThuTien.TienThuoc = tienThuoc;
                    phieuThuTien.TongTien = tienThuoc + phieuThuTien.TienKham;
                    db.SaveChanges();
                    Session["ID_PhieuKham"] = IDPhieu;
                    result = "Sửa thành công!!";
                }
                catch (Exception ex)
                {
                    ViewBag.Errors = ex.Message;
                    throw ex;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: PhieuSuaChuas/Details/5
        public JsonResult GetDetails(int IDPhieu)
        {
            var chiTiets = db.CT_PhieuKhamBenh.Where(x => x.Deleted == false && x.ID_PhieuKham == IDPhieu).ToList();
            //Tạo List<ChiTietPhieuKham> của PhieuKham(IDPhieu)
            List<ChiTietPhieuViewModel> chiTietPhieuViewModels = new List<ChiTietPhieuViewModel>();
            foreach (var ele in chiTiets)
            {
                ChiTietPhieuViewModel models = new ChiTietPhieuViewModel()
                {
                    ID_Thuoc = ele.ID_Thuoc,
                    SoLuongThuocLay = ele.SoLuongThuocLay,
                    DonGiaBan = ele.DonGiaBan,
                    ThanhTien = ele.ThanhTien
                };
                chiTietPhieuViewModels.Add(models);
            }
            return Json(chiTietPhieuViewModels, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDetailPhieuThu(int IDPhieu)
        {
            var phieuThus = db.HoaDons.Where(x => x.Deleted == false && x.ID_PhieuKham == IDPhieu).ToList();
            //Tạo List<ChiTietPhieuSuas> của PhieuSuaChua(IDPhieu)
            List<PhieuThuTienViewModel> phieuThuTienViewModels = new List<PhieuThuTienViewModel>();
            foreach (var ele in phieuThus)
            {
                PhieuThuTienViewModel models = new PhieuThuTienViewModel()
                {
                    TenBenhNhan = ele.PhieuKhamBenh.PhieuTiepNhan.HoSoBenhNhan.HoTen,
                    TienKham = ele.TienKham,
                    TienThuoc = ele.TienThuoc,
                    TongTien = ele.TongTien
                };
                phieuThuTienViewModels.Add(models);
            }
            return Json(phieuThuTienViewModels, JsonRequestBehavior.AllowGet);
        }
        // POST: PhieuSuaChuas/Delete/5
        public JsonResult DeleteConfirmation(int IDPhieu)
        {
            bool result = false;
            PhieuKhamBenh phieuKham = db.PhieuKhamBenhs.Find(IDPhieu);
            var chiTiet = db.CT_PhieuKhamBenh.Where(x => x.ID_PhieuKham == IDPhieu);
            HoaDon hoaDon = db.HoaDons.Where(x => x.ID_PhieuKham == IDPhieu).Single();
            if (phieuKham != null)
            {                
                db.HoaDons.Remove(hoaDon);
                foreach(var ele in chiTiet)
                {
                    db.CT_PhieuKhamBenh.Remove(ele);
                }
                db.PhieuKhamBenhs.Remove(phieuKham);
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
