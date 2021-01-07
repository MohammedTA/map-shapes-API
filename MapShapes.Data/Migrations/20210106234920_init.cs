using Microsoft.EntityFrameworkCore.Migrations;

namespace MapShapes.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShapeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShapeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OverlayShapes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShapeTypeId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverlayShapes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OverlayShapes_ShapeTypes_ShapeTypeId",
                        column: x => x.ShapeTypeId,
                        principalTable: "ShapeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OverlayShapes_ShapeTypeId",
                table: "OverlayShapes",
                column: "ShapeTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OverlayShapes");

            migrationBuilder.DropTable(
                name: "ShapeTypes");
        }
    }
}
