using MediatR;
using Microsoft.EntityFrameworkCore;
using StarMart.Application.ReadModels;
using StarMart.Application.Responses;
using StarMart.Domain.Aggregates.CustomerAggregate;
using StarMart.Infrastructure.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StarMart.Application.Features.OrdersListByByDate
{
    public class OrdersListByDateQueryHandler : IRequestHandler<OrdersListByDateQuery, GeneralResponse<IEnumerable<OrderReadModel>>>
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersListByDateQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<GeneralResponse<IEnumerable<OrderReadModel>>> Handle(OrdersListByDateQuery query, CancellationToken cancellationToken = default)
        {
            IEnumerable<Order> orders = await _orderRepository.Get(
            x => x.OrderDate.Date >= query.From.Date && x.OrderDate.Date <= query.To.Date,
            [
                w => w.Include(x => x.Customer),
                w => w.Include(x => x.OrderItems).ThenInclude(y => y.Product)
            ],
            cancellationToken)
            .ConfigureAwait(false);

            IList<OrderReadModel> readModel = [];

            if (orders != null && orders.Any())
            {
                readModel = orders.Select(x =>
                new OrderReadModel
                {
                    OrderId = x.Id,
                    CustomerId = x.CustomerId,
                    Customer = $"{x.Customer.FirstName} {x.Customer.Lastname}",
                    TotalPrice = x.TotalPrice,
                    OrderDate = x.OrderDate,
                    Items = x.OrderItems.Select(y =>
                    new InvoiceItemReadModel
                    {
                        OrderItemId = y.Id,
                        ProductId = y.ProductId,
                        Product = y.Product.Name,
                        Price = y.Product.Price,
                        Quantity = y.Quantity
                    })
                })
                .ToList();
            }

            return new GeneralResponse<IEnumerable<OrderReadModel>>
            {
                Success = true,
                Message = "Orders list by date.",
                Result = readModel
            };
        }
    }
}
