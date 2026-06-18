using PetStore.Inventory.Domain.Utils.Enums;

namespace PetStore.Inventory.Domain.BusinessModel
{
    public class EmailModel
    {
        public int EmailId { get; private set; }
        public string EmailAddress { get; private set; } = string.Empty;
        public string Header { get; private set; } = string.Empty;
        public string EmailBody { get; private set; } = string.Empty;
        public int EmailType { get; private set; }

        public EmailModel() { }

        public EmailModel(string emailAddress, string header, string emailBody, EEmailType emailType)
        {
            EmailAddress = emailAddress;
            Header = header;
            EmailBody = emailBody;
            EmailType = (int)emailType;
        }
        public EmailModel(string emailAddress, EEmailType emailType)
        {
            EmailAddress = emailAddress;
            EmailType = (int)emailType;
        }
        public EmailModel(string emailAddress)
        {
            EmailAddress = emailAddress;
        }
    }
}