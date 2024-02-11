namespace datapac_interview.Services;

public class EmailService: IEmailService
{
    public void SendReminderEmail(string userEmail,Guid orderId, List<string> bookTitle)
    {
        Console.WriteLine($"Reminder: return '{bookTitle}' for order '{orderId}' to the library! ({userEmail})");
    }
}