using FluentAssertions;
using FluentValidation.TestHelper;
using PetStore.Inventory.Api.ApplicationDTOs.Requests;
using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Utils.Enums;
using PetStore.Inventory.Domain.Utils.Validations;

namespace PetStore.Inventory.Tests.Domain;

public class DomainAndDtoTests
{
    [Fact]
    public void ProductModel_AddStock_WithPositiveQuantity_IncreasesStock()
    {
        var product = new ProductModel(1, "Racao", "Pacote", 10f, 5);

        product.AddStock(3);

        product.StockQuantity.Should().Be(8);
    }

    [Fact]
    public void ProductModel_AddStock_WithZeroQuantity_ThrowsArgumentException()
    {
        var product = new ProductModel(1, "Racao", "Pacote", 10f, 5);

        Action act = () => product.AddStock(0);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ProductModel_RemoveStock_WithAvailableQuantity_DecreasesStock()
    {
        var product = new ProductModel(1, "Racao", "Pacote", 10f, 5);

        product.RemoveStock(2);

        product.StockQuantity.Should().Be(3);
    }

    [Fact]
    public void ProductModel_RemoveStock_WithInsufficientQuantity_ThrowsInvalidOperationException()
    {
        var product = new ProductModel(1, "Racao", "Pacote", 10f, 1);

        Action act = () => product.RemoveStock(2);

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void OrderItemRequest_ToModel_MapsProductAndQuantity()
    {
        var request = new OrderItemRequest(5, 2);

        var model = request.ToModel();

        model.ProductId.Should().Be(5);
        model.Quantity.Should().Be(2);
    }

    [Fact]
    public void UserFirstRegisterDTO_ToBusinessRequest_MapsRoleEnum()
    {
        var dto = new UserFirstRegisterDTO
        {
            FullName = "Ana QA",
            Nickname = "anaqa",
            Email = "ana.qa@example.com",
            Password = "secret1",
            RoleId = (int)EUserRoles.ADMIN
        };

        var request = dto.ToBusinessRequest();

        request.RoleId.Should().Be(EUserRoles.ADMIN);
        request.Email.Should().Be(dto.Email);
    }

    [Fact]
    public void ProductDTO_ToBusinessRequest_MapsAllFields()
    {
        var dto = new ProductDTO
        {
            ProductName = "Coleira",
            ProductDescription = "Coleira ajustavel",
            Price = 25f,
            StockQuantity = 2
        };

        var request = dto.ToBusinessRequest();

        request.ProductName.Should().Be(dto.ProductName);
        request.ProductDescription.Should().Be(dto.ProductDescription);
        request.Price.Should().Be(25f);
        request.StockQuantity.Should().Be(2);
    }

    [Fact]
    public void OrderCreateDTO_ToBusinessRequest_MapsNestedItems()
    {
        var dto = new OrderCreateDTO
        {
            CustomerDocument = "12345678900",
            SellerName = "Ana",
            Items = [new OrderItemDTO { ProductId = 1, Quantity = 3 }]
        };

        var request = dto.ToBusinessRequest();

        request.CustomerDocument.Should().Be(dto.CustomerDocument);
        request.Items.Should().ContainSingle(i => i.ProductId == 1 && i.Quantity == 3);
    }

    [Fact]
    public void ProductValidation_WithInvalidProduct_HasExpectedFailures()
    {
        var validator = new ProductValidation();
        var model = new ProductModel(0, "", new string('x', 201), 0f, 0);

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ProductName);
        result.ShouldHaveValidationErrorFor(x => x.ProductDescription);
        result.ShouldHaveValidationErrorFor(x => x.Price);
        result.ShouldHaveValidationErrorFor(x => x.StockQuantity);
    }

    [Fact]
    public void UserValidation_DisallowsSystemOperatorCreation()
    {
        var validator = new UserValidation();
        var model = new UserRegisterModel("Ana QA", "anaqa", "ana.qa@example.com", "secret1", EUserRoles.SYSTEM_OPERATOR);

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.RoleId);
    }

    [Fact]
    public void StockValidation_WithInvalidRequest_HasExpectedFailures()
    {
        var validator = new StockValidation();
        var model = new StockMovementModel(0, 0, "");

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ProductId);
        result.ShouldHaveValidationErrorFor(x => x.Quantity);
        result.ShouldHaveValidationErrorFor(x => x.InvoiceNumber);
    }

    [Fact]
    public void LoginRegisterRequest_SetPatternLogin_CreatesHashedDefaultAccounts()
    {
        var request = new LoginRegisterRequest();

        var logins = request.SetPatternLogin();

        logins.Should().HaveCount(3);
        logins.Should().OnlyContain(x => !string.IsNullOrWhiteSpace(x.Nickname));
        logins.Should().OnlyContain(x => x.Password.StartsWith("AQAAAA", StringComparison.Ordinal));
    }
}
