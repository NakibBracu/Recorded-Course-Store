using RCS.Data.EmailUtilities;

namespace RCS.Services.Services
{
    public interface IEmailMessageService
    {
        EmailSendingHelper CreateOrderPurchaseEmail(string receiverEmail, string receiverName);
    }
}
