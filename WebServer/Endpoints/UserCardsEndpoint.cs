using HamsterCoin.Services.Interfaces;
using HamsterCoin.DTO;
using HamsterCoin.Domain;

namespace HamsterCoin.Endpoints
{
    public static class UserCardsEndpoint
    {
        public static void UserCardsEndpoints(this IEndpointRouteBuilder route)
        {
            var routeGroupBuilder = route.MapGroup("/cards");

            routeGroupBuilder.MapPost("/", async (CardDTO card, ICardService userCardService, IUserCardService userCard) =>
            {
                await userCardService.CreateAsync(new Card
                {
                    Number = card.Number,
                    Date = card.Date,
                    Cvv = card.Cvv
                });

                await userCard.CreateAsync(card.UserId, card.Number);
            });

            routeGroupBuilder.MapDelete("/{id}", async (string NumberCard, ICardService userCardService) =>
            {
                await userCardService.DeleteCard(NumberCard);
            });

            routeGroupBuilder.MapGet("/{userId}", async (long userId, ICardService userCardService) =>
            {
                return Results.Ok(await userCardService.GetCard(userId));
            });
        }
    }
}
