using BusinessLayer.Middleware;
using Microsoft.EntityFrameworkCore;
using TreeWebApi.Extensions;
using static TreeWebApi.Extensions.ServiceCollectionExtension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.EnableAnnotations());


builder.Services
    .AddMediator()                              // MediatR
    .AddFluentValidationDependencies();         // FluentValidation                                 

var connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services
    .AddPostgresDb(connectionString);



var app = builder.Build();


// handle for exceptions
app.UseMiddleware<AppMiddlewareException>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
