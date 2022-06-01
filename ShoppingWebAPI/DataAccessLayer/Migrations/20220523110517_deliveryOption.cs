using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class deliveryOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryOptionId",
                table: "Shippings",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Shippings",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "DeliveryOption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    Price = table.Column<float>(nullable: false),
                    Days = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryOption", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_DeliveryOptionId",
                table: "Shippings",
                column: "DeliveryOptionId",
                unique: true,
                filter: "[DeliveryOptionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Shippings_DeliveryOption_DeliveryOptionId",
                table: "Shippings",
                column: "DeliveryOptionId",
                principalTable: "DeliveryOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shippings_DeliveryOption_DeliveryOptionId",
                table: "Shippings");

            migrationBuilder.DropTable(
                name: "DeliveryOption");

            migrationBuilder.DropIndex(
                name: "IX_Shippings_DeliveryOptionId",
                table: "Shippings");

            migrationBuilder.DropColumn(
                name: "DeliveryOptionId",
                table: "Shippings");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Shippings");
        }
    }
}
