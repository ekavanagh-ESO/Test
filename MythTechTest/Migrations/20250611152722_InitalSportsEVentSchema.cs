using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MythTechTest.Migrations
{
    /// <inheritdoc />
    public partial class InitalSportsEVentSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SportsEvents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    StartDateLocal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduledStartTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Attendance = table.Column<int>(type: "int", nullable: true),
                    SportId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VenueId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DirectParentSportsEventId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeParticipantId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AwayParticipantId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParticipantType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportsEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventMetas",
                columns: table => new
                {
                    SportsEventId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UpdateId = table.Column<long>(type: "bigint", nullable: false),
                    UpdateAction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventMetas", x => x.SportsEventId);
                    table.ForeignKey(
                        name: "FK_EventMetas_SportsEvents_SportsEventId",
                        column: x => x.SportsEventId,
                        principalTable: "SportsEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SportsEventId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventStates_SportsEvents_SportsEventId",
                        column: x => x.SportsEventId,
                        principalTable: "SportsEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParentEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SportsEventId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ParentEventId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParentEvents_SportsEvents_SportsEventId",
                        column: x => x.SportsEventId,
                        principalTable: "SportsEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RelatedEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SportsEventId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RelatedEventId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Depth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NavigationInfo_Id = table.Column<int>(type: "int", nullable: true),
                    NavigationInfo_HasStandings = table.Column<bool>(type: "bit", nullable: true),
                    NavigationInfo_IsKnockout = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelatedEvents_SportsEvents_SportsEventId",
                        column: x => x.SportsEventId,
                        principalTable: "SportsEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SportsOrganizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SportsEventId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrganizationId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportsOrganizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SportsOrganizations_SportsEvents_SportsEventId",
                        column: x => x.SportsEventId,
                        principalTable: "SportsEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventStates_SportsEventId",
                table: "EventStates",
                column: "SportsEventId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentEvents_SportsEventId",
                table: "ParentEvents",
                column: "SportsEventId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedEvents_SportsEventId",
                table: "RelatedEvents",
                column: "SportsEventId");

            migrationBuilder.CreateIndex(
                name: "IX_SportsOrganizations_SportsEventId",
                table: "SportsOrganizations",
                column: "SportsEventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventMetas");

            migrationBuilder.DropTable(
                name: "EventStates");

            migrationBuilder.DropTable(
                name: "ParentEvents");

            migrationBuilder.DropTable(
                name: "RelatedEvents");

            migrationBuilder.DropTable(
                name: "SportsOrganizations");

            migrationBuilder.DropTable(
                name: "SportsEvents");
        }
    }
}
