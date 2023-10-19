using CounselingService.API.DbContexts;
using CounselingService.API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CounselingService.API.Services
{
    public class CounselingInfoRepository : ICounselingInfoRepository
    {
        private readonly CounselingInfoContext _context;

        public CounselingInfoRepository(CounselingInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Counseling?> GetCounselingAsync(int counselingId, bool includeSpecialEvents)
        {
            if(includeSpecialEvents)
            {
                return await _context.Counselings.Include(c => c.SpecialEvents)
                    .Where(c => c.Id == counselingId).FirstOrDefaultAsync();
            }
            return await _context.Counselings.Where(c => c.Id == counselingId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Counseling>> GetCounselingsAsync()
        {
            return await _context.Counselings.OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<(IEnumerable<Counseling>, PaginationMetadata)> GetCounselingsAsync(string? name, string? searchQuery, int pageNumber, int pageSize)
        {
            //casting to DB context to implement deferred execution.
            var collection = _context.Counselings as IQueryable<Counseling>;

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.Name == name);
            }
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.Name.Contains(searchQuery) || (a.Description != null && a.Description.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

           var collectionToReturn =  await collection.OrderBy(c => c.Name).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task<IEnumerable<SpecialEvents>> GetSpecialEventsForCounselingAsync(int counselingId)
        {
            return await _context.SpecialEvents.Where(p => p.CounselingId == counselingId).ToListAsync();
        }

        public  async Task<SpecialEvents?> GetSpecialEventsForCounselingAsync(int counselingId, int specialEventsId)
        {
            return await _context.SpecialEvents.Where(p => p.CounselingId == counselingId && p.Id == specialEventsId).FirstOrDefaultAsync();
        }

        public async Task<bool> CounselingExistsAsync(int counselId)
        {
            return await _context.Counselings.AnyAsync(c => c.Id == counselId);
        }

        public async Task AddSpecialEventForCounselingAsync(int counselingId, SpecialEvents specialEvents)
        {
            var counseling = await GetCounselingAsync(counselingId, false);
            if (counseling != null) 
            {
                counseling.SpecialEvents.Add(specialEvents);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void DeleteSpecialEvents(SpecialEvents specialEvents)
        {
            _context.SpecialEvents.Remove(specialEvents);
        }

        public async Task<bool> CounselingNameMatchesCounselingId(string? counselingName, int counselingId)
        {
            return await _context.Counselings.AnyAsync(c => c.Id == counselingId && c.Name == counselingName);
        }
    }
}
