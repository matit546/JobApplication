using Microsoft.EntityFrameworkCore.Migrations;

namespace JobApplication.Data.Migrations
{
    public partial class foreignKeyinOfferAPplied : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OffersApplied_JobOffers_JobOfferId",
                table: "OffersApplied");

            migrationBuilder.DropIndex(
                name: "IX_OffersApplied_JobOfferId",
                table: "OffersApplied");

            migrationBuilder.DropColumn(
                name: "JobOfferId",
                table: "OffersApplied");

            migrationBuilder.AlterColumn<decimal>(
                name: "PaymentMin",
                table: "JobOffers",
                type: "decimal(15, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PaymentMax",
                table: "JobOffers",
                type: "decimal(15, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "JobOffers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfViews",
                table: "JobOffers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OffersApplied_OfferId",
                table: "OffersApplied",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_OffersApplied_JobOffers_OfferId",
                table: "OffersApplied",
                column: "OfferId",
                principalTable: "JobOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OffersApplied_JobOffers_OfferId",
                table: "OffersApplied");

            migrationBuilder.DropIndex(
                name: "IX_OffersApplied_OfferId",
                table: "OffersApplied");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "JobOffers");

            migrationBuilder.DropColumn(
                name: "NumberOfViews",
                table: "JobOffers");

            migrationBuilder.AddColumn<int>(
                name: "JobOfferId",
                table: "OffersApplied",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PaymentMin",
                table: "JobOffers",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PaymentMax",
                table: "JobOffers",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15, 2)");

            migrationBuilder.CreateIndex(
                name: "IX_OffersApplied_JobOfferId",
                table: "OffersApplied",
                column: "JobOfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_OffersApplied_JobOffers_JobOfferId",
                table: "OffersApplied",
                column: "JobOfferId",
                principalTable: "JobOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
