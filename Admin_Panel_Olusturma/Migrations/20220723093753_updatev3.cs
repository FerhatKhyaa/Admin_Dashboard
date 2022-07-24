using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin_Panel_Olusturma.Migrations
{
    public partial class updatev3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Urunler",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Urunler");
        }
    }
}
