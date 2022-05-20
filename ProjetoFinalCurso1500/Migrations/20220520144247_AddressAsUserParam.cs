using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoFinalCurso1500.Migrations
{
    public partial class AddressAsUserParam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Salesman_UserId",
                table: "Salesman");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Client");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Salesman_UserId",
                table: "Salesman",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Salesman_UserId",
                table: "Salesman");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Client",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Salesman_UserId",
                table: "Salesman",
                column: "UserId");
        }
    }
}
