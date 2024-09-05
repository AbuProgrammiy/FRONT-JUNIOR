using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrontJunior.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActiveUsers",
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
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeletedUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    PassworSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    SecurityKey = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeletedUsers", x => x.Id);
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
                name: "ActiveTables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ColumnCount = table.Column<byte>(type: "smallint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActiveTables_ActiveUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ActiveUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeletedTables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ColumnCount = table.Column<byte>(type: "smallint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeletedTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeletedTables_DeletedUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "DeletedUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActiveDataStorage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TableId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    IsData = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveDataStorage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActiveDataStorage_ActiveTables_TableId",
                        column: x => x.TableId,
                        principalTable: "ActiveTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeletedDataStorage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TableId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    IsData = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeletedDataStorage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeletedDataStorage_DeletedTables_TableId",
                        column: x => x.TableId,
                        principalTable: "DeletedTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveDataStorage_TableId",
                table: "ActiveDataStorage",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveTables_UserId",
                table: "ActiveTables",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeletedDataStorage_TableId",
                table: "DeletedDataStorage",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_DeletedTables_UserId",
                table: "DeletedTables",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveDataStorage");

            migrationBuilder.DropTable(
                name: "DeletedDataStorage");

            migrationBuilder.DropTable(
                name: "Verifications");

            migrationBuilder.DropTable(
                name: "ActiveTables");

            migrationBuilder.DropTable(
                name: "DeletedTables");

            migrationBuilder.DropTable(
                name: "ActiveUsers");

            migrationBuilder.DropTable(
                name: "DeletedUsers");
        }
    }
}
