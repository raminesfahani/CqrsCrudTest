using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mc2.CrudTest.Persistence.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PhoneNumber",
                table: "Customers",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "BankAccountNumber",
                table: "Customers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BankAccountNumber", "DateCreated", "LastModifiedDate", "PhoneNumber" },
                values: new object[] { "7617238", new DateTime(2022, 2, 18, 21, 0, 8, 376, DateTimeKind.Local).AddTicks(6147), new DateTime(2022, 2, 18, 21, 0, 8, 379, DateTimeKind.Local).AddTicks(1152), 989120345399m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Customers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<int>(
                name: "BankAccountNumber",
                table: "Customers",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BankAccountNumber", "DateCreated", "LastModifiedDate", "PhoneNumber" },
                values: new object[] { 7617238, new DateTime(2022, 2, 6, 10, 17, 19, 972, DateTimeKind.Local).AddTicks(8000), new DateTime(2022, 2, 6, 10, 17, 19, 977, DateTimeKind.Local).AddTicks(5346), "+989120345399" });
        }
    }
}
