namespace SecondExam.DTOs.MaterialsDTOs
{
    public class MaterialsGetDTO
    {
        [Key]
        public int MaterialId { get; set; }
        public string MaterialTitle { get; set; }
    }
}
