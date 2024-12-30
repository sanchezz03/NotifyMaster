using NotifyMaster.Client.Extensions;
using NotifyMaster.Client.Middleware;
using NotifyMaster.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .AddConfiguration();

builder.Services
    .AddControllersWithViews().Services
    .AddMapping()
    .AddFlurlClient();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}
else
{
    app.UseMiddleware<GlobalExceptionHandler>();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}")
    .WithStaticAssets();


app.Run();
