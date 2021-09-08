using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineBanking.Domain.Migrations
{
    public partial class CreatedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    AccountNumber = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(38,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountNumber", "Balance", "CreatedAt", "CreatedBy", "CustomerId", "UpdatedAt", "UpdatedBy", "UserId" },
                values: new object[,]
                {
                    { 1, 211979756, 23456782340m, new DateTime(2021, 9, 7, 23, 21, 11, 653, DateTimeKind.Utc).AddTicks(1519), "Dara", 1, new DateTime(2021, 9, 7, 23, 21, 11, 653, DateTimeKind.Utc).AddTicks(2115), "Dara", null },
                    { 2, 317092802, 23456782340m, new DateTime(2021, 9, 7, 23, 21, 11, 653, DateTimeKind.Utc).AddTicks(3808), "Obinna", 2, new DateTime(2021, 9, 7, 23, 21, 11, 653, DateTimeKind.Utc).AddTicks(3836), "Obinna", null }
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
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
