using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer;

/// <summary>
/// EF Db context journal records
/// </summary>
public class LogContext : DbContext
{
    public DbSet<ExceptionLog> ExceptionLogs { get; set; }

    public LogContext(DbContextOptions<LogContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExceptionLog>()
            .Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<ExceptionLog>().
               ToTable("Exceptions", "dbo");
    }
}
