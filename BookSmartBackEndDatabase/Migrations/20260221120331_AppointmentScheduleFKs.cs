using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSmartBackEndDatabase.Migrations
{
    /// <inheritdoc />
    public partial class AppointmentScheduleFKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "APPOINTMENT_SCHEDULEID",
                table: "APPOINTMENTS",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "APPOINTMENT_SCHEDULEOVERRIDEID",
                table: "APPOINTMENTS",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_APPOINTMENTS_APPOINTMENT_SCHEDULEID",
                table: "APPOINTMENTS",
                column: "APPOINTMENT_SCHEDULEID");

            migrationBuilder.CreateIndex(
                name: "IX_APPOINTMENTS_APPOINTMENT_SCHEDULEOVERRIDEID",
                table: "APPOINTMENTS",
                column: "APPOINTMENT_SCHEDULEOVERRIDEID");

            migrationBuilder.AddForeignKey(
                name: "FK_APPOINTMENTS_SCHEDULEOVERRIDES_APPOINTMENT_SCHEDULEOVERRIDEID",
                table: "APPOINTMENTS",
                column: "APPOINTMENT_SCHEDULEOVERRIDEID",
                principalTable: "SCHEDULEOVERRIDES",
                principalColumn: "SCHEDULEOVERRIDE_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_APPOINTMENTS_SCHEDULES_APPOINTMENT_SCHEDULEID",
                table: "APPOINTMENTS",
                column: "APPOINTMENT_SCHEDULEID",
                principalTable: "SCHEDULES",
                principalColumn: "SCHEDULE_ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_APPOINTMENTS_SCHEDULEOVERRIDES_APPOINTMENT_SCHEDULEOVERRIDEID",
                table: "APPOINTMENTS");

            migrationBuilder.DropForeignKey(
                name: "FK_APPOINTMENTS_SCHEDULES_APPOINTMENT_SCHEDULEID",
                table: "APPOINTMENTS");

            migrationBuilder.DropIndex(
                name: "IX_APPOINTMENTS_APPOINTMENT_SCHEDULEID",
                table: "APPOINTMENTS");

            migrationBuilder.DropIndex(
                name: "IX_APPOINTMENTS_APPOINTMENT_SCHEDULEOVERRIDEID",
                table: "APPOINTMENTS");

            migrationBuilder.DropColumn(
                name: "APPOINTMENT_SCHEDULEID",
                table: "APPOINTMENTS");

            migrationBuilder.DropColumn(
                name: "APPOINTMENT_SCHEDULEOVERRIDEID",
                table: "APPOINTMENTS");
        }
    }
}
