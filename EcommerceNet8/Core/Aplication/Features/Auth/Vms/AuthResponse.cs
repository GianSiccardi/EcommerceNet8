using CloudinaryDotNet.Actions;
using EcommerceNet8.Core.Aplication.Features.Address.Vms;
using FluentEmail.Core.Models;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Vms
{
    public class AuthResponse
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string? Avatar { get; set; }
        public AddresVm? ShippingAddress { get; set; }
        public ICollection<string>? Roles { get; set; }


    }
}
