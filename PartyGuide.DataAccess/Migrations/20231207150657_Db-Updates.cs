using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartyGuide.DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class DbUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ServiceTable",
                newName: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ServiceTable",
                newName: "Id");
        }
    }
}
