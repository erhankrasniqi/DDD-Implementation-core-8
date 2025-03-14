using MediatR;
using StarMart.Application.Responses;

namespace StarMart.Application.Features.CustomerOrdersList
{
    public class CustomerOrdersListQuery : IRequest<GeneralResponse<CustomerOrderReadModel>>
    {
        public int CustomerId { get; set; }
    }
}
