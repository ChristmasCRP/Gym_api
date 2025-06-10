using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymBackend.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Hour = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GymMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MembershipType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnrollmentId = table.Column<int>(type: "int", nullable: false),
                    PlanFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GymMembers_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "Id", "Hour", "Name", "Rating" },
                values: new object[,]
                {
                    { 1088, "Thuesday 16:30", "Pilates", 2.7000000000000002 },
                    { 1133, "Monday 15:30", "Yoga", 3.7000000000000002 },
                    { 1153, "Friday 18:30", "Crossfit", 5.0 },
                    { 1232, "Saturday 10:00", "Stretching", 4.7999999999999998 }
                });

            migrationBuilder.InsertData(
                table: "GymMembers",
                columns: new[] { "Id", "EnrollmentId", "JoinDate", "MembershipType", "Name", "PlanFilePath", "Surname" },
                values: new object[,]
                {
                    { 1, 1133, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Standard", "John", null, "Deer" },
                    { 2, 1088, new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Standard", "Kamil", null, "Kowalski" },
                    { 3, 1133, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Premium", "Piotr", null, "Stanowski" },
                    { 4, 1153, new DateTime(2024, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Premium", "Jakub", null, "Stanley" },
                    { 5, 1232, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Premium", "Elon", null, "Musk" },
                    { 6, 1153, new DateTime(2024, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Standard", "Kornelia", null, "Casowsky" },
                    { 7, 1153, new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Plus", "Martin", null, "Stankiewicz" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GymMembers_EnrollmentId",
                table: "GymMembers",
                column: "EnrollmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GymMembers");

            migrationBuilder.DropTable(
                name: "Enrollments");
        }
    }
}
