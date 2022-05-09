using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umee.core.UMEEAttribute;

namespace umee.core.Entities
{
    public class Product
    {
        [PrimaryKey]
        public Guid ProductId { get; set; }

        [Require]
        [Search]
        public string ProductName { get; set; }

        [ForeignKey]
        public Guid? CategoryId{ get; set; }

        [Require]
        public int Amount { get; set; }

        [Require]
        public int Sold { get; set; }

        [Require]
        public string Description{ get; set; }

        [Require]
        public int Price { get; set; }

        [Require]
        public string ImageUrl { get; set; }
    }
}
