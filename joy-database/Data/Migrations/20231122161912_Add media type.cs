using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace joy_database.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addmediatype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Media",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Media");
        }
    }
}
