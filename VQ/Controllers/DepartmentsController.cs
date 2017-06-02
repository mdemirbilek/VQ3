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
    public class DepartmentsController : Controller
    {
        private VQEntities db = new VQEntities();

        // GET: Departments
        public ActionResult Index()
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "561" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "562" });
            }

            return View(db.Departments.ToList());
        }

        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "563" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "564" });
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "565" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "566" });
            }

            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DeptCode")] Department department)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "567" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "568" });
            }

            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "569" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "570" });
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DeptCode")] Department department)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "571" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "572" });
            }

            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "573" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "574" });
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "575" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "576" });
            }

            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
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
