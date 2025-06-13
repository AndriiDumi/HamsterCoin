using Microsoft.AspNetCore.Mvc;
using HamsterCoin.Services.Interfaces;
using HamsterCoin.Domain;
using System.Reflection.Metadata.Ecma335;
using HamsterCoin.DTO;

namespace HamsterCoin.Endpoints
{
    public static class DepositEndpoint
    {
        public static void DepositEndpoints(this IEndpointRouteBuilder route)
        {
            var routeGroupBuilder = route.MapGroup("/deposit");

            routeGroupBuilder.MapPost("/", async (DepositDTO deposit, IDepositService depositService) =>
            {

                await depositService.CreateAsync(new DepositHistory
                {
                    UserId = deposit.UserId,
                    SumDep = deposit.SumDep,
                    DateDep = deposit.DateDep
                });

                return Results.Ok();
            });

            route.MapGet("deposithistory/{UserId}", async (long UserId, [FromServices] IDepositService depositService) =>
            {
                List<DepositHistory> dep;

                dep = await depositService.GetHistoryDep(UserId);

                return Results.Ok(dep);
            });
        }
    }
}