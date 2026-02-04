namespace AwsSocialMedia.Insfrastructure.Repositories
{
    using Core.Entities;
    using Core.Interfaces;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class BaseRepository<T>(AwsSocialMediaDbContext context) : IRepository<T> where T : BaseEntity
    {
        private readonly AwsSocialMediaDbContext _context = context;
        protected DbSet<T> _entities = context.Set<T>();

        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }
        public async Task Delete(int id)
        {
            T entity = await GetById(id);

            _entities.Remove(entity);
        }
    }
}
