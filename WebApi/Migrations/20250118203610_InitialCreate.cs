using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResolutionCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResolutionCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resolutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Goal = table.Column<int>(type: "integer", nullable: true),
                    CurrentLevel = table.Column<int>(type: "integer", nullable: true),
                    IsComplete = table.Column<bool>(type: "boolean", nullable: false),
                    CompletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resolutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resolutions_ResolutionCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ResolutionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ResolutionCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Health" },
                    { 2, "Career" },
                    { 3, "Hobbies" }
                });

            migrationBuilder.InsertData(
                table: "Resolutions",
                columns: new[] { "Id", "CategoryId", "CompletedOn", "CurrentLevel", "Description", "Goal", "IsComplete", "Title" },
                values: new object[,]
                {
                    { 1, 1, null, 0, "Lose 10 pounds", 10, false, "Lose weight" },
                    { 2, 1, null, 0, "Do 1,000 pushups", 1000, false, "Gain muscle" },
                    { 3, 3, null, 0, "Practice 1,500 minutes", 1500, false, "Learn to play Guitar" },
                    { 4, 2, null, 0, "Earn a Certificate in AWS", null, false, "Earn new Certificates" },
                    { 5, 3, null, 0, "Read 10 books", 10, false, "Read more" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resolutions_CategoryId",
                table: "Resolutions",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resolutions");

            migrationBuilder.DropTable(
                name: "ResolutionCategories");
        }
    }
}
