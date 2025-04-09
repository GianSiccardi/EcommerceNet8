using EcommerceNet8.Core.Aplication.Features.Countries.Vms;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Countries.Querys.GetListCountries
{
    public class GetListQueryCountries : IRequest<IReadOnlyList<CountryVm>>
    {
    }
}
