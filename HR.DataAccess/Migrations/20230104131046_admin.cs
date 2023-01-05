using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.DataAccess.Migrations
{
    public partial class admin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "b74ddd14-6340-4840-95c2-db12554843e5", "fab4fac1-c546-41de-aebc-a14da6895711" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fdb9a932-b20a-4bd7-9ee7-2ae6346eff3d", "AQAAAAEAACcQAAAAEPUqioXBtw1gHxj7t+vgN8iCCWYoujb3ShGdkupmHmlzZ0f1/gFoFVWOJ9yFVIRhDw==", "8be2e451-5b66-48a2-bbef-4dc5b4f23315" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "b74ddd14-6340-4840-95c2-db12554843e5", "fab4fac1-c546-41de-aebc-a14da6895711" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d3edadd8-823c-4859-8fd4-69567983a021", "AQAAAAEAACcQAAAAEDMNDQRgs+PPr9ltOxohR6flRbA8zZxdiGgNKxWDBeyhC4TlW4oP80PkGOH2BHrkLg==", "b4240a1e-37d6-466e-ab4b-626a880b61bf" });
        }
    }
}
