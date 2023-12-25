using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartyGuide.DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class DbUpdates2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CATEGORY",
                table: "ServiceTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DESCRIPTION",
                table: "ServiceTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ENDPRICERANGE",
                table: "ServiceTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "IMAGE",
                table: "ServiceTable",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LOCATION",
                table: "ServiceTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PHONENUMBER",
                table: "ServiceTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STARTPRICERANGE",
                table: "ServiceTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TITLE",
                table: "ServiceTable",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CATEGORY",
                table: "ServiceTable");

            migrationBuilder.DropColumn(
                name: "DESCRIPTION",
                table: "ServiceTable");

            migrationBuilder.DropColumn(
                name: "ENDPRICERANGE",
                table: "ServiceTable");

            migrationBuilder.DropColumn(
                name: "IMAGE",
                table: "ServiceTable");

            migrationBuilder.DropColumn(
                name: "LOCATION",
                table: "ServiceTable");

            migrationBuilder.DropColumn(
                name: "PHONENUMBER",
                table: "ServiceTable");

            migrationBuilder.DropColumn(
                name: "STARTPRICERANGE",
                table: "ServiceTable");

            migrationBuilder.DropColumn(
                name: "TITLE",
                table: "ServiceTable");
        }
    }
}
