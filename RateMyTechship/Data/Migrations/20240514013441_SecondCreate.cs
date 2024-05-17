using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateMyTechship.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "LearningOpportunities",
                table: "Review",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NetworkingOpportunities",
                table: "Review",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkCulture",
                table: "Review",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Workload",
                table: "Review",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "LearningOpportunities",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "NetworkingOpportunities",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "WorkCulture",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "Workload",
                table: "Review");
        }
    }
}
