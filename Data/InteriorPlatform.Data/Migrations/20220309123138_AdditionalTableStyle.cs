using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InteriorPlatform.Data.Migrations
{
    public partial class AdditionalTableStyle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StyleId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Styles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Styles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_StyleId",
                table: "Projects",
                column: "StyleId");

            migrationBuilder.CreateIndex(
                name: "IX_Styles_IsDeleted",
                table: "Styles",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Styles_StyleId",
                table: "Projects",
                column: "StyleId",
                principalTable: "Styles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Styles_StyleId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "Styles");

            migrationBuilder.DropIndex(
                name: "IX_Projects_StyleId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "StyleId",
                table: "Projects");
        }
    }
}
