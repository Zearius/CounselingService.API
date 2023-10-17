using CounselingService.API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CounselingService.API.Entities
{
    public class Counseling
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Counselor { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        public ICollection<SpecialEvents> SpecialEvents { get; set; }
           = new List<SpecialEvents>();

        public Counseling(string name)
        {
            Name = name;
        }
    }
}

