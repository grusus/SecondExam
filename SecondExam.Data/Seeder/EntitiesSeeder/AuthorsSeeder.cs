using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondExam.Data.Seeder.EntitiesSeeder
{
    public static class AuthorsSeeder
    {
        public static void SeedAuthors(this ModelBuilder builder)
        {
            List<Author> authors = new();
            string[] fileLines = File.ReadAllLines("../SecondExam.Data/Seeder/EntitiesSeeder/Data/Authors.txt");
            foreach (string line in fileLines)
            {
                var item = GetItemFromLine(line);
                authors.Add(item);
            }

            builder.Entity<Author>().HasData(authors);
        }
        private static Author GetItemFromLine(string line)
        {
            string[] item = line.Split('|');
            int id = Convert.ToInt32(item[0]);
            string name = item[1];
            string description = item[2];
            return new Author() { AuthorId = id, AuthorName = name, AuthorDescription = description, AuthorPublications = new List<Material>() };
        }
    }
}
