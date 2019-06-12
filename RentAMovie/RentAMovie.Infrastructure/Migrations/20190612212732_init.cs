using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentAMovie.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateOfCreation = table.Column<DateTime>(nullable: false),
                    DateOfUpdate = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateOfCreation = table.Column<DateTime>(nullable: false),
                    DateOfUpdate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Director = table.Column<string>(nullable: true),
                    Genre = table.Column<string>(nullable: true),
                    ReleaseDate = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    IsRented = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Borrow",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateOfCreation = table.Column<DateTime>(nullable: false),
                    DateOfUpdate = table.Column<DateTime>(nullable: false),
                    DateOfBorrow = table.Column<DateTime>(nullable: false),
                    DateOfReturn = table.Column<DateTime>(nullable: false),
                    ClientId = table.Column<long>(nullable: true),
                    MovieId = table.Column<long>(nullable: true),
                    ClientId1 = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Borrow_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Borrow_Client_ClientId1",
                        column: x => x.ClientId1,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Borrow_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Borrow_ClientId",
                table: "Borrow",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrow_ClientId1",
                table: "Borrow",
                column: "ClientId1");

            migrationBuilder.CreateIndex(
                name: "IX_Borrow_MovieId",
                table: "Borrow",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Borrow");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Movie");
        }
    }
}
