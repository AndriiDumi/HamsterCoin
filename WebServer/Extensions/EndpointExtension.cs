using HamsterCoin.Endpoints;

namespace HamsterCoin.Extensions
{
    public static class EndpointExtensions
    {
        public static void MapHamsterCoinEndpoints(this IEndpointRouteBuilder app)
        {
            app.UserEndpoints();
            app.WithDrawEndpoints();
        }
    }
}
