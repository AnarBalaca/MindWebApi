using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mind.Data.Migrations
{
    public partial class UpdatedPsychologist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Psychologists_PsychologistId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PsychologistId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PsychologistId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Psychologists",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Psychologists_UserId",
                table: "Psychologists",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Psychologists_AspNetUsers_UserId",
                table: "Psychologists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Psychologists_AspNetUsers_UserId",
                table: "Psychologists");

            migrationBuilder.DropIndex(
                name: "IX_Psychologists_UserId",
                table: "Psychologists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Psychologists");

            migrationBuilder.AddColumn<int>(
                name: "PsychologistId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PsychologistId",
                table: "AspNetUsers",
                column: "PsychologistId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Psychologists_PsychologistId",
                table: "AspNetUsers",
                column: "PsychologistId",
                principalTable: "Psychologists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
