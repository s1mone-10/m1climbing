using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace m1project.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateClimbing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Crag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sector",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CragId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sector", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sector_Crag_CragId",
                        column: x => x.CragId,
                        principalTable: "Crag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Route",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SectorId = table.Column<int>(type: "int", nullable: false),
                    CragId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Route_Crag_CragId",
                        column: x => x.CragId,
                        principalTable: "Crag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Route_Sector_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sector",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Crag_Name",
                table: "Crag",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Route_CragId",
                table: "Route",
                column: "CragId");

            migrationBuilder.CreateIndex(
                name: "IX_Route_Name_CragId",
                table: "Route",
                columns: new[] { "Name", "CragId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Route_SectorId",
                table: "Route",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sector_CragId",
                table: "Sector",
                column: "CragId");

            migrationBuilder.CreateIndex(
                name: "IX_Sector_Name_CragId",
                table: "Sector",
                columns: new[] { "Name", "CragId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Route");

            migrationBuilder.DropTable(
                name: "Sector");

            migrationBuilder.DropTable(
                name: "Crag");
        }
    }
}
