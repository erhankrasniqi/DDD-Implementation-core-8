using MediatR;
using StarMart.Application.Responses;
using StarMart.Domain.Aggregates.CustomerAggregate;
using StarMart.Infrastructure.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StarMart.Application.Features.CustomersList
{
    public class CustomersListQueryHandler : IRequestHandler<CustomersListQuery, GeneralResponse<IEnumerable<CustomerReadModel>>>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersListQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<GeneralResponse<IEnumerable<CustomerReadModel>>> Handle(CustomersListQuery query, CancellationToken cancellationToken = default)
        {
            IEnumerable<Customer> customers = await _customerRepository.Get(cancellationToken: cancellationToken).ConfigureAwait(false);
            IEnumerable<CustomerReadModel> readModel = [];

            if (customers.Any())
            {
                readModel = customers.Select(x =>
                new CustomerReadModel
                {
                    Id = x.Id,
                    Name = $"{x.FirstName} {x.Lastname}",
                    PostalCode = x.PostalCode,
                    Address = x.Address,
                });
            }

            return new GeneralResponse<IEnumerable<CustomerReadModel>>
            {
                Success = true,
                Message = "Customers list.",
                Result = readModel
            };
        }
    }
}
