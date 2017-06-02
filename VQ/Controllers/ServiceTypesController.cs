using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VQ;
using VQ.Models;

namespace VQ.Controllers
{
    public class ServiceTypesController : Controller
    {
        private VQEntities db = new VQEntities();

        // GET: ServiceTypes
        public ActionResult Index()
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "761" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "762" });
            }

            var serviceTypes = db.ServiceTypes.Include(s => s.Department);
            return View(serviceTypes.ToList());
        }

        // GET: ServiceTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "763" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "764" });
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceType serviceType = db.ServiceTypes.Find(id);
            if (serviceType == null)
            {
                return HttpNotFound();
            }
            return View(serviceType);
        }

        // GET: ServiceTypes/Create
        public ActionResult Create()
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "765" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "766" });
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name");
            return View();
        }

        // POST: ServiceTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DepartmentId,Name,Description,IsActive")] ServiceType serviceType)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "767" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "768" });
            }

            if (ModelState.IsValid)
            {
                db.ServiceTypes.Add(serviceType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", serviceType.DepartmentId);
            return View(serviceType);
        }

        // GET: ServiceTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "769" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "770" });
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceType serviceType = db.ServiceTypes.Find(id);
            if (serviceType == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", serviceType.DepartmentId);
            return View(serviceType);
        }

        // POST: ServiceTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DepartmentId,Name,Description,IsActive")] ServiceType serviceType)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "771" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "772" });
            }

            if (ModelState.IsValid)
            {
                db.Entry(serviceType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", serviceType.DepartmentId);
            return View(serviceType);
        }

        // GET: ServiceTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "773" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "774" });
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceType serviceType = db.ServiceTypes.Find(id);
            if (serviceType == null)
            {
                return HttpNotFound();
            }
            return View(serviceType);
        }

        // POST: ServiceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "775" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "776" });
            }

            ServiceType serviceType = db.ServiceTypes.Find(id);
            db.ServiceTypes.Remove(serviceType);
            db.SaveChanges();
            return RedirectToAction("Index");
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
