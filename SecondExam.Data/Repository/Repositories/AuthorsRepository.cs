using SecondExam.Data.Repository.Interfaces;

namespace SecondExam.Data.Repository.Repositories
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly ApiContext _context;
        public AuthorsRepository(ApiContext injectedContext)
        {
            _context = injectedContext;
        }
        public async Task<Author?> CreateAsync(Author entity)
        {
            EntityEntry<Author> addedAuthor = await _context.Authors.AddAsync(entity);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1) return entity;
            return null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Author? soughtAuthor = await _context.Authors.FindAsync(id);
            if (soughtAuthor == null) return null;
            _context.Authors.Remove(soughtAuthor);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1) return true;
            return null;
        }

        public async Task<IEnumerable<Author>> RetrieveAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author?> RetrieveAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }
        public async Task<Author?> RetrieveAsyncWithPublications(int id)
        {
            return await _context.Authors
                .Include(x => x.AuthorPublications)
                .Where(x => x.AuthorId == id)
                .FirstOrDefaultAsync();
        }
        public async Task<Author?> RetrieveMostProductiveAuthor()
        {
            var authors = await _context.Authors.Include(x => x.AuthorPublications).ToListAsync();

            return authors.MaxBy(x => x.Counter);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<Author?> UpdateAsync(Author entity)
        {
            _context.Authors.Update(entity);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1) return entity;
            return null;
        }
    }
}
