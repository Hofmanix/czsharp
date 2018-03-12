using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CzSharp.Migrations
{
    public partial class migration18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Topics",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Info",
                table: "Topics",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "TopicGroups",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "TopicGroups",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TopicGroups",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Discussions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Contributions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TopicGroups_Title",
                table: "TopicGroups",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TopicGroups_UserId",
                table: "TopicGroups",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicGroups_AspNetUsers_UserId",
                table: "TopicGroups",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicGroups_AspNetUsers_UserId",
                table: "TopicGroups");

            migrationBuilder.DropIndex(
                name: "IX_TopicGroups_Title",
                table: "TopicGroups");

            migrationBuilder.DropIndex(
                name: "IX_TopicGroups_UserId",
                table: "TopicGroups");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "TopicGroups");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TopicGroups");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Topics",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Info",
                table: "Topics",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "TopicGroups",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Discussions",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Contributions",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
