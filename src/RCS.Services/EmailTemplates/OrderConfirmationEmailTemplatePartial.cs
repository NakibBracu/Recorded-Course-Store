namespace RCS.Services.EmailTemplates
{
    public partial class OrderConfirmationEmailTemplate
    {
        private string Name { get; set; }

        public OrderConfirmationEmailTemplate(string name)
        {
            Name = name;
        }
    }
}
