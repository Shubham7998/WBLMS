using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBLMS.Models;
using WBLMS.Utilities;
using static System.Net.WebRequestMethods;

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
            var hashedPassword = PasswordHashing.getHashPassword("admin@WB");
            _modelBuilder.Entity<Gender>().HasData(
                    new Gender() { Id = 1, GenderName = "Female"},
                    new Gender() { Id = 2, GenderName = "Male"},
                    new Gender() { Id = 3, GenderName = "Others"}
                );
            //_modelBuilder.Entity<Roles>().HasData(
            //        new Roles() { Id = 1, RoleName = "Admin"},
            //        new Roles() { Id = 2, RoleName = "HR Manager"},
            //        new Roles() { Id = 3, RoleName = "Team Lead"},
            //        new Roles() { Id = 4, RoleName = "HR"},
            //        new Roles() { Id = 5, RoleName = "Developer" }
            //    );
            _modelBuilder.Entity<Status>().HasData(
                    new Status() { Id = 1, StatusName = "Pending"},
                    new Status() { Id = 2, StatusName = "Approved"},
                    new Status() { Id = 3, StatusName = "Rejected"}
                );
            //_modelBuilder.Entity<LeaveType>().HasData(
            //        new LeaveType() { Id = 1, LeaveTypeName = "Vacation"},
            //        new LeaveType() { Id = 2, LeaveTypeName = "Sick"},
            //        new LeaveType() { Id = 3, LeaveTypeName = "Casual"},
            //        new LeaveType() { Id = 4, LeaveTypeName = "Marriage"},
            //        new LeaveType() { Id = 5, LeaveTypeName = "Maternity"}
            //    );
            _modelBuilder.Entity<Department>().HasData(
                    new Department()
                    {
                        Id = 1,
                        Name = "IT",
                        BranchId = 1,
                    },
                    new Department()
                    {
                        Id = 2,
                        Name = "HR",
                        BranchId = 1,
                    }
                );
            _modelBuilder.Entity<LeaveType>().HasData(
                    new LeaveType()
                    {
                        Id = 1,
                        LeaveTypeName = "Paid",
                        BranchId = 1,
                        MaxDays = 21
                    },
                    new LeaveType()
                    {
                        Id = 2,
                        LeaveTypeName = "UnPaid",
                        BranchId = 1,
                        MaxDays = 0
                    },
                    new LeaveType()
                    {
                        Id = 3,
                        LeaveTypeName = "Other",
                        BranchId = 1,
                        MaxDays = 0
                    }
                );
            _modelBuilder.Entity<SuperAdmin>().HasData(
                    new SuperAdmin() { Id = 1, FirstName = "Hemant", LastName = "Patel", Password = hashedPassword, EmailAddress = "hemant.patel@wonderbiz.in", ContactNumber = "9874563210", GenderId = 2}
                );

            _modelBuilder.Entity<Organization>().HasData(
                    new Organization()
                    {
                        Id = 1,
                        Name = "WB",
                        HeadQuarter = "Thane",
                        SuperAdminId = 1,
                    }
                );
            _modelBuilder.Entity<Reporting>().HasData(
                    new Reporting()
                    {
                        Id = 1,
                        ReportFrom = 1,
                        ReportTo = 1,
                    }
                );
            _modelBuilder.Entity<Admin>().HasData(
                    new Admin()
                    {
                        Id = 1,
                        FirstName = "Admin",
                        LastName = "Admin",
                        ContactNumber = "998889879",
                        DOB = new DateOnly(2024, 01, 01),
                        GenderId = 1,
                        ReportingId = 1
                    }
                ); 
            _modelBuilder.Entity<Branch>().HasData(
                    new Branch()
                    {
                        Id = 1,
                        OrganizationId = 1,
                        Name = "Thane",
                        Address = "Address",
                        BranchHeadId = 1
                    }
                );
            //_modelBuilder.Entity<WonderbizHolidays>().HasData(
            //        new WonderbizHolidays() { Id = 1, Date = new DateOnly(2024, 1, 1), Day = "Monday", Event = "New Year" },
            //        new WonderbizHolidays() { Id = 2, Date = new DateOnly(2024, 1, 26), Day = "Friday", Event = "Republic Day" },
            //        new WonderbizHolidays() { Id = 3, Date = new DateOnly(2024, 3, 25), Day = "Monday", Event = "Holi" },
            //        new WonderbizHolidays() { Id = 4, Date = new DateOnly(2024, 4, 11), Day = "Thursday", Event = "Eid" },
            //        new WonderbizHolidays() { Id = 5, Date = new DateOnly(2024, 5, 1), Day = "Wednesday", Event = "Maharashtra Day" },
            //        new WonderbizHolidays() { Id = 6, Date = new DateOnly(2024, 8, 15), Day = "Thursday", Event = "Independence Day" },
            //        new WonderbizHolidays() { Id = 7, Date = new DateOnly(2024, 10, 2), Day = "Wednesday", Event = "Gandhi Jayanti" },
            //        new WonderbizHolidays() { Id = 8, Date = new DateOnly(2024, 10, 31), Day = "Thursday", Event = "Diwali" },
            //        new WonderbizHolidays() { Id = 9, Date = new DateOnly(2024, 11, 1), Day = "Friday", Event = "Diwali" },
            //        new WonderbizHolidays() { Id = 10, Date = new DateOnly(2024, 12, 25), Day = "Wednesday", Event = "Christmas" }
            //);
        }
    }
}
