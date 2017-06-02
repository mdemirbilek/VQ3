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
    public class GetTicketController : Controller
    {
        private VQEntities db = new VQEntities();

        // GET: GetTicket
        public ActionResult Index()
        {
            // Sistemin ticket verme saatleri..
            DateTime now = DateTime.Now;
            DateTime tsh = DateTime.Today;
            DateTime teh = DateTime.Today;
            SysConfig tshCfg = MyFunctions.GetConfigItem("TSH"); //Ticketing Start Hour
            SysConfig tehCfg = MyFunctions.GetConfigItem("TEH"); //Ticketing End Hour

            if (tshCfg != null && tehCfg != null)
            {
                tsh = tsh.AddHours(tshCfg.IntValue);
                teh = teh.AddHours(tehCfg.IntValue);
            }
            else
            {
                teh = teh.AddDays(1);
            }

            if (now < tsh || now > teh)
            {
                return RedirectToAction("Index", "Warning", new { id = "1901" });
            }

            return View(db.ServiceTypes.Where(x => x.IsActive == true).ToList());
        }

        // GET: GetTicket
        public ActionResult Inside()
        {
            return View(db.ServiceTypes.Where(x => x.IsActive == true).ToList());
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
