﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WBLMS.Data;

#nullable disable

namespace WBLMS.Data.Migrations
{
    [DbContext(typeof(WBLMSDbContext))]
    partial class WBLMSDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WBLMS.Models.Employee", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("GenderId")
                        .HasColumnType("bigint");

                    b.Property<DateOnly?>("JoiningDate")
                        .HasColumnType("date");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ManagerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long?>("TokenId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UpdateById")
                        .HasColumnType("bigint");

                    b.Property<DateOnly?>("UpdatedDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("GenderId");

                    b.HasIndex("ManagerId");

                    b.HasIndex("RoleId");

                    b.HasIndex("TokenId");

                    b.HasIndex("EmailAddress", "ContactNumber")
                        .IsUnique();

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ContactNumber = "9874563210",
                            EmailAddress = "hemant.patel@wonderbiz.in",
                            FirstName = "Hemant",
                            GenderId = 2L,
                            LastName = "Patel",
                            Password = "05DE0BA6E0B516DACFDF253175A1B2452467865F2EA2797FB052E5B0B1AEB6A0:13FC9E36C243D0ACBE941549F167232C:50000:SHA256",
                            RoleId = 1L
                        });
                });

            modelBuilder.Entity("WBLMS.Models.Gender", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("GenderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("GenderName")
                        .IsUnique();

                    b.ToTable("Genders");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            GenderName = "Female"
                        },
                        new
                        {
                            Id = 2L,
                            GenderName = "Male"
                        },
                        new
                        {
                            Id = 3L,
                            GenderName = "Others"
                        });
                });

            modelBuilder.Entity("WBLMS.Models.LeaveBalance", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("TotalLeaves")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.ToTable("LeaveBalances");
                });

            modelBuilder.Entity("WBLMS.Models.LeaveRequest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateOnly>("ApprovedDate")
                        .HasColumnType("date");

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<long>("LeaveTypeId")
                        .HasColumnType("bigint");

                    b.Property<long>("ManagerId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("NumberOfLeaveDays")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("RequestDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<long>("StatusId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("LeaveTypeId");

                    b.HasIndex("ManagerId");

                    b.HasIndex("StatusId");

                    b.ToTable("LeaveRequests");
                });

            modelBuilder.Entity("WBLMS.Models.LeaveType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("LeaveTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("LeaveTypeName")
                        .IsUnique();

                    b.ToTable("LeaveTypes");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            LeaveTypeName = "Vacation"
                        },
                        new
                        {
                            Id = 2L,
                            LeaveTypeName = "Sick"
                        },
                        new
                        {
                            Id = 3L,
                            LeaveTypeName = "Casual"
                        },
                        new
                        {
                            Id = 4L,
                            LeaveTypeName = "Marriage"
                        },
                        new
                        {
                            Id = 5L,
                            LeaveTypeName = "Maternity"
                        });
                });

            modelBuilder.Entity("WBLMS.Models.Roles", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            RoleName = "Admin"
                        },
                        new
                        {
                            Id = 2L,
                            RoleName = "HR"
                        },
                        new
                        {
                            Id = 3L,
                            RoleName = "Team Lead"
                        },
                        new
                        {
                            Id = 4L,
                            RoleName = "Employee"
                        });
                });

            modelBuilder.Entity("WBLMS.Models.Status", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("StatusName")
                        .IsUnique();

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            StatusName = "Pending"
                        },
                        new
                        {
                            Id = 2L,
                            StatusName = "Approved"
                        },
                        new
                        {
                            Id = 3L,
                            StatusName = "Rejected"
                        });
                });

            modelBuilder.Entity("WBLMS.Models.Token", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("PasswordResetExpiry")
                        .HasColumnType("datetime2");

                    b.Property<string>("PasswordResetToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshTokenExpiry")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("WBLMS.Models.WonderbizHolidays", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Event")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WonderbizHolidays");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Date = new DateOnly(2024, 1, 1),
                            Day = "Monday",
                            Event = "New Year"
                        },
                        new
                        {
                            Id = 2L,
                            Date = new DateOnly(2024, 1, 26),
                            Day = "Friday",
                            Event = "Republic Day"
                        },
                        new
                        {
                            Id = 3L,
                            Date = new DateOnly(2024, 3, 25),
                            Day = "Monday",
                            Event = "Holi"
                        },
                        new
                        {
                            Id = 4L,
                            Date = new DateOnly(2024, 4, 11),
                            Day = "Thursday",
                            Event = "Eid"
                        },
                        new
                        {
                            Id = 5L,
                            Date = new DateOnly(2024, 5, 1),
                            Day = "Wednesday",
                            Event = "Maharashtra Day"
                        },
                        new
                        {
                            Id = 6L,
                            Date = new DateOnly(2024, 8, 15),
                            Day = "Thursday",
                            Event = "Independence Day"
                        },
                        new
                        {
                            Id = 7L,
                            Date = new DateOnly(2024, 10, 2),
                            Day = "Wednesday",
                            Event = "Gandhi Jayanti"
                        },
                        new
                        {
                            Id = 8L,
                            Date = new DateOnly(2024, 10, 31),
                            Day = "Thursday",
                            Event = "Diwali"
                        },
                        new
                        {
                            Id = 9L,
                            Date = new DateOnly(2024, 11, 1),
                            Day = "Friday",
                            Event = "Diwali"
                        },
                        new
                        {
                            Id = 10L,
                            Date = new DateOnly(2024, 12, 25),
                            Day = "Wednesday",
                            Event = "Christmas"
                        });
                });

            modelBuilder.Entity("WBLMS.Models.Employee", b =>
                {
                    b.HasOne("WBLMS.Models.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId");

                    b.HasOne("WBLMS.Models.Employee", "Manager")
                        .WithMany("Subordinates")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("WBLMS.Models.Roles", "Roles")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("WBLMS.Models.Token", "Token")
                        .WithMany()
                        .HasForeignKey("TokenId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Gender");

                    b.Navigation("Manager");

                    b.Navigation("Roles");

                    b.Navigation("Token");
                });

            modelBuilder.Entity("WBLMS.Models.LeaveBalance", b =>
                {
                    b.HasOne("WBLMS.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("WBLMS.Models.LeaveRequest", b =>
                {
                    b.HasOne("WBLMS.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WBLMS.Models.LeaveType", "LeaveType")
                        .WithMany()
                        .HasForeignKey("LeaveTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WBLMS.Models.Employee", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WBLMS.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("LeaveType");

                    b.Navigation("Manager");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("WBLMS.Models.Token", b =>
                {
                    b.HasOne("WBLMS.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("WBLMS.Models.Employee", b =>
                {
                    b.Navigation("Subordinates");
                });
#pragma warning restore 612, 618
        }
    }
}
