using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SignalRDemo;
using SignalRDemo.Application;
using SignalRDemo.WebAPI;

var builder = WebApplication.CreateBuilder(args);

const string _defaultCorsPolicyName = "localhost";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddSingleton<IAppGuid, AppGuid>();
builder.Services.AddConsuleClient(builder.Configuration);


var tokenOptions = builder.Configuration.GetSection(TokenOptions.SectionName).Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = tokenOptions.Issuer,
                ValidAudience = tokenOptions.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),
                ClockSkew = TimeSpan.Zero
            };
        });

builder.Services.AddAuthorization();

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
        )
);

builder.Services.AddAutoMapper(typeof(ApplicationService<,>).Assembly);

builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection(TokenOptions.SectionName));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var scopedServices = scope.ServiceProvider;

    try
    {
        var baseModuleContext = scopedServices.GetRequiredService<SignalRDemoDbContext>();
        baseModuleContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or initializing the database.");
    }
}

app.MapControllers();

app.Lifetime.ApplicationStarted.Register(() =>
{
    app.RegisterWithConsule(app.Urls);
});

app.Lifetime.ApplicationStopped.Register(() =>
{
    app.DeregisterWithConsule(app.Urls);
});

app.Run();
