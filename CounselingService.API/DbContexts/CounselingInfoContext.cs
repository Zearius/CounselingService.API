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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Counseling>().
                HasData(
                new Counseling("Gambelers Anonymous")
                {
                    Id = 1,
                    Description = "Bi-weekly group, designed to support those recovering from Gambling Addiction",
                    Counselor = "Catherine Forestrad"
                },
                new Counseling("Narcotics Anonymous")
                {
                    Id = 2,
                    Description = "Bi-weekly group, designed to support those recovering from Narcotics Addiction.",
                    Counselor = "Brian Brackett"
                },
                new Counseling("Alcholics Anonymous")
                {
                    Id = 3,
                    Description = "Bi-weekly group, designed to support those recovering from Alcohol Addiction.",
                    Counselor = "Sarah Johnson"
                });


            base.OnModelCreating(modelBuilder);
        }
    }
}
