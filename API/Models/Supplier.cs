using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API.Models
{
    [Table("Tb_M_Supplier")]
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Name must be of letters and number only")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Name must be of minimum 6 characters")]
        public string SupplierName { get; set; }
    }
}