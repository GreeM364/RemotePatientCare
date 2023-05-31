using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RemotePatientCare.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixBugsWithDependencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_CaregiverPatients_CaregiverPatientId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Doctors_DoctorId",
                table: "Patients");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_CaregiverPatients_CaregiverPatientId",
                table: "Patients",
                column: "CaregiverPatientId",
                principalTable: "CaregiverPatients",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Doctors_DoctorId",
                table: "Patients",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_CaregiverPatients_CaregiverPatientId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Doctors_DoctorId",
                table: "Patients");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_CaregiverPatients_CaregiverPatientId",
                table: "Patients",
                column: "CaregiverPatientId",
                principalTable: "CaregiverPatients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Doctors_DoctorId",
                table: "Patients",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
