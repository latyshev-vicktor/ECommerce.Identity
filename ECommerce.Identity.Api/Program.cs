using ECommerce.Application;
using ECommerce.DataAccess.Postgres;
using System.Text.Json.Serialization;
using ECommerce.Infrastructure.Impl;
using ECommerce.Api.Helpers;
using ECommerce.Application.Options;
using Microsoft.Extensions.Options;
using ECommerce.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPostgres(builder.Configuration)
                .AddApplication()
                .AddInfrastructure();

builder.Services.AddScoped<DbInitializer>();
builder.Services.Configure<JwtOptions>(options => builder.Configuration.GetSection(JwtOptions.Name).Bind(options));
builder.Services.AddSingleton(x => x.GetService<IOptions<JwtOptions>>()!.Value);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
    dbInitializer.Initializer(new CancellationToken()).GetAwaiter().GetResult();
}

app.UseExceptionHandler();

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
