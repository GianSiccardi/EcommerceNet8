using EcommerceNet8.Core.Aplication.Features.Shared.Queries;
using EcommerceNet8.Core.Aplication.Persistence;
using EcommerceNet8.Core.Domain;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.PaginationUsers
{
    public class PaginationUserQuery : PaginationBaseQuerys ,IRequest<PaginationVm<Usuario>>
    {


    }
}
