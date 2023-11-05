using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminToolkit
{
    internal class Product
    {
        public string productName;
        public int subscribe;

        public Product(string productName, int subscribe)
        {
            this.productName = productName;
            this.subscribe = subscribe;
        }
    }
}
