using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin_Panel_Olusturma.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogKategorileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Durum = table.Column<int>(type: "int", nullable: false),
                    KategoriId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogKategorileri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogKategorileri_BlogKategorileri_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "BlogKategorileri",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bloglar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriId = table.Column<int>(type: "int", nullable: false),
                    Durum = table.Column<int>(type: "int", nullable: false),
                    Baslik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Resim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KisaAciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icerik = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bloglar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bloglar_BlogKategorileri_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "BlogKategorileri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogKategorileri_KategoriId",
                table: "BlogKategorileri",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_Bloglar_KategoriId",
                table: "Bloglar",
                column: "KategoriId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bloglar");

            migrationBuilder.DropTable(
                name: "BlogKategorileri");
        }
    }
}
