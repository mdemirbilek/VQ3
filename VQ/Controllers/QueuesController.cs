using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VQ;
using VQ.Models;

namespace VQ.Controllers
{
    public class QueuesController : Controller
    {
        private VQEntities db = new VQEntities();

        // GET: Queues
        public ActionResult Index()
        {
            return View();
        }

        public void SetBell(int tId)
        {
            TicketsInService tiss = db.TicketsInServices.Find(tId);
            if (tiss != null)
            {
                tiss.PlaySound = false;
                db.SaveChangesAsync();
            }
        }

        public ActionResult DeptQueues(string id)
        {
            string deptId = "1";
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Warning", new { id = "101" });
            }
            deptId = id;
            ViewBag.DeptId = deptId;

            Department dept = db.Departments.Find(int.Parse(id));
            if (dept == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "102" });
            }
            ViewBag.DeptName = dept.Name;

            return View();
        }

        public ActionResult GetDeptQueues(int id)
        {
            QueuesRepository _queueeRepository = new QueuesRepository();
            return PartialView("_DeptQueue", _queueeRepository.GetDeptQueues(id));
        }


        public ActionResult DeskQueues(string id)
        {
            string deskId = "1";
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Warning", new { id = "103" });
            }
            deskId = id;
            ViewBag.DeskId = deskId;

            Desk desk = db.Desks.Find(int.Parse(id));
            if (desk == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "104" });
            }
            ViewBag.DeskName = desk.Name;

            Department dept = db.Departments.Find(int.Parse(desk.DepartmentId.ToString()));
            if (dept == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "105" });
            }
            ViewBag.DeptName = dept.Name;

            return View();
        }

        public ActionResult GetDeskQueues(int id)
        {
            QueuesRepository _queueeRepository = new QueuesRepository();
            return PartialView("_DeskQueue", _queueeRepository.GetDeskQueues(id));
        }

    }
}