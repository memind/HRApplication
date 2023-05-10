namespace IKApplication.Application.AbstractServices
{
    public interface IEmailService
    {
        bool SendMail(string userMail, string subject, string body);
    }
}
