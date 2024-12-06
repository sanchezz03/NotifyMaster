using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace NotifyMaster.WebApi.Controllers
{
    public class CommandExecutor : IUpdateHandler
    {
        private List<ICommand> commands;

        public CommandExecutor()
        {
            commands = new List<ICommand>();
            {
                new StartCommand();
            }
        }
        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Message msg = update.Message;
            if (msg.Text == null) //такое бывает, во избежании ошибок делаем проверку
                return;

            foreach (var command in commands)
            {
                if (command.Name == msg.Text)
                {
                    await command.Execute(update);
                }
            }
        }
    }
}
