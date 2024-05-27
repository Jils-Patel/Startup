using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateMyTechship.Data.Migrations
{
    /// <inheritdoc />
    public partial class reviewslist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DislikedByUserIds",
                table: "Review",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "LikedByUserIds",
                table: "Review",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DislikedByUserIds",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "LikedByUserIds",
                table: "Review");
        }
    }
}
