using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dashboard.Migrations
{
    public partial class sqliteMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServerGroupModels",
                columns: table => new
                {
                    ServerGroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ServerGroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerGroupModels", x => x.ServerGroupId);
                });

            migrationBuilder.CreateTable(
                name: "ServerModels",
                columns: table => new
                {
                    ServerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ServerName = table.Column<string>(nullable: true),
                    ServerGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerModels", x => x.ServerId);
                    table.ForeignKey(
                        name: "FK_ServerModels_ServerGroupModels_ServerGroupId",
                        column: x => x.ServerGroupId,
                        principalTable: "ServerGroupModels",
                        principalColumn: "ServerGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DriverModels",
                columns: table => new
                {
                    DriverId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DriverName = table.Column<string>(nullable: true),
                    CurrentCache = table.Column<int>(nullable: false),
                    PreviousCache = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    ServerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverModels", x => x.DriverId);
                    table.ForeignKey(
                        name: "FK_DriverModels_ServerModels_ServerId",
                        column: x => x.ServerId,
                        principalTable: "ServerModels",
                        principalColumn: "ServerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MessageModels",
                columns: table => new
                {
                    MessageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MessageType = table.Column<string>(nullable: true),
                    MessageDate = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    ServerId = table.Column<int>(nullable: false),
                    DriverId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageModels", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_MessageModels_DriverModels_DriverId",
                        column: x => x.DriverId,
                        principalTable: "DriverModels",
                        principalColumn: "DriverId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageModels_ServerModels_ServerId",
                        column: x => x.ServerId,
                        principalTable: "ServerModels",
                        principalColumn: "ServerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverModels_ServerId",
                table: "DriverModels",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageModels_DriverId",
                table: "MessageModels",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageModels_ServerId",
                table: "MessageModels",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerModels_ServerGroupId",
                table: "ServerModels",
                column: "ServerGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageModels");

            migrationBuilder.DropTable(
                name: "DriverModels");

            migrationBuilder.DropTable(
                name: "ServerModels");

            migrationBuilder.DropTable(
                name: "ServerGroupModels");
        }
    }
}
