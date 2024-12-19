using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotifyMaster.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Delete_Unused_Field_In_UserReminder_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReminders_MessageReminders_ReminderMessageId",
                table: "UserReminders");

            migrationBuilder.DropIndex(
                name: "IX_UserReminders_ReminderMessageId",
                table: "UserReminders");

            migrationBuilder.DropColumn(
                name: "ReminderMessageId",
                table: "UserReminders");

            migrationBuilder.DropColumn(
                name: "ScheduledTime",
                table: "UserReminders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ReminderMessageId",
                table: "UserReminders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledTime",
                table: "UserReminders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_UserReminders_ReminderMessageId",
                table: "UserReminders",
                column: "ReminderMessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReminders_MessageReminders_ReminderMessageId",
                table: "UserReminders",
                column: "ReminderMessageId",
                principalTable: "MessageReminders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
