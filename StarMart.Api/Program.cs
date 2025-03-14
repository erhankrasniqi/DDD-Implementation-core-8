using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StarMart.Api;
using StarMart.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

string corsPolicy = "StarMartCorsPolicy";

builder.Services.AddCorsInApplication(corsPolicy);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.InitializeServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(corsPolicy);

app.UseExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();
