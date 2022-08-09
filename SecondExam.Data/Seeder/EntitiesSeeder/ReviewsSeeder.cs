namespace SecondExam.Data.Seeder.EntitiesSeeder
{
    public static class ReviewsSeeder
    {
        public static void SeedReviews(this ModelBuilder builder)
        {
            List<Review> reviews = new();
            string[] fileLines = File.ReadAllLines("../SecondExam.Data/Seeder/EntitiesSeeder/Data/Reviews.txt");
            foreach (string line in fileLines)
            {
                var item = GetItemFromLine(line);
                reviews.Add(item);
            }

            builder.Entity<Review>().HasData(reviews);
        }

        private static Review GetItemFromLine(string line)
        {
            string[] item = line.Split('|');
            int id = Convert.ToInt32(item[0]);
            string reference = item[1];
            string textReview = item[2];
            int digitReview = Convert.ToInt32(item[3]);
            int materialId = Convert.ToInt32(item[4]);
            return new Review() { ReviewId = id, ReviewReference = reference, TextReview = textReview, DigitReview = digitReview, MaterialId = materialId };
        }
    }
}
