using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{
    public class Cartline
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

    public class Cart
    {
        private List<Cartline> lineCollection = new List<Cartline>();

        public void Additem(Product product, int quantity)
        {
            Cartline line = lineCollection.Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();
            if(line == null)
            {
                lineCollection.Add(new Cartline { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<Cartline> Lines
        {
            get { return lineCollection;}
        }











    }

  
}
