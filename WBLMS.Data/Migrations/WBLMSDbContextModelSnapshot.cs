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

                    b.Property<long>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("GenderId")
                        .HasColumnType("bigint");

                    b.Property<DateOnly>("JoiningDate")
                        .HasColumnType("date");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ManagerId")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("TokenId")
                        .HasColumnType("bigint");

                    b.Property<long>("UpdateById")
                        .HasColumnType("bigint");

                    b.Property<DateOnly>("UpdatedDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("GenderId");

                    b.HasIndex("ManagerId");

                    b.HasIndex("RoleId");

                    b.HasIndex("TokenId");

                    b.HasIndex("EmailAddress", "ContactNumber")
                        .IsUnique();

                    b.ToTable("Employees");
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
                            RoleName = "Employee"
                        },
                        new
                        {
                            Id = 2L,
                            RoleName = "Team Lead"
                        },
                        new
                        {
                            Id = 3L,
                            RoleName = "HR"
                        },
                        new
                        {
                            Id = 4L,
                            RoleName = "Admin"
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

                    b.Property<DateOnly>("PasswordResetExpiry")
                        .HasColumnType("date");

                    b.Property<string>("PasswordResetToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("RefreshTokenExpiry")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("WBLMS.Models.Employee", b =>
                {
                    b.HasOne("WBLMS.Models.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WBLMS.Models.Employee", "Manager")
                        .WithMany("Subordinates")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WBLMS.Models.Roles", "Roles")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WBLMS.Models.Token", "Token")
                        .WithMany()
                        .HasForeignKey("TokenId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

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
                    b.HasOne("WBLMS.Models.LeaveType", "LeaveType")
                        .WithMany()
                        .HasForeignKey("LeaveTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WBLMS.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WBLMS.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("LeaveType");

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
