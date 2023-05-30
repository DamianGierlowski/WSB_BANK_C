using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApp.Migrations
{
    public partial class _7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SourceId = table.Column<string>(type: "TEXT", nullable: false),
                    SourceId1 = table.Column<int>(type: "INTEGER", nullable: true),
                    RecipientId = table.Column<string>(type: "TEXT", nullable: false),
                    RecipientId1 = table.Column<int>(type: "INTEGER", nullable: true),
                    Value = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Account_RecipientId1",
                        column: x => x.RecipientId1,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Account_SourceId1",
                        column: x => x.SourceId1,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4e351ffa-f8f4-4921-af30-e4e0732959de", "AQAAAAEAACcQAAAAECW0hADmdwbYatplIh7e1rxUS7RgZgvj3ArIr8bFny9ZkX8M3LreDS4WTGSOrB6xXQ==", "436e59ee-f10f-40f0-80e1-43d5c51a2274" });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_RecipientId1",
                table: "Transactions",
                column: "RecipientId1");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SourceId1",
                table: "Transactions",
                column: "SourceId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "29f5e147-9e5a-4ed6-9bb8-781fc7bd86b6", "AQAAAAEAACcQAAAAEO6f06+r+xQHCYJhLINaw0+7mqpVmdJqXzwxLDaQmL4riI42sOXQQPsB1wiaPvYqtA==", "cb316b35-e359-4888-8e01-85e08fa28576" });
        }
    }
}
