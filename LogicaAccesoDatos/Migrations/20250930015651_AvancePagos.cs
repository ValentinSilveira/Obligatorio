using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AvancePagos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaDesde",
                table: "Pagos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaHasta",
                table: "Pagos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaPago",
                table: "Pagos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NroRecibo",
                table: "Pagos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoPago",
                table: "Pagos",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaDesde",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "FechaHasta",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "FechaPago",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "NroRecibo",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "TipoPago",
                table: "Pagos");
        }
    }
}
