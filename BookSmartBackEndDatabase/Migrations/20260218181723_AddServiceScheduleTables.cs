using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSmartBackEndDatabase.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceScheduleTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SERVICESCHEDULEOVERRIDES",
                columns: table => new
                {
                    SERVICESCHEDULEOVERRIDE_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SERVICESCHEDULEOVERRIDE_SERVICEID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDEID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SERVICESCHEDULEOVERRIDE_CREATED = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SERVICESCHEDULEOVERRIDE_UPDATED = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SERVICESCHEDULEOVERRIDE_DELETED = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SERVICESCHEDULEOVERRIDES", x => x.SERVICESCHEDULEOVERRIDE_ID);
                    table.ForeignKey(
                        name: "FK_SERVICESCHEDULEOVERRIDES_SCHEDULEOVERRIDES_SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDEID",
                        column: x => x.SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDEID,
                        principalTable: "SCHEDULEOVERRIDES",
                        principalColumn: "SCHEDULEOVERRIDE_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SERVICESCHEDULEOVERRIDES_SERVICES_SERVICESCHEDULEOVERRIDE_SERVICEID",
                        column: x => x.SERVICESCHEDULEOVERRIDE_SERVICEID,
                        principalTable: "SERVICES",
                        principalColumn: "SERVICE_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SERVICESCHEDULES",
                columns: table => new
                {
                    SERVICESCHEDULE_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SERVICESCHEDULE_SERVICEID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SERVICESCHEDULE_SCHEDULEID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SERVICESCHEDULE_CREATED = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SERVICESCHEDULE_UPDATED = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SERVICESCHEDULE_DELETED = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SERVICESCHEDULES", x => x.SERVICESCHEDULE_ID);
                    table.ForeignKey(
                        name: "FK_SERVICESCHEDULES_SCHEDULES_SERVICESCHEDULE_SCHEDULEID",
                        column: x => x.SERVICESCHEDULE_SCHEDULEID,
                        principalTable: "SCHEDULES",
                        principalColumn: "SCHEDULE_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SERVICESCHEDULES_SERVICES_SERVICESCHEDULE_SERVICEID",
                        column: x => x.SERVICESCHEDULE_SERVICEID,
                        principalTable: "SERVICES",
                        principalColumn: "SERVICE_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SERVICESCHEDULEOVERRIDES_SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDEID",
                table: "SERVICESCHEDULEOVERRIDES",
                column: "SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDEID");

            migrationBuilder.CreateIndex(
                name: "IX_SERVICESCHEDULEOVERRIDES_SERVICESCHEDULEOVERRIDE_SERVICEID",
                table: "SERVICESCHEDULEOVERRIDES",
                column: "SERVICESCHEDULEOVERRIDE_SERVICEID");

            migrationBuilder.CreateIndex(
                name: "IX_SERVICESCHEDULES_SERVICESCHEDULE_SCHEDULEID",
                table: "SERVICESCHEDULES",
                column: "SERVICESCHEDULE_SCHEDULEID");

            migrationBuilder.CreateIndex(
                name: "IX_SERVICESCHEDULES_SERVICESCHEDULE_SERVICEID",
                table: "SERVICESCHEDULES",
                column: "SERVICESCHEDULE_SERVICEID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SERVICESCHEDULEOVERRIDES");

            migrationBuilder.DropTable(
                name: "SERVICESCHEDULES");
        }
    }
}
