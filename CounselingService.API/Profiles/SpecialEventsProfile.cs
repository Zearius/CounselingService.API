using AutoMapper;

namespace CounselingService.API.Profiles
{
    public class SpecialEventsProfile : Profile
    {
        public SpecialEventsProfile()
        {
            CreateMap<Entities.SpecialEvents, Models.SpecialEventsDTO>();
            CreateMap<Models.SpecialEventsForCreationDto, Entities.SpecialEvents>();
            CreateMap<Models.SpecialEventForUpdateDto, Entities.SpecialEvents>();
            CreateMap<Entities.SpecialEvents, Models.SpecialEventForUpdateDto>();
        }
    }
}
