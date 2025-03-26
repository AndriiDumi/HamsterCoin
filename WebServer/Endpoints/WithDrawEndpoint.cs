//using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Microsoft.AspNetCore.Mvc;
using HamsterCoin.Services.Interfaces;
using HamsterCoin.Domain;
using HamsterCoin.Mapping;

namespace HamsterCoin.Endpoints
{
    public static class WithDrawEndpoint
    {
        public static void WithDrawEndpoints(this IEndpointRouteBuilder routes)
        {
            var routeGroupBuilder = routes.MapGroup("/withdrawhistory");

            routes.MapPost("/withdraw", async ([FromBody] WithdrawDTO request, [FromServices] IWithDrawService withDrawService) =>
            {

                try
                {
                    await withDrawService.CreateAsync(new WithdrawHistory
                    {
                        UserId = request.UserId,
                        SumWithdraw = request.SumWithdraw,
                        DateWithdraw = request.DateWithdraw
                    });
                }
                catch (Exception e)
                {
                    return Results.Problem(detail: e.Message, statusCode: 500);
                }

                return Results.Ok();
            });

            routes.MapGet("/{UserId}", async (long UserId, [FromServices] IWithDrawService withDrawService) =>
            {
                List<WithdrawHistory> history;
                try
                {
                    history = await withDrawService.GetAllHistoryWithdrawAsync(UserId);
                }
                catch (Exception e)
                {
                    return Results.Problem(detail: e.Message, statusCode: 500);
                }
                return Results.Ok(history);
            });

        }

    }
}
