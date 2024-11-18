using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_Book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Isbn = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Book", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "T_Book",
                columns: new[] { "Id", "Author", "Isbn", "State", "Title" },
                values: new object[,]
                {
                    { 1, "Vincent V. Severski", "9788375542967", 0, "Nielegalni" },
                    { 2, "Vincent V. Severski", "9788380156227", 0, "Niepokorni" },
                    { 3, "Vincent V. Severski", "9788375546460", 0, "Niewierni" },
                    { 4, "Vincent V. Severski", "9788375549836", 0, "Nieśmiertelni" },
                    { 5, "Piotr Wroński", "9788381439961", 1, "Plac Senacki 6PM" },
                    { 6, "Piotr Wroński", "9788382526370", 1, "Dystopia" },
                    { 7, "Vincent V. Severski", "9788380159990", 1, "Zamęt" },
                    { 8, "Vincent V. Severski", "9788381430043", 0, "Odwet" },
                    { 9, "Vincent V. Severski", "9788381433839", 0, "Nabór" },
                    { 10, "Piotr Wroński", "9788395755286", 1, "Holub" },
                    { 11, "Piotr Wroński", "9788379653089", 0, "Reset" },
                    { 12, "Piotr Wroński", "9788366498013", 0, "Spisek założycielski. Historia jednego morderstwa" },
                    { 13, "Ian Flemming", "9780713481822", 0, "Casino Royale" },
                    { 14, "Ian Flemming", "9780713481823", 0, "Live and Let Die" },
                    { 15, "Ian Flemming", "9780713481824", 0, "Dr. No" },
                    { 16, "Ian Flemming", "9780713481825", 3, "From Russia, with Love" },
                    { 17, "Ian Flemming", "9780713481826", 3, "Goldfinger" },
                    { 18, "Ian Flemming", "9780713481827", 0, "The Spy Who Leved Me" },
                    { 19, "Ian Flemming", "9780713481828", 0, "On Her Majesty's Secret Service" },
                    { 20, "Ian Flemming", "9780713481829", 0, "You Only Live Twice" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_Book_Isbn",
                table: "T_Book",
                column: "Isbn",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Book");
        }
    }
}
