using System;
using System.Collections.Generic;

namespace StarMart.Application.ReadModels
{
    public class OrderItemReadModel
    {
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public IEnumerable<InvoiceItemReadModel> Items { get; set; }
    }
}
