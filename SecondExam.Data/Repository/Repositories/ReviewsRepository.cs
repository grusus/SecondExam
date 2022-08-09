namespace SecondExam.Data.Repository.Repositories
{
    public class ReviewsRepository : IReviewsRepository
    {
        private readonly ApiContext _context;

        public ReviewsRepository(ApiContext injectedContext)
        {
            _context = injectedContext;
        }

        public async Task<Review?> CreateAsync(Review entity)
        {
            EntityEntry<Review> addedReview = await _context.Reviews.AddAsync(entity);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1) return entity;
            return null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Review? soughtReview = await _context.Reviews.FindAsync(id);
            if (soughtReview == null) return null;
            _context.Reviews.Remove(soughtReview);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1) return true;
            return null;
        }

        public async Task<IEnumerable<Review>> RetrieveAllAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review?> RetrieveAsync(int id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<Review?> UpdateAsync(Review entity)
        {
            _context.Reviews.Update(entity);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1) return entity;
            return null;
        }
    }
}
