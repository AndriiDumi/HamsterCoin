//using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Microsoft.AspNetCore.Mvc;
using HamsterCoin.Services.Interfaces;
using HamsterCoin.Mapping;
using HamsterCoin.Auth;
using HamsterCoin.Security;

namespace HamsterCoin.Endpoints
{
    public static class UserEndpoint
    {
        public static void UserEndpoints(this IEndpointRouteBuilder routes)
        {
            var routeGroupBuilder = routes.MapGroup("/users").RequireAuthorization(); //.AddFluentValidationAutoValidation();

            routeGroupBuilder.MapGet("/", async ([FromServices] IUserService userService) =>
            {
                var users = await userService.GetAllUsersAsync();

                //List<UserResponse> userResponses = users.Select(user => user.ToResponse()).ToList();

                return Results.Ok(users);
            });

            routeGroupBuilder.MapPost("/registration", async (IConfiguration config, [FromBody] UserRequest request, [FromServices] IUserService userService) =>
            {
                var user = request.FromRequest();
                await userService.CreateAsync(user);

                var token = JwtTokenGenerator.GenerateToken(user.Id, config["Jwt:Key"]!);

                return Results.Ok(token);
            }).AllowAnonymous();

            routes.MapPost("/validate-jwt", async (IConfiguration config ,[FromBody] string receivedJWT) =>
            {
                bool isValid = JwtValidator.ValidateJWT(receivedJWT, config["Jwt:Key"]!); 
                     // secret key should be more secure than that
                return isValid ? Results.Ok() : Results.Unauthorized();
            });

            routes.MapPost("/login", async (IConfiguration config,
                [FromBody] UserRequest userRequest, 
                [FromServices] IAuthenticationService authenticationService) =>
            {
                try
                {
                    var user = userRequest.FromRequest();
                    user = await authenticationService.AuthenticateByUser(user);
                    if (user == null) return Results.Unauthorized();

                    //UserResponse userResponse = user.ToResponse();

                    var token = JwtTokenGenerator.GenerateToken(user.Id, config["Jwt:Key"]!);
                    return Results.Ok(new { token });
                }
                catch (Exception ex)
                { 
                    return Results.BadRequest(ex.Message);
                }
            });
        }
    }
}
