using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Bangazon.Models.OrderViewModels
{
    public class OrderDetailViewModel
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public IEnumerable<OrderLineItem> LineItems { get; set; }

        public List<SelectListItem> PaymentTypes { get; set; }

        public int Rating { get; set; }
    }
}