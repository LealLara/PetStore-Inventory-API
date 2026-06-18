using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using PetStore.Inventory.Api.Controllers;
using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Application.Services;
using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;
using PetStore.Inventory.Domain.Utils.Enums;

namespace PetStore.Inventory.Tests.Services;

public class CoreServicesTests
{
    [Fact]
    public async Task Product_CreatePatternProducts_WhenCatalogIsEmpty_CreatesSeedProducts()
    {
        var repository = new Mock<IProductRepository>();
        repository.Setup(x => x.GetAllProducts()).ReturnsAsync([]);
        repository.Setup(x => x.CreatePatternProducts(It.IsAny<IEnumerable<ProductEntity>>())).ReturnsAsync(true);
        var service = new ProductServices(repository.Object);

        var result = await service.CreatePatternProducts();

        result.Should().BeTrue();
        repository.Verify(x => x.CreatePatternProducts(It.Is<IEnumerable<ProductEntity>>(p => p.Count() == 10)), Times.Once);
    }

    [Fact]
    public async Task Product_CreatePatternProducts_WhenCatalogExists_DoesNotCreateSeedProducts()
    {
        var repository = new Mock<IProductRepository>();
        repository.Setup(x => x.GetAllProducts()).ReturnsAsync([new ProductModel(1, "Racao", "Seca", 10f, 1)]);
        var service = new ProductServices(repository.Object);

        var result = await service.CreatePatternProducts();

        result.Should().BeFalse();
        repository.Verify(x => x.CreatePatternProducts(It.IsAny<IEnumerable<ProductEntity>>()), Times.Never);
    }

    [Fact]
    public async Task Product_CreateProduct_WithValidRequest_ValidatesAndPersists()
    {
        var request = new ProductRequest("Racao premium", "Pacote para caes adultos", 99.9f, 12);
        var repository = new Mock<IProductRepository>();
        repository.Setup(x => x.CreateProduct(It.IsAny<ProductEntity>()))
            .ReturnsAsync(new ProductModel(7, request.ProductName, request.ProductDescription, request.Price, request.StockQuantity));
        var service = new ProductServices(repository.Object);

        var result = await service.CreateProduct(request);

        result.ProductId.Should().Be(7);
        result.ProductName.Should().Be(request.ProductName);
    }

    [Fact]
    public async Task Product_CreateProduct_WithInvalidStock_ThrowsValidationException()
    {
        var service = new ProductServices(new Mock<IProductRepository>().Object);
        var request = new ProductRequest("Racao", "Descricao", 10f, 0);

        await Assert.ThrowsAsync<ValidationException>(() => service.CreateProduct(request));
    }

    [Fact]
    public async Task Product_UpdateProduct_WhenProductExists_UpdatesRepository()
    {
        var request = new ProductRequest(1, "Coleira", "Coleira ajustavel", 25f, 3);
        var repository = new Mock<IProductRepository>();
        repository.Setup(x => x.GetAllProductsFilteredById(1))
            .ReturnsAsync(new ProductModel(1, "Coleira antiga", "Descricao", 20f, 2));
        repository.Setup(x => x.UpdateProduct(It.IsAny<ProductEntity>()))
            .ReturnsAsync(new ProductModel(1, request.ProductName, request.ProductDescription, request.Price, request.StockQuantity));
        var service = new ProductServices(repository.Object);

        var result = await service.UpdateProduct(request);

        result.ProductName.Should().Be("Coleira");
        repository.Verify(x => x.UpdateProduct(It.Is<ProductEntity>(p => p.ProductId == 1)), Times.Once);
    }

    [Fact]
    public async Task Product_UpdateProduct_WhenIdIsZero_ThrowsArgumentException()
    {
        var service = new ProductServices(new Mock<IProductRepository>().Object);
        var request = new ProductRequest(0, "Coleira", "Coleira ajustavel", 25f, 3);

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateProduct(request));

