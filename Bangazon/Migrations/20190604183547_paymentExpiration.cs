using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bangazon.Migrations
{
    public partial class paymentExpiration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "PaymentType",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "PaymentType");

        }
    }
}
