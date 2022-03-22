using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalCopies",
                table: "Copies",
                newName: "PatronId");

            migrationBuilder.AddColumn<int>(
                name: "BookId1",
                table: "Copies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Checkout",
                table: "Copies",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Copies",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Search",
                table: "Authors",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Copies_BookId1",
                table: "Copies",
                column: "BookId1");

            migrationBuilder.CreateIndex(
                name: "IX_Copies_PatronId",
                table: "Copies",
                column: "PatronId");

            migrationBuilder.CreateIndex(
                name: "IX_Copies_UserId",
                table: "Copies",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_AspNetUsers_UserId",
                table: "Copies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_Books_BookId1",
                table: "Copies",
                column: "BookId1",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_Patrons_PatronId",
                table: "Copies",
                column: "PatronId",
                principalTable: "Patrons",
                principalColumn: "PatronId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copies_AspNetUsers_UserId",
                table: "Copies");

            migrationBuilder.DropForeignKey(
                name: "FK_Copies_Books_BookId1",
                table: "Copies");

            migrationBuilder.DropForeignKey(
                name: "FK_Copies_Patrons_PatronId",
                table: "Copies");

            migrationBuilder.DropIndex(
                name: "IX_Copies_BookId1",
                table: "Copies");

            migrationBuilder.DropIndex(
                name: "IX_Copies_PatronId",
                table: "Copies");

            migrationBuilder.DropIndex(
                name: "IX_Copies_UserId",
                table: "Copies");

            migrationBuilder.DropColumn(
                name: "BookId1",
                table: "Copies");

            migrationBuilder.DropColumn(
                name: "Checkout",
                table: "Copies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Copies");

            migrationBuilder.DropColumn(
                name: "Search",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "PatronId",
                table: "Copies",
                newName: "TotalCopies");
        }
    }
}
