using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSmartBackEndDatabase.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ADDRESSES",
                columns: table => new
                {
                    ADDRESS_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ADDRESS_LINE_1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ADDRESS_LINE_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ADDRESS_TOWN_CITY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ADDRESS_POSTCODE_1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ADDRESS_POSTCODE_2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ADDRESS_CREATED = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ADDRESS_DELETED = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADDRESSES", x => x.ADDRESS_ID);
                });

            migrationBuilder.CreateTable(
                name: "ROLETYPES",
                columns: table => new
                {
                    ROLETYPE_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ROLETYPE_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ROLETYPE_DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ROLETYPE_LOCKED = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLETYPES", x => x.ROLETYPE_ID);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    USER_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    USER_TITLE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USER_FORENAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USER_SURNAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USER_TELEPHONE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    USER_EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USER_PASSWORD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USER_PASSWORDEXPIRED = table.Column<bool>(type: "bit", nullable: false),
                    USER_LOCKED = table.Column<bool>(type: "bit", nullable: false),
                    USER_LASTLOGIN = table.Column<DateTime>(type: "datetime2", nullable: true),
                    USER_CREATED = table.Column<DateTime>(type: "datetime2", nullable: false),
                    USER_UPDATED = table.Column<DateTime>(type: "datetime2", nullable: false),
                    USER_DELETED = table.Column<bool>(type: "bit", nullable: false),
                    BUSINESS_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.USER_ID);
                });

            migrationBuilder.CreateTable(
                name: "BUSINESSES",
                columns: table => new
                {
                    BUSINESS_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BUSINESS_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BUSINESS_ADDRESSADDRESS_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BUSINESS_SUBSTART = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BUSINESS_SUBEND = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BUSINESS_CREATED = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BUSINESS_UPDATED = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BUSINESS_DELETED = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BUSINESSES", x => x.BUSINESS_ID);
                    table.ForeignKey(
                        name: "FK_BUSINESSES_ADDRESSES_BUSINESS_ADDRESSADDRESS_ID",
                        column: x => x.BUSINESS_ADDRESSADDRESS_ID,
                        principalTable: "ADDRESSES",
                        principalColumn: "ADDRESS_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ROLES",
                columns: table => new
                {
                    ROLE_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ROLE_USERID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ROLE_ROLETYPEID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLES", x => x.ROLE_ID);
                    table.ForeignKey(
                        name: "FK_ROLES_ROLETYPES_ROLE_ROLETYPEID",
                        column: x => x.ROLE_ROLETYPEID,
                        principalTable: "ROLETYPES",
                        principalColumn: "ROLETYPE_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ROLES_USERS_ROLE_USERID",
                        column: x => x.ROLE_USERID,
                        principalTable: "USERS",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BUSINESSES_BUSINESS_ADDRESSADDRESS_ID",
                table: "BUSINESSES",
                column: "BUSINESS_ADDRESSADDRESS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ROLES_ROLE_ROLETYPEID",
                table: "ROLES",
                column: "ROLE_ROLETYPEID");

            migrationBuilder.CreateIndex(
                name: "IX_ROLES_ROLE_USERID",
                table: "ROLES",
                column: "ROLE_USERID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BUSINESSES");

            migrationBuilder.DropTable(
                name: "ROLES");

            migrationBuilder.DropTable(
                name: "ADDRESSES");

            migrationBuilder.DropTable(
                name: "ROLETYPES");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
