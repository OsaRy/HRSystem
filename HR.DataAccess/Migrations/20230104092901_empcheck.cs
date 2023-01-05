using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.DataAccess.Migrations
{
    public partial class empcheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Emp_ID",
                table: "EmployeeLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d3edadd8-823c-4859-8fd4-69567983a021", "AQAAAAEAACcQAAAAEDMNDQRgs+PPr9ltOxohR6flRbA8zZxdiGgNKxWDBeyhC4TlW4oP80PkGOH2BHrkLg==", "b4240a1e-37d6-466e-ab4b-626a880b61bf" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLogs_Emp_ID",
                table: "EmployeeLogs",
                column: "Emp_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLogs_Employees_Emp_ID",
                table: "EmployeeLogs",
                column: "Emp_ID",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLogs_Employees_Emp_ID",
                table: "EmployeeLogs");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeLogs_Emp_ID",
                table: "EmployeeLogs");

            migrationBuilder.DropColumn(
                name: "Emp_ID",
                table: "EmployeeLogs");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "44cdfbf0-a1ec-47aa-a6b5-2fa0dcedf6b5", "AQAAAAEAACcQAAAAEKZnwzEklHIu0k3NaygsUacd6HtqZrnfEpfPU003EwIgZ0djhToXywvt2IZ7nIKP6g==", "f888258c-70f5-46a4-b035-672acad0e20c" });
        }
    }
}
