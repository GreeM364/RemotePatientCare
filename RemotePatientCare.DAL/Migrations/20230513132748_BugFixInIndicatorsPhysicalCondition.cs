using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RemotePatientCare.DAL.Migrations
{
    /// <inheritdoc />
    public partial class BugFixInIndicatorsPhysicalCondition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhysicalConditions_Patients_PatientId",
                table: "PhysicalConditions");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "PhysicalConditions");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "PhysicalConditions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PhysicalConditions_Patients_PatientId",
                table: "PhysicalConditions",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhysicalConditions_Patients_PatientId",
                table: "PhysicalConditions");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "PhysicalConditions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "PhysicalConditions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_PhysicalConditions_Patients_PatientId",
                table: "PhysicalConditions",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }
    }
}
