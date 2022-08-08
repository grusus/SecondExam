namespace SecondExam.Data.Seeder.EntitiesSeeder
{
    public static class UserSeeder
    {
        public static void SeedUsersWithCredentials(this ModelBuilder builder)
        {
            builder.Entity<User>().HasData(
                new User { UserId = 1, Role = Role.admin, CredentialsID = 1 },
                new User { UserId = 2, Role = Role.user, CredentialsID = 2 }
            );

            builder.Entity<Credentials>().HasData(
                new Credentials { CredentialsID = 1, Login = "admin", Password = "admin" },
                new Credentials { CredentialsID = 2, Login = "user", Password = "user" }
            );
        }
    }
}
