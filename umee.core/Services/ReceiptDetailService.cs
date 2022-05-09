using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umee.core.Entities;
using umee.core.Interfaces.Infrastructure;
using umee.core.Interfaces.Service;

namespace umee.core.Services
{
    public class ReceiptDetailService : BaseService<ReceiptDetail>, IReceiptDetailService
    {
        public ReceiptDetailService(IReceiptDetailRepository receipDetailRepository) :base(receipDetailRepository)
        {

        }
    }
}
