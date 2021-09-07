using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineBanking.Domain.Migrations
{
    public partial class Databasecreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "AccountType",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Customers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Customers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Accounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "Accounts",
                type: "decimal(38,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "AccountNumber",
                table: "Accounts",
                type: "int",
                maxLength: 10,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Accounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountNumber", "Balance", "CreatedAt", "CreatedBy", "CustomerId", "UpdatedAt", "UpdatedBy", "UserId" },
                values: new object[,]
                {
                    { 1, 211979756, 23456782340m, new DateTime(2021, 9, 7, 16, 5, 16, 192, DateTimeKind.Utc).AddTicks(6793), "Dara", 1, new DateTime(2021, 9, 7, 16, 5, 16, 192, DateTimeKind.Utc).AddTicks(7447), "Dara", null },
                    { 2, 317092802, 23456782340m, new DateTime(2021, 9, 7, 16, 5, 16, 192, DateTimeKind.Utc).AddTicks(9261), "Obinna", 2, new DateTime(2021, 9, 7, 16, 5, 16, 192, DateTimeKind.Utc).AddTicks(9283), "Obinna", null }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "AccountType", "Birthday", "Email", "FirstName", "Gender", "LastName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2000, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "dara@domain.com", "Dara", 1, "Success" },
                    { 2, 1, new DateTime(2004, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "obinna@gmail.com", "Obinna", 0, "Achara" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Customers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "Accounts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38,2)");

            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "AccountType",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
