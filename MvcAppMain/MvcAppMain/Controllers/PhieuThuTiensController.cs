using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using MvcAppMain.DTO;
using MvcAppMain.Models;
using MvcAppMain.Report;
using MvcAppMain.Report.XuatPhieuTableAdapters;
using PagedList;

namespace MvcAppMain.Controllers
{
    public class PhieuThuTiensController : Controller
    {
        private QLPMContext db = new QLPMContext();

        // GET: HoaDons
        public ViewResult Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var phieuThuTiens = from s in db.HoaDons
                                 join a in db.PhieuKhamBenhs on s.ID_PhieuKham equals a.ID_PhieuKham
                                 where s.Deleted == false
                                 select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                phieuThuTiens = phieuThuTiens.Where(s => s.PhieuKhamBenh.PhieuTiepNhan.HoSoBenhNhan.HoTen.Contains(searchString));
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(phieuThuTiens.OrderBy(s => s.PhieuKhamBenh.PhieuTiepNhan.HoSoBenhNhan.HoTen).ToPagedList(pageNumber, pageSize));
        }
        public JsonResult GetSearchValue(string search)
        {
            List<BenhNhan> allsearch = db.HoaDons.Where(s => s.PhieuKhamBenh.PhieuTiepNhan.HoSoBenhNhan.HoTen.Contains(search) && s.Deleted == false).Select(x => new BenhNhan
            {
                TenBenhNhan = x.PhieuKhamBenh.PhieuTiepNhan.HoSoBenhNhan.HoTen
            }).ToList();
            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult XuatPhieuThuTien(int IDPhieu)
        {
            //Khởi tạo report
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(500);
            reportViewer.Height = Unit.Percentage(500);
            //Đổ dữ liệu vào dataset
            XuatPhieu xuatPhieuDataSet = new XuatPhieu();
            CT_PhieuKhamBenhTableAdapter baoCaoDoanhSoTableAdapter = new CT_PhieuKhamBenhTableAdapter();
            baoCaoDoanhSoTableAdapter.Fill(xuatPhieuDataSet.CT_PhieuKhamBenh,IDPhieu);
            //Set datasource
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Report\XuatPhieu.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("XuatPhieu", xuatPhieuDataSet.Tables[0]));
            //
            ViewBag.ReportViewer = reportViewer;
            return View();

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
