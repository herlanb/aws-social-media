namespace AwsSocialMedia.Insfrastructure.Repositories
{
    using Core.Interfaces;
    using Core.Entities;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserRepository : IUserRepository
    {
        private readonly AwsSocialMediaDbContext _context;

        public UserRepository(AwsSocialMediaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            return users;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == id);

            return user;
        }
    }
}
