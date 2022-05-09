using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umee.core.UMEEAttribute;

namespace umee.core.Entities
{
    public class ReceiptDetail
    {
        [ForeignKey]
        public Guid ReceiptId { get; set; }

        public Guid ProductId { get; set; }

        [Require]
        public int Price { get; set; }

        [Require]
        public int Amount { get; set; }
    }
}
