using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CzSharp.Migrations
{
    public partial class migration16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TopicGroupId",
                table: "Topics",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TopicGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicGroup", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Topics_TopicGroupId",
                table: "Topics",
                column: "TopicGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_TopicGroup_TopicGroupId",
                table: "Topics",
                column: "TopicGroupId",
                principalTable: "TopicGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_TopicGroup_TopicGroupId",
                table: "Topics");

            migrationBuilder.DropTable(
                name: "TopicGroup");

            migrationBuilder.DropIndex(
                name: "IX_Topics_TopicGroupId",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "TopicGroupId",
                table: "Topics");
        }
    }
}
