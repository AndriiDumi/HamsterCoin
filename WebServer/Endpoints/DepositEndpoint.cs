using Microsoft.AspNetCore.Mvc;
using HamsterCoin.Services.Interfaces;
using HamsterCoin.Domain;
using HamsterCoin.Mapping;
using System.Reflection.Metadata.Ecma335;
namespace HamsterCoin.Endpoints
{
    public static class DepositEndpoint
    {
        public static void DepositEndpoints(this IEndpointRouteBuilder route)
        {
            var routeGroupBuilder = route.MapGroup("/deposit");

            routeGroupBuilder.MapPost("/", async (DepositDTO deposit, IDepositService depositService) =>
            {
                try
                {
                    await depositService.CreateAsync(new DepositHistory
                    {
                        UserId = deposit.UserId,
                        SumDep = deposit.SumDep,
                        DateDep = deposit.DateDep
                    });
                }
                catch (Exception e)
                {
                    return Results.Problem(detail: e.Message, statusCode: 500);
                }
                return Results.Ok();
            });
            
            route.MapGet("deposithistory/{UserId}", async (long UserId,[FromServices] IDepositService depositService) =>
            {
                List<DepositHistory> dep;
                try
                {
                    dep = await depositService.GetHistoryDep(UserId);
                }
                catch (Exception e)
                {
                    return Results.Problem(detail: e.Message, statusCode: 404);
                }
                return Results.Ok(dep);
            });
        }
    }
}