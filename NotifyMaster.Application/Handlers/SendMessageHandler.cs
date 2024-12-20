using NotifyMaster.Application.Handlers.Interfaces;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace NotifyMaster.Application.Handlers;

public class SendMessageHandler : ISendMessageHandler
{
    private const string VIDEO_NAME_PATTERN = @"[^/]+$";

    public async Task SendMessage(ITelegramBotClient bot, long chatId, string message, string videuUrl = null, 
        string callbackData = null, string button = null)
    {
        var replyMarkup = button != null && callbackData != null
            ? CreateButton(button, callbackData)
            : null;

        if (string.IsNullOrEmpty(videuUrl))
        {
            await SendMessageAsync(bot, chatId, message, replyMarkup);
        }
        else
        {
            await SendVideoAsync(bot, chatId, message, videuUrl, replyMarkup);
        }
    }

    #region Private region

    private async Task SendMessageAsync(ITelegramBotClient bot, long chatId, string message, InlineKeyboardMarkup? replyMarkup)
    {
        await bot.SendTextMessageAsync(
            chatId: chatId,
            text: message,
            parseMode: ParseMode.Markdown,
            replyMarkup: replyMarkup
        );
    }

    private async Task SendVideoAsync(ITelegramBotClient bot, long chatId, string message, 
        string videoUrl, InlineKeyboardMarkup? replyMarkup)
    {
        using (var client = new HttpClient())
        {
            var videoBytes = await client.GetByteArrayAsync(videoUrl);
            string videoName = Regex.Match(videoUrl, VIDEO_NAME_PATTERN).Value;

            var videoFile = InputFile.FromStream(new MemoryStream(videoBytes), videoName);

            await bot.SendVideoAsync(
                chatId: chatId,
                video: videoFile,
                caption: message,
                parseMode: ParseMode.Markdown,
                replyMarkup: replyMarkup,
                supportsStreaming: true
            );
        }
    }

    private InlineKeyboardMarkup CreateButton(string buttonName, string callbackData)
    {
        return new InlineKeyboardMarkup(
            InlineKeyboardButton.WithCallbackData(buttonName, callbackData)
        );
    }

    #endregion
}