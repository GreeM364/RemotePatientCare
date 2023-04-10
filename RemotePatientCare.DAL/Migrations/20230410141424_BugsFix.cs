using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RemotePatientCare.DAL.Migrations
{
    /// <inheritdoc />
    public partial class BugsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "BaseUsers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "BaseUsers");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "BaseUsers");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "BaseUsers");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Patients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Patients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "HospitalAdministrators",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "HospitalAdministrators",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "HospitalAdministrators",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "HospitalAdministrators",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Doctors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Doctors",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "HospitalAdministrators");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "HospitalAdministrators");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "HospitalAdministrators");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "HospitalAdministrators");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Doctors");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "BaseUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "BaseUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "BaseUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "BaseUsers",
                type: "datetime2",
                nullable: true);
        }
    }
}
