using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProBilling.Data.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "CustomerRemarks",
                table: "Sprint");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "Team",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SprintReportId",
                table: "Sprint",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SprintReport",
                columns: table => new
                {
                    SprintReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApprovedByClient = table.Column<bool>(type: "bit", nullable: false),
                    CustomerRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SprintId = table.Column<int>(type: "int", nullable: false),
                    TimeOfClientResponse = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalBackupHours = table.Column<double>(type: "float", nullable: false),
                    TotalBillableHours = table.Column<double>(type: "float", nullable: false),
                    TotalNonBillableHours = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SprintReport", x => x.SprintReportId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Team_CustomerId",
                table: "Team",
                column: "CustomerId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Team_AspNetUsers_CustomerId",
                table: "Team",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sprint_SprintReport_SprintReportId",
                table: "Sprint");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_AspNetUsers_CustomerId",
                table: "Team");

            migrationBuilder.DropTable(
                name: "SprintReport");

            migrationBuilder.DropIndex(
                name: "IX_Team_CustomerId",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Sprint_SprintReportId",
                table: "Sprint");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "SprintReportId",
                table: "Sprint");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Team",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerRemarks",
                table: "Sprint",
                nullable: true);
        }
    }
}
