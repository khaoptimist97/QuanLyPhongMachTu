using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcAppMain.DTO;
using MvcAppMain.Models;
using PagedList;

namespace MvcAppMain.Controllers
{
    [Authorize]
    public class HoSoBenhNhansController : Controller
    {
        private QLPMContext db = new QLPMContext();

        // GET: HoSoBenhNhans
        public ViewResult Index(string option, string currentFilter, string searchString, int? page)
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
            var hoSos = from s in db.HoSoBenhNhans
                      where s.Deleted == false
                      select s;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            if (!String.IsNullOrEmpty(searchString))
            {
                return View(hoSos.Where(s => s.HoTen.Contains(searchString)).OrderBy(s => s.HoTen).ToPagedList(pageNumber, pageSize));
            }
            return View(hoSos.OrderBy(s => s.HoTen).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public JsonResult GetSearchValue(string search)
        {
            List<BenhNhan> allsearch = db.HoSoBenhNhans.Where(s => s.HoTen.Contains(search) && s.Deleted == false).Select(x => new BenhNhan
            {
                TenBenhNhan = x.HoTen
            }).ToList();
            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // GET: HoSoBenhNhans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoSoBenhNhan hoSoBenhNhan = db.HoSoBenhNhans.Find(id);
            if (hoSoBenhNhan == null)
            {
                return HttpNotFound();
            }
            return View(hoSoBenhNhan);
        }

        // GET: HoSoBenhNhans/Create
        public ActionResult Create()
        {
            List<SelectListItem> Gender = new List<SelectListItem>()
            {
               new SelectListItem
               {
                   Value = "true",
                   Text = "Nam"
               },
               new SelectListItem
               {
                   Value = "false",
                   Text = "Nữ"
               }
            };
            ViewBag.GioiTinh = new SelectList(Gender, "Value", "Text");
            return View();
        }

        // POST: HoSoBenhNhans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_BenhNhan,HoTen,GioiTinh,NamSinh,DiaChi,Deleted")] HoSoBenhNhan hoSoBenhNhan)
        {
            if (ModelState.IsValid)
            {             
                db.HoSoBenhNhans.Add(hoSoBenhNhan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<SelectListItem> Gender = new List<SelectListItem>()
            {
               new SelectListItem
               {
                   Value = "true",
                   Text = "Nam"
               },
               new SelectListItem
               {
                   Value = "false",
                   Text = "Nữ"
               }
            };
            ViewBag.GioiTinh = new SelectList(Gender, "Value", "Text");
            return View(hoSoBenhNhan);
        }

        // GET: HoSoBenhNhans/Edit/5
        public ActionResult Edit(int? id)
        {
            List<SelectListItem> Gender = new List<SelectListItem>()
            {
               new SelectListItem
               {
                   Value = "true",
                   Text = "Nam"
               },
               new SelectListItem
               {
                   Value = "false",
                   Text = "Nữ"
               }
            };
            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoSoBenhNhan hoSoBenhNhan = db.HoSoBenhNhans.Find(id);
            if (hoSoBenhNhan == null)
            {
                return HttpNotFound();
            }
            ViewBag.GioiTinh = new SelectList(Gender, "Value", "Text");
            return View(hoSoBenhNhan);
        }

        // POST: HoSoBenhNhans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_BenhNhan,HoTen,GioiTinh,NamSinh,DiaChi,Deleted")] HoSoBenhNhan hoSoBenhNhan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoSoBenhNhan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<SelectListItem> Gender = new List<SelectListItem>()
            {
               new SelectListItem
               {
                   Value = "true",
                   Text = "Nam"
               },
               new SelectListItem
               {
                   Value = "false",
                   Text = "Nữ"
               }
            };
            ViewBag.GioiTinh = new SelectList(Gender, "Value", "Text");
            return View(hoSoBenhNhan);
        }

        // GET: HoSoBenhNhans/Delete/5
        public JsonResult DeleteConfirmation(int BenhNhanID)
        {
            bool result = false;
            HoSoBenhNhan hoSo = db.HoSoBenhNhans.Find(BenhNhanID);
            if (hoSo != null)
            {
                hoSo.Deleted = true;
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
