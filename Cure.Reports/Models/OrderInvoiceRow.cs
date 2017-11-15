using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.Reports.Models
{
    public class OrderInvoiceRow
    {
        public string Description { get; set; }
        public string Amount { get; set; }
        public string CostPerOne { get; set; }
        public decimal Price { get; set; }

        public OrderInvoiceRow(string description, string amount, string costPerOne, decimal price)
        {
            this.Description = description;
            this.Amount = amount;
            this.CostPerOne = costPerOne;
            this.Price = price;
        }
    }
}
