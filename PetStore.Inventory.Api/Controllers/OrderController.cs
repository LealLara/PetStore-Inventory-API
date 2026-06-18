using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetStore.Inventory.Api.ApplicationDTOs.Requests;
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Domain.BusinessModel; 
using PetStore.Inventory.Domain.Utils.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace PetStore.Inventory.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        /// <summary>
        /// Cria um novo pedido. Apenas usuários com as funções ADMIN, SYSTEM_OPERATOR ou SELLER podem acessar este endpoint.
        /// </summary>
        /// <param name="data">Parâmetro que contém os dados do pedido.</param>
        /// <returns>Retorna o pedido criado.</returns>
        [SwaggerOperation(Summary = "Cria um novo pedido.", Description = "Este endpoint permite a criação de um novo pedido. Apenas usuários com as funções ADMIN, SYSTEM_OPERATOR ou SELLER podem acessar este endpoint.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Pedido criado com sucesso.", typeof(OrderModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos para criar pedido.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor ao criar pedido.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autenticado.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Usuário não tem permissão para criar pedidos.")]

        [Authorize(Roles = nameof(EUserRoles.ADMIN) + "," + nameof(EUserRoles.SYSTEM_OPERATOR) + "," + nameof(EUserRoles.SELLER))]

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDTO data)
        {
            try
            {
                var result = await _orderServices.CreateOrderAsync(data.ToBusinessRequest());
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao criar pedido: {ex.Message}");
            }
        }

        /// <summary>
        /// Recupera todos os pedidos cadastrados.
        /// </summary>
        [SwaggerOperation(Summary = "Recupera todos os pedidos.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Pedidos recuperados com sucesso.", typeof(IEnumerable<OrderModel>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]

        [Authorize(Roles = nameof(EUserRoles.ADMIN) + "," + nameof(EUserRoles.SYSTEM_OPERATOR))]

        [HttpGet("get-all-orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var result = await _orderServices.GetAllOrdersAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao buscar pedidos: {ex.Message}");
            }
        }

        /// <summary>
        /// Recupera um pedido pelo ID.
        /// </summary>
        [SwaggerOperation(Summary = "Recupera um pedido pelo ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Pedido recuperado com sucesso.", typeof(OrderModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Pedido não encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]

        [HttpGet("get-order-by-id/{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            try
            {
                var result = await _orderServices.GetOrderByIdAsync(orderId);
                if (result == null)
                    return NotFound(new { message = "Pedido não encontrado." });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao buscar pedido: {ex.Message}");
            }
        }

        /// <summary>
        /// Recupera pedidos por vendedor.
        /// </summary>
        [SwaggerOperation(Summary = "Recupera pedidos por vendedor.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Pedidos recuperados com sucesso.", typeof(IEnumerable<OrderModel>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]

        [HttpGet("get-orders-by-seller/{sellerName}")]
        public async Task<IActionResult> GetOrdersBySeller(string sellerName)
        {
            try
            {
                var result = await _orderServices.GetOrdersBySellerAsync(sellerName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao buscar pedidos: {ex.Message}");
            }
        }
    }
}