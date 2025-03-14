using MediatR;
using StarMart.Application.ReadModels;
using StarMart.Application.Responses;
using System;
using System.Collections.Generic;

namespace StarMart.Application.Features.OrdersListByByDate
{
    public class OrdersListByDateQuery : IRequest<GeneralResponse<IEnumerable<OrderReadModel>>>
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
