using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AgendaService.Migrations
{
    public partial class AddAgendaServiceDBReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendas_Operadores_OperadorId",
                table: "Agendas");

            migrationBuilder.DropTable(
                name: "Operadores");

            migrationBuilder.RenameColumn(
                name: "OperadorId",
                table: "Agendas",
                newName: "ResponsavelId");

            migrationBuilder.RenameColumn(
                name: "DataRetirada",
                table: "Agendas",
                newName: "DataAgenda");

            migrationBuilder.RenameIndex(
                name: "IX_Agendas_OperadorId",
                table: "Agendas",
                newName: "IX_Agendas_ResponsavelId");

            migrationBuilder.CreateTable(
                name: "Responsaveis",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    NomeResponsavel = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsaveis", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Agendas_Responsaveis_ResponsavelId",
                table: "Agendas",
                column: "ResponsavelId",
                principalTable: "Responsaveis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendas_Responsaveis_ResponsavelId",
                table: "Agendas");

            migrationBuilder.DropTable(
                name: "Responsaveis");

            migrationBuilder.RenameColumn(
                name: "ResponsavelId",
                table: "Agendas",
                newName: "OperadorId");

            migrationBuilder.RenameColumn(
                name: "DataAgenda",
                table: "Agendas",
                newName: "DataRetirada");

            migrationBuilder.RenameIndex(
                name: "IX_Agendas_ResponsavelId",
                table: "Agendas",
                newName: "IX_Agendas_OperadorId");

            migrationBuilder.CreateTable(
                name: "Operadores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    NomeOperador = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operadores", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Agendas_Operadores_OperadorId",
                table: "Agendas",
                column: "OperadorId",
                principalTable: "Operadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
