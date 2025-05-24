using HamsterCoin.Domain;
using HamsterCoin.DTO;
using HamsterCoin.OperateException;
using HamsterCoin.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HamsterCoin.Endpoints
{
    public static class UserPromocodeEndpoints
    {
        public static void UserPromocodeEndpoint(this IEndpointRouteBuilder routes)
        {
            var routeGroupBuilder = routes.MapGroup("/promocode").RequireAuthorization().WithTags("UserEndpoints"); //.AddFluentValidationAutoValidation();

            routeGroupBuilder.MapGet("/get-all-pomocodes", async ([FromServices] IPromocodeService promocodeService) =>
            {
                var promocodes = await promocodeService.GetAllAsync();

                return Results.Ok(promocodes);
            });
            routeGroupBuilder.MapGet("/{userId}", async ([FromBody] long userId, [FromServices] IPromocodeService promocodeService) =>
            {
                var promocode = await promocodeService.GetByIdAsync(userId);

                return Results.Ok(promocode);
            });
            routeGroupBuilder.MapPost("/add", async ([FromBody] PromocodeDTO promocode, [FromServices] IPromocodeService promocodeService) =>
            {
                await promocodeService.AddAsync(new Promocode{UserId = promocode.UserId,promocode = promocode.promocode});
                return Results.Ok();
            });
            routeGroupBuilder.MapPatch("/update", async ([FromBody] PromocodeDTO newPromocode, [FromServices] IPromocodeService promocodeService) =>
           {
               var result = await promocodeService.UpdateAsync(new Promocode{UserId = newPromocode.UserId,promocode = newPromocode.promocode});
               if (!result) throw new NotFoundException("Not found user");
               Results.Ok();
           });

            routeGroupBuilder.MapDelete("/delete/{userId}/{code}", async ([FromBody] long userId, string code, [FromServices] IPromocodeService promocodeService) =>
           {
               var result = await promocodeService.DeleteAsync(userId, code);
               if (!result) throw new NotFoundException("Not found user");
               Results.Ok();
           });

        }
    }
}