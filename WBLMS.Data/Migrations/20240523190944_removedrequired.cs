using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBLMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class removedrequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Password",
                value: "4BE8D91B722315C36F224D6E2407827DB4AD7C47D4E60855AD55B5E20D66DD6F:3525B52ED5F01634A602A4420EEF5393:50000:SHA256");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Password",
                value: "43EE1C4BB8885D1ACE73A28204DAAF29FB3881780F29520666C6D1AA1FB7DF9C:AE39C260F6429FE879DC7E925C4FFE9C:50000:SHA256");
        }
    }
}
