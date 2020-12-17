using Microsoft.EntityFrameworkCore.Migrations;

namespace LogitWebApp.Migrations
{
    public partial class WhenDeleteOrderIDeleteandAllMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Orders_OrderId",
                table: "Messages");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Orders_OrderId",
                table: "Messages",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Orders_OrderId",
                table: "Messages");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Orders_OrderId",
                table: "Messages",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
