using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resturant_Api_Reposatry.Data.Migrations
{
    /// <inheritdoc />
    public partial class Modifythereservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Reservisons_ReservationId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_ReservationId",
                table: "Tables");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationId",
                table: "Tables",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_ReservationId",
                table: "Tables",
                column: "ReservationId",
                unique: true,
                filter: "[ReservationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Reservisons_ReservationId",
                table: "Tables",
                column: "ReservationId",
                principalTable: "Reservisons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Reservisons_ReservationId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_ReservationId",
                table: "Tables");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationId",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tables_ReservationId",
                table: "Tables",
                column: "ReservationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Reservisons_ReservationId",
                table: "Tables",
                column: "ReservationId",
                principalTable: "Reservisons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
