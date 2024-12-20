using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotifyMaster.Infrastructure.Data;

namespace NotifyMaster.Infrastructure.Extensions;

public static class MigrationExtension
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using NotifyMasterDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<NotifyMasterDbContext>();

        dbContext.Database.Migrate();
    }
}
