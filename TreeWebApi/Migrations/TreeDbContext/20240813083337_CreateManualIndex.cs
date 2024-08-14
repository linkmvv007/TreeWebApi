using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TreeWebApi.Migrations.TreeDbContext
{
    /// <inheritdoc />
    public partial class CreateManualIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = "CREATE UNIQUE INDEX \"IX_Trees_Name_ParentNodeId\" " +
"ON dbo.\"Trees\" (\"Name\") " +
"WHERE \"ParentNodeId\" IS NULL;";

            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP INDEX IF EXISTS \"IX_Trees_Name_ParentNodeId\";");
        }
    }
}
