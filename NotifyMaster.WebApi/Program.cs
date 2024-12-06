using Hangfire;
using NotifyMaster.Application.Services;
using NotifyMaster.Application.Services.Interfaces;
using NotifyMaster.WebApi.Controllers;
using Telegram.Bot;
using Telegram.Bot.Polling;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("tgwebhook")
    .RemoveAllLoggers()
    .AddTypedClient<ITelegramBotClient>(httpClient => new TelegramBotClient("7825825837:AAH8Q3DmW7yf9rJWzqJo08ndX619Z25tM9I", httpClient));

builder.Services.AddSingleton<IUpdateHandler, UpdateHandler>();
builder.Services.ConfigureTelegramBotMvc();

builder.Services.AddControllers();
builder.Services.AddHangfire(cfg => cfg.UseSqlServerStorage("Server=SANCHEZ\\SQLEXPRESS;Database=HangfireDb;User Id=sa;Password=68joker13;Encrypt=True;TrustServerCertificate=True;"));
builder.Services.AddHangfireServer();

builder.Services.AddTransient<IReminderService, ReminderService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();