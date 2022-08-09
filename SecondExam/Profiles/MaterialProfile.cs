

namespace SecondExam.Profiles
{
    public class MaterialProfile : Profile
    {
        public MaterialProfile()
        {
            CreateMap<Material, MaterialsGetDTO>();
            CreateMap<Material, MaterialsGetFullDTO>();
        }
    }
}
