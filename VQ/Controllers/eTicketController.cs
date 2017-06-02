using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class eTicketController : Controller
    {
        private VQEntities db = new VQEntities();

        // GET: eTicket/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "1900" });
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "2900" });
            }

            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, typeof(System.Web.UI.Page), "redirectJS", "setTimeout(function() { window.location.replace('homepage.aspx') }, 5000);", true);

            return View(ticket);
        }

        // GET: eTicket/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "3900" });
            }
            ServiceType st = db.ServiceTypes.Find(id);
            if (st == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "4900" });
            }

            ViewBag.ServiceTypeId = st.Id.ToString();
            ViewBag.ServiceTypeName = st.Name;
            ViewBag.DepartmentId = st.DepartmentId;
            ViewBag.DepartmentName = st.Department.Name;

            return View();
        }

        // POST: eTicket/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string serviceTypeId, [Bind(Include = "Name,Surname,EmailAddress")] Customer customer)
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


            if (string.IsNullOrEmpty(serviceTypeId) || !ModelState.IsValid)
            {
                return View();
            }

            Customer cust = (Customer)db.Customers.FirstOrDefault(x => x.EmailAddress == customer.EmailAddress.Trim());

            if (cust == null)
            {
                Customer newCustomer = new Customer();
                newCustomer.EmailAddress = customer.EmailAddress.Trim();
                newCustomer.IdentityNumber = customer.EmailAddress.Trim();
                newCustomer.LastTicketId = 0;
                newCustomer.MTC = 0;
                newCustomer.Name = customer.Name.Trim();
                newCustomer.StudentId = customer.EmailAddress.Trim();
                newCustomer.Surname = customer.Surname.Trim();

                db.Customers.Add(newCustomer);
                db.SaveChanges();

                cust = newCustomer;
            }

            int stId = int.Parse(serviceTypeId.Trim());
            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);

            int dailyOpenTicketLimit = int.Parse(Properties.Settings.Default["DailyOpenTicketLimit"].ToString());
            if (cust.Id > 0)
            {
                var customerOpenTickets = db.Tickets.Where(x => x.ServiceTypeId == stId && x.CustomerId == cust.Id && x.TicketTime >= today && x.TicketTime < tomorrow && x.IsCalled == false).ToList();
                if (customerOpenTickets.Count >= dailyOpenTicketLimit)
                {
                    return RedirectToAction("Index", "Warning", new { id = "901" });
                }
            }

            Random random = new Random();
            //int randomNumber = random.Next(0, 1000);
            Ticket t = new Ticket();

            try
            {
                t.CustomerId = cust.Id;
                t.IsCalled = false;
                t.ServiceTypeId = int.Parse(serviceTypeId);
                t.TicketTime = DateTime.Now;
                t.TicketNumber = GetNextTicketNumber(int.Parse(serviceTypeId));
                t.TicketPin = random.Next(0, 1000);
                t.CallTime = DateTime.Now;
                t.ForwardedBy = 0;
                t.Priority = 99;

                db.Tickets.Add(t);
                db.SaveChanges();

                cust.LastTicketId = t.Id;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

            return RedirectToAction("Details", "eTicket", new { id = t.Id });
        }

        public int GetNextTicketNumber(int serviceTypeId)
        {
            int int4Return = 0;

            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);
            Counter newCounter = new Counter();

            ServiceType st = (ServiceType)db.ServiceTypes.Find(serviceTypeId);

            if (st == null)
            {

            }

            Counter currentCounter = (Counter)db.Counters.FirstOrDefault(x => x.DepartmentId == st.DepartmentId && x.ServiceDate >= today && x.ServiceDate < tomorrow);

            try
            {
                if (currentCounter == null)
                {
                    int4Return = 1;
                    newCounter.CurrentCounter = 1;
                    newCounter.ServiceDate = DateTime.Now;
                    newCounter.DepartmentId = st.DepartmentId;
                    db.Counters.Add(newCounter);
                }
                else
                {
                    currentCounter.ServiceDate = DateTime.Now;
                    currentCounter.CurrentCounter = currentCounter.CurrentCounter + 1;
                    int4Return = currentCounter.CurrentCounter;
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

            return int4Return;
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
