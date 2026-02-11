using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookSmartBackEndDatabase.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoleTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ROLETYPES",
                columns: new[] { "ROLETYPE_ID", "ROLETYPE_DESCRIPTION", "ROLETYPE_LOCKED", "ROLETYPE_NAME" },
                values: new object[,]
                {
                    { new Guid("257b4011-ae28-4054-9194-c6045ab11c81"), "Administrator", false, "Admin" },
                    { new Guid("ce92ed6e-69a4-45fd-a5be-40c382cfdff0"), "Client", false, "Client" },
                    { new Guid("d7d33ce7-1200-4bce-9b7e-1381061e713b"), "Staff", false, "Staff" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ROLETYPES",
                keyColumn: "ROLETYPE_ID",
                keyValue: new Guid("257b4011-ae28-4054-9194-c6045ab11c81"));

            migrationBuilder.DeleteData(
                table: "ROLETYPES",
                keyColumn: "ROLETYPE_ID",
                keyValue: new Guid("ce92ed6e-69a4-45fd-a5be-40c382cfdff0"));

            migrationBuilder.DeleteData(
                table: "ROLETYPES",
                keyColumn: "ROLETYPE_ID",
                keyValue: new Guid("d7d33ce7-1200-4bce-9b7e-1381061e713b"));
        }
    }
}
