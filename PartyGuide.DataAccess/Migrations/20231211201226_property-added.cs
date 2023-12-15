using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartyGuide.DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class propertyadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "STARTPRICERANGE",
                table: "ServiceTable",
                newName: "START_PRICE_RANGE");

            migrationBuilder.RenameColumn(
                name: "PHONENUMBER",
                table: "ServiceTable",
                newName: "PHONE_NUMBER");

            migrationBuilder.RenameColumn(
                name: "ENDPRICERANGE",
                table: "ServiceTable",
                newName: "END_PRICE_RANGE");

            migrationBuilder.AddColumn<string>(
                name: "CREATED_BY",
                table: "ServiceTable",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CREATED_BY",
                table: "ServiceTable");

            migrationBuilder.RenameColumn(
                name: "START_PRICE_RANGE",
                table: "ServiceTable",
                newName: "STARTPRICERANGE");

            migrationBuilder.RenameColumn(
                name: "PHONE_NUMBER",
                table: "ServiceTable",
                newName: "PHONENUMBER");

            migrationBuilder.RenameColumn(
                name: "END_PRICE_RANGE",
                table: "ServiceTable",
                newName: "ENDPRICERANGE");
        }
    }
}
