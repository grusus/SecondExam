namespace SecondExam.DTOs.MaterialsDTOs
{
    public class MaterialsUpdateDTO
    {
#pragma warning disable CS8618
        [Required]
        [MaxLengthAttribute(50)]
        public string MaterialTitle { get; set; }
        [Required]
        [MaxLengthAttribute(200)]
        public string MaterialDescription { get; set; }
        [Required]
        [MaxLengthAttribute(200)]
        public string MaterialLocation { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int MaterialTypeId { get; set; }
#pragma warning restore CS8618
        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
