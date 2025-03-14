using MediatR;
using StarMart.Application.Responses;
using System.Collections.Generic;

namespace StarMart.Application.Features.ProductsList
{
    public class ProductsListQuery : IRequest<GeneralResponse<IEnumerable<ProductReadModel>>>
    {
        //
    }
}
