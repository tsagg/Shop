using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    /// <summary>
    /// Класс корзины
    /// </summary>
    public class Cart
    {
        ///Список товаров и кол-во
        private List<CartLine> lineCollection = new List<CartLine>();

        /// <summary>
        /// Метод добавления товара в корзину
        /// </summary>
        /// <param name="product">Объект продукта</param>
        /// <param name="quantity">Кол-во</param>
        public void AddItem(Product product, int quantity)
        {
            //Ищем существующую запись
            CartLine line = lineCollection
                .Where(p => p.Product.Id == product.Id)
                .FirstOrDefault();

            //Если не нашли, создаем новую
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            //Иначе увеличиваем кол-во
            else
            {
                line.Quantity += quantity;
            }
        }

        /// <summary>
        /// Удаление продукта
        /// </summary>
        /// <param name="product">Объект продукта</param>
        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(l => l.Product.Id == product.Id);
        }

        /// <summary>
        /// Метод расчета полной стоимости корзины
        /// </summary>
        /// <returns>Сумма цен в корзине</returns>
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.Price * e.Quantity);

        }

        /// <summary>
        /// Очистка корзины
        /// </summary>
        public void Clear()
        {
            lineCollection.Clear();
        }

        /// <summary>
        /// GET для свойства lineCollection
        /// </summary>
        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    /// <summary>
    /// Класс позиции в корзине
    /// </summary>
    public class CartLine
    {
        /// <summary>
        /// Продукт
        /// </summary>
        public Product Product { get; set; }
        /// <summary>
        /// Кол-во
        /// </summary>
        public int Quantity { get; set; }
    }
}