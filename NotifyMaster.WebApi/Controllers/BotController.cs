using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Polling;

[ApiController]
[Route("api/[Controller]")]
public class BotController : ControllerBase
{
    private readonly IUpdateHandler _updateHandler;

    public BotController(IUpdateHandler updateHandler)
    {
        _updateHandler = updateHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Update update, ITelegramBotClient bot, CancellationToken ct)
    {
        try
        {
            await _updateHandler.HandleUpdateAsync(bot, update, ct);
        }
        catch (Exception exception)
        {
            await _updateHandler.HandleErrorAsync(bot, exception, HandleErrorSource.HandleUpdateError, ct);
        }
        return Ok();
    }
}