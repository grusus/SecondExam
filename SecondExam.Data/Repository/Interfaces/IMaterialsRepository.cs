namespace SecondExam.Data.Repository.Interfaces
{
    public interface IMaterialsRepository
    {
        Task<Material?> CreateAsync(Material entity);
        Task<bool?> DeleteAsync(int id);
        Task<IEnumerable<Material>> RetrieveAllAsync();
        Task<Material?> RetrieveAsync(int id);
        Task<int> SaveChangesAsync();
        Task<Material?> UpdateAsync(Material entity);
        Task<Material?> RetrieveAsyncWithDetails(int id);
        Task<IEnumerable<Material>> RetrieveAllWithRatingAbove5Async(Author author);
    }
}