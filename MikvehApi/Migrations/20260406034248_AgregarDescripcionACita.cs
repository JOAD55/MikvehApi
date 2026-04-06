using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MikvehApi.Migrations
{
    /// <inheritdoc />
    public partial class AgregarDescripcionACita : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "descripcion",
                table: "citas",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "descripcion",
                table: "citas");
        }
    }
}
