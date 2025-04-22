using HamsterCoin.Services.Interfaces;
using HamsterCoin.Mapping;
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

            routeGroupBuilder.MapDelete("/{id}", async (string NumberCard,ICardService userCardService) =>
            {
                await userCardService.DeleteCard(NumberCard);
            });
        }
    }
}
