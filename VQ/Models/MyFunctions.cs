using Microsoft.AspNet.Identity;
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

namespace VQ.Models
{
    public static class MyFunctions
    {
        private static VQEntities db = new VQEntities();

        public static bool CheckUserRole(string emailAddress, string roleName)
        {
            bool result = false;

            if (String.IsNullOrEmpty(emailAddress) || String.IsNullOrEmpty(roleName))
            {
                return false;
            }

            Agent agent = (Agent)db.Agents.FirstOrDefault(x => x.EmailAddress == emailAddress.Trim());
            if (agent == null)
            {
                return false;
            }

            Role role = (Role)db.Roles.FirstOrDefault(x => x.Name == roleName.Trim());
            if (role == null)
            {
                return false;
            }

            AgentRole ar = db.AgentRoles.FirstOrDefault(x => x.AgentId == agent.Id && x.RoleId == role.Id);

            if (ar != null)
            {
                result = true;
            }

            return result;
        }

        public static SysConfig GetConfigItem(string sysCode)
        {
            SysConfig config = db.SysConfigs.FirstOrDefault(x => x.SysCode == sysCode && x.IsActive == true);
            return config;
        }

    }
}