using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using Serilog;
using SignalRDemo;
using SignalRDemo.Application;
using SignalRDemo.System;
using SignalRDemo.WebAPI;
using SignalRDemo.WebAPI.BackgroundWorkers;
using SignalRDemo.WebAPI.Hubs;
using Stashbox;

var builder = WebApplication.CreateBuilder(args);

const string _defaultCorsPolicyName = "localhost";

var configurationBuilder = new ConfigurationBuilder();

configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Specifying the configuration for serilog
Log.Logger = new LoggerConfiguration() // initiate the logger configuration
                .ReadFrom.Configuration(configurationBuilder.Build()) // connect serilog to our configuration folder
                .Enrich.FromLogContext() //Adds more information to our logs from built in Serilog 
                .WriteTo.Debug() // decide where the logs are going to be shown
                .CreateLogger(); //initialise the logger

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers().AddControllersAsServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddLogging();
builder.Services.AddSingleton<IAppGuid, AppGuid>();
builder.Services.AddConsuleClient(builder.Configuration);
builder.Services.AddHttpContextAccessor();

var tokenOptions = builder.Configuration.GetSection(TokenOptions.SectionName).Get<TokenOptions>();

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
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
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
        )
);

builder.Services.AddAutoMapper(typeof(ApplicationService<,>).Assembly);
builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection(TokenOptions.SectionName));

builder.Host.UseStashbox(container => // Optional configuration options.
{
    container.AddApplication();

    // This one enables the lifetime validation for production environments too.
    container.Configure(config => config.WithLifetimeValidation());
});

builder.Host.ConfigureContainer<IStashboxContainer>((context, container) =>
{
    // Execute a dependency tree validation.
    if (context.HostingEnvironment.IsDevelopment())
        container.Validate();
});

builder.Services.AddHostedService(provider => new RealTimeCarChangeWorker(
    provider.GetRequiredService<IHubContext<CarImageHub, ICarImageHub>>(),
    builder.Configuration
));

var app = builder.Build();

app.UseCors(_defaultCorsPolicyName); // Enable CORS!

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

        var sampleDataSeeder = scopedServices.GetRequiredService<SampleDataSeeder>();
        await sampleDataSeeder.SeedSampleDatasAsync();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or initializing the database.");
    }
}

app.MapHub<CarImageHub>("/hubs/car-hubs");

app.MapControllers();

// yerel bilgisayarýnýzda docker üzerinden consul'u 8500 portu ile yayýnladýktan sonra aþaðýdaki kodlarý yorum satýrýndan çýkarabilisiniz.
// Bu sayede uygulamanýn service discovery'i kullanýmýný etkinleþtirmiþ olursunuz.
//app.Lifetime.ApplicationStarted.Register(() =>
//{
//    app.RegisterWithConsule(app.Urls);
//});

//app.Lifetime.ApplicationStopped.Register(() =>
//{
//    app.DeregisterWithConsule(app.Urls);
//});

app.Run();
