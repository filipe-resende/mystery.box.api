namespace Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration) => _configuration = configuration;

        public void SendEmail(string emailTo, string subject, string body)
        {
            var message = PrepareteMessage(emailTo, subject, body);
            SendEmailBySmtp(message);
        }

        private MailMessage PrepareteMessage(string emailTo, string subject, string body)
        {
            var mail = new MailMessage();
            mail.From = new MailAddress(_configuration["EmailSecret:EmailUserName"]!);

            mail.To.Add(emailTo);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            return mail;
        }

        public bool ValidateEmail(string email)
        {
            Regex expression = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");
            if (expression.IsMatch(email))
                return true;

            return false;
        }

        private void SendEmailBySmtp(MailMessage message)
        {
            SmtpClient smtpClient = new SmtpClient(_configuration["EmailSecret:EmailProvider"]!);
            smtpClient.Host = _configuration["EmailSecret:EmailProvider"]!;
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Timeout = 50000;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(_configuration["EmailSecret:EmailUserName"], _configuration["EmailSecret:EmailPassword"]);
            smtpClient.Send(message);
            smtpClient.Dispose();
        }
    }
}
