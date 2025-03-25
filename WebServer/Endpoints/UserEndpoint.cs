//using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Microsoft.AspNetCore.Mvc;
using HamsterCoin.Services.Interfaces;
using HamsterCoin.Domain;

namespace HamsterCoin.Endpoints
{
    public static class UserEndpoint
    {
        public static void UserEndpoints(this IEndpointRouteBuilder routes)
        {
            var routeGroupBuilder = routes.MapGroup("/users"); //.AddFluentValidationAutoValidation();

            routeGroupBuilder.MapGet("/", async ([FromServices] IUserService userService) =>
            {
                var users = await userService.GetAllUsersAsync();

                //List<UserResponse> userResponses = users.Select(user => user.ToResponse()).ToList();

                return Results.Ok(users);
            });
        }
    }
}
