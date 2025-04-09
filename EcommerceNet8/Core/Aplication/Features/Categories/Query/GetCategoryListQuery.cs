using EcommerceNet8.Core.Aplication.Features.Categories.VmS;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Categories.Query
{
    public class GetCategoryListQuery : IRequest<IReadOnlyList<CategoryVm>>
    {
    }
}
