using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelAgentAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Arrival = table.Column<string>(nullable: true),
                    Depature = table.Column<string>(nullable: true),
                    TotalSeats = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlightSchedulers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FlightId = table.Column<Guid>(nullable: false),
                    DepartureTime = table.Column<string>(nullable: true),
                    ArrivalTime = table.Column<string>(nullable: true),
                    JourneyTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightSchedulers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightSchedulers_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FlightId = table.Column<Guid>(nullable: false),
                    SeatNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlightBookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BookingRef = table.Column<string>(nullable: true),
                    BookedDate = table.Column<DateTime>(nullable: false),
                    ClientID = table.Column<Guid>(nullable: false),
                    OutBookingDate = table.Column<DateTime>(nullable: false),
                    OutFlightId = table.Column<Guid>(nullable: false),
                    OutFlightSchedulerId = table.Column<Guid>(nullable: false),
                    OutSeatId = table.Column<Guid>(nullable: false),
                    InBookingDate = table.Column<DateTime>(nullable: true),
                    InSchedulerId = table.Column<Guid>(nullable: true),
                    InFlightId = table.Column<Guid>(nullable: true),
                    InSeatId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightBookings_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlightBookings_Flights_InFlightId",
                        column: x => x.InFlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlightBookings_FlightSchedulers_InSchedulerId",
                        column: x => x.InSchedulerId,
                        principalTable: "FlightSchedulers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlightBookings_Seats_InSeatId",
                        column: x => x.InSeatId,
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlightBookings_Flights_OutFlightId",
                        column: x => x.OutFlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlightBookings_FlightSchedulers_OutFlightSchedulerId",
                        column: x => x.OutFlightSchedulerId,
                        principalTable: "FlightSchedulers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlightBookings_Seats_OutSeatId",
                        column: x => x.OutSeatId,
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlightBookings_ClientID",
                table: "FlightBookings",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_FlightBookings_InFlightId",
                table: "FlightBookings",
                column: "InFlightId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightBookings_InSchedulerId",
                table: "FlightBookings",
                column: "InSchedulerId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightBookings_InSeatId",
                table: "FlightBookings",
                column: "InSeatId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightBookings_OutFlightId",
                table: "FlightBookings",
                column: "OutFlightId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightBookings_OutFlightSchedulerId",
                table: "FlightBookings",
                column: "OutFlightSchedulerId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightBookings_OutSeatId",
                table: "FlightBookings",
                column: "OutSeatId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightSchedulers_FlightId",
                table: "FlightSchedulers",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_FlightId",
                table: "Seats",
                column: "FlightId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientTypes");

            migrationBuilder.DropTable(
                name: "FlightBookings");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "FlightSchedulers");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}
