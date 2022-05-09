using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using umee.core.Entities;
using umee.core.Interfaces.Infrastructure;
using umee.core.Interfaces.Service;

namespace umee.api.Controllers
{
    public class ReceiptsController : UmeeBaseController<Receipt>
    {
        IReceiptRepository _receiptRepository;
        IReceiptService _receiptService;
        public ReceiptsController(IReceiptRepository receiptRepository, IReceiptService receiptService) : base(receiptRepository, receiptService)
        {
            _receiptRepository = receiptRepository;
        }
    }
}
