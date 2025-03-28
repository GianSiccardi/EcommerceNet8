using EcommerceNet8.Core.Aplication;
using EcommerceNet8.Core.Domain;
using EcommerceNet8.Infraestructure;
using EcommerceNet8.Infraestructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<EcommerceDbContext>()
    .AddDefaultTokenProviders();


builder.Services.TryAddSingleton<ISystemClock, SystemClock>();



var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key,
        ValidateAudience = false,
        ValidateIssuer = false,


    };
});

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


var app = builder.Build();

 if (app.Environment.IsDevelopment())
 {
     app.UseSwagger();
     app.UseSwaggerUI(); }
 app.UseHttpsRedirection();


using (var scope = app.Services.CreateScope())
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
}
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsPolicy");
app.MapControllers();



app.Run();



