using MediatR;
using StarMart.Application.Responses;

namespace StarMart.Application.Features.ChangeCustomerProfile
{
    public class ChangeCustomerProfileCommand : IRequest<GeneralResponse<int>>
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
