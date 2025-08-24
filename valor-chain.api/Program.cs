using GnDapper.Configuration;
using GnDapper.Extensions;
using GnDapper.Interfaces;
using GnSeriLog.Extensions;
using Microsoft.Data.SqlClient;
using Serilog;
using System.Data;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using valor_chain.api;
using valor_chain.api.Application.Handlers;
using valor_chain.api.Domain.Impl;
using valor_chain.api.Domain.Ports.Input;
using valor_chain.api.Domain.Ports.Output;
using valor_chain.api.Infrastructure.DataAccess;
using valor_chain.api.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);


var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // L'URL de votre client Nuxt en développement
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
// 2. Configurer les paramètres de validation du token JWT
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Valider la clé de signature (basée sur votre clé secrète)
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured"))),

            // Valider l'émetteur (issuer) du token
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            // Valider le destinataire (audience) du token
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],

            // Valider la durée de vie du token
            ValidateLifetime = true,

            // Permet une petite marge de manœuvre pour la synchronisation des horloges
            ClockSkew = TimeSpan.Zero
        };
    });


// --- 1. Configuration de Serilog (pour GnLogging) ---
// Configure Serilog pour lire sa configuration depuis appsettings.json
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration) // Lit la section "Serilog" dans appsettings.json
    .ReadFrom.Services(services) // Permet à Serilog d'utiliser les services DI (ex: ILogger)
    .Enrich.FromLogContext() // Enrichit les logs avec le contexte de log
    .Enrich.WithMachineName() // Ajoute le nom de la machine
    .Enrich.WithProcessId() // Ajoute l'ID du processus
    .Enrich.WithThreadId() // Ajoute l'ID du thread
    //.Enrich.WithCorrelationId() // Ajoute l'ID de corrélation pour le traçage distribué
    .WriteTo.Console()); // Exemple de sink par défaut, sera surchargé par appsettings.json


// --- 2. Configuration des Services (Injection de Dépendances) ---
// Les services sont ajoutés au conteneur de services ici.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddCustomLogging(builder.Configuration);
builder.Services.AddTransient<IDbConnectionFactory, SqlConnectionFactory>();
builder.Services.AddTransient<IDbConnection>(sp => new SqlConnection(connectionString));

// Register domain repositories and services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProfilRepository, ProfilRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IUserManagementService, UserManagementService>(); //Implementation to be created
builder.Services.AddScoped<ICompanyManagementService, CompanyManagementService>(); // Implementation to be created
builder.Services.AddScoped<IProjectManagementService, ProjectManagementService>(); // Implementation to be created
// Register external service clients
builder.Services.AddScoped<IBusinessPlanGeneratorService, BusinessPlanGeneratorServiceAdapter>();
// Add other clients for notification microservices, etc.

// Register command and query handlers
builder.Services.AddScoped<UserCommandHandler>();
builder.Services.AddScoped<GetCompanyByIdQueryHandler>();
builder.Services.AddScoped<CreateCompanyCommandHandler>();
builder.Services.AddScoped<CreateProjectCommandHandler>();
// Add other handlers

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(myAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("🚀 Application valor-chain.api a démarrée avec succès !");
logger.LogInformation("Environnement : {EnvironmentName}", app.Environment.EnvironmentName);
logger.LogInformation("valor-chain.api prête à recevoir des requêtes.");

app.Run();
