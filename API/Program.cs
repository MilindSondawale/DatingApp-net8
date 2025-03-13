using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.Extensions;
using API.Middleware;
using Microsoft.AspNetCore.Identity;
using API.Entities;
using API.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);


 //Jwt Configuration

 builder.Services.AddIdentityServices(builder.Configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(x=>x.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
    .WithOrigins("http://localhost:4200","https://localhost:4200"));
    
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<PresenceHub>("hubs/presence");
app.MapHub<MessageHub>("hubs/message");

using var scope=app.Services.CreateScope();
var services=scope.ServiceProvider;

try
{
    var context=services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
    await context.Database.MigrateAsync();

    await context.Database.ExecuteSqlRawAsync("DELETE FROM [Connections]");
   // await context.Database.ExecuteSqlRawAsync("DELETE FROM \"Connections\""); for another database 

    await Seed.SeedUsers(userManager,roleManager);
}
catch (Exception ex)
{
    
    var logger=services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex,"An error occured during migration");
}

app.Run();
