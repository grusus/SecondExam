namespace SecondExam.Data.Model
{
    public class Material
    {
        [Key]
        public int MaterialId { get; set; }
#pragma warning disable CS8618
        public string MaterialTitle { get; set; }
        public string MaterialDescription { get; set; }
        public string MaterialLocation { get; set; }
        public Author? MatherialAuthor { get; set; }
        public int? AuthorId { get; set; }
        public MaterialType? MaterialType { get; set; }
        public int? TypeId { get; set; }
        public List<Review>? MaterialReviews { get; set; }
#pragma warning restore CS8618
        public DateTime? CreatedDate { get; set; }
    }
}
