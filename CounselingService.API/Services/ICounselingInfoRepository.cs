using CounselingService.API.Entities;

namespace CounselingService.API.Services
{
    public interface ICounselingInfoRepository
    {
        Task<IEnumerable<Counseling>> GetCounselingsAsync();

        Task<Counseling?> GetCounselingAsync(int counselingId, bool includeSpecialEvents);

        Task<IEnumerable<SpecialEvents>> GetSpecialEventsForCounselingAsync(int counselingId);

        Task<SpecialEvents?> GetSpecialEventsForCounselingAsync(int counselingId, int specialEventsId);

        Task<bool> CounselingExistsAsync(int counselId);

        Task AddSpecialEventForCounselingAsync(int counselingId, SpecialEvents specialEvents);

        Task<bool> SaveChangesAsync();
    }
}
