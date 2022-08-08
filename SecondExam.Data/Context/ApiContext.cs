
namespace SecondExam.Data.Context
{
    public class ApiContext : DbContext
    {
#pragma warning disable CS8618
        public ApiContext(DbContextOptions<ApiContext> opt) : base(opt)
#pragma warning restore CS8618
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<MaterialType> Types { get; set; }
        public DbSet<Credentials> UserCredentials { get; set; }
        public DbSet<User> Users { get; set; }
    }
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
       
    //}
}
