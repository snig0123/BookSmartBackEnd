using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSmartBackEndDatabase.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceCapacity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
                    WHERE TABLE_NAME = 'SERVICES' AND COLUMN_NAME = 'SERVICE_CAPACITY'
                )
                BEGIN
                    ALTER TABLE SERVICES ADD SERVICE_CAPACITY INT NOT NULL DEFAULT 0;
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
                    WHERE TABLE_NAME = 'SERVICES' AND COLUMN_NAME = 'SERVICE_CAPACITY'
                )
                BEGIN
                    ALTER TABLE SERVICES DROP COLUMN SERVICE_CAPACITY;
                END
            ");
        }
    }
}
