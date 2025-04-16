using EcommerceNet8.Api.Middleware;
using EcommerceNet8.Core.Aplication;
using EcommerceNet8.Core.Aplication.Contracts.Infrastructure;
using EcommerceNet8.Core.Aplication.Features.Products.Queries.GetProductList;
using EcommerceNet8.Core.Aplication.Models.Authorization;
using EcommerceNet8.Core.Domain;
using EcommerceNet8.Infraestructure;
using EcommerceNet8.Infraestructure.ImageCloudinary;
using EcommerceNet8.Infraestructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


var basePath = Directory.GetCurrentDirectory();
var appsettingsPath = Path.Combine(basePath, "appsettings.json");
Console.WriteLine($"Buscando appsettings.json en: {appsettingsPath}");
Console.WriteLine($"¿Existe el archivo? {File.Exists(appsettingsPath)}");



builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Imprimir para depurar
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine("Cadena de conexión: " + connectionString);

if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("Error: La cadena de conexión 'DefaultConnection' no se encontró o está vacía.");
    return;
}

// Registro del DbContext
builder.Services.AddDbContext<EcommerceDbContext>(options =>
    options.UseSqlServer(connectionString,
        b => b.MigrationsAssembly(typeof(EcommerceDbContext).Assembly.FullName)
    )
);



builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        //.RequireRole(Role.ADMIN) // Agrega el rol aquí
        .Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
}).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<EcommerceDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddHttpContextAccessor();

Console.WriteLine($"Clave JWT en Program.cs: {builder.Configuration["Jwt:Key"]}");
Console.WriteLine($"Issuer esperado: {builder.Configuration["Jwt:Issuer"]}");
Console.WriteLine($"Audience esperada: {builder.Configuration["Jwt:Audience"]}");

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]));


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        RoleClaimType = ClaimTypes.Role,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"❌ ERROR de autenticación: {context.Exception.Message}");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("✅ Token validado correctamente.");
            foreach (var claim in context.Principal?.Claims ?? Enumerable.Empty<Claim>())
            {
                Console.WriteLine($"Claim: {claim.Type} = {claim.Value}");
            }
            var roles = context.Principal?.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            Console.WriteLine($"Roles encontrados: {string.Join(", ", roles ?? Enumerable.Empty<string>())}");
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            context.HandleResponse();
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(JsonConvert.SerializeObject(new { error = "No autorizado" }));
        },
        OnForbidden = context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(JsonConvert.SerializeObject(new { error = "Rol no autorizado" }));
        }
    };
});


builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//colocar esto para q funcione infrestructureServices
builder.Services.AddApplicationServices(builder.Configuration); // Desde ApplicationServiceRegistration
builder.Services.AddInfrastructureServices(builder.Configuration);



builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetProductListQueryHandler).Assembly));
builder.Services.AddScoped<IManageImageService, ManageImageService>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) // Solo en desarrollo
{
    app.UseSwagger(); // Genera el JSON de Swagger
    app.UseSwaggerUI(); // Muestra la UI
}


app.Use(async (context, next) =>
{
    Console.WriteLine($"Ruta solicitada: {context.Request.Path}");
    Console.WriteLine($"Headers: {string.Join(", ", context.Request.Headers.Select(h => $"{h.Key}: {h.Value}"))}");


    await next();
});

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == 404)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogWarning($"⚠️ Ruta no encontrada: {context.Request.Method} {context.Request.Path}");
    }
});

app.UseHttpsRedirection();
app.UseAuthentication(); // ¡Primero!
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("CorsPolicy");
app.MapControllers();

/*using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var loggerFactory = service.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = service.GetRequiredService<EcommerceDbContext>();
        var usuarioMangaer = service.GetRequiredService<UserManager<Usuario>>();
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();


        await context.Database.MigrateAsync();
        await EcommerceDbContextData.LoadDataAsync(context, usuarioMangaer, roleManager, loggerFactory);

    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Error en la migracion");
    }
}*/



app.Run();



