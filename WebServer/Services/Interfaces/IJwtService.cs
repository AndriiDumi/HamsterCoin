
namespace HamsterCoin.Services.Interfaces
{
    public interface IJwtService
    {
        long GetUserId(string token);
    }
}
