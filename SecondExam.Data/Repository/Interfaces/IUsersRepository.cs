namespace SecondExam.Data.Repository.Interfaces
{
    public interface IUsersRepository
    {
        Task<User?> CreateAsync(User entity);
        Task<User?> RetrieveAsync(int id);
        Task<int> SaveChangesAsync();
        Task<Credentials?> CreateCredentialsAsync(Credentials entity);
    }
}