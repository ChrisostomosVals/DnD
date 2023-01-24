using AutoMapper;
using DnD.Data.Models;
using DnD.Shared.Models;

namespace DnD.Api.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ClassBson, ClassModel>().ReverseMap();
            CreateMap<ClassCategoryBson, ClassCategoryModel>().ReverseMap();
            CreateMap<CharacterBson, CharacterModel>().ReverseMap();
            CreateMap<SkillBson, SkillModel>().ReverseMap();
            CreateMap<StatBson, StatModel>().ReverseMap();
            CreateMap<PropertyBson, PropertyModel>().ReverseMap();
            CreateMap<GearBson, GearModel>().ReverseMap();
            CreateMap<ArsenalBson, ArsenalModel>().ReverseMap();
            CreateMap<LocationBson, LocationModel>().ReverseMap();
            CreateMap<WorldObjectBson, WorldObjectModel>().ReverseMap();
            CreateMap<WorldObjectPropBson, WorldObjectPropModel>().ReverseMap();
            CreateMap<LocationBson, LocationModel>().ReverseMap();
            CreateMap<RaceBson, RaceModel>().ReverseMap();
            CreateMap<RaceCategoryBson, RaceCategoryModel>().ReverseMap();
            CreateMap<UserBson, UserModel>().ReverseMap();
            CreateMap<UserRoleBson, UserRoleModel>().ReverseMap();
        }
    }
}
