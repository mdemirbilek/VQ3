using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VQ;

namespace VQ.Controllers
{
    public class CountersController : Controller
    {
        private VQEntities db = new VQEntities();

        // GET: Counters
        public ActionResult Index()
        {
            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);

            //return View(db.vwTIP2.Where(x => x.TicketDay >= today && x.TicketDay < tomorrow).ToList());

            List<CounterStatus> cList = new List<CounterStatus>();
            List<ServiceType> stList = db.ServiceTypes.Where(x => x.IsActive == true).ToList();
            List<Ticket> tList = db.Tickets.Where(x => x.TicketTime >= today && x.TicketTime < tomorrow).ToList();
            List<vwTNrCalled> tnTiS = db.vwTNrCalleds.Where(x => (DateTime)x.TicketDay >= today && (DateTime)x.TicketDay < tomorrow).ToList();
            List<vwTNrNotCalled> tnNext = db.vwTNrNotCalleds.Where(x => (DateTime)x.TicketDay >= today && (DateTime)x.TicketDay < tomorrow).ToList();

            int i = 1;

            foreach (var item in stList)
            {
                CounterStatus cs = new CounterStatus();
                cs.id = i;
                cs.DeptName = item.Department.Name;
                cs.DeptCode = item.Department.DeptCode;
                cs.ServiceTypeName = item.Name;

                var called = tnTiS.FirstOrDefault(x => x.ServiceTypeId == item.Id);
                var next = tnNext.FirstOrDefault(x => x.ServiceTypeId == item.Id);
                if (next != null)
                {
                    cs.NextTicket = (int)next.MaxTN + 1;
                    if (called != null)
                    {
                        cs.TiS = (int)called.MaxTN;
                    }
                    else
                    {
                        cs.TiS = 0;
                    }
                }
                else
                {
                    if (called != null)
                    {
                        cs.NextTicket = (int)called.MaxTN + 1;
                        cs.TiS = (int)called.MaxTN;
                    }
                    else
                    {
                        cs.NextTicket = 1;
                        cs.TiS = 0;
                    }
                }
                cList.Add(cs);

                i += 1;
            }

            return View(cList.OrderBy(x =>x.DeptCode).ThenBy(x => x.ServiceTypeName));
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


    public class CounterStatus
    {
        public int id { get; set; }
        public string DeptName { get; set; }
        public string DeptCode { get; set; }
        public string ServiceTypeName { get; set; }
        public int TiS { get; set; }
        public int NextTicket { get; set; }
    }

}
