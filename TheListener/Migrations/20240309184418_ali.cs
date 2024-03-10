using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheListener.Migrations
{
    /// <inheritdoc />
    public partial class ali : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Germany" },
                    { 2, "French" },
                    { 3, "English" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, 1, "This is Jana and welcome to Storynory. I’m back with another cheeky Monkey story.", "sssssssssssss", "Listen German" },
                    { 2, 1, "Katie is learning Latin - the language of the ancient Romans", "sssssssssssss", "Listen German 2" },
                    { 3, 2, "Jana talks with our friends Matthew and Elaine Sweetapple, the creators of Lost on Infinity", "sssssssssssss", "Listen French" },
                    { 4, 2, "This year Chinese and many Asian nations are celebrating the year of the Dragon.", "sssssssssssss", "Listen French 2" },
                    { 5, 3, "Parents and older kids check out Relaxivity and our Cleopatra story.", "sssssssssssss", "Listen English" },
                    { 6, 3, "Jana, and I’m here with a story about an old friend of ours. His name is Birdy.", "sssssssssssss", "Listen English 2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
