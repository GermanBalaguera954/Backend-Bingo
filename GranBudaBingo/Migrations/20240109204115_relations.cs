using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GranBudaBingo.Migrations
{
    /// <inheritdoc />
    public partial class relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BingoGames",
                columns: table => new
                {
                    BingoGameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BingoGames", x => x.BingoGameId);
                });

            migrationBuilder.CreateTable(
                name: "BingoBalls",
                columns: table => new
                {
                    BingoBallId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BingoGameId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Column = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BingoBalls", x => x.BingoBallId);
                    table.ForeignKey(
                        name: "FK_BingoBalls_BingoGames_BingoGameId",
                        column: x => x.BingoGameId,
                        principalTable: "BingoGames",
                        principalColumn: "BingoGameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BingoCards",
                columns: table => new
                {
                    BingoCardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BingoGameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BingoCards", x => x.BingoCardId);
                    table.ForeignKey(
                        name: "FK_BingoCards_BingoGames_BingoGameId",
                        column: x => x.BingoGameId,
                        principalTable: "BingoGames",
                        principalColumn: "BingoGameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BingoBalls_BingoGameId",
                table: "BingoBalls",
                column: "BingoGameId");

            migrationBuilder.CreateIndex(
                name: "IX_BingoCards_BingoGameId",
                table: "BingoCards",
                column: "BingoGameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BingoBalls");

            migrationBuilder.DropTable(
                name: "BingoCards");

            migrationBuilder.DropTable(
                name: "BingoGames");
        }
    }
}
