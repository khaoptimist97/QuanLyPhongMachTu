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
    public class BK_HoaDonController : Controller
    {
        private QLPMContext db = new QLPMContext();

        // GET: BK_HoaDon
        public ActionResult Index()
        {
            return View(db.BK_HoaDon.ToList());
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
