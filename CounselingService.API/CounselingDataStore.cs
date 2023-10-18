using CounselingService.API.Models;

namespace CounselingService.API
{
    public class CounselingDataStore
    {

        public List<CounselingDTO> CounselingServices { get; set; }

        //public static CounselingDataStore Current { get; } = new CounselingDataStore();

        public CounselingDataStore()
        {
            CounselingServices = new List<CounselingDTO>()
            {
                new CounselingDTO()
                {
                    Id = 1,
                    Name = "Alcholics Anonymous",
                    Counselor = "Sarah Johnson",
                    Description = "Bi-weekly group, designed to support those recovering form Alcohol Addiction.",
                    SpecialEvents = new List<SpecialEventsDTO>()
                    {
                        new SpecialEventsDTO()
                        {
                            Id = 1,
                            Name = "Bi-Weekly Walk & Water",
                            Description = "This event is designed to promote physical & mental health, with our group, while we enjoy the ourdoors."
                        },
                        new SpecialEventsDTO()
                        {
                            Id = 2,
                            Name = "Bi-Weekly Community Service",
                            Description = "This event is designed to promote community restoration, and will count for hours if mandated."
                        }
                    }
                },
                new CounselingDTO()
                {
                    Id = 2,
                    Name = "Narcotics Anonymous",
                    Counselor = "Brian Brackett",
                    Description = "Bi-weekly group, designed to support those recovering from Narcotics Addiction.",
                    SpecialEvents = new List<SpecialEventsDTO>()
                    {
                        new SpecialEventsDTO()
                        {
                            Id = 3,
                            Name = "Meal Prep & Greet",
                            Description = "This event is designed to promote better habbits, and work towards building a healthier lifestyle."
                        },
                        new SpecialEventsDTO()
                        {
                            Id = 4,
                            Name = "Bi-Weekly Community Service",
                            Description = "This event is designed to promote community restoration, and will count for hours if mandated."
                        }
                    }
                },
                new CounselingDTO()
                {
                    Id = 3,
                    Name = "Gambelers Anonymous",
                    Counselor = "Catherine Forestrad",
                    Description = "Bi-weekly group, designed to support those recovering from Gambling Addiction.",
                    SpecialEvents = new List<SpecialEventsDTO>()
                    {
                        new SpecialEventsDTO()
                        {
                            Id = 5,
                            Name = "Paws & Pals",
                            Description = "This event is designed to promote better mental health, and provide dog walking services for our elderly community."
                        },
                        new SpecialEventsDTO()
                        {
                            Id = 6,
                            Name = "Bi-Weekly Community Service",
                            Description = "This event is designed to promote community restoration, and will count for hours if mandated."
                        }
                    }
                }
            };
        }
    }
}
