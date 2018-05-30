using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcAppMain.Filters;
using MvcAppMain.Models;

namespace MvcAppMain.Controllers
{
    [Authorize]
    [AdminFilter]
    public class BK_PhieuKhamBenhController : Controller
    {
        private QLPMContext db = new QLPMContext();

        // GET: BK_PhieuKhamBenh
        public ActionResult Index()
        {
            return View(db.BK_PhieuKhamBenh.ToList());
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
