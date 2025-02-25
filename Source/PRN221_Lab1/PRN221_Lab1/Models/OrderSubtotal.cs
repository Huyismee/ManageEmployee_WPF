using System;
using System.Collections.Generic;

namespace PRN221_Lab1.Models
{
    public partial class OrderSubtotal
    {
        public int OrderId { get; set; }
        public decimal? Subtotal { get; set; }
    }
}
