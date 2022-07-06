using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Thu_y.Utils.Infrastructure.Application.Models;

namespace Thu_y.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettingModel.Instance.SecretKey)),
                        ValidateAudience = false,
                        ValidateIssuer = false
                    };
                });
            return services;
        }
    }
}
