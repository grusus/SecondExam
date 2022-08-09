
namespace SecondExam.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorsGetDTO>();
            CreateMap<Author, AuthorGetDTOwithId>();
            CreateMap<Author, AuthorGetFullDto>();
        }
    }
}
