using Microsoft.AspNetCore.Mvc;
using PetStore.Inventory.Application.BusinessDTOs.Requests;
using PetStore.Inventory.Domain.Utils.Enums;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace PetStore.Inventory.Api.ApplicationDTOs.Requests
{
    public class UserFirstRegisterDTO
    {
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
         
        public UserRole RoleId { get; set; }

        public UserFirstRegisterDTO() { }
        public DataRegisterRequest ToBusiness()
        {
            return new(
                name: Name,
                nickname: Nickname,
                email: Email,
                password: Password,
                role: RoleId
            );
        }
    }
}