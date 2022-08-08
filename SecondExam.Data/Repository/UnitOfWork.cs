namespace SecondExam.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiContext _context;
        public IAuthorsRepository Authors { get; }
        public IMaterialsRepository Materials { get; }
        public IReviewsRepository Reviews { get; }
        public ITypesRepository MaterialsTypes { get; }
        public IUsersRepository Users { get; }

        public UnitOfWork(ApiContext context)
        {
            _context = context;
            Authors = new AuthorsRepository(_context);
            Materials = new MaterialsRepository(_context);
            Reviews = new ReviewsRepository(_context);
            MaterialsTypes = new TypesRepository(_context);
            Users = new UsersRepository(_context);
        }

        public int CompleteUnit()
            => _context.SaveChanges();
    }
}
