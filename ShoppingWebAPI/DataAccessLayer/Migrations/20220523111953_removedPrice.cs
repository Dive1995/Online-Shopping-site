using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class removedPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shippings_DeliveryOptionId",
                table: "Shippings");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Shippings");

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_DeliveryOptionId",
                table: "Shippings",
                column: "DeliveryOptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shippings_DeliveryOptionId",
                table: "Shippings");

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Shippings",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_DeliveryOptionId",
                table: "Shippings",
                column: "DeliveryOptionId",
                unique: true,
                filter: "[DeliveryOptionId] IS NOT NULL");
        }
    }
}
