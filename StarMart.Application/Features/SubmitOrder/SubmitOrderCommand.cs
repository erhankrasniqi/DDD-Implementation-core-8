using MediatR;
using StarMart.Application.Responses;
using System.Collections.Generic;

namespace StarMart.Application.Features.SubmitOrder
{
    public class SubmitOrderCommand : IRequest<GeneralResponse<int>>
    {
        public int CustomerId { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; } = [];
    }

    public class InvoiceItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
