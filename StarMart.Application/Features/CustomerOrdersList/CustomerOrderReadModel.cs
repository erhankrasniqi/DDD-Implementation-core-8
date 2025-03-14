using StarMart.Application.ReadModels;
using System.Collections.Generic;

namespace StarMart.Application.Features.CustomerOrdersList
{
    public class CustomerOrderReadModel
    {
        public int CustomerId { get; set; }
        public string Customer { get; set; }
        public IEnumerable<OrderItemReadModel> Orders { get; set; }
    }
}
