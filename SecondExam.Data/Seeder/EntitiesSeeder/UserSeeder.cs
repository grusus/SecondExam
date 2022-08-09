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
                new Credentials { CredentialsID = 1, Login = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918", Password = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918" },
                new Credentials { CredentialsID = 2, Login = "04f8996da763b7a969b1028ee3007569eaf3a635486ddab211d512c85b9df8fb", Password = "04f8996da763b7a969b1028ee3007569eaf3a635486ddab211d512c85b9df8fb" }
            );
        }
    }
}
