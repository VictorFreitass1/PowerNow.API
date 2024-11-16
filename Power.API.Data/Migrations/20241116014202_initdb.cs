using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PowerNow.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_ENDERECO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Cep = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Logradouro = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Bairro = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Uf = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ENDERECO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_PREVISAO_GERACAO_ENERGIA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Data = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    QuantidadeGerada = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Temperatura = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    RadiacaoSolar = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Cep = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EnderecoId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PREVISAO_GERACAO_ENERGIA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_PREVISAO_GERACAO_ENERGIA_TB_ENDERECO_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "TB_ENDERECO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_PREVISAO_GERACAO_ENERGIA_EnderecoId",
                table: "TB_PREVISAO_GERACAO_ENERGIA",
                column: "EnderecoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_PREVISAO_GERACAO_ENERGIA");

            migrationBuilder.DropTable(
                name: "TB_ENDERECO");
        }
    }
}
