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
    public class WaitingListController : Controller
    {
        private VQEntities db = new VQEntities();

        // GET: WaitingList
        public ActionResult Index()
        {
            return View(db.ServiceTypes.Where(x => x.IsActive == true).OrderBy(x => x.Department.DeptCode).ThenBy(x => x.Name).ToList());
        }

        // GET: WaitingList
        public ActionResult List(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Warning", new { id = "1800" });
            }

            int stId = 0;
            bool b = int.TryParse(id, out stId);

            if (stId == 0)
            {
                return RedirectToAction("Index", "Warning", new { id = "2800" });
            }

            ServiceType st = db.ServiceTypes.Find(stId);
            if (st == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "3800" });
            }

            ViewBag.ServiceTypeName = st.Name;

            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);


            var tickets = db.Tickets.Include(t => t.Customer).Include(t => t.ServiceType);

            List<Ticket> tk = tickets.Where(x => x.ServiceTypeId == stId && x.TicketTime >= today && x.TicketTime < tomorrow && x.IsCalled == false).OrderBy(x => x.TicketNumber).ToList();
            List<Ticket> tkt = new List<Ticket>();

            foreach (Ticket t in tk)
            {
                t.Customer.Name = MakeStar("txt", t.Customer.Name);
                t.Customer.Surname = MakeStar("txt", t.Customer.Surname);
                t.Customer.EmailAddress = MakeStar("email", t.Customer.EmailAddress);
                tkt.Add(t);
            }

            return View(tkt.OrderBy(x => x.TicketNumber));
        }

        public string MakeStar(string type, string s)
        {
            string str4Return = "";

            if (type == "txt")
            {
                str4Return = s.Substring(0, 1);
                for (int i = 1; i < s.Length; i++)
                {
                    str4Return += "*";
                }
            }
            else if (type == "email")
            {
                bool b = false;
                for (int i = 0; i < s.Length; i++)
                {
                    if (s.Substring(i, 1) == "@")
                    {
                        b = true;
                    }

                    if (i == 0)
                    {
                        str4Return = s.Substring(i, 1);
                    }
                    else
                    {
                        if (b == false)
                        {
                            str4Return += "*";
                        }
                        else
                        {
                            str4Return += s.Substring(i, 1);
                        }
                    }
                }
            }

            return str4Return;
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
