using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using SignalRDemo;

var builder = WebApplication.CreateBuilder(args);

const string _defaultCorsPolicyName = "localhost";

// Add services to the container.

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables();
});

builder.Services.AddOcelot()
    .AddConsul();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "Public Api Gateway" });
});

builder.Services.AddCors(
        options => options.AddPolicy(
                _defaultCorsPolicyName,
                policyBuilder => policyBuilder
                    .WithOrigins(
                            builder.Configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                )
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
        )
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Public Api Gateway v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(_defaultCorsPolicyName); // Enable CORS!
app.UseWebSockets();
await app.UseOcelot();

app.Run();
