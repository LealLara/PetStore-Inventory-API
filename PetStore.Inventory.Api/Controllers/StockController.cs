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
    public class StockController : ControllerBase
    {
        private readonly IStockServices _stockServices;

        public StockController(IStockServices stockServices)
        {
            _stockServices = stockServices;
        }

        /// <summary>
        /// Adiciona estoque a um produto existente. Apenas usuários com as funções ADMIN ou SYSTEM_OPERATOR podem acessar este endpoint.
        /// </summary>
        /// <param name="data">Parâmetro que contém os dados do estoque a ser adicionado.</param>
        /// <returns>Retorna o movimento de estoque criado.</returns>
        [SwaggerOperation(Summary = "Adiciona estoque a um produto existente.", Description = "Este endpoint permite adicionar estoque a um produto existente. Apenas usuários com as funções ADMIN ou SYSTEM_OPERATOR podem acessar este endpoint.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Estoque adicionado com sucesso.", typeof(StockMovementModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos para adicionar estoque.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor ao adicionar estoque.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autenticado.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Usuário não tem permissão para adicionar estoque.")]

        [Authorize(Roles = nameof(EUserRoles.ADMIN) + "," + nameof(EUserRoles.SYSTEM_OPERATOR))]

        [HttpPost("add-stock")]
        public async Task<IActionResult> AddStock([FromBody] StockAddDTO data)
        {
            try
            {
                var result = await _stockServices.AddStockAsync(data.ToBusinessRequest());
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao adicionar estoque: {ex.Message}");
            }
        }

        /// <summary>
        /// Recupera o histórico de movimentações de estoque de um produto.
        /// </summary>
        /// <param name="productId">ID do produto.</param>
        /// <returns>Retorna o histórico de movimentações.</returns>
        [SwaggerOperation(Summary = "Recupera o histórico de movimentações de estoque de um produto.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Histórico recuperado com sucesso.", typeof(IEnumerable<StockMovementModel>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]

        [HttpGet("get-stock-movements/{productId}")]
        public async Task<IActionResult> GetStockMovements(int productId)
        {
            try
            {
                var result = await _stockServices.GetStockMovementsByProductId(productId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao buscar movimentações: {ex.Message}");
            }
        }

        /// <summary>
        /// Recupera o estoque atual de um produto.
        /// </summary>
        /// <param name="productId">ID do produto.</param>
        /// <returns>Retorna o produto com o estoque atual.</returns>
        [SwaggerOperation(Summary = "Recupera o estoque atual de um produto.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Estoque recuperado com sucesso.", typeof(ProductModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Produto não encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]

        [HttpGet("get-product-stock/{productId}")]
        public async Task<IActionResult> GetProductStock(int productId)
        {
            try
            {
                var result = await _stockServices.GetProductStock(productId);
                if (result == null)
                    return NotFound(new { message = "Produto não encontrado." });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao buscar o estoque: {ex.Message}");
            }
        }
    }
}