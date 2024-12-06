using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using NotifyMaster.Application.Services.Interfaces;
using System.Collections.Concurrent;

namespace NotifyMaster.WebApi.Controllers
{
    public class UpdateHandler : IUpdateHandler
    {
        private readonly IReminderService _reminderService;
        private readonly ITelegramBotClient _bot;
        private readonly ILogger _logger;
        private readonly ConcurrentDictionary<int, string> _reminderSchedule;

        private const string JOIN_CLUB_CALLBACK = "join_club";

        public UpdateHandler(IReminderService reminderService, ITelegramBotClient bot, ILogger<UpdateHandler> logger)
        {
            _reminderService = reminderService;
            _bot = bot;
            _logger = logger;
            _reminderSchedule = new()
            {
                [3] = "Любые изменения всегда начинаются с маленького с первого шага, с малого, но решительного выбора, в сторону лучшего себя.",
                [15] = "Если вы ожидали знака, того самого момента, чтобы подняться над собой, обрести харизму, уверенность и силу слова, то это именно он, знак.",
                [180] = "Сюда вставить видео кружочек"
            };
        }

        public async Task HandleUpdateAsync(ITelegramBotClient _botClient, Update update, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await (update switch
            {
                { Message: { } message } => OnMessage(message),
                { EditedMessage: { } message } => OnMessage(message),
                { CallbackQuery: { } callbackQuery } => OnCallbackQuery(callbackQuery),

                _ => UnknownUpdateHandlerAsync(update)
            });
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
        {
            _logger.LogInformation("HandleError: {Exception}", exception);

            if (exception is RequestException)
                await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
        }

        public async Task OnMessage(Message msg)
        {
            _logger.LogInformation("Receive message type: {MessageType}", msg.Type);

            if (msg.Text is not { } messageText)
            {
                return;
            }

            switch (messageText.Split(' ')[0])
            {
                case "/start":
                    
                    ScheduleReminders(msg.Chat.Id, msg.From.Id);
                    await SendWelcomeMessage(msg);
                    break;

                default:
                    await Usage(msg);
                    break;
            }
        }

        #region Private methods  

        private void ScheduleReminders(long chatId, long userId)
        {
            foreach (var (delay, message) in _reminderSchedule)
            {
                _reminderService.ScheduleReminder(chatId, userId, message, JOIN_CLUB_CALLBACK, TimeSpan.FromMinutes(delay));
            }
        }

        private async Task SendWelcomeMessage(Message msg)
        {
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

            await SendMessage(msg.Chat.Id, welcomeMessage, JOIN_CLUB_CALLBACK);
        }


        async Task<Message> Usage(Message msg)
        {
            const string usage = """
                <b><u>Bot menu</u></b>:
                /photo          - send a photo
                /inline_buttons - send inline buttons
                /keyboard       - send keyboard buttons
                /remove         - remove keyboard buttons
                /request        - request location or contact
                /inline_mode    - send inline-mode results list
                /poll           - send a poll
                /poll_anonymous - send an anonymous poll
                /throw          - what happens if handler fails
            """;
            return await _bot.SendMessage(msg.Chat, usage, parseMode: ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
        }

        private async Task SendMessage(long chatId, string message, string callbackData)
        {
            var joinClubButton = new InlineKeyboardMarkup(
                InlineKeyboardButton.WithCallbackData("Вступить в группу", callbackData)
            );

            await _bot.SendTextMessageAsync(
                chatId: chatId,
                text: message,
                parseMode: ParseMode.Markdown,
                replyMarkup: joinClubButton
            );
        }

        private async Task OnCallbackQuery(CallbackQuery callbackQuery)
        {
            switch (callbackQuery.Data)
            {
                case "join_club":
                    _reminderService.CancelReminders(callbackQuery.From.Id);
                    await _bot.SendTextMessageAsync(
                    chatId: callbackQuery.Message.Chat.Id,
                    text: "Добро пожаловать! Мы рады видеть вас среди тех, кто горит стремлением к лучшему. " +
                    "Это полезное видео — ваш первый инструмент на пути к совершенствованию. " +
                    "[Сюда вставить тему первого видео, переписать предложение, чтобы гармонично сочеталось с тематикой]",
                    parseMode: ParseMode.Markdown);
                    break;

                default:
                    break;
            }
        }

        private Task UnknownUpdateHandlerAsync(Update update)
        {
            _logger.LogInformation("Unknown update type: {UpdateType}", update.Type);
            return Task.CompletedTask;
        }

        #endregion
    }
}
