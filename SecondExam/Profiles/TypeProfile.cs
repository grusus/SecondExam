using SecondExam.DTOs.TypesDTOs;

namespace SecondExam.Profiles
{
    public class TypeProfile : Profile
    {
        public TypeProfile()
        {
            CreateMap<MaterialType, TypesReadDTO>();
            CreateMap<MaterialType, TypesGetDTO>();
            CreateMap<MaterialType, TypesGetFullDTO>();
        }

    }
}
