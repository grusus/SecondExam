namespace SecondExam.Data.Repository.Interfaces
{
    public interface IAuthorsRepository
    {
        Task<Author?> CreateAsync(Author entity);
        Task<bool?> DeleteAsync(int id);
        Task<IEnumerable<Author>> RetrieveAllAsync();
        Task<Author?> RetrieveAsync(int id);
        Task<int> SaveChangesAsync();
        Task<Author?> UpdateAsync(Author entity);
        Task<Author?> RetrieveAsyncWithPublications(int id);
        Task<Author?> RetrieveMostProductiveAuthor();
    }
}