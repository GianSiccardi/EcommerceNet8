using AutoMapper;
using EcommerceNet8.Core.Aplication.Features.Countries.Vms;
using EcommerceNet8.Core.Aplication.Persistence;
using EcommerceNet8.Core.Domain;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Countries.Querys.GetListCountries
{
    public class GetListQueryCountriesHanlder : IRequestHandler<GetListQueryCountries, IReadOnlyList<CountryVm>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetListQueryCountriesHanlder(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CountryVm>> Handle(GetListQueryCountries request, CancellationToken cancellationToken)
        {
            var countries = await _unitOfWork.Repository<Country>().GetAsync(
                null,
                x => x.OrderBy(y => y.Name),
                string.Empty,
                false
                );

            return _mapper.Map<IReadOnlyList<CountryVm>>(countries);
        }
    }
}
