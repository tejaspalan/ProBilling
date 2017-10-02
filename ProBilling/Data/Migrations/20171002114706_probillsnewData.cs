using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProBilling.Data.Migrations
{
    public partial class probillsnewData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBillable",
                table: "SprintActivity");

            migrationBuilder.AddColumn<bool>(
                name: "IsBackup",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBackup",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsBillable",
                table: "SprintActivity",
                nullable: false,
                defaultValue: false);
        }
    }
}
