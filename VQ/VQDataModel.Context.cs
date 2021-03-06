﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VQ
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class VQEntities : DbContext
    {
        public VQEntities()
            : base("name=VQEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Agent> Agents { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Desk> Desks { get; set; }
        public virtual DbSet<ServiceType> ServiceTypes { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketsInService> TicketsInServices { get; set; }
        public virtual DbSet<Counter> Counters { get; set; }
        public virtual DbSet<ErrorMessage> ErrorMessages { get; set; }
        public virtual DbSet<vwTIP2> vwTIP2 { get; set; }
        public virtual DbSet<AgentRole> AgentRoles { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<vwTNrCalled> vwTNrCalleds { get; set; }
        public virtual DbSet<vwTNrNotCalled> vwTNrNotCalleds { get; set; }
        public virtual DbSet<SysConfig> SysConfigs { get; set; }
    }
}
