//using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Microsoft.AspNetCore.Mvc;
using HamsterCoin.Services.Interfaces;
using HamsterCoin.Domain;
using HamsterCoin.DTO;

namespace HamsterCoin.Endpoints
{
    public static class WithDrawEndpoint
    {
        public static void WithDrawEndpoints(this IEndpointRouteBuilder routes)
        {
            var routeGroupBuilder = routes.MapGroup("/withdraw");

            routeGroupBuilder.MapPost("/", async ([FromBody] WithdrawDTO withdraw, [FromServices] IWithDrawService withDrawService) =>
            {
                try
                {
                    await withDrawService.CreateRecordAsync(withdraw);
                }
                catch (Exception e)
                {
                    return Results.Problem(detail: e.Message, statusCode: 500);
                }

                return Results.Ok();
            });

            routes.MapGet("/withdrawhistory/{idUser}", async (long idUser, [FromServices] IWithDrawService withDrawService) =>
            {
                List<WithdrawHistory> history;
                try
                {
                    history = await withDrawService.GetAllHistoryWithdrawAsync(idUser);
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
