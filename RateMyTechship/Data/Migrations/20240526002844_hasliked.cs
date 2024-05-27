using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateMyTechship.Data.Migrations
{
    /// <inheritdoc />
    public partial class hasliked : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasDisliked",
                table: "Review",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasLiked",
                table: "Review",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasDisliked",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "HasLiked",
                table: "Review");
        }
    }
}
