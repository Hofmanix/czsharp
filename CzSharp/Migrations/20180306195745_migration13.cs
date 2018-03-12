using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CzSharp.Migrations
{
    public partial class migration13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Events",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Events",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "From",
                table: "Events",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Info",
                table: "Events",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Events",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "To",
                table: "Events",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Codes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_UserId",
                table: "Events",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Codes_UserId",
                table: "Codes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Codes_AspNetUsers_UserId",
                table: "Codes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_UserId",
                table: "Events",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Codes_AspNetUsers_UserId",
                table: "Codes");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_UserId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_UserId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Codes_UserId",
                table: "Codes");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "From",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Info",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "To",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Codes");
        }
    }
}
