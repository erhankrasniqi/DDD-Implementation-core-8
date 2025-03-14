using MediatR;
using Microsoft.EntityFrameworkCore;
using StarMart.Application.ReadModels;
using StarMart.Application.Responses;
using StarMart.Domain.Aggregates.CustomerAggregate;
using StarMart.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StarMart.Application.Features.CustomerOrdersList
{
    public class CustomerOrdersListQueryHandler : IRequestHandler<CustomerOrdersListQuery, GeneralResponse<CustomerOrderReadModel>>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerOrdersListQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<GeneralResponse<CustomerOrderReadModel>> Handle(CustomerOrdersListQuery query, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(query);

            Customer customer = await _customerRepository.GetById(
                query.CustomerId,
                [
                    w => w.Include(x => x.Orders).ThenInclude(y => y.OrderItems).ThenInclude(z => z.Product)
                ],
                cancellationToken)
                .ConfigureAwait(false);

            if (customer == null) throw new ApplicationException("Invalid customer.");

            IEnumerable<OrderItemReadModel> orders = null;

            if (customer.Orders != null && customer.Orders.Any())
            {
                orders = customer.Orders.Select(x =>
                new OrderItemReadModel
                {
                    OrderId = x.Id,
                    TotalPrice = x.TotalPrice,
                    OrderDate = x.OrderDate,
                    Items = x.OrderItems.Select(y =>
                    new InvoiceItemReadModel
                    {
                        OrderItemId = y.Id,
                        ProductId = y.Product.Id,
                        Product = y.Product.Name,
                        Price = y.Product.Price,
                        Quantity = y.Quantity
                    })
                });
            }

            CustomerOrderReadModel readModel = new()
            {
                CustomerId = customer.Id,
                Customer = $"{customer.FirstName} {customer.Lastname}",
                Orders = orders
            };

            return new GeneralResponse<CustomerOrderReadModel>
            {
                Success = true,
                Message = "Customer orders list.",
                Result = readModel
            };
        }
    }
}
