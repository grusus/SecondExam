namespace SecondExam.Data.Model
{
    public class MaterialType
    {
        [Key]
        public int TypeId { get; set; }
#pragma warning disable CS8618
        public string TypeName { get; set; }
#pragma warning restore CS8618
        public string? TypeDefinition { get; set; }
    }
}
