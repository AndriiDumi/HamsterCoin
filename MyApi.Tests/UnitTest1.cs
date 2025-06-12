using Xunit;
using HamsterCoin.Services.Implementations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HamsterCoin.Database;
using Microsoft.EntityFrameworkCore;
using HamsterCoin.Domain;

public class JwtServiceTests
{
    [Fact]
    public void GetUserId_ValidTokenWithUserId_ReturnsUserId()
    {
        // Arrange
        var userId = 12345L;
        var token = GenerateJwtToken(userId);
        var service = new JwtService();

        // Act
        var result = service.GetUserId(token);

        // Assert
        Assert.Equal(userId, result);
    }

    [Fact]
    public void GetUserId_InvalidToken_ThrowsException()
    {
        // Arrange
        var service = new JwtService();
        var invalidToken = "invalid.token.value";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => service.GetUserId(invalidToken));
    }

    [Fact]
    public void GetUserId_NoUserIdClaim_ThrowsException()
    {
        // Arrange
        var token = GenerateJwtTokenWithoutUserId();
        var service = new JwtService();

        // Act & Assert
        var ex = Assert.Throws<Exception>(() => service.GetUserId(token));
        Assert.Contains("Token don't contain userId", ex.Message);
    }

    private string GenerateJwtToken(long userId)
    {
        var claims = new[]
        {
            new Claim("sub", userId.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super_secure_and_long_enough_key_123456"));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateJwtTokenWithoutUserId()
    {
        var claims = new[]
        {
            new Claim("role", "admin")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super_secure_and_long_enough_key_123456"));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
   
}
