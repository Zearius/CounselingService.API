using CounselingService.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CounselingService.API.DbContexts
{

    /// <summary>
    /// DB set mapped to our entity classes.
    /// </summary>
    public class CounselingInfoContext : DbContext
    {
        /// <summary>
        /// Dbset Counseling for main entity of counseling
        /// </summary>
        public DbSet<Counseling> Counselings { get; set; } = null!;

        /// <summary>
        /// Dbset Special Events for entity of specialevent
        /// </summary>
        public DbSet<SpecialEvents> SpecialEvents { get; set; } = null!;


        /// <summary>
        /// Default constructor with options parameter
        /// </summary>
        /// <param name="options"></param>
        public CounselingInfoContext(DbContextOptions<CounselingInfoContext> options)
            : base(options)
        {

        }


        /// <summary>
        /// override of model building with some default items.
        /// </summary>
        /// <param name="modelBuilder"></param>
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
