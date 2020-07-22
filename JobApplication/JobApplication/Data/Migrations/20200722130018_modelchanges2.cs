using Microsoft.EntityFrameworkCore.Migrations;

namespace JobApplication.Data.Migrations
{
    public partial class modelchanges2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IsShowing",
                table: "AppUserEmployeeExtensions",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "ExperiencesProfile",
                table: "AppUserEmployeeExtensions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Levels",
                table: "AppUserEmployeeExtensions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Localization",
                table: "AppUserEmployeeExtensions",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PaymentMinProfile",
                table: "AppUserEmployeeExtensions",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "SkillsProfile",
                table: "AppUserEmployeeExtensions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypePreferes",
                table: "AppUserEmployeeExtensions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExperiencesProfile",
                table: "AppUserEmployeeExtensions");

            migrationBuilder.DropColumn(
                name: "Levels",
                table: "AppUserEmployeeExtensions");

            migrationBuilder.DropColumn(
                name: "Localization",
                table: "AppUserEmployeeExtensions");

            migrationBuilder.DropColumn(
                name: "PaymentMinProfile",
                table: "AppUserEmployeeExtensions");

            migrationBuilder.DropColumn(
                name: "SkillsProfile",
                table: "AppUserEmployeeExtensions");

            migrationBuilder.DropColumn(
                name: "TypePreferes",
                table: "AppUserEmployeeExtensions");

            migrationBuilder.AlterColumn<bool>(
                name: "IsShowing",
                table: "AppUserEmployeeExtensions",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
