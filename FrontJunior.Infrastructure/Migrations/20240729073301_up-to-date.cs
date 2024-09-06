using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrontJunior.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class uptodate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    PassworSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    SecurityKey = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Verifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    SentPassword = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ColumnCount = table.Column<byte>(type: "smallint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tables_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataStorage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Column1 = table.Column<string>(type: "text", nullable: true),
                    Column2 = table.Column<string>(type: "text", nullable: true),
                    Column3 = table.Column<string>(type: "text", nullable: true),
                    Column4 = table.Column<string>(type: "text", nullable: true),
                    Column5 = table.Column<string>(type: "text", nullable: true),
                    Column6 = table.Column<string>(type: "text", nullable: true),
                    Column7 = table.Column<string>(type: "text", nullable: true),
                    Column8 = table.Column<string>(type: "text", nullable: true),
                    Column9 = table.Column<string>(type: "text", nullable: true),
                    Column10 = table.Column<string>(type: "text", nullable: true),
                    Column11 = table.Column<string>(type: "text", nullable: true),
                    Column12 = table.Column<string>(type: "text", nullable: true),
                    Column13 = table.Column<string>(type: "text", nullable: true),
                    Column14 = table.Column<string>(type: "text", nullable: true),
                    Column15 = table.Column<string>(type: "text", nullable: true),
                    Column16 = table.Column<string>(type: "text", nullable: true),
                    Column17 = table.Column<string>(type: "text", nullable: true),
                    Column18 = table.Column<string>(type: "text", nullable: true),
                    Column19 = table.Column<string>(type: "text", nullable: true),
                    Column20 = table.Column<string>(type: "text", nullable: true),
                    IsData = table.Column<bool>(type: "boolean", nullable: false),
                    TableId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataStorage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataStorage_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataStorage_TableId",
                table: "DataStorage",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_UserId",
                table: "Tables",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataStorage");

            migrationBuilder.DropTable(
                name: "Verifications");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
