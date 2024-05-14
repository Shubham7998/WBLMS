using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBLMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmployeeDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "ContactNumber", "CreatedById", "EmailAddress", "FirstName", "GenderId", "JoiningDate", "LastName", "ManagerId", "Password", "RoleId", "TokenId", "UpdateById", "UpdatedDate" },
                values: new object[] { 1L, "9874563210", null, "hemant.patel@wonderbiz.in", "Hemant", 1L, null, "Patel", null, "1C65EBCA954C775123390B3EAC54A4124D4B92B627120F98D71866A00E6371C2:2C771C99BADBC2A2D3CDAF81E6801CA2:50000:SHA256", 1L, null, null, null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "RoleName",
                value: "Admin");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "RoleName",
                value: "HR");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "RoleName",
                value: "Team Lead");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "RoleName",
                value: "Employee");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "RoleName",
                value: "Employee");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "RoleName",
                value: "Team Lead");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "RoleName",
                value: "HR");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "RoleName",
                value: "Admin");
        }
    }
}
