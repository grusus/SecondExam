namespace SecondExam.Data.Repository.Interfaces
{
    public interface IReviewsRepository
    {
        Task<Review?> CreateAsync(Review entity);
        Task<bool?> DeleteAsync(int id);
        Task<IEnumerable<Review>> RetrieveAllAsync();
        Task<Review?> RetrieveAsync(int id);
        Task<int> SaveChangesAsync();
        Task<Review?> UpdateAsync(Review entity);
    }
}