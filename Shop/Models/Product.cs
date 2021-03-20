using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class Product
    {
        public int Id { get; set; }
        [StringLength(128)]
        public string Name { get; set; }
        [StringLength(15)]
        public string Code { get; set; }
        public decimal Price { get; set; }
        [StringLength(256)]
        public string Description { get; set; }
    }
}