using Microsoft.EntityFrameworkCore;
using TrainMonitor.domain.Entities;
using TrainMonitor.repository.Configuration;

namespace TrainMonitor.repository;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Train> Trains { get; set; }
    public DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new TrainConfiguration());
        modelBuilder.ApplyConfiguration(new AccountConfiguration());

    }
}
