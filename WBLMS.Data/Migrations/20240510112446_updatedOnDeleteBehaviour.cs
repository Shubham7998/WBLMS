using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBLMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedOnDeleteBehaviour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_ManagerId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Tokens_TokenId",
                table: "Employees");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_ManagerId",
                table: "Employees",
                column: "ManagerId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Tokens_TokenId",
                table: "Employees",
                column: "TokenId",
                principalTable: "Tokens",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_ManagerId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Tokens_TokenId",
                table: "Employees");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_ManagerId",
                table: "Employees",
                column: "ManagerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Tokens_TokenId",
                table: "Employees",
                column: "TokenId",
                principalTable: "Tokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
