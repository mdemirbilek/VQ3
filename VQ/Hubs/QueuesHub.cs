using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using VQ.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace VQ.Hubs
{
    public class QueuesHub : Hub
    {
        private static string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        public void Hello()
        {
            Clients.All.hello();
        }

        [HubMethodName("sendDeptQueues")]
        public static void SendDeptQueues()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<QueuesHub>();
            context.Clients.All.updateDeptQueues();
        }

        [HubMethodName("sendDeskQueues")]
        public static void SendDeskQueues()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<QueuesHub>();
            context.Clients.All.updateDeskQueues();
        }


    }
}