namespace SecondExam.Data.Seeder.EntitiesSeeder
{
    public static class MaterialsSeeder
    {
        public static void SeedMaterials(this ModelBuilder builder)
        {
            List<Material> material = new();
            string[] fileLines = File.ReadAllLines("../SecondExam.Data/Seeder/EntitiesSeeder/Data/Materials.txt");
            foreach (string line in fileLines)
            {
                var item = GetItemFromLine(line);
                material.Add(item);
            }

            builder.Entity<Material>().HasData(material);
        }

        private static Material GetItemFromLine(string line)
        {
            string[] item = line.Split('|');
            int id = Convert.ToInt32(item[0]);
            string title = item[1];
            string description = item[2];
            string location = item[3];
            int authorId = Convert.ToInt32(item[4]);
            int typeId = Convert.ToInt32(item[5]);
            return new Material() { MaterialId = id, MaterialTitle = title, MaterialDescription = description, MaterialLocation = location, AuthorId = authorId,
                MaterialTypeId = typeId, MaterialReviews = new List<Review>(), CreatedDate = GetRandomDate(new DateTime(1990,01,01),new DateTime(2020,01,01)) };
        }

        public static DateTime GetRandomDate(DateTime from, DateTime to)
        {
            Random rnd = new();
            var range = to - from;
            var randTimeSpan = new TimeSpan((long)(rnd.NextDouble() * range.Ticks));

            return from + randTimeSpan;
        }
    }
}
