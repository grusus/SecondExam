
namespace SecondExam.Data.Seeder
{
    public static class DataSeeder
    {
        public static void SeedDB(this ModelBuilder builder)
        {
            builder.SeedAuthors();
            builder.SeedMaterials();
            builder.SeedReviews();
            builder.SeedTypes();
            builder.SeedUsersWithCredentials();
        }
    }
}
