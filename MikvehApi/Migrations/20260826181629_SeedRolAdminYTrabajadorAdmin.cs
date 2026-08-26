using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MikvehApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolAdminYTrabajadorAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // No se fijan IDs explicitos: se reutiliza el rol "Administrador" si ya existe
            // y se identifica el trabajador admin por su usuario, para no chocar con datos
            // que ya pudieran existir en la base de datos.
            migrationBuilder.Sql(
                """
                IF NOT EXISTS (SELECT 1 FROM roles WHERE nombre = N'Administrador')
                BEGIN
                    INSERT INTO roles (nombre, descripcion)
                    VALUES (N'Administrador', N'Administrador del sistema con acceso total');
                END;

                IF NOT EXISTS (SELECT 1 FROM trabajadores WHERE usuario = N'admin')
                BEGIN
                    INSERT INTO trabajadores (nombre, apellidos, usuario, contrasena_hash, rol_id)
                    VALUES (
                        N'Administrador',
                        N'Sistema',
                        N'admin',
                        N'$2a$11$4EWovpviCFsVx9Z5ekrUtOWsl3iBVl63yfKZFxNrlvFL/4Fe/MUaW',
                        (SELECT TOP 1 rol_id FROM roles WHERE nombre = N'Administrador'));
                END;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM trabajadores WHERE usuario = N'admin';");
        }
    }
}
