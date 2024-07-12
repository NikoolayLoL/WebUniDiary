using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUniDiary.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateOn",
                table: "SpecialtyBooks",
                newName: "CreatedOn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "SpecialtyBooks",
                newName: "CreateOn");
        }
    }
}
