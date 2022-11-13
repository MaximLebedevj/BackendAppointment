using domain.Repository;
using Moq;
using domain.Models;

public class UserServiceTests
{
    private readonly UserService _userService;
    private readonly Mock<IUserRepository> _userRepositoryMock;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public void GetUser_LoginIsEmptyOrNull_Fail()
    {
        var result = _userService.GetUserByLogin(string.Empty);

        Assert.True(result.IsFailure);
        Assert.Equal("Invalid login", result.Error); 
    }

    [Fact]
    public void GetUser_UserNotFound_Fail()
    {
        _userRepositoryMock.Setup(repository => repository.GetUserByLogin(It.IsAny<string>()))
            .Returns(() => null);

        var result = _userService.GetUserByLogin("NewUser");

        Assert.True(result.IsFailure);
        Assert.Equal("User doesn't exist", result.Error);
    }

    [Fact]
    public void GetUser_UserFound_Success()
    {
        _userRepositoryMock.Setup(repository => repository.IsUserExists("NewUser"))
            .Returns(true);
        _userRepositoryMock.Setup(repository => repository.GetUserByLogin("NewUser"))
            .Returns(() => new User("NewUser", "...", 1, "...", "...", Role.Patient));

        var result = _userService.GetUserByLogin("NewUser");

        Assert.True(result.Success);
    }

    [Fact]
    public void UserExistence_LoginIsEmptyOrNull_Fail()
    {
        var result = _userService.IsUserExists(string.Empty);

        Assert.True(result.IsFailure);
        Assert.Equal("Invalid login", result.Error);
    }

    [Fact]
    public void UserExistence_UserNotFound_Fail()
    {
        _userRepositoryMock.Setup(repository => repository.IsUserExists(It.IsAny<string>()))
            .Returns(() => false);

        var result = _userService.IsUserExists("NewUser2");

        Assert.True(result.Success);
        Assert.False(result.Value);
    }

    [Fact]
    public void UserExistence_UserFound_Success()
    {
        _userRepositoryMock.Setup(repository => repository.IsUserExists(It.IsAny<string>()))
            .Returns(() => true);

        var result = _userService.IsUserExists("NewUser2");

        Assert.True(result.Success);
        Assert.True(result.Value);
    }

    [Fact]
    public void CreateUser_InvalidLogin_Fail()
    {
        User invalidUser = new User(String.Empty, "...", 1, "...", "...", Role.Patient);
        var result = _userService.CreateUser(invalidUser);

        Assert.True(result.IsFailure);
        Assert.Equal("Invalid Login", result.Error);
    }

    [Fact]
    public void CreateUser_InvalidPassword_Fail()
    {
        User invalidUser = new User("...", String.Empty, 1, "...", "...", Role.Patient);
        var result = _userService.CreateUser(invalidUser);

        Assert.True(result.IsFailure);
        Assert.Equal("Invalid Password", result.Error);
    }

    [Fact]
    public void CreateUser_InvalidPhoneNumber_Fail()
    {
        User invalidUser = new User("...", "...", 1, String.Empty, "...", Role.Patient);
        var result = _userService.CreateUser(invalidUser);

        Assert.True(result.IsFailure);
        Assert.Equal("Invalid PhoneNumber", result.Error);
    }

    [Fact]
    public void CreateUser_InvalidFullName_Fail()
    {
        User invalidUser = new User("...", "...", 1, "...", String.Empty, Role.Patient);
        var result = _userService.CreateUser(invalidUser);

        Assert.True(result.IsFailure);
        Assert.Equal("Invalid FullName", result.Error);
    }

    [Fact]
    public void CreateUser_FailedToCreate_Fail()
    {
        _userRepositoryMock.Setup(repository => repository.CreateUser(It.IsAny<User>()))
            .Returns(() => false);

        User validUser = new User("...", "...", 1, "...", "...", Role.Patient);
        var result = _userService.CreateUser(validUser);

        Assert.True(result.IsFailure);
        Assert.Equal("User has not been created", result.Error);
    }

    [Fact]
    public void CreateUser_SuccessfullyCreated_Success()
    {
        _userRepositoryMock.Setup(repository => repository.CreateUser(It.IsAny<User>()))
            .Returns(() => true);

        User validUser = new User("...", "...", 1, "...", "...", Role.Patient);
        var result = _userService.CreateUser(validUser);

        Assert.True(result.Success);
    }

}
