using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.DataAccess.Migrations
{
    public partial class updateUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "44cdfbf0-a1ec-47aa-a6b5-2fa0dcedf6b5", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEKZnwzEklHIu0k3NaygsUacd6HtqZrnfEpfPU003EwIgZ0djhToXywvt2IZ7nIKP6g==", "f888258c-70f5-46a4-b035-672acad0e20c", "admin@admin.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "4f7f84ed-8d1e-48fb-842c-6854e238b5b6", "ADMIN", "AQAAAAEAACcQAAAAEJDY2ur4HZjWhlexv00dxDF9qTnUzKKu8ID3Ucs8bX7Erttj2zG+EL+s0hgq187Jrw==", "377efc07-8ec4-436f-8f3b-da65f29f424f", "Admin" });
        }
    }
}
