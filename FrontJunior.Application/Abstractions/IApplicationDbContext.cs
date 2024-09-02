using FrontJunior.Domain.Entities.Models;
using FrontJunior.Domain.MainModels;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.Abstractions
{
    public interface IApplicationDbContext
    {
        public DbSet<ActiveUser> ActiveUsers { get; set; }
        public DbSet<ActiveTable> ActiveTables { get; set; }
        public DbSet<ActiveDataStorage> ActiveDataStorage { get; set; }
        public DbSet<Verification> Verifications { get; set; }

        public DbSet<DeletedUser> DeletedUsers { get; set; }
        public DbSet<DeletedTable> DeletedTables { get; set; }
        public DbSet<DeletedDataStorage> DeletedDataStorage { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
