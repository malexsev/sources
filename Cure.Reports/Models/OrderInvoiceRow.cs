using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.Reports.Models
{
    public class OrderInvoiceRow
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public OrderInvoiceRow(string name, string description, decimal price)
        {
            this.Name = name;
            this.Description = description;
            this.Price = price;
        }
    }
}
