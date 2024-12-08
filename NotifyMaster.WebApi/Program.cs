using NotifyMaster.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .AddConfiguration();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();