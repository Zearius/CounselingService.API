using CounselingService.API.Entities;

namespace CounselingService.API.Services
{
    public interface ICounselingInfoRepository
    {
        Task<IEnumerable<Counseling>> GetCounselingsAsync();

        Task<(IEnumerable<Counseling>, PaginationMetadata)> GetCounselingsAsync(string? name, string? searchQuery, int pageNumber, int pageSize);

        Task<Counseling?> GetCounselingAsync(int counselingId, bool includeSpecialEvents);

        Task<IEnumerable<SpecialEvents>> GetSpecialEventsForCounselingAsync(int counselingId);

        Task<SpecialEvents?> GetSpecialEventsForCounselingAsync(int counselingId, int specialEventsId);

        Task<bool> CounselingExistsAsync(int counselId);

        Task AddSpecialEventForCounselingAsync(int counselingId, SpecialEvents specialEvents);

        Task<bool> SaveChangesAsync();

        void DeleteSpecialEvents(SpecialEvents specialEvents);
    }
}
