namespace CounselingService.API.Models
{
    /// <summary>
    /// A DTO for a Counseling service without a special event.
    /// </summary>
    public class CounselingWithoutSpecialEventsDTO
    {
        /// <summary>
        /// Id of Counseling Service special event is associated it.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of Counseling Serivce.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Name of Counselor associated with Counseling service.
        /// </summary>
        public string Counselor { get; set; } = string.Empty;

        /// <summary>
        /// Brief description of counseling service.
        /// </summary>
        public string? Description { get; set; }
    }
}
