using Microsoft.EntityFrameworkCore.Migrations;

namespace InteriorPlatform.Data.Migrations
{
    public partial class AddedCloudinaryImageModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfilePictureId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CloudImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PicturePublicId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloudImage", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfilePictureId",
                table: "AspNetUsers",
                column: "ProfilePictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CloudImage_ProfilePictureId",
                table: "AspNetUsers",
                column: "ProfilePictureId",
                principalTable: "CloudImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CloudImage_ProfilePictureId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CloudImage");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProfilePictureId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePictureId",
                table: "AspNetUsers");
        }
    }
}
