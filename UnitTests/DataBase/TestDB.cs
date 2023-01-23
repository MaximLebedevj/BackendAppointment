using Microsoft.EntityFrameworkCore;
using Database.Repository;
using DataBase;
using DataBase.Models;
using domain.Models;

namespace UnitTests.DatabaseTests;

public class TestDB
{
    private readonly DbContextOptionsBuilder<ApplicationContext> _optionsBuilder;

    public TestDB()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionsBuilder.UseNpgsql(
             $"Host=localhost;Port=5432;Database=my_db;Username=postgres;Password=12345");
        _optionsBuilder = optionsBuilder;
    }

    [Fact]
    public void Test1()
    {
        var context = new ApplicationContext(_optionsBuilder.Options);

        var userUserRepository = new UserRepository(context);

        userUserRepository.Create(new User("User1", "Qwerty1", 1, "+7123123123", "FullName", Role.Patient));

        context.SaveChanges();

        Assert.True(context.Users.Any(u => u.FullName == "FullName"));
    }

    [Fact]
    public void Test2()
    {
        using var context = new ApplicationContext(_optionsBuilder.Options);
        context.Users.Add(new UserDB
        {
            Id = 2,
            FullName = "FullName2"
        });

        context.SaveChanges();

        Assert.True(context.Users.Any(u => u.FullName == "FullName2"));
    }

    [Fact]
    public void Test3()
    {
        using var context = new ApplicationContext(_optionsBuilder.Options);
        var u = context.Users.FirstOrDefault(u => u.FullName == "Name");
        context.Users.Remove(u);
        context.SaveChanges();

        Assert.True(!context.Users.Any(u => u.FullName == "Name"));
    }

    [Fact]
    public void Test4()
    {

        using var context = new ApplicationContext(_optionsBuilder.Options);
        var userRepository = new UserRepository(context);
        var userService = new UserService(userRepository);

        var res = userService.GetUserByLogin("TEST");

        Assert.NotNull(res.Value);
    }
}