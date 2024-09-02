using FrontJunior.Application.Abstractions;
using FrontJunior.Domain.Entities.Models;
using FrontJunior.Domain.MainModels;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<ActiveUser> ActiveUsers { get; set; }
        public DbSet<ActiveTable> ActiveTables { get; set; }
        public DbSet<ActiveDataStorage> ActiveDataStorage { get; set; }
        public DbSet<Verification> Verifications { get; set; }

        public DbSet<DeletedUser> DeletedUsers { get; set; }
        public DbSet<DeletedTable> DeletedTables { get; set; }
        public DbSet<DeletedDataStorage> DeletedDataStorage { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
