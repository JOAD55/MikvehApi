using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MikvehApi.Migrations
{
    /// <inheritdoc />
    public partial class AgregarIndices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_citas_cliente_id",
                table: "citas");

            migrationBuilder.DropIndex(
                name: "IX_citas_trabajador_id",
                table: "citas");

            migrationBuilder.AlterColumn<int>(
                name: "cliente_id",
                table: "citas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_trabajadores_usuario",
                table: "trabajadores",
                column: "usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_clientes_nombre_apellidos",
                table: "clientes",
                columns: new[] { "nombre", "apellidos" });

            migrationBuilder.CreateIndex(
                name: "IX_citas_cliente_id_nombre_campo",
                table: "citas",
                columns: new[] { "cliente_id", "nombre_campo" });

            migrationBuilder.CreateIndex(
                name: "IX_citas_nombre_campo",
                table: "citas",
                column: "nombre_campo");

            migrationBuilder.CreateIndex(
                name: "IX_citas_trabajador_id_nombre_campo",
                table: "citas",
                columns: new[] { "trabajador_id", "nombre_campo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_trabajadores_usuario",
                table: "trabajadores");

            migrationBuilder.DropIndex(
                name: "IX_clientes_nombre_apellidos",
                table: "clientes");

            migrationBuilder.DropIndex(
                name: "IX_citas_cliente_id_nombre_campo",
                table: "citas");

            migrationBuilder.DropIndex(
                name: "IX_citas_nombre_campo",
                table: "citas");

            migrationBuilder.DropIndex(
                name: "IX_citas_trabajador_id_nombre_campo",
                table: "citas");

            migrationBuilder.AlterColumn<int>(
                name: "cliente_id",
                table: "citas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_citas_cliente_id",
                table: "citas",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "IX_citas_trabajador_id",
                table: "citas",
                column: "trabajador_id");
        }
    }
}
