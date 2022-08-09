namespace SecondExam.DTOs.AuthorsDTOs
{
    public class AuthorGetFullDTO
    {
        public string AuthorName { get; set; }
        public string AuthorDescription { get; set; }
#pragma warning restore CS8618
        public List<MaterialsGetDTO>? AuthorPublications { get; set; }
        public int? Counter { get; set; }
    }
}
