using OpenMoonBoard.Application;
using OpenMoonBoard.Infrastructure;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//This code is duplicated in every single proj, might need to have a think about removing this, maybe with a nuget package?
var openMoonBoardApplicationAssembly = typeof(OpenMoonBoard.Application.DependencyInjection).Assembly;

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(openMoonBoardApplicationAssembly)
);

//Add other DI files
builder.Services.AddOpenMoonBoardApiInfrastructure();
builder.Services.AddOpenMoonBoardApiApplication();


//App shiz
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.Use(async (context, next) =>
    {
        var claims = new List<Claim>
        {
            new Claim("LoginId", "1") // Mocked LoginId
        };

        var identity = new ClaimsIdentity(claims, "FakeAuthentication");
        context.User = new ClaimsPrincipal(identity);

        await next.Invoke();
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
