using PetStore.Inventory.Domain.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Inventory.Domain.Entities
{
    public class EmailEntity
    {
        public int EmailId { get; private set; }
        public string EmailAddress { get; private set; } = string.Empty;
        public string Header { get; private set; } = string.Empty;
        public string EmailBody { get; private set; } = string.Empty;
        public int EmailType { get; private set; }

        public EmailEntity() { }

        public EmailEntity(string emailAddress, string header, string emailBody, EEmailType emailType)
        {
            EmailAddress = emailAddress;
            Header = header;
            EmailBody = emailBody;
            EmailType = (int)emailType;
        }
        public EmailEntity(string emailAddress, EEmailType emailType)
        {
            EmailAddress = emailAddress;
            EmailType = (int)emailType;
        }
    }
}