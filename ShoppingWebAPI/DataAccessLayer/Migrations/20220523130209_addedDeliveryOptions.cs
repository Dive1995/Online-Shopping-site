using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class addedDeliveryOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shippings_DeliveryOption_DeliveryOptionId",
                table: "Shippings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryOption",
                table: "DeliveryOption");

            migrationBuilder.RenameTable(
                name: "DeliveryOption",
                newName: "DeliveryOptions");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryOptionId",
                table: "Shippings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryOptions",
                table: "DeliveryOptions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shippings_DeliveryOptions_DeliveryOptionId",
                table: "Shippings",
                column: "DeliveryOptionId",
                principalTable: "DeliveryOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shippings_DeliveryOptions_DeliveryOptionId",
                table: "Shippings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryOptions",
                table: "DeliveryOptions");

            migrationBuilder.RenameTable(
                name: "DeliveryOptions",
                newName: "DeliveryOption");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryOptionId",
                table: "Shippings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryOption",
                table: "DeliveryOption",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shippings_DeliveryOption_DeliveryOptionId",
                table: "Shippings",
                column: "DeliveryOptionId",
                principalTable: "DeliveryOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
