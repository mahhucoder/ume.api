using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using umee.core.Entities;
using umee.core.Interfaces.Infrastructure;
using umee.core.Interfaces.Service;

namespace umee.api.Controllers
{
    public class RequestsController : UmeeBaseController<Request>
    {
        IRequestRepository _requestRepository;
        IRequestService _requestService;
        public RequestsController(IRequestRepository requestRepository, IRequestService requestService) : base(requestRepository, requestService)
        {
            _requestRepository = requestRepository;
        }
    }
}
