using HamsterCoin.Domain;

namespace HamsterCoin.Auth
{
    public interface IAuthenticationService
    {
        Task<User> AuthenticateByUser(User user);
    }
}
