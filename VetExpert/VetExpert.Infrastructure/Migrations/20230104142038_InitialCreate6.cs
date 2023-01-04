using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyVetAppointment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Clinics",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Clinics_ApplicationUserId",
                table: "Clinics",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clinics_ApplicationUsers_ApplicationUserId",
                table: "Clinics",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clinics_ApplicationUsers_ApplicationUserId",
                table: "Clinics");

            migrationBuilder.DropIndex(
                name: "IX_Clinics_ApplicationUserId",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Clinics");
        }
    }
}
