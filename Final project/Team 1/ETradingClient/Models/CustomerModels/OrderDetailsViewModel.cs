using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETradingClient.Models
{
    public class OrderDetailsViewModel
    {
        public int OrderID { get; set; } // The ID of the order
        public decimal OrderTotal { get; set; } // Total amount for the order
        public DateTime OrderDate { get; set; } // Date the order was placed
        public string OrderStatus { get; set; } // Status of the order (e.g., Pending, Completed, etc.)

        // List of items in the order
        public List<OrderItem> Items { get; set; }

        public OrderDetailsViewModel()
        {
            Items = new List<OrderItem>();
        }
    }

    public class OrderItem
    {
        public string ProductName { get; set; } // Name of the product
        public int Quantity { get; set; } // Quantity of the product ordered
        public decimal Price { get; set; } // Price of a single product
        public decimal TotalPrice => Quantity * Price; // Total price for the item
    }
}
