using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PetStore.Inventory.Api.ApplicationDTOs.Requests;
using PetStore.Inventory.Api.Controllers;
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Utils.Enums;

namespace PetStore.Inventory.Tests.Controllers;

public class ControllerTests
{
    [Fact]
    public async Task Product_GetAllProducts_WhenServiceReturnsProducts_ReturnsOk()
    {
        var service = new Mock<IProductServices>();
        service.Setup(x => x.GetAllProducts())
            .ReturnsAsync([new ProductModel(1, "Racao", "Pacote", 10f, 2)]);
        var controller = new ProductController(service.Object);

        var result = await controller.GetAllProducts();

        var ok = result.Should().BeOfType<OkObjectResult>().Subject;
        ok.Value.Should().BeAssignableTo<IEnumerable<ProductModel>>();
    }

    [Fact]
    public async Task Product_CreateProduct_WhenServiceReturnsNull_ReturnsBadRequest()
    {
        var service = new Mock<IProductServices>();
        service.Setup(x => x.CreateProduct(It.IsAny<PetStore.Inventory.Application.ApplicationModel.Requests.ProductRequest>()))
            .ReturnsAsync((ProductModel?)null!);
        var controller = new ProductController(service.Object);

        var result = await controller.CreateProduct(new ProductDTO
        {
            ProductName = "Racao",
            ProductDescription = "Pacote",
            Price = 10f,
            StockQuantity = 1
        });

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Product_RemoveProduct_WhenServiceReturnsFalse_ReturnsBadRequest()
    {
        var service = new Mock<IProductServices>();
        service.Setup(x => x.RemoveProduct(99)).ReturnsAsync(false);
        var controller = new ProductController(service.Object);

        var result = await controller.RemoveProduct(99);

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Order_CreateOrder_WhenServiceThrowsArgumentException_ReturnsBadRequest()
    {
        var service = new Mock<IOrderServices>();
        service.Setup(x => x.CreateOrderAsync(It.IsAny<PetStore.Inventory.Application.ApplicationModel.Requests.OrderCreateRequest>()))
            .ThrowsAsync(new ArgumentException("Produto invalido"));
        var controller = new OrderController(service.Object);

        var result = await controller.CreateOrder(new OrderCreateDTO
        {
            CustomerDocument = "12345678900",
            SellerName = "Ana",
            Items = [new OrderItemDTO { ProductId = 1, Quantity = 1 }]
        });

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Order_GetOrderById_WhenServiceReturnsNull_ReturnsNotFound()
    {
        var service = new Mock<IOrderServices>();
        service.Setup(x => x.GetOrderByIdAsync(1)).ReturnsAsync((OrderModel?)null!);
        var controller = new OrderController(service.Object);

        var result = await controller.GetOrderById(1);

        result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Stock_GetProductStock_WhenServiceReturnsNull_ReturnsNotFound()
    {
        var service = new Mock<IStockServices>();
        service.Setup(x => x.GetProductStock(1)).ReturnsAsync((ProductModel?)null!);
        var controller = new StockController(service.Object);

        var result = await controller.GetProductStock(1);

        result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Stock_AddStock_WhenServiceThrowsArgumentException_ReturnsBadRequest()
    {
        var service = new Mock<IStockServices>();
        service.Setup(x => x.AddStockAsync(It.IsAny<PetStore.Inventory.Application.ApplicationModel.Requests.StockAddRequest>()))
            .ThrowsAsync(new ArgumentException("Produto nao encontrado"));
        var controller = new StockController(service.Object);

        var result = await controller.AddStock(new StockAddDTO { ProductId = 1, Quantity = 2, InvoiceNumber = "NF-1" });

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Role_GetAll_WhenNoRolesExist_ReturnsNotFound()
    {
        var service = new Mock<IRoleServices>();
        service.Setup(x => x.GetAllRoles()).ReturnsAsync([]);
        var controller = new RoleController(service.Object);

        var result = await controller.GetAll();

        result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Login_Login_WhenTokenIsEmpty_ReturnsUnauthorized()
    {
        var service = new Mock<ILoginServices>();
        service.Setup(x => x.Login(It.IsAny<PetStore.Inventory.Application.ApplicationModel.Requests.LoginRegisterRequest>()))
            .ReturnsAsync(string.Empty);
        var controller = new LoginController(service.Object);

        var result = await controller.Login(new LoginDTO { Nickname = "ana", Password = "wrong" });

        result.Should().BeOfType<UnauthorizedObjectResult>();
    }

    [Fact]
    public async Task AccessConfig_StartApp_WhenServiceThrows_ReturnsInternalServerError()
    {
        var service = new Mock<IAccessConfigServices>();
        service.Setup(x => x.StartApp()).ThrowsAsync(new Exception("seed failed"));
        var controller = new AccessConfigController(service.Object);

        var result = await controller.StartApp();

        var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
        objectResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }

    [Fact]
    public async Task User_GetUsersFilteredById_WhenUserExists_ReturnsOk()
    {
        var service = new Mock<IUserServices>();
        service.Setup(x => x.GetUsersFilteredById(1))
            .ReturnsAsync(new UserRegisterModel(1, "Ana QA", "anaqa", "ana.qa@example.com", "secret1", EUserRoles.ADMIN));
        var controller = new UserController(service.Object);

        var result = await controller.GetUsersFilteredById(1);

        result.Should().BeOfType<OkObjectResult>();
    }
}