        exception.Message.Should().Contain("ID do produto");
    }

    [Fact]
    public async Task User_CreatePatternUsers_WhenNoUsersExist_CreatesSeedUsers()
    {
        var users = new Mock<IUserRepository>();
        var roles = new Mock<IRoleServices>();
        users.Setup(x => x.GetAllUsers()).ReturnsAsync([]);
        users.Setup(x => x.CreatePatternUsers(It.IsAny<List<UserEntity>>())).ReturnsAsync(true);
        var service = new UserServices(users.Object, roles.Object);

        var result = await service.CreatePatternUsers();

        result.Should().BeTrue();
        users.Verify(x => x.CreatePatternUsers(It.Is<List<UserEntity>>(u => u.Count == 3)), Times.Once);
    }

    [Fact]
    public async Task User_CreateUser_WithValidData_CreatesUser()
    {
        var users = new Mock<IUserRepository>();
        var roles = new Mock<IRoleServices>();
        var request = ValidUserRequest();
        roles.Setup(x => x.GetAllRoles()).ReturnsAsync([new RoleModel(1, "Admin", "Administrador", true)]);
        users.Setup(x => x.GetUserFilteredByEmail(request.Email)).ReturnsAsync((UserRegisterModel?)null!);
        users.Setup(x => x.GetUserFilteredByNickname(request.Nickname)).ReturnsAsync((UserRegisterModel?)null!);
        users.Setup(x => x.CreateUser(It.IsAny<UserEntity>()))
            .ReturnsAsync(new UserRegisterModel(10, request.FullName, request.Nickname, request.Email, request.Password, request.RoleId));
        var service = new UserServices(users.Object, roles.Object);

        var result = await service.CreateUser(request);

        result.UserId.Should().Be(10);
        result.Email.Should().Be(request.Email);
    }

    [Fact]
    public async Task User_CreateUser_WhenRolesWereNotSeeded_ThrowsException()
    {
        var roles = new Mock<IRoleServices>();
        roles.Setup(x => x.GetAllRoles()).ReturnsAsync([]);
        var service = new UserServices(new Mock<IUserRepository>().Object, roles.Object);

        var exception = await Assert.ThrowsAsync<Exception>(() => service.CreateUser(ValidUserRequest()));

        exception.Message.Should().Contain("Inicie a aplic");
    }

    [Fact]
    public async Task User_CreateUser_WithDuplicatedEmail_ThrowsException()
    {
        var users = new Mock<IUserRepository>();
        var roles = new Mock<IRoleServices>();
        var request = ValidUserRequest();
        roles.Setup(x => x.GetAllRoles()).ReturnsAsync([new RoleModel(1, "Admin", "Administrador", true)]);
        users.Setup(x => x.GetUserFilteredByEmail(request.Email))
            .ReturnsAsync(new UserRegisterModel(1, "Maria", "maria", request.Email, "secret", EUserRoles.ADMIN));
        var service = new UserServices(users.Object, roles.Object);

        var exception = await Assert.ThrowsAsync<Exception>(() => service.CreateUser(request));

        exception.Message.Should().Contain("email informado");
    }

    [Fact]
    public async Task User_GetById_WithInvalidId_ThrowsArgumentException()
    {
        var service = new UserServices(new Mock<IUserRepository>().Object, new Mock<IRoleServices>().Object);

        await Assert.ThrowsAsync<ArgumentException>(() => service.GetUsersFilteredById(0));
    }

    [Fact]
    public async Task Order_CreateOrderAsync_WithValidItems_DecrementsStockAndCreatesOrder()
    {
        var orders = new Mock<IOrderRepository>();
        var products = new Mock<IProductRepository>();
        var request = new OrderCreateRequest("12345678900", "Ana", [new OrderItemRequest(1, 2)]);
        products.Setup(x => x.GetAllProductsFilteredById(1))
            .ReturnsAsync(new ProductModel(1, "Racao", "Pacote", 30f, 5));
        products.Setup(x => x.UpdateProduct(It.IsAny<ProductEntity>()))
            .ReturnsAsync((ProductEntity entity) => new ProductModel(entity.ProductId, entity.ProductName, entity.ProductDescription, entity.Price, entity.StockQuantity));
        orders.Setup(x => x.CreateOrder(It.IsAny<OrderEntity>(), It.IsAny<List<OrderItemEntity>>()))
            .ReturnsAsync((OrderEntity order, List<OrderItemEntity> _) =>
                new OrderModel(1, order.CustomerDocument, order.SellerName, order.TotalAmount, order.CreatedAt, []));
        var service = new OrderServices(orders.Object, products.Object);

        var result = await service.CreateOrderAsync(request);

        result.TotalAmount.Should().Be(60m);
        products.Verify(x => x.UpdateProduct(It.Is<ProductEntity>(p => p.StockQuantity == 3)), Times.Once);
    }

    [Fact]
    public async Task Order_CreateOrderAsync_WhenProductDoesNotExist_ThrowsArgumentException()
    {
        var products = new Mock<IProductRepository>();
        products.Setup(x => x.GetAllProductsFilteredById(99)).ReturnsAsync((ProductModel?)null!);
        var service = new OrderServices(new Mock<IOrderRepository>().Object, products.Object);
        var request = new OrderCreateRequest("12345678900", "Ana", [new OrderItemRequest(99, 1)]);

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => service.CreateOrderAsync(request));

        exception.Message.Should().Contain("Produto com ID 99");
    }

    [Fact]
    public async Task Order_CreateOrderAsync_WhenStockIsInsufficient_ThrowsInvalidOperationException()
    {
        var products = new Mock<IProductRepository>();
        products.Setup(x => x.GetAllProductsFilteredById(1))
            .ReturnsAsync(new ProductModel(1, "Racao", "Pacote", 30f, 1));
        var service = new OrderServices(new Mock<IOrderRepository>().Object, products.Object);
        var request = new OrderCreateRequest("12345678900", "Ana", [new OrderItemRequest(1, 3)]);

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.CreateOrderAsync(request));
    }

    [Fact]
    public async Task Order_GetOrdersBySellerAsync_WithBlankSeller_ThrowsArgumentException()
    {
        var service = new OrderServices(new Mock<IOrderRepository>().Object, new Mock<IProductRepository>().Object);

        await Assert.ThrowsAsync<ArgumentException>(() => service.GetOrdersBySellerAsync(" "));
    }

    [Fact]
    public async Task Role_CreatePatternRoles_WhenEmpty_CreatesThreeRoles()
    {
        var repository = new Mock<IRoleRepository>();
        repository.Setup(x => x.GetAllRoles()).ReturnsAsync([]);
        repository.Setup(x => x.CreatePatternRoles(It.IsAny<List<RoleEntity>>())).ReturnsAsync(true);
        var service = new RoleServices(repository.Object);

        var result = await service.CreatePatternRoles();

        result.Should().BeTrue();
        repository.Verify(x => x.CreatePatternRoles(It.Is<List<RoleEntity>>(r => r.Count == 3)), Times.Once);
    }

    [Fact]
    public async Task Role_CreateRole_WithInvalidName_ThrowsValidationException()
    {
        var service = new RoleServices(new Mock<IRoleRepository>().Object);
        var request = new RoleRequest("Adm", "Descricao valida", true);

        await Assert.ThrowsAsync<ValidationException>(() => service.CreateRole(request));
    }

    [Fact]
    public async Task LogType_CreatePatternLogTypes_WhenEmpty_CreatesThreeTypes()
    {
        var repository = new Mock<ILogTypeRepository>();
        repository.Setup(x => x.GetAllLogTypes()).ReturnsAsync([]);
        repository.Setup(x => x.CreatePatternLogTypes(It.IsAny<List<LogTypeEntity>>())).ReturnsAsync(true);
        var service = new LogTypeServices(repository.Object);

        var result = await service.CreatePatternLogTypes();

        result.Should().BeTrue();
        repository.Verify(x => x.CreatePatternLogTypes(It.Is<List<LogTypeEntity>>(l => l.Count == 3)), Times.Once);
    }

    [Fact]
    public async Task LogType_CreateLogTypes_WithInvalidDescription_ThrowsValidationException()
    {
        var service = new LogTypeServices(new Mock<ILogTypeRepository>().Object);
        var request = new LogTypeRequest("Erro", "curta");

        await Assert.ThrowsAsync<ValidationException>(() => service.CreateLogTypes(request));
    }

    [Fact]
    public async Task Stock_AddStockAsync_WithValidRequest_IncreasesProductStockAndRegistersMovement()
    {
        var movements = new Mock<IStockMovementRepository>();
        var products = new Mock<IProductRepository>();
        products.Setup(x => x.GetAllProductsFilteredById(1))
            .ReturnsAsync(new ProductModel(1, "Racao", "Pacote", 30f, 5));
        products.Setup(x => x.UpdateProduct(It.IsAny<ProductEntity>()))
            .ReturnsAsync((ProductEntity entity) => new ProductModel(entity.ProductId, entity.ProductName, entity.ProductDescription, entity.Price, entity.StockQuantity));
        movements.Setup(x => x.AddStockMovement(It.IsAny<StockMovementEntity>()))
            .ReturnsAsync(new StockMovementModel(1, "Racao", 4, "NF-1", DateTime.UtcNow, "Inbound"));
        var service = new StockServices(movements.Object, products.Object);

        var result = await service.AddStockAsync(new StockAddRequest(1, 4, "NF-1"));

        result.Quantity.Should().Be(4);
        products.Verify(x => x.UpdateProduct(It.Is<ProductEntity>(p => p.StockQuantity == 9)), Times.Once);
    }

    [Fact]
    public async Task Stock_AddStockAsync_WhenProductDoesNotExist_ThrowsArgumentException()
    {
        var products = new Mock<IProductRepository>();
        products.Setup(x => x.GetAllProductsFilteredById(1)).ReturnsAsync((ProductModel?)null!);
        var service = new StockServices(new Mock<IStockMovementRepository>().Object, products.Object);

        await Assert.ThrowsAsync<ArgumentException>(() => service.AddStockAsync(new StockAddRequest(1, 1, "NF-1")));
    }

    [Fact]
    public async Task Login_CreatePatternLogin_WhenRepositoryReturnsFalse_ThrowsException()
    {
        var repository = new Mock<ILoginRepository>();
        repository.Setup(x => x.CreatePatternLogin(It.IsAny<IEnumerable<LoginEntity>>())).ReturnsAsync(false);
        var service = new LoginServices(repository.Object, new Mock<IEmailServices>().Object, new Mock<IAuthenticationServices>().Object);

        await Assert.ThrowsAsync<Exception>(() => service.CreatePatternLogin());
    }

    [Fact]
    public async Task Login_CreateLogin_HashesPasswordBeforePersisting()
    {
        LoginEntity? captured = null;
        var repository = new Mock<ILoginRepository>();
        repository.Setup(x => x.Login(It.IsAny<LoginEntity>()))
            .Callback<LoginEntity>(entity => captured = entity)
            .ReturnsAsync(new LoginModel(1, "ana", "hashed", 5));
        var service = new LoginServices(repository.Object, new Mock<IEmailServices>().Object, new Mock<IAuthenticationServices>().Object);

        await service.CreateLogin(new LoginRegisterRequest("ana", "plain-secret", 5));

        captured.Should().NotBeNull();
        captured!.Password.Should().NotBe("plain-secret");
    }

    [Fact]
    public async Task Login_Login_WithValidPassword_ReturnsGeneratedTokenMessage()
    {
        var hasher = new PasswordHasher<LoginModel>();
        var login = new LoginModel(1, "ana", string.Empty, 5);
        login.Password = hasher.HashPassword(login, "plain-secret");
        var repository = new Mock<ILoginRepository>();
        repository.Setup(x => x.GetByNickname("ana")).ReturnsAsync(login);
        var authentication = new Mock<IAuthenticationServices>();
        authentication.Setup(x => x.GenerateJwt(login))
            .ReturnsAsync(new UserDataToSendLoginEmailModel(new UserRegisterModel(), login, "jwt-token"));
        var service = new LoginServices(repository.Object, new Mock<IEmailServices>().Object, authentication.Object);

        var result = await service.Login(new LoginRegisterRequest(" ana ", "plain-secret"));

        result.Should().Be("Token gerado: jwt-token");
    }

    [Fact]
    public async Task Login_Login_WithInvalidPassword_ReturnsNull()
    {
        var hasher = new PasswordHasher<LoginModel>();
        var login = new LoginModel(1, "ana", string.Empty, 5);
        login.Password = hasher.HashPassword(login, "plain-secret");
        var repository = new Mock<ILoginRepository>();
        repository.Setup(x => x.GetByNickname("ana")).ReturnsAsync(login);
        var service = new LoginServices(repository.Object, new Mock<IEmailServices>().Object, new Mock<IAuthenticationServices>().Object);

        var result = await service.Login(new LoginRegisterRequest("ana", "wrong"));

        result.Should().BeNull();
    }

    [Fact]
    public async Task Authentication_GenerateJwt_IncludesUserDataAndToken()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?> { ["Jwt:Key"] = "0123456789abcdef0123456789abcdef" })
            .Build();
        var users = new Mock<IUserServices>();
        users.Setup(x => x.GetUsersFilteredById(2))
            .ReturnsAsync(new UserRegisterModel(2, "Ana QA", "ana", "ana@example.com", "secret", EUserRoles.ADMIN));
        var service = new AuthenticationServices(configuration, users.Object);

        var result = await service.GenerateJwt(new LoginModel(1, "ana", "hash", 2));

        result.Register.Email.Should().Be("ana@example.com");
        result.Token.Should().NotBeNullOrWhiteSpace();
        result.Token.Split('.').Should().HaveCount(3);
    }

    [Fact]
    public async Task Email_SetLoginEmail_WhenRepositorySends_ReturnsSuccessMessage()
    {
        var login = new LoginModel(1, "ana", "hash", 2);
        var authentication = new Mock<IAuthenticationServices>();
        authentication.Setup(x => x.GenerateJwt(login))
            .ReturnsAsync(new UserDataToSendLoginEmailModel(
                new UserRegisterModel(2, "Ana QA", "ana", "ana@example.com", "secret", EUserRoles.ADMIN),
                login,
                "jwt-token"));
        var emailRepository = new Mock<IEmailRepository>();
        emailRepository.Setup(x => x.SendAsync(It.IsAny<EmailEntity>())).ReturnsAsync(true);
        var service = new EmailServices(authentication.Object, emailRepository.Object);

        var result = await service.SetLoginEmail(login);

        result.Should().Contain("token secreto");
        emailRepository.Verify(x => x.SendAsync(It.Is<EmailEntity>(e => e.EmailAddress == "ana@example.com")), Times.Once);
    }

    [Fact]
    public async Task AccessConfig_StartApp_WhenRolesAreCreated_RunsAllSeedSteps()
    {
        var roles = new Mock<IRoleServices>();
        var logTypes = new Mock<ILogTypeServices>();
        var users = new Mock<IUserServices>();
        var logins = new Mock<ILoginServices>();
        var products = new Mock<IProductServices>();
        roles.Setup(x => x.CreatePatternRoles()).ReturnsAsync(true);
        logTypes.Setup(x => x.CreatePatternLogTypes()).ReturnsAsync(true);
        users.Setup(x => x.CreatePatternUsers()).ReturnsAsync(true);
        logins.Setup(x => x.CreatePatternLogin()).ReturnsAsync(true);
        products.Setup(x => x.CreatePatternProducts()).ReturnsAsync(true);
        var service = new AccessConfigServices(new Mock<IAccessConfigRepository>().Object, roles.Object, logTypes.Object, users.Object, logins.Object, products.Object);

        var result = await service.StartApp();

        result.Should().BeTrue();
        logTypes.Verify(x => x.CreatePatternLogTypes(), Times.Once);
        users.Verify(x => x.CreatePatternUsers(), Times.Once);
        logins.Verify(x => x.CreatePatternLogin(), Times.Once);
        products.Verify(x => x.CreatePatternProducts(), Times.Once);
    }

    [Fact]
    public async Task AccessConfig_StartApp_WhenRolesAlreadyExist_ThrowsException()
    {
        var roles = new Mock<IRoleServices>();
        roles.Setup(x => x.CreatePatternRoles()).ReturnsAsync(false);
        var service = new AccessConfigServices(
            new Mock<IAccessConfigRepository>().Object,
            roles.Object,
            new Mock<ILogTypeServices>().Object,
            new Mock<IUserServices>().Object,
            new Mock<ILoginServices>().Object,
            new Mock<IProductServices>().Object);

        await Assert.ThrowsAsync<Exception>(() => service.StartApp());
    }

    [Fact]
    public async Task AccessRegister_CreateAccessRegister_WhenUserAndLoginAreCreated_ReturnsUser()
    {
        var request = ValidUserRequest();
        var users = new Mock<IUserServices>();
        var logins = new Mock<ILoginServices>();
        users.Setup(x => x.CreateUser(request))
            .ReturnsAsync(new UserRegisterModel(12, request.FullName, request.Nickname, request.Email, request.Password, request.RoleId));
        logins.Setup(x => x.CreateLogin(It.IsAny<LoginRegisterRequest>()))
            .ReturnsAsync(new LoginModel(1, request.Nickname, "hash", 12));
        var service = new AccessRegisterServices(new Mock<IAccessRegisterRepository>().Object, users.Object, logins.Object);

        var result = await service.CreateAccessRegister(request);

        result.UserId.Should().Be(12);
        logins.Verify(x => x.CreateLogin(It.Is<LoginRegisterRequest>(l => l.UserId == 12)), Times.Once);
    }

    [Fact]
    public async Task AccessRegister_RemoveAccount_WhenLoginRemovalSucceeds_RemovesUser()
    {
        var users = new Mock<IUserServices>();
        var logins = new Mock<ILoginServices>();
        logins.Setup(x => x.RemoveAccount(12)).ReturnsAsync(true);
        users.Setup(x => x.RemoveUser(12)).ReturnsAsync(true);
        var service = new AccessRegisterServices(new Mock<IAccessRegisterRepository>().Object, users.Object, logins.Object);

        var result = await service.RemoveAccount(12);

        result.Should().BeTrue();
        users.Verify(x => x.RemoveUser(12), Times.Once);
    }

    private static UserRegisterRequest ValidUserRequest() =>
        new("Ana QA", "anaqa", "ana.qa@example.com", "secret1", EUserRoles.ADMIN);
}
