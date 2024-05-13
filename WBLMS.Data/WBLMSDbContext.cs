using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Reflection.Metadata;
using WBLMS.Models;

namespace WBLMS.Data
{
    public class WBLMSDbContext : DbContext
    {
        public WBLMSDbContext(DbContextOptions<WBLMSDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<LeaveBalance> LeaveBalances { get; set;}
        public DbSet<LeaveType> LeaveTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
            .HasOne(e => e.Manager)              // Specifies that each employee has one manager.
            .WithMany(e => e.Subordinates)       // Specifies that each manager can have many Subordinates
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
            new DbInitializer(modelBuilder).seed();

            //modelBuilder.Entity<Employee>()
            //.HasMany(e => e.Subordinates)              // Specifies that each employee has one manager.
            //.WithOne(e => e.Manager)       // Specifies that each manager can have many Subordinates
            //.HasForeignKey(e => e.Manager.ManagerId); 
            // Specifies the foreign key property for this relationship.
        }

    }
}
