namespace CounselingService.API.Models
{
    public class CounselingDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Counselor { get; set; } = string.Empty;   
        public string? Description { get; set; }
        
        public int NumberOfSpecialEvents
        {
            get
            {
                return SpecialEvents.Count;
            }
        }

        public ICollection<SpecialEventsDTO> SpecialEvents { get; set; }
           = new List<SpecialEventsDTO>();
    }
}
