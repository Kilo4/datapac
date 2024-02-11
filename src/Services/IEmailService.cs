namespace datapac_interview.Services;

public interface IEmailService
{
    void SendReminderEmail(string userEmail, Guid orderId, List<string> bookTitle);
}