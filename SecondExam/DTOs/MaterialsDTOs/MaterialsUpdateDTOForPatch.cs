namespace SecondExam.DTOs.MaterialsDTOs
{
    public class MaterialsUpdateDTOForPatch
    {
#pragma warning disable CS8618
        [MaxLengthAttribute(50)]
        public string MaterialTitle { get; set; }
        [MaxLengthAttribute(200)]
        public string MaterialDescription { get; set; }
        [MaxLengthAttribute(200)]
        public string MaterialLocation { get; set; }
        public int AuthorId { get; set; }
        public int MaterialTypeId { get; set; }
#pragma warning restore CS8618
        public DateTime CreatedDate { get; set; }
    }
}
