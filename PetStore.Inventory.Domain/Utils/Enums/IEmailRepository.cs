using PetStore.Inventory.Domain.Entities;

namespace PetStore.Inventory.Domain.Utils.Enums
{
    public interface IEmailRepository
    {
        Task<bool> SendAsync(EmailEntity body); 
    }
}