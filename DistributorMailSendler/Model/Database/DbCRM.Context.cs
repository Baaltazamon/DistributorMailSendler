﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DistributorMailSendler.Model.Database
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CRMEntities : DbContext
    {
        public CRMEntities()
            : base("name=CRMEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Asp_Persons> Asp_Persons { get; set; }
        public virtual DbSet<Asp_SpravDistr> Asp_SpravDistr { get; set; }
        public virtual DbSet<Asp_Warehouses> Asp_Warehouses { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<DistInvoicePositions> DistInvoicePositions { get; set; }
        public virtual DbSet<DistInvoices> DistInvoices { get; set; }
        public virtual DbSet<DocLifeCycles> DocLifeCycles { get; set; }
        public virtual DbSet<Faults> Faults { get; set; }
        public virtual DbSet<HandlingEmails> HandlingEmails { get; set; }
        public virtual DbSet<InstructionFiles> InstructionFiles { get; set; }
        public virtual DbSet<InstructionMainMenus> InstructionMainMenus { get; set; }
        public virtual DbSet<InstructionSubMenus> InstructionSubMenus { get; set; }
        public virtual DbSet<NotErrors> NotErrors { get; set; }
        public virtual DbSet<Recons> Recons { get; set; }
        public virtual DbSet<StatusUpdates> StatusUpdates { get; set; }
        public virtual DbSet<Stories> Stories { get; set; }
    }
}
