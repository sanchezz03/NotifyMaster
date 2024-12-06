using Telegram.Bot;
using Telegram.Bot.Types;

namespace NotifyMaster.WebApi.Controllers
{
    public class StartCommand //: ICommand
    {
        public string Name => "/start";
        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;
           // await Client.SendTextMessageAsync(chatId, "Hello!");
        }
    }
}
