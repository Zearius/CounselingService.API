using CounselingService.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CounselingService.API.DbContexts
{
    public class CounselingInfoContext : DbContext
    {
        public DbSet<Counseling> Counselings { get; set; } = null!;

        public DbSet<SpecialEvents> SpecialEvents { get; set; } = null!;

        public CounselingInfoContext(DbContextOptions<CounselingInfoContext> options)
            : base(options)
        {

        }
    }
}
