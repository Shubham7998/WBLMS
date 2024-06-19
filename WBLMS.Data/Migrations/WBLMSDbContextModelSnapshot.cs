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

            modelBuilder.Entity("WBLMS.Models.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("OrganizationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("OrganizationId");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("WBLMS.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<int?>("DepartmentHead")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("DepartmentHead");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Departments");
                });

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

                    b.Property<int?>("GenderId")
                        .HasColumnType("int");

                    b.Property<DateOnly?>("JoiningDate")
                        .HasColumnType("date");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("LeaveBalanceId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ManagerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
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

                    b.HasIndex("LeaveBalanceId");

                    b.HasIndex("ManagerId");

                    b.HasIndex("RoleId");

                    b.HasIndex("TokenId");

                    b.HasIndex("EmailAddress", "ContactNumber")
                        .IsUnique();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("WBLMS.Models.Employee2", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GenderId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReportingId")
                        .HasColumnType("int");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<int>("UpdatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GenderId");

                    b.HasIndex("ReportingId");

                    b.HasIndex("TeamId");

                    b.ToTable("Employee2s");
                });

            modelBuilder.Entity("WBLMS.Models.Gender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("GenderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("GenderName")
                        .IsUnique();

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("WBLMS.Models.Holiday", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("HolidayDate")
                        .HasColumnType("date");

                    b.Property<string>("HolidayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("HolidayDate")
                        .IsUnique();

                    b.ToTable("Holidays");
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

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<int>("LeaveTypeId")
                        .HasColumnType("int");

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

                    b.Property<int>("UpdatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("LeaveTypeId");

                    b.HasIndex("ManagerId");

                    b.HasIndex("StatusId");

                    b.ToTable("LeaveRequests");
                });

            modelBuilder.Entity("WBLMS.Models.LeaveSubType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("LeaveSubTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("LeaveTypeId")
                        .HasColumnType("int");

                    b.Property<decimal>("MaxLeaveDays")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("LeaveSubTypeName")
                        .IsUnique();

                    b.HasIndex("LeaveTypeId");

                    b.ToTable("LeaveSubTypes");
                });

            modelBuilder.Entity("WBLMS.Models.LeaveType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<string>("LeaveTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("MaxDays")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("LeaveTypeName")
                        .IsUnique();

                    b.ToTable("LeaveTypes");
                });

            modelBuilder.Entity("WBLMS.Models.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("HeadQuarter")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("WBLMS.Models.Reporting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ReportFrom")
                        .HasColumnType("int");

                    b.Property<int>("ReportTo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReportFrom", "ReportTo")
                        .IsUnique();

                    b.ToTable("Reportings");
                });

            modelBuilder.Entity("WBLMS.Models.Roles", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.ToTable("Roles");
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
                });

            modelBuilder.Entity("WBLMS.Models.SuperAdmin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GenderId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UpdatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GenderId");

                    b.ToTable("SuperAdmins");
                });

            modelBuilder.Entity("WBLMS.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("TeamLeader")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("TeamLeader");

                    b.ToTable("Teams");
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
                });

            modelBuilder.Entity("WBLMS.Models.WorkingDays", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<bool>("Friday")
                        .HasColumnType("bit");

                    b.Property<bool>("Monday")
                        .HasColumnType("bit");

                    b.Property<bool>("Saturday")
                        .HasColumnType("bit");

                    b.Property<bool>("Sunday")
                        .HasColumnType("bit");

                    b.Property<bool>("Thursday")
                        .HasColumnType("bit");

                    b.Property<bool>("Tuesday")
                        .HasColumnType("bit");

                    b.Property<bool>("Wednesday")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("BranchId")
                        .IsUnique();

                    b.ToTable("WorkingDays");
                });

            modelBuilder.Entity("WBLMS.Models.Branch", b =>
                {
                    b.HasOne("WBLMS.Models.Organization", "Organization")
                        .WithMany("Branches")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("WBLMS.Models.Department", b =>
                {
                    b.HasOne("WBLMS.Models.Branch", "Branch")
                        .WithMany("Departments")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WBLMS.Models.Employee2", "Employee2")
                        .WithMany()
                        .HasForeignKey("DepartmentHead");

                    b.Navigation("Branch");

                    b.Navigation("Employee2");
                });

            modelBuilder.Entity("WBLMS.Models.Employee", b =>
                {
                    b.HasOne("WBLMS.Models.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId");

                    b.HasOne("WBLMS.Models.LeaveBalance", "LeaveBalance")
                        .WithMany()
                        .HasForeignKey("LeaveBalanceId");

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

                    b.Navigation("LeaveBalance");

                    b.Navigation("Manager");

                    b.Navigation("Roles");

                    b.Navigation("Token");
                });

            modelBuilder.Entity("WBLMS.Models.Employee2", b =>
                {
                    b.HasOne("WBLMS.Models.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WBLMS.Models.Reporting", "Reporting")
                        .WithMany()
                        .HasForeignKey("ReportingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WBLMS.Models.Team", "Team")
                        .WithMany("TeamMembers")
                        .HasForeignKey("TeamId");

                    b.Navigation("Gender");

                    b.Navigation("Reporting");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("WBLMS.Models.Holiday", b =>
                {
                    b.HasOne("WBLMS.Models.Branch", "Branch")
                        .WithMany("Holidays")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
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

            modelBuilder.Entity("WBLMS.Models.LeaveSubType", b =>
                {
                    b.HasOne("WBLMS.Models.LeaveType", "LeaveType")
                        .WithMany("LeaveSubTypes")
                        .HasForeignKey("LeaveTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LeaveType");
                });

            modelBuilder.Entity("WBLMS.Models.LeaveType", b =>
                {
                    b.HasOne("WBLMS.Models.Branch", "Branch")
                        .WithMany("LeaveTypes")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("WBLMS.Models.Roles", b =>
                {
                    b.HasOne("WBLMS.Models.Branch", "Branch")
                        .WithMany("Roles")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("WBLMS.Models.SuperAdmin", b =>
                {
                    b.HasOne("WBLMS.Models.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gender");
                });

            modelBuilder.Entity("WBLMS.Models.Team", b =>
                {
                    b.HasOne("WBLMS.Models.Department", "Department")
                        .WithMany("Teams")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WBLMS.Models.Employee2", "Employee2")
                        .WithMany()
                        .HasForeignKey("TeamLeader");

                    b.Navigation("Department");

                    b.Navigation("Employee2");
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

            modelBuilder.Entity("WBLMS.Models.WorkingDays", b =>
                {
                    b.HasOne("WBLMS.Models.Branch", "Branch")
                        .WithOne("WorkingDays")
                        .HasForeignKey("WBLMS.Models.WorkingDays", "BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("WBLMS.Models.Branch", b =>
                {
                    b.Navigation("Departments");

                    b.Navigation("Holidays");

                    b.Navigation("LeaveTypes");

                    b.Navigation("Roles");

                    b.Navigation("WorkingDays")
                        .IsRequired();
                });

            modelBuilder.Entity("WBLMS.Models.Department", b =>
                {
                    b.Navigation("Teams");
                });

            modelBuilder.Entity("WBLMS.Models.Employee", b =>
                {
                    b.Navigation("Subordinates");
                });

            modelBuilder.Entity("WBLMS.Models.LeaveType", b =>
                {
                    b.Navigation("LeaveSubTypes");
                });

            modelBuilder.Entity("WBLMS.Models.Organization", b =>
                {
                    b.Navigation("Branches");
                });

            modelBuilder.Entity("WBLMS.Models.Team", b =>
                {
                    b.Navigation("TeamMembers");
                });
#pragma warning restore 612, 618
        }
    }
}
