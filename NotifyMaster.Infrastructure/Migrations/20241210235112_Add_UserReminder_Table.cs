using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotifyMaster.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_UserReminder_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserReminders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduledTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReminderMessageId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReminders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserReminders_MessageReminders_ReminderMessageId",
                        column: x => x.ReminderMessageId,
                        principalTable: "MessageReminders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserReminders_ReminderMessageId",
                table: "UserReminders",
                column: "ReminderMessageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserReminders");
        }
    }
}
