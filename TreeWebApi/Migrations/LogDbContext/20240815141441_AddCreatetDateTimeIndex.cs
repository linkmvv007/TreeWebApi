using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TreeWebApi.Migrations.LogDbContext
{
    /// <inheritdoc />
    public partial class AddCreatetDateTimeIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Exceptions_CreatedAt",
                schema: "dbo",
                table: "Exceptions",
                column: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Exceptions_CreatedAt",
                schema: "dbo",
                table: "Exceptions");
        }
    }
}
