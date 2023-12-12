using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartyGuide.DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class extendeddescr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EXTENDED_DESCRIPTION",
                table: "ServiceTable",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EXTENDED_DESCRIPTION",
                table: "ServiceTable");
        }
    }
}
