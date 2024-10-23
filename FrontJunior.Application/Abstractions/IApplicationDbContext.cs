using FrontJunior.Domain.Entities.Models.PrimaryModels;
using FrontJunior.Domain.Entities.Models.SecondaryModels;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.Abstractions
{
    public interface IApplicationDbContext
    {
        public DbSet<User> Users { get; set; } 
        public DbSet<Table> Tables { get; set; } 
        public DbSet<DataStorage> DataStorage { get; set; } 
        public DbSet<Verification> Verifications { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
