namespace CounselingService.API.Services
{
    public class LocalMailService : IMailService
    {
        private string _mailTo = "admin@testingcompany.com";
        private string _mailFrom = "no-reply@testingcompany.com";

        public void Send(string subject, string message)
        {
            //demo-send mail toficiation via consol output.
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, " + $"with {nameof(LocalMailService)}.");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}
