namespace InteriorPlatform.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddtionalTableStyle2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Styles_StyleId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_StyleId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "StyleId",
                table: "Projects");

            migrationBuilder.CreateTable(
                name: "ProjectStyle",
                columns: table => new
                {
                    ProjectsId = table.Column<int>(type: "int", nullable: false),
                    StylesId = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStyle", x => new { x.ProjectsId, x.StylesId });
                    table.ForeignKey(
                        name: "FK_ProjectStyle_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectStyle_Styles_StylesId",
                        column: x => x.StylesId,
                        principalTable: "Styles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectStyle_StylesId",
                table: "ProjectStyle",
                column: "StylesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectStyle");

            migrationBuilder.AddColumn<int>(
                name: "StyleId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_StyleId",
                table: "Projects",
                column: "StyleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Styles_StyleId",
                table: "Projects",
                column: "StyleId",
                principalTable: "Styles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
