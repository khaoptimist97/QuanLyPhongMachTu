using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAppMain.Report;
using System.Web.UI.WebControls;
using MvcAppMain.Report.DSBaoCaoDoanhThuTableAdapters;

namespace MvcAppMain.Controllers
{
    public class BaoCaoController : Controller
    {
        // GET: BaoCao
        // GET: BaoCao
        public ActionResult Report()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Report(FormCollection collection)
        {
            try
            {
                DateTime date1 = Convert.ToDateTime(collection["dtp1"].ToString());
                DateTime date2 = Convert.ToDateTime(collection["dtp2"].ToString());
                return RedirectToAction("ReportDoanhSo", new { dt1 = date1, dt2 = date2 });
            }
            catch (FormatException)
            {
                ModelState.AddModelError("ErrorMessage", "Vui lòng nhập đúng định dạng dd/MM/yyyy");
                return View();
            }
            catch (Exception)
            {
                ModelState.AddModelError("ErrorMessage", "Vui lòng nhập đúng định dạng dd/MM/yyyy");
                return View();
            }
        }
        public ActionResult ReportDoanhSo(DateTime dt1, DateTime dt2)
        {
            SetLocalReportDoanhSo(dt1, dt2);

            return View();
        }

        private void SetLocalReportDoanhSo(DateTime dt1, DateTime dt2)
        {
            //Khởi tạo report
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(500);
            reportViewer.Height = Unit.Percentage(500);
            //Đổ dữ liệu vào dataset
            DSBaoCaoDoanhThu doanhThuDataSet = new DSBaoCaoDoanhThu();
            PhieuKhamBenhTableAdapter baoCaoDoanhSoTableAdapter = new PhieuKhamBenhTableAdapter();
            baoCaoDoanhSoTableAdapter.Fill(doanhThuDataSet.PhieuKhamBenh, dt1.ToString(), dt2.ToString());
            //Set datasource
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Report\ReportBaoCaoDoanhThu.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DSBaoCaoDoanhThu", doanhThuDataSet.Tables[0]));
            //
            ViewBag.ReportViewer = reportViewer;
        }
    }
}