namespace SecondExam.Data.Repository.Interfaces
{
    public interface ITypesRepository
    {
        Task<MaterialType?> CreateAsync(MaterialType entity);
        Task<bool?> DeleteAsync(int id);
        Task<IEnumerable<MaterialType>> RetrieveAllAsync();
        Task<MaterialType?> RetrieveAsync(int id);
        Task<int> SaveChangesAsync();
        Task<MaterialType?> UpdateAsync(MaterialType entity);
    }
}