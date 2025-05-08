namespace Application.Services;

public interface IEmailService
{
    void SendEmail(string emailTo, string subject, string body);
    public bool ValidateEmail(string email);

}
