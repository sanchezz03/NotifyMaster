using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NotifyMaster.Common.ConfigurationModels;
using NotifyMaster.Core.Entities;

namespace NotifyMaster.Infrastructure.Data;

public class NotifyMasterDbContext : DbContext
{
    private readonly string _connectionString;

    #region Entities DbSet

    public DbSet<User> Users { get; set; }
    public DbSet<UserReminder> UserReminders { get; set; }
    public DbSet<MessageReminder> MessageReminders { get; set; }

    #endregion

    public NotifyMasterDbContext(DbContextOptions options, IOptions<DatabaseConfiguration> databaseConfiguration) : base(options)
    {
        _connectionString = databaseConfiguration.Value.ConnectionString;
    }

    #region Protected Methods

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotifyMasterDbContext).Assembly);
    }

    #endregion
}