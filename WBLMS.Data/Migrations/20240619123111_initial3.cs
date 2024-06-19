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
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Employee2s_TeamLeader",
                table: "Teams");

            migrationBuilder.AlterColumn<int>(
                name: "TeamLeader",
                table: "Teams",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentHead",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_DepartmentHead",
                table: "Departments",
                column: "DepartmentHead");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Employee2s_DepartmentHead",
                table: "Departments",
                column: "DepartmentHead",
                principalTable: "Employee2s",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Employee2s_TeamLeader",
                table: "Teams",
                column: "TeamLeader",
                principalTable: "Employee2s",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employee2s_DepartmentHead",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Employee2s_TeamLeader",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Departments_DepartmentHead",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DepartmentHead",
                table: "Departments");

            migrationBuilder.AlterColumn<int>(
                name: "TeamLeader",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Employee2s_TeamLeader",
                table: "Teams",
                column: "TeamLeader",
                principalTable: "Employee2s",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
