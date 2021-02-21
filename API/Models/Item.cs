using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API.Models
{
    [Table("Tb_Item")]
    public class Item
    {
        [Key]
        public int Id { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        [ForeignKey("Supplier")] // <- foregin key ambil dari get set dibawah 
        [Display(Name = "Supplier Id")]
        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; }
    }
}