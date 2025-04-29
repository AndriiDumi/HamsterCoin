using HamsterCoin.Domain;

namespace HamsterCoin.Auth
{
    public interface IAuthenticationService
    {
        Task<User> AuthenticateByUser(User user);
        Task<RefreshToken> FindRefreshTokenByTokenAsync(string refreshToken);
        Task SaveRefreshTokenAsync(RefreshToken refreshToken);
        Task UpdateRefreshTokenAsync(RefreshToken updatedRefreshToken);
        Task LogoutAsync(string refreshToken);
    }
}
