﻿using EcommerceNet8.Core.Aplication.Features.Products.Queries.Vms;
using EcommerceNet8.Core.Domain;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Products.Queries.GetProductList
{
    public class GetProductListQuery:IRequest<IReadOnlyList<ProductVm>>
    {
    }
}
