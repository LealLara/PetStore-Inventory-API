using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PetStore.Inventory.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SalesPortalController : ControllerBase
    {
        private readonly ISalesPortalServices _salesPortalServices;
        public SalesPortalController(ISalesPortalServices salesPortalServices)
        {
            _salesPortalServices = salesPortalServices;
        }





        //portal para cadastrar vendas,gerar exportação de relatorios: relatório geral do estoque(produtos, quantidade, preços)/ vendas por vendedor/vendas gerais / relatório geral de clientes(info dos clientes) / produtos mais vendidos

    }
}