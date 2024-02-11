using datapac_interview.Presistance;

namespace datapac_interview.Services;

    public class ReminderService(IOrderRepository orderRepository, EmailService emailService): IReminderService
    {
        public async Task SendReminders()
        {
            // Zde získáte seznam knih, u kterých je termín vrácení za jeden den
            var booksDueTomorrow = await orderRepository.GetBooksDueTomorrowAsync();

            foreach (var order in booksDueTomorrow)
            {
                var booksTitle = order.Books.AsEnumerable().Select(w => w.Title).ToList();
                
                emailService.SendReminderEmail(order.UserMailAdress, order.Id, booksTitle);
            }
        }
    }