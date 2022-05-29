using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItAcademy.Migrations
{
    public partial class inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Substancia = table.Column<string>(type: "TEXT", nullable: false),
                    CNPJ = table.Column<string>(type: "TEXT", nullable: false),
                    Laboratorio = table.Column<string>(type: "TEXT", nullable: false),
                    CodGGREM = table.Column<long>(type: "INTEGER", nullable: true),
                    Registro = table.Column<long>(type: "INTEGER", nullable: true),
                    EAN1 = table.Column<long>(type: "INTEGER", nullable: true),
                    EAN2 = table.Column<long>(type: "INTEGER", nullable: true),
                    EAN3 = table.Column<long>(type: "INTEGER", nullable: true),
                    Produto = table.Column<string>(type: "TEXT", nullable: false),
                    Apresentacao = table.Column<string>(type: "TEXT", nullable: false),
                    ClasseTerapeutica = table.Column<string>(type: "TEXT", nullable: false),
                    TipoProduto = table.Column<string>(type: "TEXT", nullable: false),
                    RegimePreco = table.Column<string>(type: "TEXT", nullable: false),
                    PFIsento = table.Column<double>(type: "REAL", nullable: true),
                    PFZero = table.Column<double>(type: "REAL", nullable: true),
                    PF12 = table.Column<double>(type: "REAL", nullable: true),
                    PF17 = table.Column<double>(type: "REAL", nullable: true),
                    PF17ALC = table.Column<double>(type: "REAL", nullable: true),
                    PF175 = table.Column<double>(type: "REAL", nullable: true),
                    PF175ALC = table.Column<double>(type: "REAL", nullable: true),
                    PF18 = table.Column<double>(type: "REAL", nullable: true),
                    PF18ALC = table.Column<double>(type: "REAL", nullable: true),
                    PF20 = table.Column<double>(type: "REAL", nullable: true),
                    PMCZero = table.Column<double>(type: "REAL", nullable: true),
                    PMC12 = table.Column<double>(type: "REAL", nullable: true),
                    PMC17 = table.Column<double>(type: "REAL", nullable: true),
                    PMC17ALC = table.Column<double>(type: "REAL", nullable: true),
                    PMC175 = table.Column<double>(type: "REAL", nullable: true),
                    PMC175ALC = table.Column<double>(type: "REAL", nullable: true),
                    PMC18 = table.Column<double>(type: "REAL", nullable: true),
                    PMC18ALC = table.Column<double>(type: "REAL", nullable: true),
                    PMC20 = table.Column<double>(type: "REAL", nullable: true),
                    Restricao = table.Column<bool>(type: "INTEGER", nullable: false),
                    CAP = table.Column<bool>(type: "INTEGER", nullable: false),
                    CONFAZ = table.Column<bool>(type: "INTEGER", nullable: false),
                    ICMSZero = table.Column<bool>(type: "INTEGER", nullable: false),
                    AnaliseRecursal = table.Column<string>(type: "TEXT", nullable: false),
                    ConcessaoCredito = table.Column<string>(type: "TEXT", nullable: false),
                    Comercializacao2020 = table.Column<bool>(type: "INTEGER", nullable: false),
                    TARJA = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicamentos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medicamentos");
        }
    }
}
