using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VQ;
using VQ.Models;

namespace VQ.Controllers
{
    public class AgentController : Controller
    {
        // GET: Agent
        public ActionResult Index()
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "1019" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "agent"))
            {
                return RedirectToAction("Index", "Warning", new { id = "1020" });
            }

            return View();
        }
    }
}