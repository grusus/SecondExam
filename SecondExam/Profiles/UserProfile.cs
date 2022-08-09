namespace SecondExam.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserCreateDto>().ReverseMap();
        }
    }
}
