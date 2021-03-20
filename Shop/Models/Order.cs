using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class Order
    {
        public int Id { get; set; }
        [StringLength(12)]
        public string UserPhone { get; set; }
        [StringLength(128)]
        public string UserComment { get; set; }
        [StringLength(20)]
        public string Date { get; set; }
        [StringLength(128)]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [StringLength(100)]
        public string OrderedProducts { get; set; }
    }
}