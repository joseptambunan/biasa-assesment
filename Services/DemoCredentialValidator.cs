using System.Security.Cryptography;
using System.Text;

namespace AspNetMvcLogin.Services;

public sealed class DemoCredentialValidator
{
    private readonly IConfiguration _configuration;

    public DemoCredentialValidator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public bool IsValid(string? username, string? password)
    {
        var expectedUsername = _configuration["DemoUser:Username"] ?? "admin";
        var expectedPassword = _configuration["DemoUser:Password"] ?? "Admin123!";
        Console.WriteLine("Hello ASP.NET", expectedUsername);
        Console.WriteLine($"Hello {expectedUsername}");
        if (string.IsNullOrWhiteSpace(username) || password is null)
        {
            return false;
        }

        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var expectedPasswordBytes = Encoding.UTF8.GetBytes(expectedPassword);

        try
        {
            return string.Equals(username.Trim(), expectedUsername, StringComparison.OrdinalIgnoreCase)
                && CryptographicOperations.FixedTimeEquals(passwordBytes, expectedPasswordBytes);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(passwordBytes);
            CryptographicOperations.ZeroMemory(expectedPasswordBytes);
        }
    }

    public string GetDisplayName(string username)
    {
        return _configuration["DemoUser:DisplayName"] ?? username;
    }
}
