using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoinsCollection.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyAlbum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coins_CoinAlbumPagePosition_AlbumPagePositionID",
                table: "Coins");

            migrationBuilder.DropForeignKey(
                name: "FK_Coins_Pages_CoinAlbumPageID",
                table: "Coins");

            migrationBuilder.DropTable(
                name: "CoinAlbumPagePosition");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Coins_AlbumPagePositionID",
                table: "Coins");

            migrationBuilder.DropIndex(
                name: "IX_Coins_CoinAlbumPageID",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "AlbumPagePositionID",
                table: "Coins");

            migrationBuilder.RenameColumn(
                name: "CoinAlbumPageID",
                table: "Coins",
                newName: "AlbumRow");

            migrationBuilder.RenameColumn(
                name: "DefaultRowsCount",
                table: "Albums",
                newName: "RowsCount");

            migrationBuilder.RenameColumn(
                name: "DefaultColumnsCount",
                table: "Albums",
                newName: "PagesCount");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Countries",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AlbumColumn",
                table: "Coins",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AlbumID",
                table: "Coins",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AlbumPage",
                table: "Coins",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ColumnsCount",
                table: "Albums",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Coins_AlbumID",
                table: "Coins",
                column: "AlbumID");

            migrationBuilder.AddForeignKey(
                name: "FK_Coins_Albums_AlbumID",
                table: "Coins",
                column: "AlbumID",
                principalTable: "Albums",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coins_Albums_AlbumID",
                table: "Coins");

            migrationBuilder.DropIndex(
                name: "IX_Coins_AlbumID",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "AlbumColumn",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "AlbumID",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "AlbumPage",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "ColumnsCount",
                table: "Albums");

            migrationBuilder.RenameColumn(
                name: "AlbumRow",
                table: "Coins",
                newName: "CoinAlbumPageID");

            migrationBuilder.RenameColumn(
                name: "RowsCount",
                table: "Albums",
                newName: "DefaultRowsCount");

            migrationBuilder.RenameColumn(
                name: "PagesCount",
                table: "Albums",
                newName: "DefaultColumnsCount");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "AlbumPagePositionID",
                table: "Coins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlbumID = table.Column<int>(type: "int", nullable: false),
                    ColumnsCount = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    RowsCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pages_Albums_AlbumID",
                        column: x => x.AlbumID,
                        principalTable: "Albums",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoinAlbumPagePosition",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageID = table.Column<int>(type: "int", nullable: false),
                    CoinID = table.Column<int>(type: "int", nullable: false),
                    ColumnPosition = table.Column<int>(type: "int", nullable: false),
                    RowPosition = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinAlbumPagePosition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CoinAlbumPagePosition_Pages_PageID",
                        column: x => x.PageID,
                        principalTable: "Pages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coins_AlbumPagePositionID",
                table: "Coins",
                column: "AlbumPagePositionID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coins_CoinAlbumPageID",
                table: "Coins",
                column: "CoinAlbumPageID");

            migrationBuilder.CreateIndex(
                name: "IX_CoinAlbumPagePosition_PageID",
                table: "CoinAlbumPagePosition",
                column: "PageID");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_AlbumID",
                table: "Pages",
                column: "AlbumID");

            migrationBuilder.AddForeignKey(
                name: "FK_Coins_CoinAlbumPagePosition_AlbumPagePositionID",
                table: "Coins",
                column: "AlbumPagePositionID",
                principalTable: "CoinAlbumPagePosition",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Coins_Pages_CoinAlbumPageID",
                table: "Coins",
                column: "CoinAlbumPageID",
                principalTable: "Pages",
                principalColumn: "ID");
        }
    }
}
