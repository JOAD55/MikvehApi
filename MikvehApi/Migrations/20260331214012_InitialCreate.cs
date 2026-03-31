using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MikvehApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    cliente_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    apellidos = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    telefono = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    fecha_nacimiento = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.cliente_id);
                });

            migrationBuilder.CreateTable(
                name: "paquetes",
                columns: table => new
                {
                    paquete_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paquetes", x => x.paquete_id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    rol_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.rol_id);
                });

            migrationBuilder.CreateTable(
                name: "servicios",
                columns: table => new
                {
                    servicio_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    duracion_minutos = table.Column<int>(type: "int", nullable: false),
                    precio_base = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_servicios", x => x.servicio_id);
                });

            migrationBuilder.CreateTable(
                name: "trabajadores",
                columns: table => new
                {
                    trabajador_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    apellidos = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    usuario = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    contrasena_hash = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    telefono = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    fecha_nacimiento = table.Column<DateTime>(type: "date", nullable: true),
                    rol_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trabajadores", x => x.trabajador_id);
                    table.ForeignKey(
                        name: "FK_trabajadores_rol",
                        column: x => x.rol_id,
                        principalTable: "roles",
                        principalColumn: "rol_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "detalles_paquete",
                columns: table => new
                {
                    detalle_paquete_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paquete_id = table.Column<int>(type: "int", nullable: true),
                    servicio_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalles_paquete", x => x.detalle_paquete_id);
                    table.ForeignKey(
                        name: "FK_detalles_paquete_paquete",
                        column: x => x.paquete_id,
                        principalTable: "paquetes",
                        principalColumn: "paquete_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_detalles_paquete_servicio",
                        column: x => x.servicio_id,
                        principalTable: "servicios",
                        principalColumn: "servicio_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "citas",
                columns: table => new
                {
                    cita_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_campo = table.Column<DateTime>(type: "datetime", nullable: false),
                    total_pagar = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    cliente_id = table.Column<int>(type: "int", nullable: true),
                    trabajador_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_citas", x => x.cita_id);
                    table.ForeignKey(
                        name: "FK_citas_cliente",
                        column: x => x.cliente_id,
                        principalTable: "clientes",
                        principalColumn: "cliente_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_citas_trabajador",
                        column: x => x.trabajador_id,
                        principalTable: "trabajadores",
                        principalColumn: "trabajador_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "detalles_cita",
                columns: table => new
                {
                    detalle_cita_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cita_id = table.Column<int>(type: "int", nullable: false),
                    servicio_id = table.Column<int>(type: "int", nullable: true),
                    paquete_id = table.Column<int>(type: "int", nullable: true),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    sutotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalles_cita", x => x.detalle_cita_id);
                    table.ForeignKey(
                        name: "FK_detalles_cita_cita",
                        column: x => x.cita_id,
                        principalTable: "citas",
                        principalColumn: "cita_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_detalles_cita_paquete",
                        column: x => x.paquete_id,
                        principalTable: "paquetes",
                        principalColumn: "paquete_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_detalles_cita_servicio",
                        column: x => x.servicio_id,
                        principalTable: "servicios",
                        principalColumn: "servicio_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_citas_cliente_id",
                table: "citas",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "IX_citas_trabajador_id",
                table: "citas",
                column: "trabajador_id");

            migrationBuilder.CreateIndex(
                name: "IX_detalles_cita_cita_id",
                table: "detalles_cita",
                column: "cita_id");

            migrationBuilder.CreateIndex(
                name: "IX_detalles_cita_paquete_id",
                table: "detalles_cita",
                column: "paquete_id");

            migrationBuilder.CreateIndex(
                name: "IX_detalles_cita_servicio_id",
                table: "detalles_cita",
                column: "servicio_id");

            migrationBuilder.CreateIndex(
                name: "IX_detalles_paquete_paquete_id",
                table: "detalles_paquete",
                column: "paquete_id");

            migrationBuilder.CreateIndex(
                name: "IX_detalles_paquete_servicio_id",
                table: "detalles_paquete",
                column: "servicio_id");

            migrationBuilder.CreateIndex(
                name: "IX_trabajadores_rol_id",
                table: "trabajadores",
                column: "rol_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "detalles_cita");

            migrationBuilder.DropTable(
                name: "detalles_paquete");

            migrationBuilder.DropTable(
                name: "citas");

            migrationBuilder.DropTable(
                name: "paquetes");

            migrationBuilder.DropTable(
                name: "servicios");

            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "trabajadores");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
