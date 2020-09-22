using Microsoft.EntityFrameworkCore.Migrations;

namespace HolidayMakerAPI.Migrations
{
    public partial class _12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Home_HomeId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_User_UserId",
                table: "Reservation");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Reservation",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HomeId",
                table: "Reservation",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Home",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Home_UserId",
                table: "Home",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Home_User_UserId",
                table: "Home",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Home_HomeId",
                table: "Reservation",
                column: "HomeId",
                principalTable: "Home",
                principalColumn: "HomeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_User_UserId",
                table: "Reservation",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Home_User_UserId",
                table: "Home");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Home_HomeId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_User_UserId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Home_UserId",
                table: "Home");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Home");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Reservation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "HomeId",
                table: "Reservation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Home_HomeId",
                table: "Reservation",
                column: "HomeId",
                principalTable: "Home",
                principalColumn: "HomeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_User_UserId",
                table: "Reservation",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
