using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartyGuide.DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class RATINGSYSTEM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NUMBER_OF_RATINGS",
                table: "ServiceTable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "RATING",
                table: "ServiceTable",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NUMBER_OF_RATINGS",
                table: "ServiceTable");

            migrationBuilder.DropColumn(
                name: "RATING",
                table: "ServiceTable");
        }
    }
}
