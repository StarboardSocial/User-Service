using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StarboardSocial.UserService.Server.Helpers;

public static class UserIdHelper
{
    public static string GetUserId(HttpRequest httpRequest)
    {
        string token = httpRequest.Headers.Authorization.ToString();
        var tokenArray = token.Split(" ");
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(tokenArray[1]);

        return jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value ?? throw new UnauthorizedAccessException("No sub claim was found");
    }
}