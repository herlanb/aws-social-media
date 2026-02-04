
namespace AwsSocialMedia.Insfrastructure.Repositories
{
    using Core.Entities;
    using Core.Interfaces;
    using Data;
    using System.Threading.Tasks;

    public class UnitOfWork(AwsSocialMediaDbContext context) : IUnitOfWork
    {
        private readonly AwsSocialMediaDbContext _context = context;
        private IPostRepository _postRepository;
        private IRepository<User> _userRepository;
        private IRepository<Comment> _commentRepository;

        public IPostRepository PostRepository
        {
            get
            {
                _postRepository ??= new PostRepository(_context);

                return _postRepository;
            }
        }

        public IRepository<User> UserRepository
        {
            get
            {
                _userRepository ??= new BaseRepository<User>(_context);
                return _userRepository;
            }
        }

        public IRepository<Comment> CommentRepository
        {
            get
            {
                _commentRepository ??= new BaseRepository<Comment>(_context);
                return _commentRepository;
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
