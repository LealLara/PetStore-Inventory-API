using Microsoft.AspNetCore.Mvc;

namespace PetStore.Inventory.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccessConfigController : ControllerBase
    {

        private readonly IAccessConfigService _service;
        public AccessConfigController(IAccessConfigService service)
        {
            _service = service;
        }

    }
}