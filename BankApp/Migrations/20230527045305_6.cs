using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApp.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "29f5e147-9e5a-4ed6-9bb8-781fc7bd86b6", "AQAAAAEAACcQAAAAEO6f06+r+xQHCYJhLINaw0+7mqpVmdJqXzwxLDaQmL4riI42sOXQQPsB1wiaPvYqtA==", "cb316b35-e359-4888-8e01-85e08fa28576" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0217ed19-ca34-42aa-841d-55cfed40012c", "AQAAAAEAACcQAAAAEI1LNzTjZM48UtRlYAYN1RJleUmaIwEFSJANOwrzKgMEruMsPfbKrEFksfZcPcS1LA==", "cf543be6-6ca2-495e-b186-6e888f18e5f5" });
        }
    }
}
