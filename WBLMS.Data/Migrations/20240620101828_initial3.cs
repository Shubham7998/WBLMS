using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBLMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "HeadQuarter", "Name", "SuperAdminId" },
                values: new object[] { 1, "Thane", "WB", 1 });

            migrationBuilder.UpdateData(
                table: "SuperAdmins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Password", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 20, 15, 48, 27, 889, DateTimeKind.Local).AddTicks(1653), "D9A734A67D4E894B9C15F97E450CEE79055E692A7AE7F2CAE81D990171CD1CDF:BF2853E605B3B7EF93B1348B782B1C81:50000:SHA256", new DateTime(2024, 6, 20, 15, 48, 27, 889, DateTimeKind.Local).AddTicks(1664) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "SuperAdmins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Password", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 20, 15, 38, 8, 787, DateTimeKind.Local).AddTicks(1419), "DD1C23E4B3AB50A0E5D961C3B93BE469F82E398DD37E3F492281AE069AF743F2:5CAECA767B732FCA3C8A8D0C6C8D82CB:50000:SHA256", new DateTime(2024, 6, 20, 15, 38, 8, 787, DateTimeKind.Local).AddTicks(1428) });
        }
    }
}
