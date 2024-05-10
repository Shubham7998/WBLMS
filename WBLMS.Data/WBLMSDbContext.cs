using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
            base.OnModelCreating(modelBuilder);
            new DbInitializer(modelBuilder).seed();
        }

    }
}
