namespace SecondExam.Data.Model
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
#pragma warning disable CS8618
        public string AuthorName { get; set; }
        public string AuthorDescription { get; set; }
#pragma warning restore CS8618
        public List<Material>? AuthorPublications { get; set; }
        [NotMapped]
        public int? Counter => AuthorPublications.Count;
    }
}
