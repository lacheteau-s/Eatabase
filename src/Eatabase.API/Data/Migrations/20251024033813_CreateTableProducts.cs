using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eatabase.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ServingSize = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ServingSizeMetric = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Calories = table.Column<int>(type: "int", nullable: false),
                    TotalFat = table.Column<decimal>(type: "decimal(3,1)", precision: 3, scale: 1, nullable: false),
                    SaturatedFat = table.Column<decimal>(type: "decimal(3,1)", precision: 3, scale: 1, nullable: true),
                    TransFat = table.Column<decimal>(type: "decimal(3,1)", precision: 3, scale: 1, nullable: true),
                    TotalCarbs = table.Column<decimal>(type: "decimal(3,1)", precision: 3, scale: 1, nullable: false),
                    Sugars = table.Column<decimal>(type: "decimal(3,1)", precision: 3, scale: 1, nullable: true),
                    Fiber = table.Column<decimal>(type: "decimal(3,1)", precision: 3, scale: 1, nullable: true),
                    Protein = table.Column<decimal>(type: "decimal(3,1)", precision: 3, scale: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
