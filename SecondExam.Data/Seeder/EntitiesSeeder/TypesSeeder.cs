using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondExam.Data.Seeder.EntitiesSeeder
{
    public static class TypesSeeder
    {
        public static void SeedTypes(this ModelBuilder builder)
        {
            List<MaterialType> types = new();
            string[] fileLines = File.ReadAllLines("../SecondExam.Data/Seeder/EntitiesSeeder/Data/Types.txt");
            foreach (string line in fileLines)
            {
                var item = GetItemFromLine(line);
                types.Add(item);
            }

            builder.Entity<MaterialType>().HasData(types);
        }
        private static MaterialType GetItemFromLine(string line)
        {
            string[] item = line.Split('|');
            int id = Convert.ToInt32(item[0]);
            string name = item[1];
            string definition = item[2];
            return new MaterialType() { TypeId = id, TypeName = name, TypeDefinition = definition };
        }
    }
}
