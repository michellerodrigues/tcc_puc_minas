using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DescarteService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoteDescartes",
                columns: table => new
                {
                    LoteDescarteId = table.Column<Guid>(nullable: false),
                    EmailResponsavelDescarte = table.Column<string>(maxLength: 255, nullable: false),
                    NomeResponsavelDescarte = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoteDescartes", x => x.LoteDescarteId);
                });

            migrationBuilder.CreateTable(
                name: "AgendamentoDescartes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataEnvioEmail = table.Column<DateTime>(nullable: false),
                    DataPropostaAgendamento = table.Column<DateTime>(nullable: false),
                    LoteDescarteId = table.Column<Guid>(nullable: false),
                    StatusProposta = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendamentoDescartes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgendamentoDescartes_LoteDescartes_LoteDescarteId",
                        column: x => x.LoteDescarteId,
                        principalTable: "LoteDescartes",
                        principalColumn: "LoteDescarteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoDescartes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataVecimentoProduto = table.Column<DateTime>(nullable: false),
                    IdITemEstoque = table.Column<Guid>(nullable: false),
                    LoteDescarteId = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(maxLength: 255, nullable: false),
                    QtdeDisponivelEstoque = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoDescartes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutoDescartes_LoteDescartes_LoteDescarteId",
                        column: x => x.LoteDescarteId,
                        principalTable: "LoteDescartes",
                        principalColumn: "LoteDescarteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgendamentoDescartes_LoteDescarteId",
                table: "AgendamentoDescartes",
                column: "LoteDescarteId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoDescartes_LoteDescarteId",
                table: "ProdutoDescartes",
                column: "LoteDescarteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgendamentoDescartes");

            migrationBuilder.DropTable(
                name: "ProdutoDescartes");

            migrationBuilder.DropTable(
                name: "LoteDescartes");
        }
    }
}
