namespace SecondExam.DTOs.ReviewsDTOs
{
    public class ReviewsReadDTO
    {
        public string? ReviewReference { get; set; }
#pragma warning disable CS8618
        public string TextReview { get; set; }
#pragma warning restore CS8618
        [Range(1, 10, ErrorMessage = "Range must be within 1 and 10")]
        public int DigitReview { get; set; }
    }
}
