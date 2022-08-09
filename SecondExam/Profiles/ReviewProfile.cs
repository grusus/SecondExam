using SecondExam.DTOs.ReviewsDTOs;

namespace SecondExam.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewsReadDTO>().ReverseMap();
            CreateMap<ReviewsCreateDTO, Review>();
            CreateMap<Review, ReviewsGetSimpleDTO>().ReverseMap();
            CreateMap<Review, ReviewsCreateDTO>();
            CreateMap<Review, ReviewsUpdateDTO>().ReverseMap();
        }
    }
}
