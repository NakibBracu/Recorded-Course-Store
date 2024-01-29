using RCS.Data.EmailUtilities;
using RCS.Services.EmailTemplates;
using System.Text.Encodings.Web;

namespace RCS.Services.Services
{
    public class EmailMessageService : IEmailMessageService
    {
        public EmailSendingHelper CreateOrderPurchaseEmail(string receiverEmail, string receiverName)
        {
            var template = new OrderConfirmationEmailTemplate(receiverName);
            string body = template.TransformText();
            string subject = "Order Purchase Info";

            EmailSendingHelper emailEssentials = new EmailSendingHelper()
            {
                Subject = subject,
                Body = body
            };

            return emailEssentials;
        }
    }
}
