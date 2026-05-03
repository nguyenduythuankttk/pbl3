using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_BookingChange_BookingID",
                table: "Booking");

            migrationBuilder.CreateIndex(
                name: "IX_BookingChange_BookingID",
                table: "BookingChange",
                column: "BookingID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingChange_Booking_BookingID",
                table: "BookingChange",
                column: "BookingID",
                principalTable: "Booking",
                principalColumn: "BookingID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingChange_Booking_BookingID",
                table: "BookingChange");

            migrationBuilder.DropIndex(
                name: "IX_BookingChange_BookingID",
                table: "BookingChange");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_BookingChange_BookingID",
                table: "Booking",
                column: "BookingID",
                principalTable: "BookingChange",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
