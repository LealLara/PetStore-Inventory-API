using Moq;
using FluentAssertions;
using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Application.Services;
using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Domain.BusinessModel;

namespace PetStore.Inventory.Tests.Services
{
    public class UserServicesTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IRoleServices> _mockRoleServices;
        private readonly UserServices _userServices;

        public UserServicesTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockRoleServices = new Mock<IRoleServices>();
            _userServices = new UserServices(_mockUserRepository.Object, _mockRoleServices.Object);
        }

        #region GetAllUsers Tests

        [Fact]
        public async Task GetAllUsers_ShouldReturnAllUsers()
        {
            // Arrange
            var users = new List<UserRegisterModel>
            {
                new UserRegisterModel { UserId = 1, Email = "user1@example.com", Nickname = "user1" },
                new UserRegisterModel { UserId = 2, Email = "user2@example.com", Nickname = "user2" }
            };

            _mockUserRepository
                .Setup(r => r.GetAllUsers())
                .ReturnsAsync(users);

            // Act
            var result = await _userServices.GetAllUsers();

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(u => u.Email == "user1@example.com");
        }

        [Fact]
        public async Task GetAllUsers_WhenEmpty_ShouldReturnEmptyList()
        {
            // Arrange
            _mockUserRepository
                .Setup(r => r.GetAllUsers())
                .ReturnsAsync(new List<UserRegisterModel>());

            // Act
            var result = await _userServices.GetAllUsers();

            // Assert
            result.Should().BeEmpty();
        }

        #endregion

        #region CreatePatternUsers Tests

        [Fact]
        public async Task CreatePatternUsers_WhenNoUsersExist_ShouldCreatePatternUsers()
        {
            // Arrange
            _mockUserRepository
                .Setup(r => r.GetAllUsers())
                .ReturnsAsync(new List<UserRegisterModel>());

            _mockUserRepository
                .Setup(r => r.CreatePatternUsers(It.IsAny<List<object>>()))
                .ReturnsAsync(true);

            // Act
            var result = await _userServices.CreatePatternUsers();

            // Assert
            result.Should().BeTrue();
            _mockUserRepository.Verify(
                r => r.CreatePatternUsers(It.IsAny<List<object>>()),
                Times.Once
            );
        }

        [Fact]
        public async Task CreatePatternUsers_WhenUsersExist_ShouldReturnFalse()
        {
            // Arrange
            var existingUsers = new List<UserRegisterModel>
            {
                new UserRegisterModel { UserId = 1, Email = "user1@example.com" }
            };

            _mockUserRepository
                .Setup(r => r.GetAllUsers())
                .ReturnsAsync(existingUsers);

            // Act
            var result = await _userServices.CreatePatternUsers();

            // Assert
            result.Should().BeFalse();
        }

        #endregion

        #region CreateUser Tests

        [Fact]
        public async Task CreateUser_WithValidRequest_ShouldCreateUser()
        {
            // Arrange
            var userRequest = new UserRegisterRequest
            {
                Email = "newuser@example.com",
                Nickname = "newuser",
                Password = "SecurePassword123!"
            };

            var roles = new List<RoleModel>
            {
                new RoleModel { RoleId = 1, RoleName = "User" }
            };

            var createdUser = new UserRegisterModel
            {
                UserId = 1,
                Email = "newuser@example.com",
                Nickname = "newuser"
            };

            _mockRoleServices
                .Setup(r => r.GetAllRoles())
                .ReturnsAsync(roles);

            _mockUserRepository
                .Setup(r => r.GetUserFilteredByEmail("newuser@example.com"))
                .ReturnsAsync((UserRegisterModel)null);

            _mockUserRepository
                .Setup(r => r.GetUserFilteredByNickname("newuser"))
                .ReturnsAsync((UserRegisterModel)null);

            _mockUserRepository
                .Setup(r => r.CreateUser(It.IsAny<object>()))
                .ReturnsAsync(createdUser);

            // Act
            var result = await _userServices.CreateUser(userRequest);

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be("newuser@example.com");
            result.Nickname.Should().Be("newuser");
        }

        [Fact]
        public async Task CreateUser_WithDuplicateEmail_ShouldThrowException()
        {
            // Arrange
            var userRequest = new UserRegisterRequest
            {
                Email = "existing@example.com",
                Nickname = "newuser"
            };

            var roles = new List<RoleModel>
            {
                new RoleModel { RoleId = 1, RoleName = "User" }
            };

            var existingUser = new UserRegisterModel
            {
                UserId = 1,
                Email = "existing@example.com"
            };

            _mockRoleServices
                .Setup(r => r.GetAllRoles())
                .ReturnsAsync(roles);

            _mockUserRepository
                .Setup(r => r.GetUserFilteredByEmail("existing@example.com"))
                .ReturnsAsync(existingUser);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(
                () => _userServices.CreateUser(userRequest)
            );

            exception.Message.Should().Contain("email informado já está em uso");
        }

        [Fact]
        public async Task CreateUser_WithDuplicateNickname_ShouldThrowException()
        {
            // Arrange
            var userRequest = new UserRegisterRequest
            {
                Email = "newuser@example.com",
                Nickname = "existingnickname"
            };

            var roles = new List<RoleModel>
            {
                new RoleModel { RoleId = 1, RoleName = "User" }
            };

            var existingUser = new UserRegisterModel
            {
                UserId = 1,
                Nickname = "existingnickname"
            };

            _mockRoleServices
                .Setup(r => r.GetAllRoles())
                .ReturnsAsync(roles);

            _mockUserRepository
                .Setup(r => r.GetUserFilteredByEmail(It.IsAny<string>()))
                .ReturnsAsync((UserRegisterModel)null);

            _mockUserRepository
                .Setup(r => r.GetUserFilteredByNickname("existingnickname"))
                .ReturnsAsync(existingUser);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(
                () => _userServices.CreateUser(userRequest)
            );

            exception.Message.Should().Contain("nickname informado já está em uso");
        }

        [Fact]
        public async Task CreateUser_WhenNoRolesExist_ShouldThrowException()
        {
            // Arrange
            var userRequest = new UserRegisterRequest
            {
                Email = "newuser@example.com",
                Nickname = "newuser"
            };

            _mockRoleServices
                .Setup(r => r.GetAllRoles())
                .ReturnsAsync(new List<RoleModel>());

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(
                () => _userServices.CreateUser(userRequest)
            );

            exception.Message.Should().Contain("Inicie a aplicação antes de criar usuários");
        }

        #endregion

        #region GetUsersFilteredById Tests

        [Fact]
        public async Task GetUsersFilteredById_WithValidId_ShouldReturnUser()
        {
            // Arrange
            var userId = 1;
            var expectedUser = new UserRegisterModel
            {
                UserId = userId,
                Email = "user@example.com"
            };

            _mockUserRepository
                .Setup(r => r.GetUsersFilteredById(userId))
                .ReturnsAsync(expectedUser);

            // Act
            var result = await _userServices.GetUsersFilteredById(userId);

            // Assert
            result.Should().NotBeNull();
            result.UserId.Should().Be(userId);
        }

        [Fact]
        public async Task GetUsersFilteredById_WithInvalidId_ShouldThrowArgumentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _userServices.GetUsersFilteredById(0)
            );

            exception.Message.Should().Contain("ID do usuário");
        }

        [Fact]
        public async Task GetUsersFilteredById_WithNegativeId_ShouldThrowArgumentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _userServices.GetUsersFilteredById(-1)
            );

            exception.Message.Should().Contain("ID do usuário");
        }

        #endregion

        #region GetUsersFilteredByRoleId Tests

        [Fact]
        public async Task GetUsersFilteredByRoleId_WithValidId_ShouldReturnUsers()
        {
            // Arrange
            var roleId = 1;
            var users = new List<UserRegisterModel>
            {
                new UserRegisterModel { UserId = 1, Email = "admin1@example.com" },
                new UserRegisterModel { UserId = 2, Email = "admin2@example.com" }
            };

            _mockUserRepository
                .Setup(r => r.GetUsersFilteredByRoleId(roleId))
                .ReturnsAsync(users);

            // Act
            var result = await _userServices.GetUsersFilteredByRoleId(roleId);

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetUsersFilteredByRoleId_WithInvalidId_ShouldThrowArgumentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _userServices.GetUsersFilteredByRoleId(0)
            );

            exception.Message.Should().Contain("ID de papel");
        }

        #endregion

        #region GetUsersFilteredByString Tests

        [Fact]
        public async Task GetUsersFilteredByString_WithValidFilter_ShouldReturnUsers()
        {
            // Arrange
            var filter = "john";
            var users = new List<UserRegisterModel>
            {
                new UserRegisterModel { UserId = 1, Nickname = "john_doe" }
            };

            _mockUserRepository
                .Setup(r => r.GetUsersFilteredByString(filter))
                .ReturnsAsync(users);

            // Act
            var result = await _userServices.GetUsersFilteredByString(filter);

            // Assert
            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetUsersFilteredByString_WithEmptyFilter_ShouldThrowArgumentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _userServices.GetUsersFilteredByString("")
            );

            exception.Message.Should().Contain("string de filtro");
        }

        #endregion

        #region GetUserFilteredByEmail Tests

        [Fact]
        public async Task GetUserFilteredByEmail_WithValidEmail_ShouldReturnUser()
        {
            // Arrange
            var email = "user@example.com";
            var user = new UserRegisterModel
            {
                UserId = 1,
                Email = email
            };

            _mockUserRepository
                .Setup(r => r.GetUserFilteredByEmail(email))
                .ReturnsAsync(user);

            // Act
            var result = await _userServices.GetUserFilteredByEmail(email);

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be(email);
        }

        [Fact]
        public async Task GetUserFilteredByEmail_WithEmptyEmail_ShouldThrowArgumentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _userServices.GetUserFilteredByEmail("")
            );

            exception.Message.Should().Contain("Email");
        }

        #endregion

        #region GetUserFilteredByNickname Tests

        [Fact]
        public async Task GetUserFilteredByNickname_WithValidNickname_ShouldReturnUser()
        {
            // Arrange
            var nickname = "johndoe";
            var user = new UserRegisterModel
            {
                UserId = 1,
                Nickname = nickname
            };

            _mockUserRepository
                .Setup(r => r.GetUserFilteredByNickname(nickname))
                .ReturnsAsync(user);

            // Act
            var result = await _userServices.GetUserFilteredByNickname(nickname);

            // Assert
            result.Should().NotBeNull();
            result.Nickname.Should().Be(nickname);
        }

        [Fact]
        public async Task GetUserFilteredByNickname_WithEmptyNickname_ShouldThrowArgumentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _userServices.GetUserFilteredByNickname("")
            );

            exception.Message.Should().Contain("string de filtro");
        }

        #endregion

        #region RemoveUser Tests

        [Fact]
        public async Task RemoveUser_WithValidId_ShouldReturnTrue()
        {
            // Arrange
            var userId = 1;

            _mockUserRepository
                .Setup(r => r.RemoveUser(userId))
                .ReturnsAsync(true);

            // Act
            var result = await _userServices.RemoveUser(userId);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task RemoveUser_WithInvalidId_ShouldThrowArgumentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _userServices.RemoveUser(0)
            );

            exception.Message.Should().Contain("codigo do usuario");
        }

        #endregion
    }
}
