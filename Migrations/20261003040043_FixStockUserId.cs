using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Site_Cadastro.Migrations
{
    /// <inheritdoc />
    public partial class FixStockUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Users_UserId1",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_UserId1",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Stocks");

            migrationBuilder.Sql("""
                ALTER TABLE "Stocks"
                ALTER COLUMN "UserId" TYPE integer
                USING "UserId"::integer;
            """);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_UserId",
                table: "Stocks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Users_UserId",
                table: "Stocks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Users_UserId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_UserId",
                table: "Stocks");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Stocks",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Stocks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_UserId1",
                table: "Stocks",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Users_UserId1",
                table: "Stocks",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
