using Microsoft.EntityFrameworkCore.Migrations;

namespace JobApplication.Data.Migrations
{
    public partial class ModelsChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyNameOffer",
                table: "JobOffers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "JobOffers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "JobOffers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                table: "JobOffers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkingTime",
                table: "JobOffers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppUserEmployeeExtensions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender = table.Column<string>(nullable: true),
                    IsShowing = table.Column<bool>(nullable: false),
                    CVFile = table.Column<string>(nullable: true),
                    Languages = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserEmployeeExtensions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserEmployeeExtensions_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AwardsEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Award = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwardsEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwardsEmployees_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EducationEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<string>(nullable: true),
                    SchoolName = table.Column<string>(nullable: true),
                    DateStartEnd = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationEmployees_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExperiencesEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    DateStartEnd = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperiencesEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperiencesEmployees_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SkillsEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<string>(nullable: true),
                    Percentage = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillsEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillsEmployees_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserEmployeeExtensions_AppUserId",
                table: "AppUserEmployeeExtensions",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AwardsEmployees_AppUserId",
                table: "AwardsEmployees",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationEmployees_AppUserId",
                table: "EducationEmployees",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperiencesEmployees_AppUserId",
                table: "ExperiencesEmployees",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillsEmployees_AppUserId",
                table: "SkillsEmployees",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserEmployeeExtensions");

            migrationBuilder.DropTable(
                name: "AwardsEmployees");

            migrationBuilder.DropTable(
                name: "EducationEmployees");

            migrationBuilder.DropTable(
                name: "ExperiencesEmployees");

            migrationBuilder.DropTable(
                name: "SkillsEmployees");

            migrationBuilder.DropColumn(
                name: "CompanyNameOffer",
                table: "JobOffers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "JobOffers");

            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "JobOffers");

            migrationBuilder.DropColumn(
                name: "Languages",
                table: "JobOffers");

            migrationBuilder.DropColumn(
                name: "WorkingTime",
                table: "JobOffers");
        }
    }
}
