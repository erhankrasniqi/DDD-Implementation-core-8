using MediatR;
using StarMart.Application.Responses;

namespace StarMart.Application.Features.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<GeneralResponse<int>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
    }
}
