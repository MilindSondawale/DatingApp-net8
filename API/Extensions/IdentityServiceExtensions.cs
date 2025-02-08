using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class IdentityServiceExtensions
{
 public static IServiceCollection AddIdentityServices(this IServiceCollection services,
 IConfiguration config)
 {
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
    var tokenKey=config["tokenKey"] ?? throw new Exception("TokenKey not Found");
     options.TokenValidationParameters = new TokenValidationParameters
     {
          ValidateIssuer = false,
          ValidateAudience = false,
        //  ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
        //  ValidIssuer = builder.Configuration["Jwt:Issuer"],
        // ValidAudience = builder.Configuration["Jwt:Audience"],
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.
         GetBytes(tokenKey)),

     };
 });
   return services;
 }
}
