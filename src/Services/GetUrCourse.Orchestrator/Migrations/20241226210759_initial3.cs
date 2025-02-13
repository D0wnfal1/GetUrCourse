using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetUrCourse.Orchestrator.Migrations
{
    /// <inheritdoc />
    public partial class initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Faulted",
                table: "RegisterNewUserSagaData",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Faulted",
                table: "RegisterNewUserSagaData");
        }
    }
}
