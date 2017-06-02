using VQ.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace VQ.Models
{
    public class QueuesRepository
    {
        private VQEntities db = new VQEntities();

        readonly string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public IEnumerable<Queues> GetDeptQueues(int? id)
        {
            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);

            int deptId = 1;
            if (id != null)
            {
                deptId = (int)id;
            }

            Department dept = (Department)db.Departments.Find(deptId);
            if (dept == null)
            {

            }

            var queuesTemp = new List<Queues>();
            var queues = new List<Queues>();
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"SELECT   [Id]
                                                              ,[DepartmentId]
                                                              ,[DepartmentName]
                                                              ,[DeptCode]
                                                              ,[ServiceTypeId]
                                                              ,[ServiceTypeName]
                                                              ,[DeskId]
                                                              ,[DeskName]
                                                              ,[AgentId]
                                                              ,[TicketId]
                                                              ,[TicketTime]
                                                              ,[TicketNumber]
                                                              ,[FirstCallTime]
                                                              ,[LastCallDate]
                                                              ,[CallCount]
                                                              ,[IsCurrent]
                                                              ,[IsServed]
                                                              ,[ServiceEndTime]
                                                              ,[PlaySound]
                                                        FROM [dbo].[TicketsInService]
                                                        where IsCurrent = 1 AND DepartmentId = " + id.ToString() + " Order By [LastCallDate] desc", connection))
                {
                    command.Notification = null;

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnDeptChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        queuesTemp.Add(item: new Queues { Id = (int)reader["Id"], DepartmentId = (int)reader["DepartmentId"], DepartmentName = (string)reader["DepartmentName"], DeptCode = (string)reader["DeptCode"], DeskId = (int)reader["DeskId"], DeskName = (string)reader["DeskName"], TicketNumber = (int)reader["TicketNumber"], TicketDate = (DateTime)reader["TicketTime"], CallCount = (int)reader["CallCount"], PlaySound = (bool)reader["PlaySound"] });
                    }
                }

            }

            foreach (var item in queuesTemp)
            {
                if (item.TicketDate >= today && item.TicketDate < tomorrow)
                {
                    queues.Add(item);
                }
            }

            return queues;
        }

        private void dependency_OnDeptChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                QueuesHub.SendDeptQueues();
            }
        }


        public IEnumerable<Queues> GetDeskQueues(int? id)
        {
            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);

            int deskId = 1;
            if (id != null)
            {
                deskId = (int)id;
            }

            var queuesTemp = new List<Queues>();
            var queues = new List<Queues>();
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"SELECT   [Id]
                                                              ,[DepartmentId]
                                                              ,[DepartmentName]
                                                              ,[DeptCode]
                                                              ,[ServiceTypeId]
                                                              ,[ServiceTypeName]
                                                              ,[DeskId]
                                                              ,[DeskName]
                                                              ,[AgentId]
                                                              ,[TicketId]
                                                              ,[TicketTime]
                                                              ,[TicketNumber]
                                                              ,[FirstCallTime]
                                                              ,[LastCallDate]
                                                              ,[CallCount]
                                                              ,[IsCurrent]
                                                              ,[IsServed]
                                                              ,[ServiceEndTime]
                                                              ,[PlaySound]
                                                        FROM [dbo].[TicketsInService]
                                                        where IsCurrent = 1 AND DeskId = " + id.ToString() + "", connection))
                {
                    command.Notification = null;

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnDeskChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        queuesTemp.Add(item: new Queues { Id = (int)reader["Id"], DepartmentId = (int)reader["DepartmentId"], DepartmentName = (string)reader["DepartmentName"], DeptCode = (string)reader["DeptCode"], DeskId = (int)reader["DeskId"], DeskName = (string)reader["DeskName"], TicketNumber = (int)reader["TicketNumber"], TicketDate = (DateTime)reader["TicketTime"], CallCount = (int)reader["CallCount"], PlaySound = (bool)reader["PlaySound"] });
                    }
                }

            }

            foreach (var item in queuesTemp)
            {
                if (item.TicketDate >= today && item.TicketDate < tomorrow)
                {
                    queues.Add(item);
                }
            }

            return queues;

        }

        private void dependency_OnDeskChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                QueuesHub.SendDeskQueues();
            }
        }
    }
}