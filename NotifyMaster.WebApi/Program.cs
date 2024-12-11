using NotifyMaster.Application.Extensions;
using NotifyMaster.Common.Extensions;
using NotifyMaster.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .AddConfiguration();

builder.Services
    .AddTelegramBotClient()
    .ConfigureTelegramBotMvc()
    .AddControllers().Services
    .AddDatabase()
    .AddDataProviders()
    .AddServices()
    .AddHandlers()
    .AddHangfire()
    .AddMapping();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();