using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using System.Reflection.Metadata;
using WBLMS.Models;

namespace WBLMS.Data
{
    public class WBLMSDbContext : DbContext
    {
        public WBLMSDbContext(DbContextOptions<WBLMSDbContext> options) : base(options) { }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Department> Departments { get; set; }  
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<LeaveSubType> LeaveSubTypes { get; set; }
        public DbSet<Reporting> Reportings { get; set; }
        public DbSet<Employee2> Employee2s { get; set; }
        public DbSet<SuperAdmin> SuperAdmins { get; set; }


        public DbSet<Employee> Employees { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<LeaveBalance> LeaveBalances { get; set; }
        public DbSet<WonderbizHolidays> WonderbizHolidays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>()
                .HasMany(o => o.Branches)
                .WithOne(b => b.Organization)
                .HasForeignKey(b => b.OrganizationId);

            modelBuilder.Entity<Branch>()
                .HasMany(b => b.Departments)
                .WithOne(d => d.Branch)
                .HasForeignKey(d => d.BranchId);

            modelBuilder.Entity<Branch>()
                .HasMany(b => b.LeaveTypes)
                .WithOne(l => l.Branch)
                .HasForeignKey(l => l.BranchId);

            modelBuilder.Entity<Branch>()
                .HasOne(b => b.WorkingDays)
                .WithOne(w => w.Branch);

            modelBuilder.Entity<Branch>()
                .HasMany(b => b.Holidays)
                .WithOne(d => d.Branch)
                .HasForeignKey(d => d.BranchId);
            
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Teams)
                .WithOne(t => t.Department)
                .HasForeignKey(t => t.DepartmentId);

            modelBuilder.Entity<LeaveType>()
                .HasMany(l => l.LeaveSubTypes)
                .WithOne(l => l.LeaveType)
                .HasForeignKey(l => l.LeaveTypeId);

            modelBuilder.Entity<Team>()
                .HasMany(t => t.TeamMembers)
                .WithOne(e => e.Team);


            modelBuilder.Entity<Employee>()
            .HasOne(e => e.Manager)
            .WithMany(e => e.Subordinates)
            .HasForeignKey(e => e.ManagerId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Token)
                .WithMany()
                .HasForeignKey(e => e.TokenId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LeaveRequest>()
                .HasOne(e => e.Employee)
                .WithMany()
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LeaveRequest>()
                .HasOne(e => e.Manager)
                .WithMany()
                .HasForeignKey(e => e.ManagerId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
            //new DbInitializer(modelBuilder).seed();


        }

    }
}
