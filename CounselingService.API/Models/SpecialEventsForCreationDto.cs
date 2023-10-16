using System.ComponentModel.DataAnnotations;

namespace CounselingService.API.Models
{
    public class SpecialEventsForCreationDto
    {
        [Required(ErrorMessage = "You must provide a name.")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(250)]
        public string? Description { get; set; }
    }
}
