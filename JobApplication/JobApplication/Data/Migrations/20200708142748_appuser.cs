using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobApplication.Data.Migrations
{
    public partial class appuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BackgroundImage",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Categories",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanySize",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Descrption",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacebookProfile",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FoundingDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Gallery",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Headline",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedinProfile",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterProfile",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VimeoProfile",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YoutubeProfile",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BackgroundImage",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Categories",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CompanySize",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Descrption",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FacebookProfile",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FoundingDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gallery",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Headline",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LinkedinProfile",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TwitterProfile",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VimeoProfile",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "YoutubeProfile",
                table: "AspNetUsers");
        }
    }
}
