using Company.MVC.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Company.MVC.DAL.Data.Context
{
    internal class ConpanyDbContext : DbContext
    {
        public ConpanyDbContext() : base()
        { 

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly());    
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-LA0JIU9\\MSSQLSERVER01 ; Database=CompanyMVC;Trusted_Connection=true;trustSeverCertificate=true");


        }

        public DbSet<Department>  Departments { get; set; }
        public Assembly Assebly { get; private set; }
    }
}
