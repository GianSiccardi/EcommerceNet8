using EcommerceNet8.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using EcommerceNet8.Core.Aplication.Models.Authorization;

namespace EcommerceNet8.Infraestructure.Persistence
{
    public class EcommerceDbContextData
    {


        public static async Task LoadDataAsync(
          EcommerceDbContext context,
          UserManager<Usuario> usuarioManager,
          RoleManager<IdentityRole> roleManager,
          ILoggerFactory loggerFactory

          )
        {
            try
            {
                if (!await roleManager.RoleExistsAsync(Core.Aplication.Models.Authorization.Role.ADMIN))
                {
                    await roleManager.CreateAsync(new IdentityRole(Core.Aplication.Models.Authorization.Role.ADMIN));
                }

                if (!await roleManager.RoleExistsAsync(Core.Aplication.Models.Authorization.Role.USER))
                {
                    await roleManager.CreateAsync(new IdentityRole(Core.Aplication.Models.Authorization.Role.USER));
                }

                // Verifica si los usuarios ya están creados
                if (!usuarioManager.Users.Any())
                {
                    var usuarioAdmin = new Usuario
                    {
                        Name = "Gian",
                        LastName = "Siccardi",
                        Email = "giansicca@gmail.com",
                        UserName = "Gian",
                        PhoneNumber = "123456789",
                        AvatarUrl = "avatar"
                    };

                    var result = await usuarioManager.CreateAsync(usuarioAdmin, "Admin@1234");

                    if (result.Succeeded)
                    {
                        // Asigna el rol ADMIN después de que el usuario haya sido creado correctamente
                        await usuarioManager.AddToRoleAsync(usuarioAdmin, Core.Aplication.Models.Authorization.Role.ADMIN);
                    }
                    else
                    {
                        // Si no se pudo crear el usuario, registra un error
                        var logger = loggerFactory.CreateLogger<EcommerceDbContextData>();
                        logger.LogError("Error al crear el usuario admin: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                    }

                    var usuario = new Usuario
                    {
                        Name = "Lucas",
                        LastName = "Fernandez",
                        Email = "lucas.fernandez@gmail.com",
                        UserName = "LucasF",
                        PhoneNumber = "987654321",
                        AvatarUrl = "profile_pic_lucas"
                    };

                    result = await usuarioManager.CreateAsync(usuario, "User@1234");

                    if (result.Succeeded)
                    {
                        // Asigna el rol USER después de que el usuario haya sido creado correctamente
                        await usuarioManager.AddToRoleAsync(usuario, Core.Aplication.Models.Authorization.Role.USER);
                    }
                    else
                    {
                        var logger = loggerFactory.CreateLogger<EcommerceDbContextData>();
                        logger.LogError("Error al crear el usuario: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                    }
                }

                if (!context.Categories!.Any())
                {
                    var categoryData = File.ReadAllText("Infraestructure/Data/category.json");
                    var categories = JsonConvert.DeserializeObject<List<Category>>(categoryData);
                    await context.Categories!.AddRangeAsync(categories!);
                    await context.SaveChangesAsync();
                }



                if (!context.Products!.Any())
                {
                    var productData = File.ReadAllText("Infraestructure/Data/product.json");
                    var products = JsonConvert.DeserializeObject<List<Product>>(productData);
                    await context.Products!.AddRangeAsync(products!);
                    await context.SaveChangesAsync();
                }



                if (!context.Images!.Any())
                {
                    var imageData = File.ReadAllText("Infraestructure/Data/image.json");
                    var images = JsonConvert.DeserializeObject<List<Imagee>>(imageData);
                    await context.Images!.AddRangeAsync(images!);
                    await context.SaveChangesAsync();
                }



                if (!context.Reviews!.Any())
                {
                    var reviewData = File.ReadAllText("Infraestructure/Data/review.json");
                    var review = JsonConvert.DeserializeObject<List<Review>>(reviewData);
                    await context.Reviews!.AddRangeAsync(review!);
                    await context.SaveChangesAsync();
                }

                if (!context.Countries!.Any())
                {
                    var countryData = File.ReadAllText("Infraestructure/Data/countries.json");
                    var countries = JsonConvert.DeserializeObject<List<Country>>(countryData);
                    await context.Countries!.AddRangeAsync(countries!);
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<EcommerceDbContextData>();
                logger.LogError(e.Message);
            }

        }
    }
}
