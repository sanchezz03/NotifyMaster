using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NotifyMaster.Common.Enums;

#nullable disable

namespace NotifyMaster.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_MessageReminder_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageReminders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delay = table.Column<TimeSpan>(type: "time", nullable: false),
                    NotificationPhase = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReminders", x => x.Id);
                });

            migrationBuilder.InsertData(
            table: "MessageReminders",
            columns: new[] { "Message", "Delay", "NotificationPhase" },
            values: new object[,]
            {
                { "Любые изменения всегда начинаются с маленького с первого шага, с малого, но решительного выбора, в сторону лучшего себя.", TimeSpan.FromMinutes(3), (int)NotificationPhase.Welcome },
                { "Если вы ожидали знака, того самого момента, чтобы подняться над собой, обрести харизму, уверенность и силу слова, то это именно он, знак.", TimeSpan.FromMinutes(15), (int)NotificationPhase.EarlyReminder },
                { "Сюда вставить видео кружочек", TimeSpan.FromMinutes(180), (int)NotificationPhase.LateReminder },
                { "Напоминаем вам, что наше занятие уже близко. Запишитесь сейчас и начни свой путь обретению уверенности и харизмы. Стоимость участия $15. На первое занятие скидка 50%.", TimeSpan.FromMinutes(10), (int)NotificationPhase.EventPromotion }
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageReminders");
        }
    }
}
