using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.DataAccess.Migrations
{
    public partial class BirthDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BithDate",
                table: "Employees");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Employees",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c2c75d6d-8f21-4743-875b-6eb33ec81411", "AQAAAAEAACcQAAAAECHQyrTk2EFW6BNMZ5GMpAW7LfQvxgcFDA248evwBrMnbQ0pKEfHMk3TFCNUOcdYdA==", "f0ff1ef8-7427-4503-97fb-ffb2464e28fd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Employees");

            migrationBuilder.AddColumn<DateTime>(
                name: "BithDate",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fdb9a932-b20a-4bd7-9ee7-2ae6346eff3d", "AQAAAAEAACcQAAAAEPUqioXBtw1gHxj7t+vgN8iCCWYoujb3ShGdkupmHmlzZ0f1/gFoFVWOJ9yFVIRhDw==", "8be2e451-5b66-48a2-bbef-4dc5b4f23315" });
        }
    }
}
