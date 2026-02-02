namespace AwsSocialMedia.Insfrastructure.Repositories
{
    using Core.Entities;
    using Core.Interfaces;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AwsSocialMediaDbContext _context;
        private DbSet<T> _entities;
        public BaseRepository(AwsSocialMediaDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task Add(T entity)
        {
            _entities.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _entities.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            T entity = await GetById(id);

            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
