using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBLMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamLeader",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Employee2s",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamLeader",
                table: "Teams",
                column: "TeamLeader");

            migrationBuilder.CreateIndex(
                name: "IX_Employee2s_TeamId",
                table: "Employee2s",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee2s_Teams_TeamId",
                table: "Employee2s",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Employee2s_TeamLeader",
                table: "Teams",
                column: "TeamLeader",
                principalTable: "Employee2s",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee2s_Teams_TeamId",
                table: "Employee2s");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Employee2s_TeamLeader",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamLeader",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Employee2s_TeamId",
                table: "Employee2s");

            migrationBuilder.DropColumn(
                name: "TeamLeader",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Employee2s");
        }
    }
}
