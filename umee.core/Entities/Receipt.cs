using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umee.core.UMEEAttribute;

namespace umee.core.Entities
{
    public class Receipt
    {
        [PrimaryKey]
        [NotMap]
        public Guid ReceiptId { get; set; }

        [ForeignKey]
        [NotMap]
        public Guid? AccountId { get; set; }

        [NotMap]
        public DateTime CreatedAt { get; set; }

        public bool? Status { get; set; }

        [NotMap]
        public int TransportFee { get; set; }

        [NotMap]
        [Require]
        public string ReceiverName { get; set; }

        [Require]
        [NotMap]
        public string Address { get; set; }

        [Require]
        [NotMap]
        public string PhoneNumber { get; set; }
    }
}
