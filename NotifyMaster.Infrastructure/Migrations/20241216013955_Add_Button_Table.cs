using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NotifyMaster.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Button_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Buttons",
               columns: table => new
               {
                   Id = table.Column<long>(type: "bigint", nullable: false)
                       .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                   Name = table.Column<string>(type: "text", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Buttons", x => x.Id);
               });

            migrationBuilder.Sql(@"              INSERT INTO ""Buttons"" (""Name"")
              VALUES                ('Подписаться на канал'),
              ('Записаться на онлайн мастеркласс');              ");

            migrationBuilder.AddColumn<long>(
                name: "ButtonId",
                table: "MessageReminders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.Sql(@"
                UPDATE ""MessageReminders""          
                SET ""ButtonId"" = 1
                WHERE ""Id"" IN(1, 2, 3);
                UPDATE ""MessageReminders""          
                SET ""ButtonId"" = 2
                WHERE ""Id"" IN(4, 5, 6);          
            ");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageReminders_Buttons_ButtonId",
                table: "MessageReminders",
                column: "ButtonId",
                principalTable: "Buttons",
                principalColumn: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReminders_ButtonId",
                table: "MessageReminders",
                column: "ButtonId");

            const string welcomeMessage = """
            Знакомо это?
            Вы стоите перед группой и внимание на вас. От волнения охватывает
            дрожь, ладони сыреют, а мысли рассыпаются.
            Вас перебивают, а в споре, вместо того чтобы отстаивать своё, вы
            уступаете.
            Ваши истории кажутся скучными, а народ то и дело отвлекается на
            телефон.
            Мы знаем, каково это — страх быть не услышанным и не быть способным
            убедительно донести свою точку зрения.
            Но есть хорошая новость: всё это можно изменить! Наш клуб — это
            пространство, где вы:
            - Станете уверенным и красноречивым.
            - Научитесь отстаивать свои границы и распознавать манипуляции.
            - Будете рассказывать истории так, чтобы вас слушали с замиранием.
            - Сможете мыслить чётко и говорить ярко даже в самых стрессовых
            ситуациях.

            Присоединяйтесь к нам. Здесь вы найдёте знания, поддержку и вдохновение.
            И, как первый шаг, мы отправим вам видео, которое поможет начать.
            """;

            migrationBuilder.Sql($@"
            INSERT INTO ""MessageReminders"" (""Message"", ""ButtonId"", ""Delay"", ""NotificationPhase"")
            VALUES 
                ('{welcomeMessage}', 1,  '00:10:00', 0);
            ");

            migrationBuilder.Sql(@"
            UPDATE ""MessageReminders""
            SET ""NotificationPhase"" = 4
            WHERE ""Id"" = 4;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageReminders_Buttons_ButtonId",
                table: "MessageReminders");

            migrationBuilder.DropTable(
                name: "Buttons");

            migrationBuilder.DropIndex(
                name: "IX_MessageReminders_ButtonId",
                table: "MessageReminders");

            migrationBuilder.DropColumn(
                name: "ButtonId",
                table: "MessageReminders");
        }
    }
}
