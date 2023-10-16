using CounselingService.API.Models;

namespace CounselingService.API
{
    public class CounselingDataStore
    {

        public List<CounselingDTO> CounselingServices { get; set; }

        public static CounselingDataStore Current { get; } = new CounselingDataStore();

        public CounselingDataStore() 
        { 
            CounselingServices = new List<CounselingDTO>()
            {
                new CounselingDTO()
                {
                    Id = 1,
                    Name = "Alcholics Anonymous",
                    Counselor = "Sarah Johnson",
                    Description = "Bi-weekly group, designed to support those recovering form Alcohol Addiction."
                },
                new CounselingDTO()
                {
                    Id = 2,
                    Name = "Narcotics Anonymous",
                    Counselor = "Brian Brackett",
                    Description = "Bi-weekly group, designed to support those recovering from Narcotics Addiction."
                },
                new CounselingDTO()
                {
                    Id = 3,
                    Name = "Gambelers Anonymous",
                    Counselor = "Catherine Forestrad",
                    Description = "Bi-weekly group, designed to support those recovering from Gambling Addiction."
                }
            };
        }
    }
}
