using SecondExam.Data.Repository.Interfaces;

namespace SecondExam.Data.Repository.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApiContext _context;
        public UsersRepository(ApiContext injectedContext)
        {
            _context = injectedContext;
        }
        public async Task<User?> CreateAsync(User entity)
        {
            EntityEntry<User> addedUser = await _context.Users.AddAsync(entity);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1)
            {
                return entity;
            }
            return null;
        }

        public async Task<User?> RetrieveAsync(int id)
        {
            return await _context.Users.Include(u => u.Credentials).FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
