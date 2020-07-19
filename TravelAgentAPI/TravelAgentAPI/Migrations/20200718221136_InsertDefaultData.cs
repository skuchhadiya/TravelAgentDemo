using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Linq.Expressions;

namespace TravelAgentAPI.Migrations
{
    public partial class InsertDefaultData : Migration
    {
        Guid id1 = Guid.NewGuid();
        Guid id2 = Guid.NewGuid();
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
             table: "Flights",
             columns: new[] { "Id", "Code", "Arrival", "Depature", "TotalSeats", "Price" },
             values: new object[] { id1, "BA452", "London(Any)", "Manchester(MAN)", 2, 350 });

            migrationBuilder.InsertData(
            table: "Seats",
            columns: new[] { "Id", "FlightId", "SeatNumber" },
            values: new object[] { Guid.NewGuid(), id1, "A1" });

            migrationBuilder.InsertData(
               table: "Seats",
               columns: new[] { "Id", "FlightId", "SeatNumber" },
               values: new object[] { Guid.NewGuid(), id1, "B1", });


            migrationBuilder.InsertData(
             table: "Flights",
             columns: new[] { "Id", "Code", "Arrival", "Depature", "TotalSeats", "Price" },
             values: new object[] { id2, "BA451", "Manchester(MAN)", "London(Any)", 2, 350 });

            migrationBuilder.InsertData(
              table: "Seats",
              columns: new[] { "Id", "FlightId", "SeatNumber" },
              values: new object[] { Guid.NewGuid(), id2, "A1" });


            migrationBuilder.InsertData(
            table: "Seats",
            columns: new[] { "Id", "FlightId", "SeatNumber" },
            values: new object[] { Guid.NewGuid(), id2, "B1", });


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValues: new object[] { id1, id2 }
            );
        }
    }
}
