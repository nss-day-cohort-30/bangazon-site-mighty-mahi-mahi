using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Bangazon.Models.OrderViewModels
{
    public class OrderDetailViewModel
    {
        public Order Order { get; set; }

        public IEnumerable<OrderLineItem> LineItems { get; set; }

        public List<SelectListItem> PaymentTypes { get; set; }
    }
}