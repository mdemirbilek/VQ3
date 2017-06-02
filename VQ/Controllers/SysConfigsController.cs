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
    public class SysConfigsController : Controller
    {
        private VQEntities db = new VQEntities();

        // GET: SysConfigs
        public ActionResult Index()
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "1001" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "1002" });
            }

            return View(db.SysConfigs.ToList());
        }

        // GET: SysConfigs/Details/5
        public ActionResult Details(int? id)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "1003" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "1004" });
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysConfig sysConfig = db.SysConfigs.Find(id);
            if (sysConfig == null)
            {
                return HttpNotFound();
            }
            return View(sysConfig);
        }

        // GET: SysConfigs/Create
        public ActionResult Create()
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "1005" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "1006" });
            }

            return View();
        }

        // POST: SysConfigs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,SysCode,IntValue,StrValue,IsActive")] SysConfig sysConfig)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "1007" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "1008" });
            }

            if (ModelState.IsValid)
            {
                db.SysConfigs.Add(sysConfig);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sysConfig);
        }

        // GET: SysConfigs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "1009" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "1010" });
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysConfig sysConfig = db.SysConfigs.Find(id);
            if (sysConfig == null)
            {
                return HttpNotFound();
            }
            return View(sysConfig);
        }

        // POST: SysConfigs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,SysCode,IntValue,StrValue,IsActive")] SysConfig sysConfig)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "1011" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "1012" });
            }

            if (ModelState.IsValid)
            {
                db.Entry(sysConfig).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sysConfig);
        }

        // GET: SysConfigs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "1013" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "1014" });
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysConfig sysConfig = db.SysConfigs.Find(id);
            if (sysConfig == null)
            {
                return HttpNotFound();
            }
            return View(sysConfig);
        }

        // POST: SysConfigs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "1015" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "1016" });
            }

            SysConfig sysConfig = db.SysConfigs.Find(id);
            db.SysConfigs.Remove(sysConfig);
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
