using Microsoft.EntityFrameworkCore.Migrations;
using NotifyMaster.Core.Entities;
using System.Collections.Generic;

#nullable disable

namespace NotifyMaster.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Add_VideoUrl_Field : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "VideoUrl",
            table: "MessageReminders",
            type: "text",
            nullable: false,
        defaultValue: "");

        migrationBuilder.Sql(@"
            UPDATE ""MessageReminders""
            SET ""Message"" = 'Если вы все еще сомневаетесь - ничего страшного, мы понимаем. Мы будем рады видеть вас в нашем канале, где вы найдете полезную информацию, советы и расписание наших событий. \nПодписка ни к чему вас не обязывает, но даст возможность оставаться в курсе и получать вдохновение. А когда вы будете готовы сделать первый шаг к изменениям, мы обязательно будем рядом, чтобы поддержать вас!',
                ""VideoUrl"" = 'https://videocdn.cdnpk.net/joy/content/video/free/2014-12/large_preview/Raindrops_Videvo.mp4'
            WHERE ""Id"" = 3;
        ");

        migrationBuilder.Sql(@"
            INSERT INTO ""MessageReminders"" (""Message"", ""VideoUrl"", ""Delay"", ""NotificationPhase"")
            VALUES 
            (
                'Добро пожаловать! Мы рады видеть вас среди тех, кто горит стремлением к лучшему. Это полезное видео - ваш первый инструмент на пути к совершенствованию.',
                'https://videocdn.cdnpk.net/joy/content/video/free/2014-12/large_preview/Raindrops_Videvo.mp4',
                '00:00:00',
                5
            ),
            (
                'Вы сделали важный шаг, и я поздравляю вас. Но перемены требуют действий, шаг за шагом. Наше следующее мероприятие — это ваш шанс учиться, расти и становиться более уверенным. Стоимость участия $15. Запишитесь прямо сейчас и получите скидку 50% на первое занятие.',
                '',
                '00:00:00',
                5
            );
        ");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "VideoUrl",
            table: "MessageReminders");
    }
}
