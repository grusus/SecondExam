namespace SecondExam.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewsReadDTO>().ReverseMap();
            CreateMap<ReviewsCreateDTO, Review>().ReverseMap();
            CreateMap<Review, ReviewsGetSimpleDTO>().ReverseMap();
            CreateMap<Review, ReviewsUpdateDTO>().ReverseMap();
        }
    }
}
