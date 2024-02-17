using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.BusinessLogic.Models.ConfigurationModels;

namespace OnlineStore.BusinessLogic.Extensions
{
    public static class JwtConfigurationExtension
    {
        public static WebApplicationBuilder JwtConfiguration
            (this WebApplicationBuilder builder)
        {

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecurityKey"])),
                    };
                });
            
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

            return builder;
        }
    }
}
