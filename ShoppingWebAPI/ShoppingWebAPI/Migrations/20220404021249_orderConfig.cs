using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingWebAPI.Migrations
{
    public partial class orderConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Shippings_ShippingId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ShippingId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ShippingId",
                table: "OrderItems");

            migrationBuilder.AddColumn<int>(
                name: "ShippingId",
                table: "Orders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "ShippingId",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ShippingId",
                table: "OrderItems",
                column: "ShippingId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Shippings_ShippingId",
                table: "OrderItems",
                column: "ShippingId",
                principalTable: "Shippings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
