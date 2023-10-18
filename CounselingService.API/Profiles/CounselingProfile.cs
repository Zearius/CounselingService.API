using AutoMapper;

namespace CounselingService.API.Profiles
{
    public class CounselingProfile : Profile
    {
        public CounselingProfile()
        {
            CreateMap<Entities.Counseling, Models.CounselingWithoutSpecialEventsDTO>();
            CreateMap<Entities.Counseling, Models.CounselingDTO>();
        }
    }
}
