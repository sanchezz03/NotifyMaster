using NotifyMaster.Application.Extensions;
using NotifyMaster.Common.Extensions;
using NotifyMaster.Infrastructure.Extensions;
using NotifyMaster.WebApi.Extensions;

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
    .AddMapping()
    .AddCORS();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();