namespace CounselingService.API.Models
{
    /// <summary>
    /// Counseling DTO containing basic informaiton with a list of special events and number of events
    /// </summary>
    public class CounselingDTO
    {
        public int Id { get; set; }

        /// <summary>
        /// Name of Counseling Service.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Name of Counselor providing service.
        /// </summary>
        public string Counselor { get; set; } = string.Empty;  
        
        /// <summary>
        /// Brief Description of Service provided.
        /// </summary>
        public string? Description { get; set; }
        
        public int NumberOfSpecialEvents
        {
            get
            {
                return SpecialEvents.Count;
            }
        }

        /// <summary>
        /// Collection of Special events.
        /// </summary>
        public ICollection<SpecialEventsDTO> SpecialEvents { get; set; }
           = new List<SpecialEventsDTO>();
    }
}
