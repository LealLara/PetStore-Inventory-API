using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetStore.Inventory.Api.ApplicationDTOs.Requests;
using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Interfaces.Services;
using PetStore.Inventory.Domain.Utils.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace PetStore.Inventory.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _service;
        public ProductController(IProductServices service)
        {
            _service = service;
        }

        /// <summary>
        /// Recupera todos os produtos cadastrados no sistema.
        /// </summary>
        /// <reSturns>Retorna uma lista de todos os produtos cadastrados.</returns>
        [SwaggerOperation(Summary = "Recupera todos os produtos cadastrados no sistema.", Description = "Este endpoint permite a recuperação de todos os produtos cadastrados no sistema.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Produtos recuperados com sucesso.", typeof(IEnumerable<ProductModel>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor ao recuperar os produtos.")]

        [HttpGet("get-all-products")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                IEnumerable<ProductModel> products = await _service.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao buscar os produtos: {ex.Message}");
            }
        }

        /// <summary>
        /// Cria um novo produto no sistema. Apenas usuários com as funções ADMIN ou SYSTEM_OPERATOR podem acessar este endpoint.
        /// </summary>
        /// <param name="data">Parâmetro que contém os dados do produto a ser criado.</param>
        /// <returns>Retorna o produto criado.</returns>
        [SwaggerOperation(Summary = "Cria um novo produto no sistema.", Description = "Este endpoint permite a criação de um novo produto no sistema. Apenas usuários com as funções ADMIN ou SYSTEM_OPERATOR podem acessar este endpoint.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Produto criado com sucesso.", typeof(ProductModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos para criação do produto.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor ao criar o produto.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autenticado.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Usuário não tem permissão para criar produtos.")]

        [Authorize(Roles = nameof(EUserRoles.ADMIN) + "," + nameof(EUserRoles.SYSTEM_OPERATOR))]

        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct(ProductDTO data)
        {
            try
            {
                ProductModel result = await _service.CreateProduct(data.ToBusinessRequest());

                if (result is null)
                {
                    return BadRequest("O cadastro do produto falhou.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao cadastrar o produto: {ex.Message}");
            }
        }

        /// <summary>
        /// Recupera o produto cadastrado no sistema filtrados pelo ID fornecido.
        /// </summary>
        /// <param name="id">ID do produto a ser filtrado.</param>
        /// <returns>Retorna os produtos filtrados pelo ID.</returns>
        [HttpGet("get-products-filtered-by-id")]
        public async Task<IActionResult> GetAllProductsFilteredById(int id)
        {
            try
            {
                ProductModel product = await _service.GetAllProductsFilteredById(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao buscar o produto: {ex.Message}");
            }
        }

        /// <summary>
        /// Recupera os produtos cadastrados no sistema filtrados pela string fornecida. A string pode ser utilizada para buscar produtos cujo nome ou descrição contenha o valor especificado.
        /// </summary>
        /// <param name="filter">String de filtro para buscar produtos.</param>
        /// <returns>Retorna os produtos filtrados pela string.</returns>
        [SwaggerOperation(Summary = "Recupera os produtos cadastrados no sistema filtrados pela string fornecida.", Description = "A string pode ser utilizada para buscar produtos cujo nome ou descrição contenha o valor especificado.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Produtos recuperados com sucesso.", typeof(IEnumerable<ProductModel>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor ao recuperar os produtos.")]
        [HttpGet("get-products-filtered-by-string")]
        public async Task<IActionResult> GetAllProductsFilteredByString(string filter)
        {
            try
            {
                IEnumerable<ProductModel> products = await _service.GetAllProductsFilteredByString(filter);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao buscar os produtos: {ex.Message}");
            }
        }

        /// <summary>
        /// Cria um novo produto no sistema. Apenas usuários com as funções ADMIN ou SYSTEM_OPERATOR podem acessar este endpoint.
        /// </summary>
        /// <param name="data">Parâmetro que contém os dados do produto a ser atualizado.</param>
        /// <returns>Retorna o produto atualizado.</returns>
        [SwaggerOperation(Summary = "Atualiza um novo produto no sistema.", Description = "Este endpoint permite a atualização de um produto no sistema. Apenas usuários com as funções ADMIN ou SYSTEM_OPERATOR podem acessar este endpoint.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Produto atualizado com sucesso.", typeof(ProductModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos para atualização do produto.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor ao atualizar o produto.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autenticado.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Usuário não tem permissão para atualizar produtos.")]

        [Authorize(Roles = nameof(EUserRoles.ADMIN) + "," + nameof(EUserRoles.SYSTEM_OPERATOR))]

        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateProduct(ProductManagementDTO data)
        {
            try
            {
                ProductModel result = await _service.UpdateProduct(data.ToBusinessRequest());
                if (result is null)
                {
                    return BadRequest("A atualização do produto falhou.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao atualizar o produto: {ex.Message}");
            }
        }

        /// <summary>
        /// Remove um produto do sistema. Apenas usuários com as funções ADMIN ou SYSTEM_OPERATOR podem acessar este endpoint.
        /// </summary>
        /// <param name="id">ID do produto a ser removido.</param>
        /// <returns>Retorna uma mensagem indicando o sucesso ou fracasso da operação.</returns>
        [SwaggerOperation(Summary = "Remove um produto do sistema.", Description = "Este endpoint permite a remoção de um produto do sistema. Apenas usuários com as funções ADMIN ou SYSTEM_OPERATOR podem acessar este endpoint.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Produto removido com sucesso.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Falha ao remover o produto.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor ao remover o produto.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Acesso negado.")]

        [Authorize(Roles = nameof(EUserRoles.ADMIN) + "," + nameof(EUserRoles.SYSTEM_OPERATOR))]

        [HttpDelete("remove-product")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            try
            {
                bool result = await _service.RemoveProduct(id);
                if (!result)
                {
                    return BadRequest("A remoção do produto falhou.");
                }

                return Ok("Produto removido com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao remover o produto: {ex.Message}");
            }
        }

        /// <summary>
        /// Cria produtos padrão no sistema. Este endpoint é utilizado para inicializar o sistema com produtos de exemplo. Não é necessário fornecer dados adicionais, pois os produtos padrão são pré-definidos.
        /// </summary>
        /// <returns>Retorna uma mensagem indicando o sucesso ou fracasso da operação.</returns>
        [SwaggerOperation(Summary = "Cria produtos padrão no sistema.", Description = "Este endpoint é utilizado para inicializar o sistema com produtos de exemplo. Não é necessário fornecer dados adicionais, pois os produtos padrão são pré-definidos.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Produtos padrão criados com sucesso.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Falha ao criar os produtos padrão.")]

        [ApiExplorerSettings(IgnoreApi = true)]

        [HttpPost("create-pattern-products")]
        public async Task<IActionResult> CreatePatternProducts()
        {
            try
            {
                bool result = await _service.CreatePatternProducts();
                if (!result)
                {
                    return BadRequest("A criação dos produtos padrão falhou.");
                }

                return Ok("Produtos padrão criados com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao criar os produtos padrão: {ex.Message}");
            }
        }

    }
}