//using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Microsoft.AspNetCore.Mvc;
using HamsterCoin.Services.Interfaces;
using HamsterCoin.DTO;
using HamsterCoin.Auth;
using HamsterCoin.Mapping;

namespace HamsterCoin.Endpoints
{
    public static class UserEndpoint
    {
        public static void UserEndpoints(this IEndpointRouteBuilder routes)
        {
            var routeGroupBuilder = routes.MapGroup("/users").RequireAuthorization().WithTags("UserEndpoints"); //.AddFluentValidationAutoValidation();

            routeGroupBuilder.MapGet("/", async ([FromServices] IUserService userService) =>
            {
                var users = await userService.GetAllUsersAsync();

                return Results.Ok(users);
            });


            routeGroupBuilder.MapPost("/registration", async (IConfiguration config, [FromBody] UserRequest request, [FromServices] IUserService userService) =>
            {
                var user = request.FromRequest();
                await userService.CreateAsync(user);

                return Results.Ok();
            }).AllowAnonymous();

            routes.MapPost("/login", async (IConfiguration config,
                [FromBody] AuthenticationRequest authenticationRequest,
                [FromServices] IAuthenticationService authenticationService) =>
            {
                try
                {
                    var user = authenticationRequest.FromRequest();
                    user = await authenticationService.AuthenticateByUser(user);
                    if (user == null) return Results.Unauthorized();

                    var access_Token = JwtTokenGenerator.GenerateToken(user.Id, config["Jwt:Key"]!, config);
                    var refresh_Token = JwtTokenGenerator.GenerateRefreshToken(user.Id, config["Jwt:Key"]!);

                    await authenticationService.SaveRefreshTokenAsync(refresh_Token);

                    return Results.Ok(new LoginDto
                    {
                        Nick = user.Nickname,
                        balance = user.Balance,
                        email = user.Mail,
                        accessToken = access_Token,
                        refreshToken = refresh_Token.Token
                    });
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            routes.MapPost("/refresh-token", async ([FromBody] string refreshToken,
                IConfiguration config,
                [FromServices] IAuthenticationService authenticationService) =>
            {
                var oldtoken = await authenticationService.FindRefreshTokenByTokenAsync(refreshToken);
                if (oldtoken == null || !oldtoken.IsActive) return Results.Unauthorized();

                oldtoken.IsRevoked = true;
                await authenticationService.UpdateRefreshTokenAsync(oldtoken);

                var newAccessToken = JwtTokenGenerator.GenerateToken(oldtoken.UserId, config["Jwt:Key"]!, config);
                var newRefreshToken = JwtTokenGenerator.GenerateRefreshToken(oldtoken.UserId, config["Jwt:Key"]!);

                await authenticationService.SaveRefreshTokenAsync(newRefreshToken);

                return Results.Ok(new
                {
                    accessToken = newAccessToken,
                    refreshToken = newRefreshToken.Token
                });
            });

            routes.MapPost("/logout", async
            (
                [FromBody] string refreshToken,
                IConfiguration config,
                [FromServices] IAuthenticationService authenticationService
            ) =>
            {
                await authenticationService.LogoutAsync(refreshToken);
                return Results.Ok("Logged out successfully.");
            });
            
            routeGroupBuilder.MapPut("/refresh-balance", async (
                [FromBody] RefreshBalanceRequest refreshBalanceRequest,
                [FromServices] IUserService userService,
                [FromServices] IJwtService jwtService) =>
            {
                long userId = jwtService.GetUserId(refreshBalanceRequest.JWTtoken);

                await userService.UpdateBalanceByUserIdAsync(refreshBalanceRequest.Balance, userId);
                return Results.Ok();
            });


        }
    }
}
