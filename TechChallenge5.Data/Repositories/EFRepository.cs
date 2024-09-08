using Microsoft.EntityFrameworkCore;
using TechChallenge5.Data.DataContext;
using TechChallenge5.Domain.Entities;
using TechChallenge5.Domain.Interfaces.Repositories;

namespace TechChallenge5.Data.Repositories
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected DatabaseContext _context;
        protected DbSet<T> _dbSet;

        public EFRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<IList<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T> Update(T entity)
        {
            var result = _dbSet.Update(entity);

            await _context.SaveChangesAsync();

            return result.Entity;
        }
    }
}
