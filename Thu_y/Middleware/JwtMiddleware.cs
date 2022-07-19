using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Thu_y.Modules.UserModule.Ports;
using Thu_y.Utils.Infrastructure.Application.Models;

namespace Thu_y.Middleware
{
    public class JwtMiddleware
    {
        public RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserRepository userRepository)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if(token != null) await AttachAccountToContext(context, userRepository, token);


            await _next(context);
        }

        private static async Task AttachAccountToContext(HttpContext context, IUserRepository userRepository, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(JWTSettingModel.Instance.SecretKey);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            var accountId = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
            if (!string.IsNullOrWhiteSpace(accountId))
            {
                context.Items["UserEntity"] = userRepository.GetByKey(accountId);
            }
        }
    }
}
