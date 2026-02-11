using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSmartBackEndDatabase.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SCHEDULES",
                columns: table => new
                {
                    SCHEDULE_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SCHEDULE_USERID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SCHEDULE_DAYOFWEEK = table.Column<int>(type: "int", nullable: false),
                    SCHEDULE_STARTTIME = table.Column<TimeOnly>(type: "time", nullable: false),
                    SCHEDULE_ENDTIME = table.Column<TimeOnly>(type: "time", nullable: false),
                    SCHEDULE_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    SCHEDULE_CREATED = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SCHEDULE_UPDATED = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SCHEDULE_DELETED = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SCHEDULES", x => x.SCHEDULE_ID);
                    table.ForeignKey(
                        name: "FK_SCHEDULES_USERS_SCHEDULE_USERID",
                        column: x => x.SCHEDULE_USERID,
                        principalTable: "USERS",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SERVICES",
                columns: table => new
                {
                    SERVICE_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SERVICE_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SERVICE_DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SERVICE_DURATION = table.Column<int>(type: "int", nullable: false),
                    SERVICE_PRICE = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    SERVICE_BUSINESSID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SERVICE_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    SERVICE_CREATED = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SERVICE_UPDATED = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SERVICE_DELETED = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SERVICES", x => x.SERVICE_ID);
                    table.ForeignKey(
                        name: "FK_SERVICES_BUSINESSES_SERVICE_BUSINESSID",
                        column: x => x.SERVICE_BUSINESSID,
                        principalTable: "BUSINESSES",
                        principalColumn: "BUSINESS_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "APPOINTMENTS",
                columns: table => new
                {
                    APPOINTMENT_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    APPOINTMENT_CLIENTUSERID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    APPOINTMENT_STAFFUSERID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    APPOINTMENT_SERVICEID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    APPOINTMENT_STARTDATETIME = table.Column<DateTime>(type: "datetime2", nullable: false),
                    APPOINTMENT_ENDDATETIME = table.Column<DateTime>(type: "datetime2", nullable: false),
                    APPOINTMENT_STATUS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    APPOINTMENT_COMMENT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    APPOINTMENT_CREATED = table.Column<DateTime>(type: "datetime2", nullable: false),
                    APPOINTMENT_UPDATED = table.Column<DateTime>(type: "datetime2", nullable: false),
                    APPOINTMENT_DELETED = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APPOINTMENTS", x => x.APPOINTMENT_ID);
                    table.ForeignKey(
                        name: "FK_APPOINTMENTS_SERVICES_APPOINTMENT_SERVICEID",
                        column: x => x.APPOINTMENT_SERVICEID,
                        principalTable: "SERVICES",
                        principalColumn: "SERVICE_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_APPOINTMENTS_USERS_APPOINTMENT_CLIENTUSERID",
                        column: x => x.APPOINTMENT_CLIENTUSERID,
                        principalTable: "USERS",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_APPOINTMENTS_USERS_APPOINTMENT_STAFFUSERID",
                        column: x => x.APPOINTMENT_STAFFUSERID,
                        principalTable: "USERS",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_APPOINTMENTS_APPOINTMENT_CLIENTUSERID",
                table: "APPOINTMENTS",
                column: "APPOINTMENT_CLIENTUSERID");

            migrationBuilder.CreateIndex(
                name: "IX_APPOINTMENTS_APPOINTMENT_SERVICEID",
                table: "APPOINTMENTS",
                column: "APPOINTMENT_SERVICEID");

            migrationBuilder.CreateIndex(
                name: "IX_APPOINTMENTS_APPOINTMENT_STAFFUSERID",
                table: "APPOINTMENTS",
                column: "APPOINTMENT_STAFFUSERID");

            migrationBuilder.CreateIndex(
                name: "IX_SCHEDULES_SCHEDULE_USERID",
                table: "SCHEDULES",
                column: "SCHEDULE_USERID");

            migrationBuilder.CreateIndex(
                name: "IX_SERVICES_SERVICE_BUSINESSID",
                table: "SERVICES",
                column: "SERVICE_BUSINESSID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APPOINTMENTS");

            migrationBuilder.DropTable(
                name: "SCHEDULES");

            migrationBuilder.DropTable(
                name: "SERVICES");
        }
    }
}
