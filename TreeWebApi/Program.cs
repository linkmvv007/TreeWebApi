using BusinessLayer;
using BusinessLayer.Middleware;
using Microsoft.EntityFrameworkCore;
using TreeWebApi.Extensions;
using TreeWebApi.Options;
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

builder.Services.ConfigureOptions<DatabaseOptionsSetup>();

builder.Services
    .AddPostgresDb();

var app = builder.Build();


// handle for exceptions
app.UseMiddleware<AppMiddlewareException>();

// add migrations:
using (var scope = app.Services.CreateScope())
{
    var logContext = scope.ServiceProvider.GetRequiredService<LogContext>();
    logContext.Database.Migrate();

    var treeContext = scope.ServiceProvider.GetRequiredService<TreeContext>();
    treeContext.Database.Migrate();
}

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
