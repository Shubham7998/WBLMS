using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBLMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class leaveReqConnected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LeaveBalanceId",
                table: "Employees",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "GenderId", "LeaveBalanceId", "Password" },
                values: new object[] { 2L, null, "976CD11455FD8260FAC223CD683404BBF9FE1A69EF067DC230C7A54FB49D8BE4:51345705419A05A43D74E12B5AC0035C:50000:SHA256" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_LeaveBalanceId",
                table: "Employees",
                column: "LeaveBalanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_LeaveBalances_LeaveBalanceId",
                table: "Employees",
                column: "LeaveBalanceId",
                principalTable: "LeaveBalances",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_LeaveBalances_LeaveBalanceId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_LeaveBalanceId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "LeaveBalanceId",
                table: "Employees");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "GenderId", "Password" },
                values: new object[] { 1L, "4BE8D91B722315C36F224D6E2407827DB4AD7C47D4E60855AD55B5E20D66DD6F:3525B52ED5F01634A602A4420EEF5393:50000:SHA256" });
        }
    }
}
