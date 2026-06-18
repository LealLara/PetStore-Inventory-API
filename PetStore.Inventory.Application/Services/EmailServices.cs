using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;
using PetStore.Inventory.Domain.Utils.Enums;
using PetStore.Inventory.Domain.Utils.StringTools;
using PetStore.Inventory.Domain.Utils.Templates;

namespace PetStore.Inventory.Application.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly IAuthenticationServices _authenticationServices;
        private readonly IEmailRepository _emailRepository;

        public EmailServices(IAuthenticationServices authenticationServices, IEmailRepository emailRepository)
        {
            _authenticationServices = authenticationServices;
            _emailRepository = emailRepository;
        }

        public async Task<string> SetLoginEmail(LoginModel login)
        {
            string mailMessage = string.Empty;

            UserDataToSendLoginEmailModel userData = await _authenticationServices.GenerateJwt(login);

            EmailModel emailBody = new(emailAddress: userData.Register.Email, emailType: EEmailType.Welcome);

            return await SendAsync(emailBody, userData.Token);
        }

        private async Task<string> SendAsync(EmailModel data, string token)
        {
            string mailMessage = string.Empty;
            EmailEntity emailBody = new();

            string head = ((EEmailType)data.EmailType).GetDescription();
            string text = EmailTemplates.GetTemplate((EEmailType)data.EmailType, token);

            emailBody = new(emailAddress: data.EmailAddress, header: head, emailBody: text, emailType: (EEmailType)data.EmailType);

            bool success = await _emailRepository.SendAsync(emailBody);

            if (success)
            {
                mailMessage = "Acabamos de enviar um token secreto para o seu e-mail. Com ele, você poderá acessar sua conta com segurança.";
            }
            else
            {
                mailMessage = "Algo de errado não está certo";
            }
            return mailMessage;
        }
    }
}