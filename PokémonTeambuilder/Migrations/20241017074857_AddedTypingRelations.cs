using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokémonTeambuilder.Migrations
{
    /// <inheritdoc />
    public partial class AddedTypingRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TypingImmunity",
                columns: table => new
                {
                    ImmunityId = table.Column<int>(type: "int", nullable: false),
                    TypingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypingImmunity", x => new { x.ImmunityId, x.TypingId });
                    table.ForeignKey(
                        name: "FK_TypingImmunity_Typings_ImmunityId",
                        column: x => x.ImmunityId,
                        principalTable: "Typings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TypingImmunity_Typings_TypingId",
                        column: x => x.TypingId,
                        principalTable: "Typings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TypingResistance",
                columns: table => new
                {
                    ResistanceId = table.Column<int>(type: "int", nullable: false),
                    TypingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypingResistance", x => new { x.ResistanceId, x.TypingId });
                    table.ForeignKey(
                        name: "FK_TypingResistance_Typings_ResistanceId",
                        column: x => x.ResistanceId,
                        principalTable: "Typings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TypingResistance_Typings_TypingId",
                        column: x => x.TypingId,
                        principalTable: "Typings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TypingWeakness",
                columns: table => new
                {
                    TypingId = table.Column<int>(type: "int", nullable: false),
                    WeaknessId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypingWeakness", x => new { x.TypingId, x.WeaknessId });
                    table.ForeignKey(
                        name: "FK_TypingWeakness_Typings_TypingId",
                        column: x => x.TypingId,
                        principalTable: "Typings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TypingWeakness_Typings_WeaknessId",
                        column: x => x.WeaknessId,
                        principalTable: "Typings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TypingImmunity_TypingId",
                table: "TypingImmunity",
                column: "TypingId");

            migrationBuilder.CreateIndex(
                name: "IX_TypingResistance_TypingId",
                table: "TypingResistance",
                column: "TypingId");

            migrationBuilder.CreateIndex(
                name: "IX_TypingWeakness_WeaknessId",
                table: "TypingWeakness",
                column: "WeaknessId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TypingImmunity");

            migrationBuilder.DropTable(
                name: "TypingResistance");

            migrationBuilder.DropTable(
                name: "TypingWeakness");
        }
    }
}
