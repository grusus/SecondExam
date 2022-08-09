
using SecondExam.DTOs.ReviewsDTOs;
using SecondExam.DTOs.TypesDTOs;

namespace SecondExam.DTOs.MaterialsDTOs
{
    public class MaterialsGetFullDTO
    {
        public string MaterialTitle { get; set; }
        public string MaterialDescription { get; set; }
        public string MaterialLocation { get; set; }
        public AuthorsGetDTO? MatherialAuthor { get; set; }
        public TypesReadDTO? MaterialType { get; set; }
        public List<ReviewsReadDTO>? MaterialReviews { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
