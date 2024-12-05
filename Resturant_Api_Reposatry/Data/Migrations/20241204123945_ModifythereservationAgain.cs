using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resturant_Api_Reposatry.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifythereservationAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ReservationEndTime",
                table: "Reservisons",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "ReservationEndTime",
                table: "Reservisons",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
