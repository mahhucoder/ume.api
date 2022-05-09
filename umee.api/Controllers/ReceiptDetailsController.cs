using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using umee.core.Entities;
using umee.core.Interfaces.Infrastructure;
using umee.core.Interfaces.Service;

namespace umee.api.Controllers
{
    public class ReceiptDetailsController : UmeeBaseController<ReceiptDetail>
    {
        IReceiptDetailRepository _receiptDetailRepository;
        IReceiptDetailService _receiptDetailService;
        public ReceiptDetailsController(IReceiptDetailRepository receiptDetailRepository, IReceiptDetailService receiptDetailService) : base(receiptDetailRepository, receiptDetailService)
        {
            _receiptDetailRepository = receiptDetailRepository;
        }

        public override IActionResult Get(Guid id)
        {
            var res = _receiptDetailRepository.GetReceiptDetail(id);

            return Ok(res);
        }

        public override IActionResult Delete(Guid id)
        {
            var res = _receiptDetailRepository.Delete(id);

            return Ok(res);
        }
    }
}
