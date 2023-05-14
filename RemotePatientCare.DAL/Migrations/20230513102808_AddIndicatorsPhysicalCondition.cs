using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RemotePatientCare.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddIndicatorsPhysicalCondition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhysicalConditions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Pulse = table.Column<int>(type: "int", nullable: false),
                    UpperArterialPressure = table.Column<int>(type: "int", nullable: false),
                    LowerArterialPressure = table.Column<int>(type: "int", nullable: false),
                    BodyTemperature = table.Column<double>(type: "float", nullable: false),
                    BreathingRate = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalConditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhysicalConditions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalConditions_PatientId",
                table: "PhysicalConditions",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhysicalConditions");
        }
    }
}
