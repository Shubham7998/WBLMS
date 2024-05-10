using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBLMS.Models;

namespace WBLMS.Data
{
    public class DbInitializer
    {
        private readonly ModelBuilder _modelBuilder;
        public DbInitializer(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }
        public void seed()
        {
            _modelBuilder.Entity<Gender>().HasData(
                    new Gender() { Id = 1, GenderName = "Female"},
                    new Gender() { Id = 2, GenderName = "Male"},
                    new Gender() { Id = 3, GenderName = "Others"}
                );
            _modelBuilder.Entity<Roles>().HasData(
                    new Roles() { Id = 1, RoleName = "Employee" },
                    new Roles() { Id = 2, RoleName = "Team Lead"},
                    new Roles() { Id = 3, RoleName = "HR"},
                    new Roles() { Id = 4, RoleName = "Admin"}
                );
            _modelBuilder.Entity<Status>().HasData(
                    new Status() { Id = 1, StatusName = "Pending"},
                    new Status() { Id = 2, StatusName = "Approved"},
                    new Status() { Id = 3, StatusName = "Rejected"}
                );
            _modelBuilder.Entity<LeaveType>().HasData(
                    new LeaveType() { Id = 1, LeaveTypeName = "Vacation"},
                    new LeaveType() { Id = 2, LeaveTypeName = "Sick"},
                    new LeaveType() { Id = 3, LeaveTypeName = "Casual"},
                    new LeaveType() { Id = 4, LeaveTypeName = "Marriage"},
                    new LeaveType() { Id = 5, LeaveTypeName = "Maternity"}
                );

        }
    }
}
