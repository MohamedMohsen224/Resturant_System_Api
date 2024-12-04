using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resturant_Api_Reposatry.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_OrderItems_OrderItemId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Meals_OrderItemId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "Meals");

            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "Reservisons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Reservisons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "mealsId",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MealsInOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealsInOrder", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_mealsId",
                table: "OrderItems",
                column: "mealsId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_MealsInOrder_mealsId",
                table: "OrderItems",
                column: "mealsId",
                principalTable: "MealsInOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_MealsInOrder_mealsId",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "MealsInOrder");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_mealsId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "Reservisons");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Reservisons");

            migrationBuilder.DropColumn(
                name: "mealsId",
                table: "OrderItems");

            migrationBuilder.AddColumn<int>(
                name: "OrderItemId",
                table: "Meals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meals_OrderItemId",
                table: "Meals",
                column: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_OrderItems_OrderItemId",
                table: "Meals",
                column: "OrderItemId",
                principalTable: "OrderItems",
                principalColumn: "Id");
        }
    }
}
