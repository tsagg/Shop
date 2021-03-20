using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    /// <summary>
    /// Класс продукта
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Айди продукта
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        [StringLength(128)]
        public string Name { get; set; }
        /// <summary>
        /// Артикул
        /// </summary>
        [StringLength(15)]
        public string Code { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        [StringLength(256)]
        public string Description { get; set; }
    }
}