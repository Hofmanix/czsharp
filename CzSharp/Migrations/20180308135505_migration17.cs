using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CzSharp.Migrations
{
    public partial class migration17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_TopicGroup_TopicGroupId",
                table: "Topics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TopicGroup",
                table: "TopicGroup");

            migrationBuilder.RenameTable(
                name: "TopicGroup",
                newName: "TopicGroups");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TopicGroups",
                table: "TopicGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_TopicGroups_TopicGroupId",
                table: "Topics",
                column: "TopicGroupId",
                principalTable: "TopicGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_TopicGroups_TopicGroupId",
                table: "Topics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TopicGroups",
                table: "TopicGroups");

            migrationBuilder.RenameTable(
                name: "TopicGroups",
                newName: "TopicGroup");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TopicGroup",
                table: "TopicGroup",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_TopicGroup_TopicGroupId",
                table: "Topics",
                column: "TopicGroupId",
                principalTable: "TopicGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
