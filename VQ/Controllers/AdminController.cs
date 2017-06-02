using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VQ;
using VQ.Models;

namespace VQ.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "451" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "man"))
            {
                return RedirectToAction("Index", "Warning", new { id = "452" });
            }

            return View();
        }
    }
}