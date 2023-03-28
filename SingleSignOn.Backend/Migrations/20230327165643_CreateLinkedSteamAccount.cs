using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SingleSignOn.Backend.Migrations
{
    /// <inheritdoc />
    public partial class CreateLinkedSteamAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinkedSteamAccounts",
                columns: table => new
                {
                    steam_id = table.Column<string>(type: "TEXT", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkedSteamAccounts", x => x.steam_id);
                    table.ForeignKey(
                        name: "FK_LinkedSteamAccounts_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkedSteamAccounts_ApplicationUserId",
                table: "LinkedSteamAccounts",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkedSteamAccounts");
        }
    }
}
