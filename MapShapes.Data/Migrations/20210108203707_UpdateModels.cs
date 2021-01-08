using Microsoft.EntityFrameworkCore.Migrations;

namespace MapShapes.Data.Migrations
{
    public partial class UpdateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OverlayShapes_ShapeTypes_ShapeTypeId",
                table: "OverlayShapes");

            migrationBuilder.DropTable(
                name: "ShapeTypes");

            migrationBuilder.DropIndex(
                name: "IX_OverlayShapes_ShapeTypeId",
                table: "OverlayShapes");

            migrationBuilder.RenameColumn(
                name: "ShapeTypeId",
                table: "OverlayShapes",
                newName: "Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "OverlayShapes",
                newName: "ShapeTypeId");

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

            migrationBuilder.CreateIndex(
                name: "IX_OverlayShapes_ShapeTypeId",
                table: "OverlayShapes",
                column: "ShapeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OverlayShapes_ShapeTypes_ShapeTypeId",
                table: "OverlayShapes",
                column: "ShapeTypeId",
                principalTable: "ShapeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
