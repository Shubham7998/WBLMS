using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WBLMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "GenderName" },
                values: new object[,]
                {
                    { 1, "Female" },
                    { 2, "Male" },
                    { 3, "Others" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[,]
                {
                    { 1L, "Pending" },
                    { 2L, "Approved" },
                    { 3L, "Rejected" }
                });

            migrationBuilder.InsertData(
                table: "SuperAdmins",
                columns: new[] { "Id", "ContactNumber", "CreatedById", "CreatedDate", "EmailAddress", "FirstName", "GenderId", "LastName", "Password", "ProfilePicture", "TokenId", "UpdatedById", "UpdatedDate" },
                values: new object[] { 1, "9874563210", 0, new DateTime(2024, 6, 20, 15, 38, 8, 787, DateTimeKind.Local).AddTicks(1419), "hemant.patel@wonderbiz.in", "Hemant", 2, "Patel", "DD1C23E4B3AB50A0E5D961C3B93BE469F82E398DD37E3F492281AE069AF743F2:5CAECA767B732FCA3C8A8D0C6C8D82CB:50000:SHA256", null, null, 0, new DateTime(2024, 6, 20, 15, 38, 8, 787, DateTimeKind.Local).AddTicks(1428) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "SuperAdmins",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
