using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GPScript.NET.src.infraestructura.datos.migraciones
{
    /// <inheritdoc />
    public partial class AppDbMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatosDePredicciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreEnte = table.Column<string>(type: "text", nullable: false),
                    PromedioInicioComFinCom = table.Column<int>(type: "integer", nullable: false),
                    PromedioFinComInicioFactura = table.Column<int>(type: "integer", nullable: false),
                    PromedioInicioFacturaFinPagado = table.Column<int>(type: "integer", nullable: false),
                    PromedioInicioComFinPagado = table.Column<int>(type: "integer", nullable: false),
                    CategoriaRiesgo = table.Column<string>(type: "text", nullable: false),
                    DiasDemoraRealPromedio = table.Column<float>(type: "real", nullable: false),
                    DiasDemoraPredicho = table.Column<float>(type: "real", nullable: false),
                    DiferenciaDias = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatosDePredicciones", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatosDePredicciones");
        }
    }
}
