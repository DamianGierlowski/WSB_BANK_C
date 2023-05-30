using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApp.Migrations
{
    public partial class _8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Account_RecipientId1",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Account_SourceId1",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_RecipientId1",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_SourceId1",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "RecipientId1",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "SourceId1",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "SourceId",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "RecipientId",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a81cdea1-512d-4e9f-b254-d37dc53a1b83", "AQAAAAEAACcQAAAAEKarcRnrF1pjX2vNb4J0jIJo49VXk4BSaYyNxmGHN1C30cvhhtyCqilk9qKmaRbaLQ==", "81bfa285-4179-4123-81d9-fe3bee749ad2" });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_RecipientId",
                table: "Transactions",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SourceId",
                table: "Transactions",
                column: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Account_RecipientId",
                table: "Transactions",
                column: "RecipientId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Account_SourceId",
                table: "Transactions",
                column: "SourceId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Account_RecipientId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Account_SourceId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_RecipientId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_SourceId",
                table: "Transactions");

            migrationBuilder.AlterColumn<string>(
                name: "SourceId",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "RecipientId",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "RecipientId1",
                table: "Transactions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceId1",
                table: "Transactions",
                type: "INTEGER",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Account_RecipientId1",
                table: "Transactions",
                column: "RecipientId1",
                principalTable: "Account",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Account_SourceId1",
                table: "Transactions",
                column: "SourceId1",
                principalTable: "Account",
                principalColumn: "Id");
        }
    }
}
