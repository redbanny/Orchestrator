using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrchestratorAPI.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Turns",
                columns: table => new
                {
                    TurnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurnName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turns", x => x.TurnId);
                });

            migrationBuilder.CreateTable(
                name: "TurnItems",
                columns: table => new
                {
                    TurnItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurnItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TurnId = table.Column<int>(type: "int", nullable: false),
                    Item_Status = table.Column<int>(type: "int", nullable: false),
                    Create_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InputDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurnItems", x => x.TurnItemId);
                    table.ForeignKey(
                        name: "FK_TurnItems_Turns_TurnId",
                        column: x => x.TurnId,
                        principalTable: "Turns",
                        principalColumn: "TurnId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TurnItems_TurnId",
                table: "TurnItems",
                column: "TurnId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TurnItems");

            migrationBuilder.DropTable(
                name: "Turns");
        }
    }
}
