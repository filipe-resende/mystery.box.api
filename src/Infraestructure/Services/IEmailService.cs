using System.Net.Mail;

namespace Domain
{
    public interface IEmailService
    {
         void SendEmail(string emailTo, string subject, string body);
         public bool ValidateEmail(string email);

    }
}
