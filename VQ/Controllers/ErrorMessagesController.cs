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
    public class ErrorMessagesController : Controller
    {
        private VQEntities db = new VQEntities();

        // GET: ErrorMessages
        public ActionResult Index()
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "661" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "662" });
            }

            return View(db.ErrorMessages.ToList());
        }

        // GET: ErrorMessages/Details/5
        public ActionResult Details(int? id)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "663" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "664" });
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ErrorMessage errorMessage = db.ErrorMessages.Find(id);
            if (errorMessage == null)
            {
                return HttpNotFound();
            }
            return View(errorMessage);
        }

        // GET: ErrorMessages/Create
        public ActionResult Create()
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "665" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "666" });
            }

            return View();
        }

        // POST: ErrorMessages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MsgId,MessageEN,MessagePL,BackController,BackAction")] ErrorMessage errorMessage)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "667" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "668" });
            }

            if (ModelState.IsValid)
            {
                db.ErrorMessages.Add(errorMessage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(errorMessage);
        }

        // GET: ErrorMessages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "669" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "670" });
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ErrorMessage errorMessage = db.ErrorMessages.Find(id);
            if (errorMessage == null)
            {
                return HttpNotFound();
            }
            return View(errorMessage);
        }

        // POST: ErrorMessages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MsgId,MessageEN,MessagePL,BackController,BackAction")] ErrorMessage errorMessage)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "671" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "672" });
            }

            if (ModelState.IsValid)
            {
                db.Entry(errorMessage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(errorMessage);
        }

        // GET: ErrorMessages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "673" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "674" });
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ErrorMessage errorMessage = db.ErrorMessages.Find(id);
            if (errorMessage == null)
            {
                return HttpNotFound();
            }
            return View(errorMessage);
        }

        // POST: ErrorMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "675" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "676" });
            }

            ErrorMessage errorMessage = db.ErrorMessages.Find(id);
            db.ErrorMessages.Remove(errorMessage);
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
