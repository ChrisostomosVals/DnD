using AutoMapper;

namespace DnD.Api.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Data.Models.ClassModel, Shared.Models.ClassModel>().ReverseMap();
            CreateMap<Data.Models.ClassCategoryModel, Shared.Models.ClassCategoryModel>().ReverseMap();
            CreateMap<Data.Models.CharacterModel, Shared.Models.CharacterModel>().ReverseMap();
            CreateMap<Data.Models.CharacterGearModel, Shared.Models.CharacterGearModel>().ReverseMap();
            CreateMap<Data.Models.CharacterArsenalModel, Shared.Models.CharacterArsenalModel>().ReverseMap();
            CreateMap<Data.Models.CharacterSkillModel, Shared.Models.CharacterSkillModel>().ReverseMap();
            CreateMap<Data.Models.CharacterMainStatsModel, Shared.Models.CharacterMainStatsModel>().ReverseMap();
            CreateMap<Data.Models.SkillModel, Shared.Models.SkillModel>().ReverseMap();
            CreateMap<Data.Models.WorldMiscModel, Shared.Models.WorldMiscModel>().ReverseMap();
            CreateMap<Data.Models.WorldObjectModel, Shared.Models.WorldObjectModel>().ReverseMap();
            CreateMap<Data.Models.WorldObjectPropModel, Shared.Models.WorldObjectPropModel>().ReverseMap();
            CreateMap<Data.Models.LocationModel, Shared.Models.LocationModel>().ReverseMap();
            CreateMap<Data.Models.LocationEventModel, Shared.Models.LocationEventModel>().ReverseMap();
            CreateMap<Data.Models.RaceModel, Shared.Models.RaceModel>().ReverseMap();
            CreateMap<Data.Models.RaceCategoryModel, Shared.Models.RaceCategoryModel>().ReverseMap();
            CreateMap<Data.Models.UserModel, Shared.Models.UserModel>().ReverseMap();
        }
    }
}
