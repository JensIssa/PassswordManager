using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PassswordManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPasswordVisible",
                table: "PasswordItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPasswordVisible",
                table: "PasswordItems");
        }
    }
}
