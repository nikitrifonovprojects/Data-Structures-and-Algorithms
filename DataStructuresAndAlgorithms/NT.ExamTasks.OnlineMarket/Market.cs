using System.Collections.Generic;
using System.Linq;

namespace NT.ExamTasks.OnlineMarket
{
    public class Market
    {
        public HashSet<Product> products;
        public Dictionary<string, SortedSet<Product>> byType;
        public Dictionary<double, SortedSet<Product>> byPrice;
        public SortedSet<double> prices;

        public Market()
        {
            this.products = new HashSet<Product>();
            this.byType = new Dictionary<string, SortedSet<Product>>();
            this.byPrice = new Dictionary<double, SortedSet<Product>>();
            this.prices = new SortedSet<double>();
        }

        public bool AddProduct(Product product)
        {
            if (this.products.Contains(product))
            {
                return false;
            }

            if (!this.byPrice.ContainsKey(product.Price))
            {
                this.byPrice[product.Price] = new SortedSet<Product>();
            }

            if (!this.byType.ContainsKey(product.Type))
            {
                this.byType[product.Type] = new SortedSet<Product>();
            }

            this.products.Add(product);
            this.byPrice[product.Price].Add(product);
            this.byType[product.Type].Add(product);
            this.prices.Add(product.Price);

            return true;
        }

        public IEnumerable<Product> FilterProducts(double from, double to)
        {
            var selectedPrices = this.prices.GetViewBetween(from, to);
            List<Product> products = new List<Product>();

            foreach (var price in selectedPrices)
            {
                if (products.Count >= 10)
                {
                    break;
                }

                foreach (var product in this.byPrice[price])
                {
                    if (products.Count >= 10)
                    {
                        break;
                    }

                    products.Add(product);
                }
            }

            return products;
        }

        public IEnumerable<Product> FilterProducts(string type)
        {
            if (!this.byType.ContainsKey(type))
            {
                return null;
            }

            return this.byType[type].Take(10);
        }
    }
}
