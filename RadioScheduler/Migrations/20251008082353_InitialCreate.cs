using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RadioScheduler.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "global_radio_host",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    first_name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    last_name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    is_guest = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_global_radio_host", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "global_radio_show",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    title = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    duration_min = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_global_radio_show", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "global_schedule",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    year = table.Column<int>(type: "INTEGER", nullable: false),
                    month = table.Column<int>(type: "INTEGER", nullable: false),
                    tableau_ids = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_global_schedule", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "global_studio",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    booking_price = table.Column<decimal>(type: "TEXT", nullable: false),
                    capacity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_global_studio", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tableau",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    schedule_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tableau", x => x.id);
                    table.ForeignKey(
                        name: "FK_tableau_global_schedule_id",
                        column: x => x.id,
                        principalTable: "global_schedule",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "timeslot",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    start_time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    end_time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    tableau_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_timeslot", x => x.id);
                    table.ForeignKey(
                        name: "FK_timeslot_global_radio_show_id",
                        column: x => x.id,
                        principalTable: "global_radio_show",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_timeslot_global_studio_id",
                        column: x => x.id,
                        principalTable: "global_studio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_timeslot_tableau_tableau_id",
                        column: x => x.tableau_id,
                        principalTable: "tableau",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "timeslot_host",
                columns: table => new
                {
                    host_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    timeslot_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_timeslot_host", x => new { x.host_id, x.timeslot_id });
                    table.ForeignKey(
                        name: "FK_timeslot_host_global_radio_host_host_id",
                        column: x => x.host_id,
                        principalTable: "global_radio_host",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_timeslot_host_timeslot_timeslot_id",
                        column: x => x.timeslot_id,
                        principalTable: "timeslot",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_timeslot_tableau_id",
                table: "timeslot",
                column: "tableau_id");

            migrationBuilder.CreateIndex(
                name: "IX_timeslot_host_timeslot_id",
                table: "timeslot_host",
                column: "timeslot_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "global_schedule");

            migrationBuilder.DropTable(
                name: "timeslot_host");

            migrationBuilder.DropTable(
                name: "global_radio_host");

            migrationBuilder.DropTable(
                name: "timeslot");

            migrationBuilder.DropTable(
                name: "global_radio_show");

            migrationBuilder.DropTable(
                name: "global_studio");

            migrationBuilder.DropTable(
                name: "tableau");
        }
    }
}
