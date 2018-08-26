using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EstoqueService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    FornecedorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    Nome = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.FornecedorId);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 255, nullable: false),
                    PesoCheio = table.Column<decimal>(nullable: false),
                    PesoVazio = table.Column<decimal>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    VolumeEmbalagem = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Revendedores",
                columns: table => new
                {
                    RevendedorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    Nome = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revendedores", x => x.RevendedorId);
                });

            migrationBuilder.CreateTable(
                name: "FornecedorProduto",
                columns: table => new
                {
                    FornecedorId = table.Column<int>(nullable: false),
                    ProdutoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FornecedorProduto", x => new { x.FornecedorId, x.ProdutoId });
                    table.ForeignKey(
                        name: "FK_FornecedorProduto_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "FornecedorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FornecedorProduto_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estoques",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataInclusao = table.Column<DateTime>(nullable: false),
                    DataVecimentoProduto = table.Column<DateTime>(nullable: false),
                    Descartado = table.Column<bool>(nullable: false),
                    FornecidoPorFornecedorId = table.Column<int>(nullable: false),
                    IdProdutoId = table.Column<int>(nullable: false),
                    Lote = table.Column<string>(maxLength: 10, nullable: false),
                    QtdeDispUnidade = table.Column<decimal>(nullable: false),
                    RevendidoPorRevendedorId = table.Column<int>(nullable: false),
                    Serie = table.Column<string>(maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estoques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estoques_Fornecedores_FornecidoPorFornecedorId",
                        column: x => x.FornecidoPorFornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "FornecedorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Estoques_Produtos_IdProdutoId",
                        column: x => x.IdProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Estoques_Revendedores_RevendidoPorRevendedorId",
                        column: x => x.RevendidoPorRevendedorId,
                        principalTable: "Revendedores",
                        principalColumn: "RevendedorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RevendedorProduto",
                columns: table => new
                {
                    RevendedorId = table.Column<int>(nullable: false),
                    ProdutoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RevendedorProduto", x => new { x.RevendedorId, x.ProdutoId });
                    table.ForeignKey(
                        name: "FK_RevendedorProduto_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RevendedorProduto_Revendedores_RevendedorId",
                        column: x => x.RevendedorId,
                        principalTable: "Revendedores",
                        principalColumn: "RevendedorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_FornecidoPorFornecedorId",
                table: "Estoques",
                column: "FornecidoPorFornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_IdProdutoId",
                table: "Estoques",
                column: "IdProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_RevendidoPorRevendedorId",
                table: "Estoques",
                column: "RevendidoPorRevendedorId");

            migrationBuilder.CreateIndex(
                name: "IX_FornecedorProduto_ProdutoId",
                table: "FornecedorProduto",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_RevendedorProduto_ProdutoId",
                table: "RevendedorProduto",
                column: "ProdutoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estoques");

            migrationBuilder.DropTable(
                name: "FornecedorProduto");

            migrationBuilder.DropTable(
                name: "RevendedorProduto");

            migrationBuilder.DropTable(
                name: "Fornecedores");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Revendedores");
        }
    }
}
