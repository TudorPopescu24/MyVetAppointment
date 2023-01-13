using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyVetAppointment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClinicId",
                table: "Drugs",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_ClinicId",
                table: "Drugs",
                column: "ClinicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drugs_Clinics_ClinicId",
                table: "Drugs",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drugs_Clinics_ClinicId",
                table: "Drugs");

            migrationBuilder.DropIndex(
                name: "IX_Drugs_ClinicId",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "ClinicId",
                table: "Drugs");
        }
    }
}
