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
                    new Gender() { GenderName = "Female"},
                    new Gender() { GenderName = "Male"},
                    new Gender() { GenderName = "Others"}
                );
            _modelBuilder.Entity<Roles>().HasData(
                    new Roles() { RoleName = "Employee" },
                    new Roles() { RoleName = "Team Lead"},
                    new Roles() { RoleName = "HR"},
                    new Roles() { RoleName = "Admin"}
                );
            _modelBuilder.Entity<Status>().HasData(
                    new Status() { StatusName = "Pending"},
                    new Status() { StatusName = "Approved"},
                    new Status() { StatusName = "Rejected"}
                );
            _modelBuilder.Entity<LeaveType>().HasData(
                    new LeaveType() { LeaveTypeName = "Vacation"},
                    new LeaveType() { LeaveTypeName = "Sick"},
                    new LeaveType() { LeaveTypeName = "Casual"},
                    new LeaveType() { LeaveTypeName = "Marriage"},
                    new LeaveType() { LeaveTypeName = "Maternity"}
                );

        }
    }
}
