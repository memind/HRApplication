using IKApplication.Application.AbstractServices;
using System.Net.Mail;
using System.Net;

namespace IKApplication.Infrastructure.ConcreteServices
{
    public class EmailService : IEmailService
    {
        public bool SendMail(string userMail, string subject, string body)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.Credentials = new NetworkCredential("", ""); // Enter your email and Google Authenticate token
                client.EnableSsl = true;

                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(userMail);
                mailMessage.From = new MailAddress("ikappteam3@gmail.com", "İkApp Teams No-reaply");
                mailMessage.IsBodyHtml = true;

                mailMessage.Subject = subject;
                mailMessage.Body = body;

                client.Send(mailMessage);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
