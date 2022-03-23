using Microsoft.EntityFrameworkCore.Migrations;

namespace InteriorPlatform.Data.Migrations
{
    public partial class ChangeLikeTypeValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikeType",
                table: "Likes");

            migrationBuilder.AddColumn<byte>(
                name: "Value",
                table: "Likes",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Likes");

            migrationBuilder.AddColumn<int>(
                name: "LikeType",
                table: "Likes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
