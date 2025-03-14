using MediatR;
using StarMart.Application.Responses;
using System.Collections.Generic;

namespace StarMart.Application.Features.CustomersList
{
    public class CustomersListQuery : IRequest<GeneralResponse<IEnumerable<CustomerReadModel>>>
    {
        //
    }
}
