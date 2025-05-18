using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_api.Migrations
{
    /// <inheritdoc />
    public partial class AddPlanFilePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlanFilePath",
                table: "GymMembers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "GymMembers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PlanFilePath",
                value: null);

            migrationBuilder.UpdateData(
                table: "GymMembers",
                keyColumn: "Id",
                keyValue: 2,
                column: "PlanFilePath",
                value: null);

            migrationBuilder.UpdateData(
                table: "GymMembers",
                keyColumn: "Id",
                keyValue: 3,
                column: "PlanFilePath",
                value: null);

            migrationBuilder.UpdateData(
                table: "GymMembers",
                keyColumn: "Id",
                keyValue: 4,
                column: "PlanFilePath",
                value: null);

            migrationBuilder.UpdateData(
                table: "GymMembers",
                keyColumn: "Id",
                keyValue: 5,
                column: "PlanFilePath",
                value: null);

            migrationBuilder.UpdateData(
                table: "GymMembers",
                keyColumn: "Id",
                keyValue: 6,
                column: "PlanFilePath",
                value: null);

            migrationBuilder.UpdateData(
                table: "GymMembers",
                keyColumn: "Id",
                keyValue: 7,
                column: "PlanFilePath",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanFilePath",
                table: "GymMembers");
        }
    }
}
