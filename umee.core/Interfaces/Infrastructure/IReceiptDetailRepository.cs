using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umee.core.Entities;

namespace umee.core.Interfaces.Infrastructure
{
    public interface IReceiptDetailRepository : IBaseRepository<ReceiptDetail>
    {
        object GetReceiptDetail(Guid receiptId);
    }
}
