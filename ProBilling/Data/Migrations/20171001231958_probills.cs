using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProBilling.Data.Migrations
{
    public partial class probills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sprint_SprintReport_SprintReportId",
                table: "Sprint");

            migrationBuilder.DropIndex(
                name: "IX_Sprint_SprintReportId",
                table: "Sprint");

            migrationBuilder.DropColumn(
                name: "SprintReportId",
                table: "Sprint");

            migrationBuilder.CreateIndex(
                name: "IX_SprintReport_SprintId",
                table: "SprintReport",
                column: "SprintId");

            migrationBuilder.AddForeignKey(
                name: "FK_SprintReport_Sprint_SprintId",
                table: "SprintReport",
                column: "SprintId",
                principalTable: "Sprint",
                principalColumn: "SprintId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SprintReport_Sprint_SprintId",
                table: "SprintReport");

            migrationBuilder.DropIndex(
                name: "IX_SprintReport_SprintId",
                table: "SprintReport");

            migrationBuilder.AddColumn<int>(
                name: "SprintReportId",
                table: "Sprint",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sprint_SprintReportId",
                table: "Sprint",
                column: "SprintReportId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sprint_SprintReport_SprintReportId",
                table: "Sprint",
                column: "SprintReportId",
                principalTable: "SprintReport",
                principalColumn: "SprintReportId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
