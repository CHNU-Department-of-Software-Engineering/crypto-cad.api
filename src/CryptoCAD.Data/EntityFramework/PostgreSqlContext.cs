using Microsoft.EntityFrameworkCore;
using CryptoCAD.Domain.Entities;
using CryptoCAD.Domain.Repositories;
using CryptoCAD.Data.EntityFramework.EntityConfigurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace CryptoCAD.Data.EntityFramework
{
    public class PostgreSqlContext : DbContext
    {

#if DEBUG
        private static readonly ILoggerFactory LoggerFactory =
            new LoggerFactory(
                new[] { new DebugLoggerProvider() }
                );
#endif

        public DbSet<CipherSetup> CipherConfigurations { get; set; }

        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
#if DEBUG
            optionsBuilder
                .UseLoggerFactory(LoggerFactory)
                .EnableSensitiveDataLogging(); //In Debug all generated Sql queries will be logged in Debug Output
#endif
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);

            RegisterModelConfigurations(modelBuilder);
        }

        private static void RegisterModelConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CipherSetupConfiguration());
        }
    }
}