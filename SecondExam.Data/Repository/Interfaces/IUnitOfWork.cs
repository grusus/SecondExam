namespace SecondExam.Data.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IAuthorsRepository Authors { get; }
        IMaterialsRepository Materials { get; }
        ITypesRepository MaterialsTypes { get; }
        IReviewsRepository Reviews { get; }
        IUsersRepository Users { get; }

        int CompleteUnit();
    }
}