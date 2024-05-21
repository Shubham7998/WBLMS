using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WBLMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedWonderbizHolidays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WonderbizHolidays",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Event = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WonderbizHolidays", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Password",
                value: "43EE1C4BB8885D1ACE73A28204DAAF29FB3881780F29520666C6D1AA1FB7DF9C:AE39C260F6429FE879DC7E925C4FFE9C:50000:SHA256");

            migrationBuilder.InsertData(
                table: "WonderbizHolidays",
                columns: new[] { "Id", "Date", "Day", "Event" },
                values: new object[,]
                {
                    { 1L, new DateOnly(2024, 1, 1), "Monday", "New Year" },
                    { 2L, new DateOnly(2024, 1, 26), "Friday", "Republic Day" },
                    { 3L, new DateOnly(2024, 3, 25), "Monday", "Holi" },
                    { 4L, new DateOnly(2024, 4, 11), "Thursday", "Eid" },
                    { 5L, new DateOnly(2024, 5, 1), "Wednesday", "Maharashtra Day" },
                    { 6L, new DateOnly(2024, 8, 15), "Thursday", "Independence Day" },
                    { 7L, new DateOnly(2024, 10, 2), "Wednesday", "Gandhi Jayanti" },
                    { 8L, new DateOnly(2024, 10, 31), "Thursday", "Diwali" },
                    { 9L, new DateOnly(2024, 11, 1), "Friday", "Diwali" },
                    { 10L, new DateOnly(2024, 12, 25), "Wednesday", "Christmas" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WonderbizHolidays");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Password",
                value: "1C65EBCA954C775123390B3EAC54A4124D4B92B627120F98D71866A00E6371C2:2C771C99BADBC2A2D3CDAF81E6801CA2:50000:SHA256");
        }
    }
}
