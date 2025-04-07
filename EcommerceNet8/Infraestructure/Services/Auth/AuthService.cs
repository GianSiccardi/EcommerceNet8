using EcommerceNet8.Core.Aplication.Contracts.Identity;
using EcommerceNet8.Core.Aplication.Models.Token;
using EcommerceNet8.Core.Domain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceNet8.Infraestructure.Services.Auth
{
    public class AuthService : IAuthService
    {


        public JwtSettings _jwtSettings { get; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(IHttpContextAccessor httpContextAccessor ,IOptions<JwtSettings>jwtSettings)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtSettings = jwtSettings.Value;
            
        }

        public string CreateToken(Usuario usuario, IList<string>? roles)
        {
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName!),
        new Claim("userId", usuario.Id),
        new Claim("email", usuario.Email!)
    };


            foreach (var rol in roles ?? Enumerable.Empty<string>())
            {
                claims.Add(new Claim(ClaimTypes.Role, rol)); // Usa ClaimTypes.Role en lugar de "role"
            }
            Console.WriteLine($"Clave JWT en CreateToken: {_jwtSettings.Key}");
            Console.WriteLine($"Issuer en CreateToken: {_jwtSettings.Issuer}");
            Console.WriteLine($"Audience en CreateToken: {_jwtSettings.Audience}");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key!));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);


            var now = DateTime.UtcNow;
            Console.WriteLine($"Fecha actual UTC en CreateToken: {now}");

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.Add(_jwtSettings.ExpireTime),
                Issuer = _jwtSettings.Issuer, // "mi-aplicacion"
                Audience = _jwtSettings.Audience, // "mi-cliente-frontend"
                SigningCredentials = credenciales
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }



        //obtiene el nombre de usuario de la sesión actual a partir de los claims del usuario autenticado en la aplicación.
        public string GetSessionUser()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
            {
                throw new InvalidOperationException("HttpContext es null. No se puede acceder a la sesión.");
            }

            var user = context.User;

         
            

     

            if (!user.Identity?.IsAuthenticated ?? true)
            {
                throw new UnauthorizedAccessException("🔴 ERROR: Usuario no autenticado. El token podría ser inválido o haber expirado.");
            }

           
            var claims = user.Claims.Select(c => $"{c.Type}: {c.Value}");
            Console.WriteLine("Claims recibidos: " + string.Join(", ", claims));


            var username = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("⚠️ ADVERTENCIA: Claim 'NameIdentifier' no encontrado. Buscando 'nameid'...");
                username = user.Claims.FirstOrDefault(x => x.Type == "nameid")?.Value;
            }

            if (string.IsNullOrEmpty(username))
            {
                throw new UnauthorizedAccessException("🔴 ERROR: No se encontró un identificador de usuario válido en los claims.");
            }

            Console.WriteLine($"✅ Usuario autenticado: {username}");
            return username;
        }

    }}
