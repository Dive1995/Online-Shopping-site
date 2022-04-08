using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class UpdatedCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PhoneNum",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Shippings",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Shippings",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Shippings",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhoneNum",
                table: "Shippings",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PostalCode",
                table: "Shippings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Customers",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Username",
                value: "Will");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Username",
                value: "Elon");

            migrationBuilder.UpdateData(
                table: "Shippings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "FirstName", "LastName", "PhoneNum", "PostalCode" },
                values: new object[] { "Colombo", "Elon", "Musk", 212223333, 33 });

            migrationBuilder.UpdateData(
                table: "Shippings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "FirstName", "LastName", "PhoneNum", "PostalCode" },
                values: new object[] { "Galle", "Will", "Smith", 212224444, 233 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Shippings");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Shippings");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Shippings");

            migrationBuilder.DropColumn(
                name: "PhoneNum",
                table: "Shippings");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Shippings");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Customers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Customers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Customers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhoneNum",
                table: "Customers",
                type: "int",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PostalCode",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "FirstName", "LastName", "PhoneNum", "PostalCode" },
                values: new object[] { "Colombo", "Will", "Smith", 771231234, 100 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "FirstName", "LastName", "PhoneNum", "PostalCode" },
                values: new object[] { "Galle", "Elon", "Musk", 777461334, 300 });
        }
    }
}
