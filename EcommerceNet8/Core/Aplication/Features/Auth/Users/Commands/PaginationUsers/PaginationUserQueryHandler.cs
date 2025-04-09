using EcommerceNet8.Core.Aplication.Features.Shared.Queries;
using EcommerceNet8.Core.Aplication.Persistence;
using EcommerceNet8.Core.Aplication.Specifications.Users;
using EcommerceNet8.Core.Domain;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.PaginationUsers
{
    public class PaginationUserQueryHandler : IRequestHandler<PaginationUserQuery, PaginationVm<Usuario>>

    {

        private readonly IUnitOfWork _unitOfWork;

        public PaginationUserQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PaginationVm<Usuario>> Handle(PaginationUserQuery request, CancellationToken cancellationToken)
        {
            var userSpecificationParams = new UserSpecificationParams
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Search = request.Search,
                Sort = request.Sort
            };


            var spec = new UserSpecification(userSpecificationParams);
            var users = await _unitOfWork.Repository<Usuario>().GetAllWithSpec(spec);
            Console.WriteLine($"Usuarios encontrados: {users.Count}");


            var specCount = new UserForCountingSpecification(userSpecificationParams);
            var totalUsers = await _unitOfWork.Repository<Usuario>().CountAsync(specCount);


            var rounded = Math.Ceiling(Convert.ToDecimal(totalUsers) / Convert.ToDecimal(request.PageSize));
            var totalPages = Convert.ToInt32(rounded);

            var userByPage = users.Count();

            var pagination = new PaginationVm<Usuario>
            {
                Count = totalUsers,
                Data = users,
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = userByPage
            };

            return pagination;
        }
    }
}
