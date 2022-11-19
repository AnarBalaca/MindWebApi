using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mind.Data.Migrations
{
    public partial class ImagesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Blog_BlogId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Psychologists_PsychologistId",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "PsychologistId",
                table: "Images",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BlogId",
                table: "Images",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Blog_BlogId",
                table: "Images",
                column: "BlogId",
                principalTable: "Blog",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Psychologists_PsychologistId",
                table: "Images",
                column: "PsychologistId",
                principalTable: "Psychologists",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Blog_BlogId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Psychologists_PsychologistId",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "PsychologistId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BlogId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Blog_BlogId",
                table: "Images",
                column: "BlogId",
                principalTable: "Blog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Psychologists_PsychologistId",
                table: "Images",
                column: "PsychologistId",
                principalTable: "Psychologists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
