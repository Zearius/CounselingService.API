namespace CounselingService.API.Models
{
    public class CounselingWithoutSpecialEventsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Counselor { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
