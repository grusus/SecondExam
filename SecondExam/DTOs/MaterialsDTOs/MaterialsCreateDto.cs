namespace SecondExam.DTOs.MaterialsDTOs
{
    public class MaterialsCreateDto
    {
        public int MaterialId { get; set; }
#pragma warning disable CS8618
        public string MaterialTitle { get; set; }
        public string MaterialDescription { get; set; }
        public string MaterialLocation { get; set; }
        public int AuthorId { get; set; }
        public int MaterialTypeId { get; set; }
#pragma warning restore CS8618
        public DateTime CreatedDate { get; set; }
    }
}
