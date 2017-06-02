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
    public class TicketManController : Controller
    {
        private VQEntities db = new VQEntities();

        // GET: TicketMan
        public ActionResult Index(string id, string ServiceList)
        {
            SetVisibilities("visible", "visible", "visible", "visible", "visible", "visible");

            if (User == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "300" });
            }

            if (!MyFunctions.CheckUserRole(User.Identity.Name, "agent"))
            {
                return RedirectToAction("Index", "Warning", new { id = "301" });
            }

            Agent agent = (Agent)db.Agents.FirstOrDefault(x => x.EmailAddress == User.Identity.Name);
            if (agent == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "302" });
            }

            Department department = (Department)db.Departments.Find(agent.DepartmentId);
            if (department == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "303" });
            }

            Desk desk = (Desk)db.Desks.Find(agent.CurrentDeskId);
            if (desk == null)
            {
                return RedirectToAction("Index", "Warning", new { id = "304" });
            }

            List<SelectListItem> serviceItems = new List<SelectListItem>();
            List<ServiceType> dbSTList = db.ServiceTypes.ToList();
            SelectListItem sli0 = new SelectListItem();
            sli0.Text = "Select Service";
            sli0.Value = "0";
            serviceItems.Add(sli0);
            foreach (ServiceType st in dbSTList)
            {
                if (st.DepartmentId == desk.DepartmentId)
                {
                    SelectListItem sli = new SelectListItem();
                    sli.Text = st.Department.Name + " - " + st.Name;
                    sli.Value = st.Id.ToString();
                    serviceItems.Add(sli);
                }
            }
            ViewBag.ServiceList = serviceItems;

            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);
            int ticketId = 0;



            if (!string.IsNullOrEmpty(id) && id != "x")
            {
                if (Session["CurrentTicketId"] != null)
                {
                    ticketId = int.Parse(Session["CurrentTicketId"].ToString());
                }

                if (ticketId > 0)
                {
                    TicketsInService tis = (TicketsInService)db.TicketsInServices.Find(ticketId);
                    if (tis != null)
                    {
                        if (id == "mc")
                        {
                            tis.IsCurrent = true;
                            tis.IsServed = false;
                            tis.Status = "Missing";
                            tis.ServiceEndTime = DateTime.Now;
                            tis.PlaySound = false;

                            if (tis.Ticket.CustomerId > 0)
                            {
                                Customer cust = (Customer)db.Customers.Find(tis.Ticket.CustomerId);
                                if (tis.TicketId == cust.LastTicketId)
                                {
                                    cust.MTC = cust.MTC + 1;
                                    cust.LastTicketId = 0;
                                }
                            }
                        }
                        else if (id == "sc")
                        {
                            tis.IsCurrent = true;
                            tis.IsServed = true;
                            tis.Status = "Closed OK";
                            tis.ServiceEndTime = DateTime.Now;
                            tis.PlaySound = false;
                        }
                        else if (id == "uc")
                        {
                            tis.IsCurrent = true;
                            tis.IsServed = true;
                            tis.Status = "Closed NOK";
                            tis.ServiceEndTime = DateTime.Now;
                            tis.PlaySound = false;
                        }
                        else if (id == "rc")
                        {
                            tis.IsCurrent = true;
                            tis.IsServed = false;
                            tis.Status = "Re-Called";
                            tis.CallCount = tis.CallCount + 1;
                            tis.LastCallDate = DateTime.Now;
                            tis.PlaySound = true;
                        }
                        else if (id == "fw")
                        {
                            int st = 0;
                            if (!string.IsNullOrEmpty(ServiceList))
                            {
                                bool b = int.TryParse(ServiceList, out st);
                                if (st > 0)
                                {
                                    tis.IsCurrent = false;
                                    tis.IsServed = true;
                                    tis.Status = "Forwarded To";
                                    tis.ServiceEndTime = DateTime.Now;
                                    tis.ForwardedBy = tis.ServiceTypeId;
                                    tis.ForwardedTo = st;
                                    tis.PlaySound = false;

                                    Ticket fwTicket = new Ticket();
                                    fwTicket.CallTime = DateTime.Now;
                                    fwTicket.CustomerId = tis.Ticket.CustomerId;
                                    fwTicket.ForwardedBy = tis.ServiceTypeId;
                                    fwTicket.IsCalled = false;
                                    fwTicket.Priority = 66;
                                    fwTicket.ServiceTypeId = st;
                                    fwTicket.TicketNumber = tis.TicketNumber;
                                    fwTicket.TicketPin = tis.Ticket.TicketPin;
                                    fwTicket.TicketTime = DateTime.Now;

                                    db.Tickets.Add(fwTicket);

                                    db.SaveChanges();
                                }
                            }
                        }

                        db.SaveChanges();
                    }
                }

                if (id == "cn")
                {
                    if (ticketId > 0)
                    {
                        TicketsInService oldTicket = (TicketsInService)db.TicketsInServices.Find(ticketId);
                        if (oldTicket != null)
                        {
                            oldTicket.IsCurrent = false;
                            //Session["CurrentTicketId"] = null;
                            if (oldTicket.Status == "Called" || oldTicket.Status == "Re-Called")
                            {
                                oldTicket.Status = "Passed";
                                oldTicket.ServiceEndTime = DateTime.Now;
                            }
                            db.SaveChanges();
                        }
                    }

                    Ticket newTicket;
                    newTicket = (Ticket)db.Tickets.OrderBy(x => x.Priority).ThenBy(x => x.TicketNumber).FirstOrDefault(x => x.ServiceTypeId == agent.PrimaryServiceTypeId && x.IsCalled == false && x.TicketTime >= today && x.TicketTime < tomorrow);

                    if (newTicket == null)
                    {
                        newTicket = (Ticket)db.Tickets.OrderBy(x => x.Priority).ThenBy(x => x.TicketNumber).FirstOrDefault(x => x.ServiceType.DepartmentId == agent.DepartmentId && x.IsCalled == false && x.TicketTime >= today && x.TicketTime < tomorrow);
                    }

                    if (newTicket != null)
                    {
                        newTicket.IsCalled = true;
                        db.SaveChanges();

                        TicketsInService newTis = new TicketsInService();
                        newTis.AgentId = agent.Id;
                        newTis.CallCount = 1;
                        newTis.DepartmentId = department.Id;
                        newTis.DepartmentName = department.Name;
                        newTis.DeptCode = department.DeptCode;
                        newTis.DeskId = desk.Id;
                        newTis.DeskName = desk.Name;
                        newTis.FirstCallTime = DateTime.Now;
                        newTis.IsCurrent = true;
                        newTis.IsServed = false;
                        newTis.LastCallDate = DateTime.Now;
                        newTis.ServiceEndTime = DateTime.Parse("2000-01-01");
                        newTis.ServiceTypeId = newTicket.ServiceType.Id;
                        newTis.ServiceTypeName = newTicket.ServiceType.Name;
                        newTis.Status = "Called";
                        newTis.TicketId = newTicket.Id;
                        newTis.TicketNumber = newTicket.TicketNumber;
                        newTis.TicketTime = newTicket.TicketTime;
                        newTis.ForwardedBy = newTicket.ForwardedBy;
                        newTis.ForwardedTo = 0;
                        newTis.PlaySound = true;

                        db.TicketsInServices.Add(newTis);

                        db.SaveChanges();
                    }

                }
                else if (id == "cd")
                {
                    if (ticketId > 0)
                    {
                        TicketsInService oldTicket = (TicketsInService)db.TicketsInServices.Find(ticketId);
                        if (oldTicket != null)
                        {
                            oldTicket.IsCurrent = false;
                            if (oldTicket.Status == "Called" || oldTicket.Status == "Re-Called")
                            {
                                oldTicket.Status = "Passed";
                                oldTicket.ServiceEndTime = DateTime.Now;
                                oldTicket.PlaySound = false;
                            }

                            db.SaveChanges();
                        }
                    }

                    Session.Abandon();
                    return RedirectToAction("Index", "Home");
                }

                return RedirectToAction("Index", "TicketMan", new { id = "x" });

            }

            TicketsInService currentTicket = (TicketsInService)db.TicketsInServices.OrderByDescending(x => x.TicketTime).ThenByDescending(x => x.TicketNumber).FirstOrDefault(x => x.AgentId == agent.Id && x.IsCurrent == true && x.TicketTime >= today && x.TicketTime < tomorrow);
            if (currentTicket != null)
            {
                Session["CurrentTicketId"] = currentTicket.Id.ToString();
                SetVisibilities("hidden", "visible", "visible", "visible", "visible", "visible");

                if (currentTicket.Status == "Called" || currentTicket.Status == "Re-Called")
                {
                    SetVisibilities("hidden", "visible", "visible", "visible", "hidden", "visible");
                }
            }
            else
            {
                SetVisibilities("visible", "hidden", "hidden", "visible", "visible", "visible");
            }

            return View(currentTicket);
        }

        protected void SetVisibilities(string div1, string div2, string div3, string div4, string div5, string div6)
        {
            Session["div1"] = div1;
            Session["div2"] = div2;
            Session["div3"] = div3;
            Session["div4"] = div4;
            Session["div5"] = div5;
            Session["div6"] = div6;
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
