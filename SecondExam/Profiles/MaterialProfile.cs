

namespace SecondExam.Profiles
{
    public class MaterialProfile : Profile
    {
        public MaterialProfile()
        {
            CreateMap<Material, MaterialsGetDTO>();
            CreateMap<Material, MaterialsGetFullDTO>();
            CreateMap<IEnumerable<Material>, MaterialsGetDTO>();
            CreateMap<MaterialsCreateDto, Material>().ReverseMap();
        }
    }
}
