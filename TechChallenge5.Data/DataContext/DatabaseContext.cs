using Microsoft.EntityFrameworkCore;
using TechChallenge5.Domain.Entities;

namespace TechChallenge5.Data.DataContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UsuarioEntity> Usuarios { get; set; }
        public DbSet<PortifolioEntity> Portifolios { get; set; }
        public DbSet<TransacaoEntity> Transacoes { get; set; }
        public DbSet<AtivoEntity> Ativos { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.AtualizadoEm = DateTime.Now;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CriadoEm = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
