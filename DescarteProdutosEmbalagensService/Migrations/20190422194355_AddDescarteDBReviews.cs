using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DescarteService.Migrations
{
    public partial class AddDescarteDBReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QtdeDisponivelEstoque",
                table: "ProdutoDescartes");

            migrationBuilder.AddColumn<decimal>(
                name: "QtdeDispUnidade",
                table: "ProdutoDescartes",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QtdeDispUnidade",
                table: "ProdutoDescartes");

            migrationBuilder.AddColumn<string>(
                name: "QtdeDisponivelEstoque",
                table: "ProdutoDescartes",
                nullable: false,
                defaultValue: "");
        }
    }
}
