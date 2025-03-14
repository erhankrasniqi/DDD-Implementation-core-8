using MediatR;
using StarMart.Application.Responses;
using StarMart.Domain.Aggregates.CustomerAggregate;
using StarMart.Domain.Aggregates.ProductAggregate;
using StarMart.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StarMart.Application.Features.SubmitOrder
{
    public class SubmitOrderCommandHandler : IRequestHandler<SubmitOrderCommand, GeneralResponse<int>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SubmitOrderCommandHandler(
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GeneralResponse<int>> Handle(SubmitOrderCommand request, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (request.InvoiceItems.Count == 0) throw new ApplicationException("Invalid order.");

            Customer customer = await _customerRepository.GetById(request.CustomerId, cancellationToken: cancellationToken).ConfigureAwait(false);

            if (customer == null) throw new ApplicationException("Customer not found.");

            IList<ItemDetail> invoiceProducts = [];
            int orderId = 0;

            foreach (InvoiceItem invoiceItem in request.InvoiceItems)
            {
                Product product = await _productRepository.GetById(invoiceItem.ProductId, cancellationToken: cancellationToken).ConfigureAwait(false);

                if (product == null) throw new ApplicationException($"Product Id {invoiceItem.ProductId} not found.");

                invoiceProducts.Add(new ItemDetail(product, invoiceItem.Quantity));
            }

            if (invoiceProducts.Any())
            {
                Order order = customer.SubmitOrder(invoiceProducts);

                await _orderRepository.Add(order, cancellationToken).ConfigureAwait(false);
                await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

                orderId = order.Id;
            }

            return new GeneralResponse<int>
            {
                Success = orderId > 0,
                Message = orderId > 0 ? "Order has been submitted successfully." : "Unable to submit order.",
                Result = orderId
            };
        }
    }
}
