using System;
using System.Collections.Generic;
using System.Linq;

namespace BonusApp
{
    public class Order
    {
        public Func<double, double> Bonus { get; set; }
        private List<Product> _products = new List<Product>();

        public void AddProduct(Product p)
        {
            _products.Add(p);
        }

        public double GetValueOfProducts()
        {
            return _products.Sum(product => product.Value);
        }

        public double GetValueOfProducts(DateTime date)
        {
            return _products.Sum(product => (product.AvailableFrom <= date && product.AvailableTo >= date) ? product.Value : 0);
        }

        public double GetBonus()
        {
            return Bonus(GetValueOfProducts());
        }

        public double GetBonus(DateTime date)
        {
            return Bonus(GetValueOfProducts(date));
        }

        public double GetTotalPrice()
        {
            return GetValueOfProducts() - GetBonus();
        }

        public double GetTotalPrice(DateTime date)
        {
            return GetValueOfProducts(date) - GetBonus(date);
        }

        public List<Product> SortProductOrderBy(Func<Product, object> keySelector)
        {
            return _products.OrderBy(keySelector).ToList();
        }
    }
}
