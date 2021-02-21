using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class ItemViewModel
    {
        public int Id { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public string SupplierId { get; set; }

        public string SupplierName { get; set; }
    }
}