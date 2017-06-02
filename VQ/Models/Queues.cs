using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VQ.Models
{
    public class Queues
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DeptCode { get; set; }
        public int DeskId { get; set; }
        public string DeskName { get; set; }
        public int TicketNumber { get; set; }
        public DateTime TicketDate { get; set; }
        public int CallCount { get; set; }
        public bool PlaySound { get; set; }
    }
}