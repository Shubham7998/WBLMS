using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBLMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class employee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "ContactNumber", "CreatedById", "EmailAddress", "FirstName", "GenderId", "JoiningDate", "LastName", "ManagerId", "Password", "RoleId", "TokenId", "UpdateById", "UpdatedDate" },
                values: new object[] { 1L, "1234567890", null, "admin@gmail.in", "Admin", 1L, null, "Admin", null, "123", 1L, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
