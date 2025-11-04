using Microsoft.AspNetCore.Identity;
using TaskLite.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskLite.UnitTests.Identity;

public class UserRegistrationTests : IClassFixture<IdentityTestFixture>
{
    private readonly IdentityTestFixture _context;
    public UserRegistrationTests(IdentityTestFixture context) => _context = context;

    [Fact]
    public async Task Register_New_User_Should_Succeed()
    {
        User user = new()
        {
            UserName = "john@example.com",
            Email = "john@example.com",
            FullName = "John Doe"
        };

        string password = "Passw0rd!";

        var result = await _context.UserManager.CreateAsync(user, password);

        // the result is successful
        Assert.True(result.Succeeded);
        Assert.NotNull(user.PasswordHash);
        Assert.NotEqual(password, user.PasswordHash);

        // the user is actually in the in-memory DB
        var createdUser = await _context.UserManager.FindByEmailAsync("john@example.com");
        Assert.NotNull(createdUser);
        Assert.Equal("John Doe", createdUser.FullName);
    }

    [Fact]
    public async Task Register_With_Duplicate_Email_Should_Fail()
    {
        string email = "duplicate@example.com";
        string password = "Passw0rd!";

        User user1 = new()
        {
            UserName = email,
            Email = email,
            FullName = "User X"
        };

        User user2 = new()
        {
            UserName = email,
            Email = email,
            FullName = "User Y"
        };

        var firstResult = await _context.UserManager.CreateAsync(user1, password);
        var secondResult = await _context.UserManager.CreateAsync(user2, password);

        Assert.True(firstResult.Succeeded);
        Assert.False(secondResult.Succeeded);
    }

    [Fact]
    public async Task Password_Should_Be_Hashed_Not_PlainText()
    {
        string email = "hashcheck@example.com";
        string password = "Passw0rd!";

        User user = new()
        {
            UserName = email,
            Email = email,
            FullName = "Hash Test"
        };

        await _context.UserManager.CreateAsync(user, password);

        Assert.NotNull(user.PasswordHash);
        Assert.NotEqual(password, user.PasswordHash);

        // Verify that the hash matches the original password
        var verification = _context.UserManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash!, password);
        Assert.Equal(PasswordVerificationResult.Success, verification);
    }

    [Fact]
    public async Task Login_With_Correct_Credentials_Should_Succeed()
    {
        string email = "login@example.com";
        string password = "Passw0rd!";

        User user = new()
        {
            UserName = email,
            Email = email,
            FullName = "Login User"
        };

        await _context.UserManager.CreateAsync(user, password);

        var result = await _context.SignInManager.CheckPasswordSignInAsync(user, password, false);
        Assert.True(result.Succeeded);
    }

    [Fact]
    public async Task Login_With_Wrong_Password_Should_Fail()
    {
        string email = "wrongpass@example.com";
        string password = "Passw0rd!";

        User user = new ()
        {
            UserName = email,
            Email = email,
            FullName = "Wrong Password User"
        };

        await _context.UserManager.CreateAsync(user, password);

        var result = await _context.SignInManager.CheckPasswordSignInAsync(user, "WrongPass123!", false);
        Assert.False(result.Succeeded);
        Assert.Equal(SignInResult.Failed, result);
    }
}