using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSmartBackEndDatabase.Migrations
{
    /// <inheritdoc />
    public partial class AddScheduleOverrides : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SCHEDULEOVERRIDES",
                columns: table => new
                {
                    SCHEDULEOVERRIDE_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SCHEDULEOVERRIDE_USERID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SCHEDULEOVERRIDE_DATE = table.Column<DateOnly>(type: "date", nullable: false),
                    SCHEDULEOVERRIDE_STARTTIME = table.Column<TimeOnly>(type: "time", nullable: true),
                    SCHEDULEOVERRIDE_ENDTIME = table.Column<TimeOnly>(type: "time", nullable: true),
                    SCHEDULEOVERRIDE_ISAVAILABLE = table.Column<bool>(type: "bit", nullable: false),
                    SCHEDULEOVERRIDE_CREATED = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SCHEDULEOVERRIDE_UPDATED = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SCHEDULEOVERRIDE_DELETED = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SCHEDULEOVERRIDES", x => x.SCHEDULEOVERRIDE_ID);
                    table.ForeignKey(
                        name: "FK_SCHEDULEOVERRIDES_USERS_SCHEDULEOVERRIDE_USERID",
                        column: x => x.SCHEDULEOVERRIDE_USERID,
                        principalTable: "USERS",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SCHEDULEOVERRIDES_SCHEDULEOVERRIDE_USERID",
                table: "SCHEDULEOVERRIDES",
                column: "SCHEDULEOVERRIDE_USERID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SCHEDULEOVERRIDES");
        }
    }
}
