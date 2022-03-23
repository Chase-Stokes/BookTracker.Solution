using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Migrations
{
    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copies_Books_BookId1",
                table: "Copies");

            migrationBuilder.DropIndex(
                name: "IX_Copies_BookId1",
                table: "Copies");

            migrationBuilder.DropColumn(
                name: "BookId1",
                table: "Copies");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Copies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Copies_BookId",
                table: "Copies",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_Books_BookId",
                table: "Copies",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copies_Books_BookId",
                table: "Copies");

            migrationBuilder.DropIndex(
                name: "IX_Copies_BookId",
                table: "Copies");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Copies");

            migrationBuilder.AddColumn<int>(
                name: "BookId1",
                table: "Copies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Copies_BookId1",
                table: "Copies",
                column: "BookId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_Books_BookId1",
                table: "Copies",
                column: "BookId1",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
