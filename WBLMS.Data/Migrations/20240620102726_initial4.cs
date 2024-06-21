using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBLMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Reportings",
                columns: new[] { "Id", "ReportFrom", "ReportTo" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.UpdateData(
                table: "SuperAdmins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Password", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 20, 15, 57, 25, 672, DateTimeKind.Local).AddTicks(1705), "6D126D2F240A0078942C1C507E0746E7325C2F6942F4666CA3202B6CAFAED324:983161A24A633FA3AFC6B32F09CB6C92:50000:SHA256", new DateTime(2024, 6, 20, 15, 57, 25, 672, DateTimeKind.Local).AddTicks(1714) });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "ContactNumber", "CreatedById", "CreatedDate", "DOB", "FirstName", "GenderId", "LastName", "ReportingId", "UpdatedById", "UpdatedDate" },
                values: new object[] { 1, "998889879", 0, new DateTime(2024, 6, 20, 15, 57, 25, 672, DateTimeKind.Local).AddTicks(1802), new DateOnly(2024, 1, 1), "Admin", 1, "Admin", 1, 0, new DateTime(2024, 6, 20, 15, 57, 25, 672, DateTimeKind.Local).AddTicks(1802) });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Id", "Address", "BranchHeadId", "Name", "OrganizationId" },
                values: new object[] { 1, "Address", 1, "Thane", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reportings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "SuperAdmins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Password", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 20, 15, 48, 27, 889, DateTimeKind.Local).AddTicks(1653), "D9A734A67D4E894B9C15F97E450CEE79055E692A7AE7F2CAE81D990171CD1CDF:BF2853E605B3B7EF93B1348B782B1C81:50000:SHA256", new DateTime(2024, 6, 20, 15, 48, 27, 889, DateTimeKind.Local).AddTicks(1664) });
        }
    }
}
