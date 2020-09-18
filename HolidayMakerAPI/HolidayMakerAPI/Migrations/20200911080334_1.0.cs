using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HolidayMakerAPI.Migrations
{
    public partial class _10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addon",
                columns: table => new
                {
                    AddonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddonType = table.Column<string>(maxLength: 50, nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addon", x => x.AddonId);
                });

            migrationBuilder.CreateTable(
                name: "Home",
                columns: table => new
                {
                    HomeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeType = table.Column<string>(maxLength: 50, nullable: true),
                    Rooms = table.Column<int>(nullable: false),
                    Location = table.Column<string>(maxLength: 50, nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    HasBalcony = table.Column<bool>(nullable: false),
                    HasWifi = table.Column<bool>(nullable: false),
                    Image = table.Column<string>(maxLength: 1000, nullable: true),
                    HasHalfPension = table.Column<bool>(nullable: false),
                    HasFullPension = table.Column<bool>(nullable: false),
                    HasAllInclusive = table.Column<bool>(nullable: false),
                    HasExtraBed = table.Column<bool>(nullable: false),
                    CityDistance = table.Column<int>(nullable: false),
                    BeachDistance = table.Column<int>(nullable: false),
                    NumberOfBeds = table.Column<int>(nullable: false),
                    HasPool = table.Column<bool>(nullable: false),
                    AllowSmoking = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 3000, nullable: true),
                    AllowPets = table.Column<bool>(nullable: false),
                    NumberOfRatings = table.Column<int>(nullable: false),
                    SumOfRatings = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Home", x => x.HomeId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    IsCompany = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ReservationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    HomeId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservation_Home_HomeId",
                        column: x => x.HomeId,
                        principalTable: "Home",
                        principalColumn: "HomeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservation_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservationAddon",
                columns: table => new
                {
                    ReservationId = table.Column<int>(nullable: false),
                    AddonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationAddon", x => new { x.AddonId, x.ReservationId });
                    table.ForeignKey(
                        name: "FK_ReservationAddon_Addon_AddonId",
                        column: x => x.AddonId,
                        principalTable: "Addon",
                        principalColumn: "AddonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationAddon_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "ReservationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_HomeId",
                table: "Reservation",
                column: "HomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_UserId",
                table: "Reservation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationAddon_ReservationId",
                table: "ReservationAddon",
                column: "ReservationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationAddon");

            migrationBuilder.DropTable(
                name: "Addon");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Home");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
