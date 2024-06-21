using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WBLMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 20, 18, 25, 4, 68, DateTimeKind.Local).AddTicks(7410), new DateTime(2024, 6, 20, 18, 25, 4, 68, DateTimeKind.Local).AddTicks(7453) });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "BranchId", "DepartmentHeadId", "Name" },
                values: new object[,]
                {
                    { 1, 1, null, "IT" },
                    { 2, 1, null, "HR" }
                });

            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "Id", "BranchId", "LeaveTypeName", "MaxDays" },
                values: new object[,]
                {
                    { 1, 1, "Paid", 21m },
                    { 2, 1, "UnPaid", 0m },
                    { 3, 1, "Other", 0m }
                });

            migrationBuilder.UpdateData(
                table: "SuperAdmins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Password", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 20, 18, 25, 4, 68, DateTimeKind.Local).AddTicks(6876), "380724551D05CF358E82CFC8D17B3188079E09EBC4A7FE3492891290D00CA14E:DD13E8B2A16B30AEB78D86364ADAE14A:50000:SHA256", new DateTime(2024, 6, 20, 18, 25, 4, 68, DateTimeKind.Local).AddTicks(6892) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 20, 15, 57, 25, 672, DateTimeKind.Local).AddTicks(1802), new DateTime(2024, 6, 20, 15, 57, 25, 672, DateTimeKind.Local).AddTicks(1802) });

            migrationBuilder.UpdateData(
                table: "SuperAdmins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Password", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 20, 15, 57, 25, 672, DateTimeKind.Local).AddTicks(1705), "6D126D2F240A0078942C1C507E0746E7325C2F6942F4666CA3202B6CAFAED324:983161A24A633FA3AFC6B32F09CB6C92:50000:SHA256", new DateTime(2024, 6, 20, 15, 57, 25, 672, DateTimeKind.Local).AddTicks(1714) });
        }
    }
}
