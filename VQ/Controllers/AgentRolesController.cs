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
    public class AgentRolesController : Controller
    {
        private VQEntities db = new VQEntities();

        // GET: AgentRoles
        public ActionResult Index()
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "481" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "482" });
            }

            var agentRoles = db.AgentRoles.Include(a => a.Agent).Include(a => a.Role);
            return View(agentRoles.ToList());
        }

        // GET: AgentRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "483" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "484" });
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgentRole agentRole = db.AgentRoles.Find(id);
            if (agentRole == null)
            {
                return HttpNotFound();
            }
            return View(agentRole);
        }

        // GET: AgentRoles/Create
        public ActionResult Create()
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "485" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "486" });
            }

            ViewBag.AgentId = new SelectList(db.Agents, "Id", "Name");
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name");
            return View();
        }

        // POST: AgentRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AgentId,RoleId,IsActive")] AgentRole agentRole)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "487" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "488" });
            }

            if (ModelState.IsValid)
            {
                db.AgentRoles.Add(agentRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgentId = new SelectList(db.Agents, "Id", "Name", agentRole.AgentId);
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name", agentRole.RoleId);
            return View(agentRole);
        }

        // GET: AgentRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "489" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "490" });
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgentRole agentRole = db.AgentRoles.Find(id);
            if (agentRole == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgentId = new SelectList(db.Agents, "Id", "Name", agentRole.AgentId);
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name", agentRole.RoleId);
            return View(agentRole);
        }

        // POST: AgentRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AgentId,RoleId,IsActive")] AgentRole agentRole)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "491" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "492" });
            }

            if (ModelState.IsValid)
            {
                db.Entry(agentRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgentId = new SelectList(db.Agents, "Id", "Name", agentRole.AgentId);
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name", agentRole.RoleId);
            return View(agentRole);
        }

        // GET: AgentRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "493" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "494" });
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgentRole agentRole = db.AgentRoles.Find(id);
            if (agentRole == null)
            {
                return HttpNotFound();
            }
            return View(agentRole);
        }

        // POST: AgentRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "495" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "496" });
            }

            AgentRole agentRole = db.AgentRoles.Find(id);
            db.AgentRoles.Remove(agentRole);
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
