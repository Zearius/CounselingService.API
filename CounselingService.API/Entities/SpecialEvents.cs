using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CounselingService.API.Entities
{
    public class SpecialEvents
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [ForeignKey("CounselingId")]
        public Counseling? Counseling { get; set; }

        public int CounselingId { get; set; }

        public SpecialEvents(string name)
        {
            Name = name;
        }
    }
}
