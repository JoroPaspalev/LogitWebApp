using Microsoft.EntityFrameworkCore.Migrations;

namespace LogitWebApp.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromInt",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "ToInt",
                table: "Shipments");

            migrationBuilder.AddColumn<int>(
                name: "FromCityId",
                table: "Shipments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToCityId",
                table: "Shipments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_FromCityId",
                table: "Shipments",
                column: "FromCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_ToCityId",
                table: "Shipments",
                column: "ToCityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Cities_FromCityId",
                table: "Shipments",
                column: "FromCityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Cities_ToCityId",
                table: "Shipments",
                column: "ToCityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Cities_FromCityId",
                table: "Shipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Cities_ToCityId",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_FromCityId",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_ToCityId",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "FromCityId",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "ToCityId",
                table: "Shipments");

            migrationBuilder.AddColumn<int>(
                name: "FromInt",
                table: "Shipments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToInt",
                table: "Shipments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
