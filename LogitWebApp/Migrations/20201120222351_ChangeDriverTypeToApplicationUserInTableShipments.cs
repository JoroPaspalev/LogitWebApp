using Microsoft.EntityFrameworkCore.Migrations;

namespace LogitWebApp.Migrations
{
    public partial class ChangeDriverTypeToApplicationUserInTableShipments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Drivers_DriverId",
                table: "Shipments");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_AspNetUsers_DriverId",
                table: "Shipments",
                column: "DriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_AspNetUsers_DriverId",
                table: "Shipments");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Drivers_DriverId",
                table: "Shipments",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
