namespace CounselingService.API.Models
{
    public class SpecialEventsDTO
    {
        //Child resource of CounselingServices.
        public int Id { get; set; }
        public string Name { get; set; } =string.Empty;
        public string? Description { get; set; }
    }
}
