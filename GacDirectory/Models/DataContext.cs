using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GacDirectory.Models
{
    public class DataContext : DbContext
    {

        string conStr = "";

        public DataContext(string dbConStr)
        {
            conStr = dbConStr;
        }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {                
                optionsBuilder.UseSqlServer(conStr);
            }
        }

        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeData> EmployeeData { get; set; }
        public virtual DbSet<EmployeeContact> EmployeeContact { get; set; }

        public virtual DbSet<EmployeeModels> LeaveBalance { get; set; }
        public virtual DbSet<LeaveReport> LeaveReport { get; set; }
        public virtual DbSet<DepartmentChart> DepartmentChart { get; set; }


        public virtual DbSet<PublicHolidays> PublicHolidays { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Item>().Property(i => i.IsFruit).HasDefaultValue(false);
            //builder.Entity<Item>().Property(i => i.IsVegetable).HasDefaultValue(false);

            //builder.Ignore<LeaveBalance>();

            builder.Entity<Employee>(e =>
            {
                e.HasKey(e => e.EmployeeID);                
                e.ToTable("Employee");
            });

            builder.Entity<Employee>(e => { e.Property(e => e.EmployeeID).ValueGeneratedNever(); });


            builder.Entity<EmployeeData>(e => { e.Property(e => e.Id).ValueGeneratedNever(); });
            builder.Entity<EmployeeContact>(e => { e.Property(e => e.Id).ValueGeneratedNever(); });

            builder.Entity<EmployeeModels>(e => { e.Property(e => e.Id).ValueGeneratedNever(); });
            builder.Entity<LeaveReport>(e => { e.Property(e => e.Id).ValueGeneratedNever(); });
            builder.Entity<DepartmentChart>(e => { e.Property(e => e.Id).ValueGeneratedNever(); });

            builder.Entity<PublicHolidays>(e => { e.Property(e => e.Id).ValueGeneratedNever(); });

        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
